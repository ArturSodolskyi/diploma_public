import { Routes } from '@angular/router';
import { adminGuard } from 'src/app/modules/shared/guards/admin.guard';
import { ROUTES_MAP } from '../shared/constants/routes-map.const';

export const DASHBOARD_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./dashboard.component')
      .then(x => x.DashboardComponent),
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: ROUTES_MAP.Explorer
      },
      {
        path: ROUTES_MAP.Explorer,
        loadChildren: () => import('./modules/explorer/exlorer.routes')
          .then(x => x.EXPLORER_ROUTES)
      },
      {
        path: ROUTES_MAP.Companies,
        loadChildren: () => import('./modules/companies/companies.routes')
          .then(x => x.COMPANIES_ROUTES)
      },
      {
        path: ROUTES_MAP.Reviews,
        loadChildren: () => import('./modules/reviews/reviews.routes')
          .then(x => x.REVIEWS_ROUTES)
      },
      {
        path: ROUTES_MAP.Users,
        canActivate: [adminGuard],
        loadChildren: () => import('./modules/users/users.routes')
          .then(x => x.USERS_ROUTES)
      },
      {
        path: '**',
        redirectTo: ROUTES_MAP.Explorer
      }
    ]
  }
];
