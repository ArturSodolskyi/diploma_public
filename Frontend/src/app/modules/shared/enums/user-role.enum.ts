export enum UserRole {
  User = 1,
  Administrator
}

export const USER_ROLE_NAME_MAP: Record<UserRole, string> = {
  [UserRole.Administrator]: 'Administrator',
  [UserRole.User]: 'User',
}

//TODO: move
export const USER_ROLE_KEY_VALUE_MAP = {
  [UserRole.Administrator]: { key: UserRole.Administrator, value: USER_ROLE_NAME_MAP[UserRole.Administrator] },
  [UserRole.User]: { key: UserRole.User, value: USER_ROLE_NAME_MAP[UserRole.User] }
}
