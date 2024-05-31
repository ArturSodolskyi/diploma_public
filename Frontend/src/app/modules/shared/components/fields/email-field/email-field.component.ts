import { NgIf } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-email-field[control]',
  templateUrl: './email-field.component.html',
  styleUrls: ['./email-field.component.scss'],
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, NgIf]
})
export class EmailFieldComponent implements OnInit {
  @Input() control!: FormControl<string>;


  ngOnInit(): void {
    this.control.addValidators([Validators.required, Validators.email]);
  }
}
