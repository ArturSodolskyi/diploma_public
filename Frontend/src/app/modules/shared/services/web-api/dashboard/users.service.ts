import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserRole } from '../../../enums/user-role.enum';
import { UserViewModel } from '../../../models/wep-api/dashboard/users/userViewModel';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private apiUrl = environment.apiUrl + 'Dashboard/Users/';

  constructor(private httpClient: HttpClient) { }

  public get(): Observable<UserViewModel[]> {
    return this.httpClient.get<UserViewModel[]>(this.apiUrl + 'Get');
  }

  public updateUserRole(userId: number, role: UserRole) {
    return this.httpClient.put(this.apiUrl + 'UpdateUserRole', null, {
      params: { userId, role }
    });
  }

  public deleteUser(userId: number) {
    return this.httpClient.delete(this.apiUrl + 'DeleteUser', {
      params: { userId }
    });
  }
}
