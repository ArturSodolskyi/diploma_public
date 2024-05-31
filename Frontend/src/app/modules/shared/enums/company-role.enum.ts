export enum CompanyRole {
  Administrator = 1,
  Watcher
}

export const COMPANY_ROLE_NAME_MAP: Record<CompanyRole, string> = {
  [CompanyRole.Administrator]: 'Administrator',
  [CompanyRole.Watcher]: 'Watcher'
}

//TODO: move
export const COMPANY_ROLE_KEY_VALUE_MAP = {
  [CompanyRole.Administrator]: { key: CompanyRole.Administrator, value: COMPANY_ROLE_NAME_MAP[CompanyRole.Administrator] },
  [CompanyRole.Watcher]: { key: CompanyRole.Watcher, value: COMPANY_ROLE_NAME_MAP[CompanyRole.Watcher] }
}
