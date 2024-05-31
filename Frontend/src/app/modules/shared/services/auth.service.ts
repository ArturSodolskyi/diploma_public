import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AUTH_ROUTES_MAP, DASHBOARD_ROUTES_MAP } from '../constants/routes-map.const';
import { Token } from '../enums/token.enum';
import { LoginRequestModel } from '../models/wep-api/domain/auth/loginRequestModel';
import { RefreshTokenRequestModel } from '../models/wep-api/domain/auth/refreshTokenRequestModel';
import { RegisterRequestModel } from '../models/wep-api/domain/auth/registerRequestModel';
import { TokenModel } from '../models/wep-api/domain/auth/tokenModel';
import { AuthService as AuthApiService } from './web-api/domain/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private authService: AuthApiService,
    private router: Router) { }

  public login(model: LoginRequestModel): Observable<TokenModel | null> {
    return this.authService.login(model)
      .pipe(tap(x => this.handleLoginResponse(x)));
  }

  public register(model: RegisterRequestModel): Observable<TokenModel> {
    return this.authService.register(model)
      .pipe(tap(x => this.handleLoginResponse(x)));
  }

  public refreshToken(model: RefreshTokenRequestModel): Observable<TokenModel> {
    return this.authService.refreshToken(model)
      .pipe(tap(x => this.setTokens(x)));
  }

  public logout(): void {
    localStorage.clear();
    this.router.navigate([AUTH_ROUTES_MAP.Login]);
  }

  public isAuthenticated(): boolean {
    return !!localStorage.getItem(Token.AccessToken);
  }

  public getTokens(): TokenModel {
    return {
      accessToken: localStorage.getItem(Token.AccessToken)!,
      refreshToken: localStorage.getItem(Token.RefreshToken)!,
    };
  }

  private handleLoginResponse(model: TokenModel | null): void {
    if (model == null) {
      return;
    }

    this.setTokens(model);
    this.router.navigate([DASHBOARD_ROUTES_MAP.Explorer]);
  }

  private setTokens(model: TokenModel): void {
    localStorage.setItem(Token.AccessToken, model.accessToken);
    localStorage.setItem(Token.RefreshToken, model.refreshToken);
  }
}
