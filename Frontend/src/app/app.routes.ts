import { Routes } from '@angular/router';
import { ROUTES_MAP } from './modules/shared/constants/routes-map.const';
import { authGuard } from './modules/shared/guards/auth.guard';
import { userResolver } from './modules/shared/resolvers/user.resolver';

export const APP_ROUTES: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: ROUTES_MAP.Dashboard
  },
  {
    path: ROUTES_MAP.Dashboard,
    canActivate: [authGuard],
    resolve: { userResolver },
    loadChildren: () => import('./modules/dashboard/dashboard.routes')
      .then(x => x.DASHBOARD_ROUTES)
  },
  {
    path: ROUTES_MAP.Auth,
    loadChildren: () => import('./modules/auth/auth.routes')
      .then(x => x.AUTH_ROUTES)
  },
  {
    path: '**',
    redirectTo: ROUTES_MAP.Dashboard
  }
];
