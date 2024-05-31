import { KeyValue } from "@angular/common";
import { CompanyRole } from "src/app/modules/shared/enums/company-role.enum";

export class UserTableModel {
    id!: number;
    firstName!: string;
    lastName!: string;
    email!: string;
    role!: KeyValue<CompanyRole, string>;
    canChangeRole!: boolean;
    canDelete!: boolean; 
}