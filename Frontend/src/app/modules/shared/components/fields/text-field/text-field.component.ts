import { NgIf } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-text-field',
  templateUrl: './text-field.component.html',
  styleUrls: ['./text-field.component.scss'],
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, NgIf]
})
export class TextFieldComponent implements OnInit {
  @Input() control!: FormControl<string>;
  @Input() title: string = '';

  ngOnInit(): void {
    this.control.addValidators(Validators.required);
  }
}
