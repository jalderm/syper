import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ClientService, ClientDto } from '../proxy/clients';
import { LoadingService } from '../services/loading.service';
import { ClientState } from '../proxy/client-state-enum';

@Component({
  standalone: false,
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.scss'],
  providers: [ListService],
})
export class ClientComponent implements OnInit {
  clients = { items: [], totalCount: 0 } as PagedResultDto<ClientDto>;
  clientStates = Object.keys(ClientState)
  .filter(key => isNaN(Number(key)))
  .map(key => ({ value: ClientState[key], name: key }));

  selectedClient = {} as ClientDto; // declare selectedClient

  form: FormGroup;

  isModalOpen = false;

  constructor(
    public readonly list: ListService,
    private clientService: ClientService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private loading: LoadingService
  ) {}

  ngOnInit() {
    const clientStreamCreator = (query) => this.clientService.getList(query);

    this.list.hookToQuery(clientStreamCreator).subscribe((response) => {
      this.clients = response;
    });
  }

  createClient() {
    this.selectedClient = {} as ClientDto; // reset the selected client
    this.buildForm();
    this.isModalOpen = true;
  }

  editClient(id: string) {
    this.loading.setLoading(true);
    this.clientService.get(id).subscribe((client) => {
      this.selectedClient = client;
      this.buildForm();
      this.isModalOpen = true;
      this.loading.setLoading(false);
    },
    (error) => {
      this.loading.setLoading(false);
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.clientService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      firstName: [this.selectedClient.firstName || '', Validators.required],
      lastName: [this.selectedClient.lastName || '', Validators.required],
      email: [this.selectedClient.email || '', Validators.required],
      clientState: [this.selectedClient.clientState, Validators.required]
    });
  }

  // change the save method
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedClient.id
      ? this.clientService.update(this.selectedClient.id, this.form.value)
      : this.clientService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
