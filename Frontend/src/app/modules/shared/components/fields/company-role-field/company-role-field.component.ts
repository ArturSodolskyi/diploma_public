import { CommonModule, KeyValue } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatSelectChange, MatSelectModule } from '@angular/material/select';
import { COMPANY_ROLE_KEY_VALUE_MAP, CompanyRole } from '../../../enums/company-role.enum';

@Component({
  selector: 'app-company-role-field',
  standalone: true,
  imports: [CommonModule, MatSelectModule],
  templateUrl: './company-role-field.component.html',
  styleUrls: ['./company-role-field.component.scss']
})
export class CompanyRoleFieldComponent implements OnInit {
  protected roles: KeyValue<CompanyRole, string>[] = [];

  @Input({ required: true }) value!: KeyValue<CompanyRole, string>;

  @Output() valueChange = new EventEmitter<KeyValue<CompanyRole, string>>();
  @Output() onSelectionChange = new EventEmitter<void>();

  ngOnInit(): void {
    this.setRoles();
  }

  private setRoles(): void {
    this.roles = [
      COMPANY_ROLE_KEY_VALUE_MAP[CompanyRole.Administrator],
      COMPANY_ROLE_KEY_VALUE_MAP[CompanyRole.Watcher]
    ];
  }

  protected handleSelectionChange(event: MatSelectChange): void {
    this.valueChange.emit(event.value);
    this.onSelectionChange.emit();
  }
}
