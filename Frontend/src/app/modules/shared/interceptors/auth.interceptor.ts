import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AUTH_ROUTES_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { Token } from 'src/app/modules/shared/enums/token.enum';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { RefreshTokenRequestModel } from '../models/wep-api/domain/auth/refreshTokenRequestModel';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private router: Router,
    private authService: AuthService,
    private matSnackBar: MatSnackBar) {

  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = this.setToken(req);
    return next.handle(req)
      .pipe(catchError(error => this.handleError(error, req, next)));
  }


  private handleError(error: HttpErrorResponse, req: HttpRequest<any>, next: HttpHandler) {
    switch (error.status) {
      case HttpStatusCode.Unauthorized:
        return this.handleUnauthorizedError(req, next)
      case HttpStatusCode.Forbidden:
        this.openSnackBar('Forbidden')
        return throwError(() => error)
      case HttpStatusCode.BadRequest:
        this.openSnackBar('Bad Request')
        return throwError(() => error)
      default:
        this.openSnackBar('Something went wrong')
        return throwError(() => error)
    }
  }

  private handleUnauthorizedError(req: HttpRequest<any>, next: HttpHandler) {
    const model = this.authService.getTokens();
    const requestModel: RefreshTokenRequestModel = {
      refreshToken: model.refreshToken
    }
    return this.authService.refreshToken(requestModel)
      .pipe(
        switchMap(_ => {
          req = this.setToken(req);
          return next.handle(req);
        }),
        catchError(_ =>
          throwError(() => {
            this.router.navigate([AUTH_ROUTES_MAP.Login]);
          })
        )
      );
  }

  private setToken(req: HttpRequest<any>): HttpRequest<any> {
    const acessToken = localStorage.getItem(Token.AccessToken);
    if (acessToken) {
      req = req.clone({
        setHeaders: { Authorization: `Bearer ${acessToken}` },
      });
    }

    return req;
  }

  private openSnackBar(message: string) {
    this.matSnackBar.open(message, 'Close', {
      duration: 5000,
    });
  }
}
