import { NgIf } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-password-field[control]',
  templateUrl: './password-field.component.html',
  styleUrls: ['./password-field.component.scss'],
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, NgIf]
})
export class PasswordFieldComponent implements OnInit {
  private readonly passwordPattern = '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}';

  @Input() control!: FormControl<string>;
  @Input() title: string = 'Password';

  ngOnInit(): void {
    this.control.addValidators([Validators.required, Validators.pattern(this.passwordPattern)]);
  }
}
