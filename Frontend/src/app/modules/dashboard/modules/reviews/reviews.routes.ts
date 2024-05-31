import { Routes } from '@angular/router';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';

export const REVIEWS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./reviews.component')
      .then(x => x.ReviewsComponent),
    children: [
      {
        path: `:${PARAMS_MAP.Id}`,
        loadComponent: () => import('./components/review/review.component')
          .then(x => x.ReviewComponent),
        children: [
          {
            path: `:${PARAMS_MAP.Id}`,
            loadComponent: () => import('./components/tasks/tasks.component')
              .then(x => x.TasksComponent),
          }
        ]
      }
    ]
  }
];
