import { Injectable } from '@angular/core';
import { RoleService } from './role.service';

@Injectable({
  providedIn: 'root'
})
//TODO: refactor?
export class PermissionService {
  constructor(private roleService: RoleService) { }

  public canCRUD(): boolean {
    return this.roleService.isAdministrator()
      || this.roleService.isCompanyAdministrator();
  }
}
