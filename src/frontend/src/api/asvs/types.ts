import type { AsvsLevel, AsvsRequirementDetails } from '../types/requirement';

export interface AsvsVersionMetadata {
  id: string;
  versionNumber: string;
  name: string;
  description?: string;
  isReadOnly: boolean;
}

export interface AsvsSectionDetails {
  id: string;
  code: string;
  ordinal: number;
  name: string;
  requirements: AsvsRequirementDetails[];
}

export interface AsvsChapterDetails {
  id: string;
  code: string;
  ordinal: number;
  title: string;
  description?: string;
  section: AsvsSectionDetails[];
}

export interface AsvsVersionDetails {
  id: string;
  versionNumber: string;
  name: string;
  description?: string;
  isReadOnly: boolean;
  chapters: AsvsChapterDetails[];
}

export interface CreateAsvsVersionRequest {
  name: string;
  versionNumber: string;
  description?: string;
}
