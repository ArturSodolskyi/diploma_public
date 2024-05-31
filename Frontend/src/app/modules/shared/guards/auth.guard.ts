import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AUTH_ROUTES_MAP } from '../constants/routes-map.const';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = () => {
  if (inject(AuthService).isAuthenticated()) {
    return true;
  }

  inject(Router).navigate([AUTH_ROUTES_MAP.Login]);
  return false;
};
