import type { AsvsRequirementDetails } from '../types/requirement';

export enum ProjectStatus {
  Active = 'Active',
  Archived = 'Archived',
}

export enum Requirementstatus {
  NotApplicable = 'NotApplicable',
  NotImplemented = 'NotImplemented',
  InProgress = 'InProgress',
  Implemented = 'Implemented',
  Verified = 'Verified',
}

export interface ProjectMetadata {
  id: string;
  title: string;
  description?: string;
  owner: string;
  status: ProjectStatus;
  createdAt: string;
  archivedAt?: string;
  asvsVersionId: string;
}

export interface ProjectRequirementDetails {
  id: string;
  requirement: AsvsRequirementDetails;
  status: Requirementstatus;
  notes?: string;
  evidenceLink?: string;
  lastUpdatedAt?: string;
}

export interface ProjectDetails {
  id: string;
  title: string;
  description?: string;
  owner: string;
  status: ProjectStatus;
  createdAt: string;
  archivedAt?: string;
  asvsVersionId: string;
  requirements: AsvsRequirementDetails[];
}

export interface CreateProjectRequest {
  title: string;
  description?: string;
  owner: string;
  asvsVersionId: string;
}
