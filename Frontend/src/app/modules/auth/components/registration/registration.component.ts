import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { AUTH_ROUTES_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { RegisterRequestModel } from 'src/app/modules/shared/models/wep-api/domain/auth/registerRequestModel';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { EmailFieldComponent } from '../../../shared/components/fields/email-field/email-field.component';
import { PasswordFieldComponent } from '../../../shared/components/fields/password-field/password-field.component';
import { TextFieldComponent } from '../../../shared/components/fields/text-field/text-field.component';
import { confirmPasswordValidator } from '../../validators/confirm-password.validator';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, TextFieldComponent, EmailFieldComponent, PasswordFieldComponent, MatButtonModule, RouterLink]
})
export class RegistrationComponent {
  protected AUTH_ROUTES_MAP = AUTH_ROUTES_MAP;
  protected form!: FormGroup<{
    firstName: FormControl<string>,
    lastName: FormControl<string>,
    email: FormControl<string>,
    password: FormControl<string>,
    confirmPassword: FormControl<string>
  }>;

  constructor(private fb: FormBuilder,
    private authService: AuthService) {
    this.createForm();
  }

  private createForm(): void {
    this.form = this.fb.group({
      firstName: this.fb.nonNullable.control<string>(''),
      lastName: this.fb.nonNullable.control<string>(''),
      email: this.fb.nonNullable.control<string>(''),
      password: this.fb.nonNullable.control<string>(''),
      confirmPassword: this.fb.nonNullable.control<string>('')
    }, {
      validator: confirmPasswordValidator('password', 'confirmPassword')
    });
  }

  protected register(): void {
    const model: RegisterRequestModel = {
      firstName: this.form.controls.firstName.value,
      lastName: this.form.controls.lastName.value,
      email: this.form.controls.email.value,
      password: this.form.controls.password.value
    };
    this.authService.register(model)
      .subscribe();
  }
}
