import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-dynamic-text-input',
  templateUrl: './dynamic-text-input.component.html',
  styleUrls: ['./dynamic-text-input.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class DynamicTextInputComponent {
  @Input({ required: true }) value!: string;
  @Input({ required: true }) showInput!: boolean;
  @Output() onEnter = new EventEmitter<string>();
  @Output() onFocusout = new EventEmitter<string>();
}
