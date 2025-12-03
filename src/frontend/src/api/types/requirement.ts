export enum AsvsLevel {
  Level1 = 'L1',
  Level2 = 'L2',
  Level3 = 'L3',
}

export interface AsvsRequirementDetails {
  id: string;
  code: string;
  ordinal: number;
  description: string;
  level: AsvsLevel;
}
