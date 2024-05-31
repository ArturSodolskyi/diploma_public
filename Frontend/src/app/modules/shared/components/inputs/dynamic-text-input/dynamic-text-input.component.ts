import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AutofocusDirective } from '../../../directives/autofocus.directive';

@Component({
  selector: 'app-dynamic-text-input',
  templateUrl: './dynamic-text-input.component.html',
  standalone: true,
  imports: [CommonModule, AutofocusDirective]
})
export class DynamicTextInputComponent {
  @Input({ required: true }) value!: string;
  @Input({ required: true }) showInput!: boolean;
  @Output() onEnter = new EventEmitter<string>();
  @Output() onFocusout = new EventEmitter<string>();
}
