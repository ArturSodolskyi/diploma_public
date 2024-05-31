import { Routes } from '@angular/router';
import { ROUTES_MAP } from '../shared/constants/routes-map.const';

export const AUTH_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./auth.component')
      .then(x => x.AuthComponent),
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: ROUTES_MAP.Login
      },
      {
        path: ROUTES_MAP.Login,
        loadComponent: () => import('./components/login/login.component')
          .then(x => x.LoginComponent)
      },
      {
        path: ROUTES_MAP.Registration,
        loadComponent: () => import('./components/registration/registration.component')
          .then(x => x.RegistrationComponent)
      },
      {
        path: '**',
        redirectTo: ROUTES_MAP.Login
      },
    ]
  }
];
