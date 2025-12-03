import httpClient from '../httpClient';
import type { CreateProjectRequest, ProjectDetails, ProjectMetadata } from './types';

export async function getProjects(): Promise<ProjectMetadata[]> {
  const { data } = await httpClient.get<ProjectMetadata[]>('/projects');
  return data;
}

export async function getProject(id: string): Promise<ProjectDetails> {
  const { data } = await httpClient.get<ProjectDetails>(`/projects/${id}`);
  return data;
}

export async function createProject(payload: CreateProjectRequest): Promise<ProjectDetails> {
  const { data } = await httpClient.post<ProjectDetails>('/projects', payload);
  return data;
}
