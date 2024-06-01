import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { RouterLink } from '@angular/router';
import { AUTH_ROUTES_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { LoginRequestModel } from 'src/app/modules/shared/models/wep-api/domain/auth/loginRequestModel';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { EmailFieldComponent } from '../../../shared/components/fields/email-field/email-field.component';
import { PasswordFieldComponent } from '../../../shared/components/fields/password-field/password-field.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, EmailFieldComponent, PasswordFieldComponent, MatButtonModule, RouterLink, MatFormFieldModule, NgIf]
})
export class LoginComponent {
  protected AUTH_ROUTES_MAP = AUTH_ROUTES_MAP;
  protected form!: FormGroup<{
    email: FormControl<string>,
    password: FormControl<string>
  }>;

  constructor(private fb: FormBuilder,
    private authService: AuthService) {
    this.createForm();
  }

  private createForm(): void {
    this.form = this.fb.group({
      email: this.fb.nonNullable.control<string>(''),
      password: this.fb.nonNullable.control<string>('')
    });
  }

  protected login() {
    const model: LoginRequestModel = {
      email: this.email.value,
      password: this.password.value
    };
    this.authService.login(model)
      .subscribe(x => {
        if (x == null) {
          this.setInvalidCredentialsError();
        }
      });
  }

  private setInvalidCredentialsError(): void {
    this.form.setErrors({ invalidCredentials: true });
  }

  private get email() {
    return this.form.controls.email;
  }

  private get password() {
    return this.form.controls.password;
  }
}
