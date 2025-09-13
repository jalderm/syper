import { mapEnumToOptions } from '@abp/ng.core';

export enum ClientState {
  Pending = 0,
  Active = 1,
  Inactive = 2,
}

export const clientStateOptions = mapEnumToOptions(ClientState);
