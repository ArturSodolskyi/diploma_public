import { Routes } from '@angular/router';

export const USERS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./users.component')
      .then(x => x.UsersComponent),
  }
];
