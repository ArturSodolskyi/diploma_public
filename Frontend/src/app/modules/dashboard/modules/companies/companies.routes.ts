import { Routes } from '@angular/router';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';

export const COMPANIES_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./companies.component')
      .then(x => x.CompaniesComponent),
    children: [
      {
        path: `:${PARAMS_MAP.Id}`,
        loadComponent: () => import('./components/users/users.component')
          .then(x => x.UsersComponent),
      }
    ]
  }
];
