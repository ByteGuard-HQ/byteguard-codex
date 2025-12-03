import httpClient from '../httpClient';
import type { AsvsVersionDetails, AsvsVersionMetadata, CreateAsvsVersionRequest } from './types';

export async function getAsvsVersions(): Promise<AsvsVersionMetadata[]> {
  const { data } = await httpClient.get<AsvsVersionMetadata[]>('/asvs');
  return data;
}

export async function getAsvsVersion(id: string): Promise<AsvsVersionDetails> {
  const { data } = await httpClient.get<AsvsVersionDetails>(`/asvs/${id}`);
  return data;
}

export async function createAsvsVersion(payload: CreateAsvsVersionRequest): Promise<AsvsVersionMetadata> {
  const { data } = await httpClient.post<AsvsVersionMetadata>('/asvs', payload);
  return data;
}

export async function updateAsvsVersion(id: string, payload: CreateAsvsVersionRequest): Promise<AsvsVersionMetadata> {
  const { data } = await httpClient.put<AsvsVersionMetadata>(`/asvs/${id}`, payload);
  return data;
}
