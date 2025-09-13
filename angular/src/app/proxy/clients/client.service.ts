import type { ClientDto, CreateUpdateClientDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  apiName = 'Default';
  

  create = (input: CreateUpdateClientDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ClientDto>({
      method: 'POST',
      url: '/api/app/client',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/client/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ClientDto>({
      method: 'GET',
      url: `/api/app/client/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ClientDto>>({
      method: 'GET',
      url: '/api/app/client',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateClientDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ClientDto>({
      method: 'PUT',
      url: `/api/app/client/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
