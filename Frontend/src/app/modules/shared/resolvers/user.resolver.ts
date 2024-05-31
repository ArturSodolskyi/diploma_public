import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { tap } from 'rxjs';
import { UserViewModel } from 'src/app/modules/shared/models/wep-api/domain/users/userViewModel';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { UserService as UserApiService } from 'src/app/modules/shared/services/web-api/domain/user.service';

export const userResolver: ResolveFn<UserViewModel> = () => {
  const userService = inject(UserService);
  return inject(UserApiService).get()
    .pipe(tap(x => userService.user$.next(x)));
};
