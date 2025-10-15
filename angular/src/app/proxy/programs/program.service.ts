import type { CreateUpdateProgramDto, ProgramDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProgramService {
  apiName = 'Default';
  

  create = (input: CreateUpdateProgramDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProgramDto>({
      method: 'POST',
      url: '/api/app/program',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/program/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProgramDto>({
      method: 'GET',
      url: `/api/app/program/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProgramDto>>({
      method: 'GET',
      url: '/api/app/program',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateProgramDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProgramDto>({
      method: 'PUT',
      url: `/api/app/program/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
