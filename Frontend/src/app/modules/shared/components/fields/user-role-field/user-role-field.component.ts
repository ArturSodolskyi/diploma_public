import { CommonModule, KeyValue } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSelectChange, MatSelectModule } from '@angular/material/select';
import { USER_ROLE_KEY_VALUE_MAP, UserRole } from '../../../enums/user-role.enum';

@Component({
  selector: 'app-user-role-field',
  standalone: true,
  imports: [CommonModule, MatSelectModule],
  templateUrl: './user-role-field.component.html',
  styleUrls: ['./user-role-field.component.scss']
})
export class UserRoleFieldComponent {
  protected roles: KeyValue<UserRole, string>[] = [
    USER_ROLE_KEY_VALUE_MAP[UserRole.Administrator],
    USER_ROLE_KEY_VALUE_MAP[UserRole.User],
  ];

  @Input({ required: true }) value!: KeyValue<UserRole, string>;

  @Output() valueChange = new EventEmitter<KeyValue<UserRole, string>>();
  @Output() onSelectionChange = new EventEmitter<void>();

  protected handleSelectionChange(event: MatSelectChange): void {
    this.valueChange.emit(event.value);
    this.onSelectionChange.emit();
  }
}
