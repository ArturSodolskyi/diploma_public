import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { DASHBOARD_ROUTES_MAP } from '../constants/routes-map.const';
import { RoleService } from '../services/role.service';

export const adminGuard: CanActivateFn = () => {
  let isAdministrator = inject(RoleService).isAdministrator()

  if (!isAdministrator) {
    inject(Router).navigate([DASHBOARD_ROUTES_MAP.Explorer]);
  }

  return isAdministrator;
};
