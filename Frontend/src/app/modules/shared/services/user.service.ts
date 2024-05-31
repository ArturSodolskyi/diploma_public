import { Injectable } from '@angular/core';
import { BehaviorSubject, take } from 'rxjs';
import { UserViewModel } from '../models/wep-api/domain/users/userViewModel';
import { UserService as UserApiService } from './web-api/domain/user.service';
import { CompanyService } from './web-api/domain/company.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  public readonly user$ = new BehaviorSubject<UserViewModel>(new UserViewModel());

  constructor(private userService: UserApiService,
    private companyService: CompanyService) {
      this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.userService.onCompanyUpdate$
      .subscribe(_ => this.reload());

    this.companyService.onDelete$
      .subscribe(x => {
        if (x === this.user$.value.companyId) {
          this.reload();
        }
      });
  }

  reload(): void {
    this.userService.get()
      .pipe(take(1))
      .subscribe(x => this.user$.next(x));
  }

  public get userId() {
    return this.user$.value.id;
  }

  public get companyId() {
    return this.user$.value.companyId;
  }
}
