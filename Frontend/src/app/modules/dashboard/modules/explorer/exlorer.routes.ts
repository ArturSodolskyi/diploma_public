import { Routes } from '@angular/router';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';

export const EXPLORER_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./explorer.component')
      .then(x => x.ExplorerComponent),
    children: [
      {
        path: `:${PARAMS_MAP.Id}`,
        loadComponent: () => import('./components/job/job.component')
          .then(x => x.JobComponent),
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
