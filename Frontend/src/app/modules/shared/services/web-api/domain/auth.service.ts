import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginRequestModel } from '../../../models/wep-api/domain/auth/loginRequestModel';
import { RefreshTokenRequestModel } from '../../../models/wep-api/domain/auth/refreshTokenRequestModel';
import { RegisterRequestModel } from '../../../models/wep-api/domain/auth/registerRequestModel';
import { TokenModel } from '../../../models/wep-api/domain/auth/tokenModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl + 'Auth/';

  constructor(private http: HttpClient) { }

  public login(model: LoginRequestModel): Observable<TokenModel> {
    return this.http.post<TokenModel>(this.apiUrl + 'Login', model);
  }

  public register(model: RegisterRequestModel): Observable<TokenModel> {
    return this.http.post<TokenModel>(this.apiUrl + 'Register', model);
  }

  public refreshToken(model: RefreshTokenRequestModel): Observable<TokenModel> {
    return this.http.post<TokenModel>(this.apiUrl + 'RefreshToken', model);
  }
}
