export const ROUTES_MAP = {
  Auth: 'auth',
  Login: 'login',
  Registration: 'registration',
  Dashboard: 'dashboard',
  Explorer: 'explorer',
  Companies: 'companies',
  Reviews: 'reviews',
  Users: 'users'
};

export const PARAMS_MAP = {
  Id: 'id'
};

export const DASHBOARD_ROUTES_MAP = {
  Reviews: `${ROUTES_MAP.Dashboard}/${ROUTES_MAP.Reviews}`,
  Explorer: `${ROUTES_MAP.Dashboard}/${ROUTES_MAP.Explorer}`,
  Companies: `${ROUTES_MAP.Dashboard}/${ROUTES_MAP.Companies}`
};

export const AUTH_ROUTES_MAP = {
  Login: `${ROUTES_MAP.Auth}/${ROUTES_MAP.Login}`,
  Registration: `${ROUTES_MAP.Auth}/${ROUTES_MAP.Registration}`,
};
