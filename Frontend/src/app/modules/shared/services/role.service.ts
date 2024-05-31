import { Injectable } from '@angular/core';
import { CompanyRole } from '../enums/company-role.enum';
import { UserRole } from '../enums/user-role.enum';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  constructor(private userService: UserService) {

  }

  public isAdministrator(): boolean {
    return this.role === UserRole.Administrator;
  }

  public isCompanyAdministrator(): boolean {
    return this.companyRole === CompanyRole.Administrator;
  }

  private get role() {
    return this.userService.user$.value.role;
  }

  private get companyRole() {
    return this.userService.user$.value.companyRole;
  }
}
