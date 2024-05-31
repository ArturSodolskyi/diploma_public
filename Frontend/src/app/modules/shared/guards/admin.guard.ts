import { CanActivateFn } from '@angular/router';

//TODO: not working
// this.userService.reload();
// return this.userService.user$
//   .pipe(switchMap(_ => of(this.roleService.isAdministrator())));
// if (this.roleService.isAdministrator()) {
//   return true;
// }

// // this.router.navigate([ROUTES_MAP.Dashboard]);
// return false;
export const adminGuard: CanActivateFn = () => {
  return true;
};