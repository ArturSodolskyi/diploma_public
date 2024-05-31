import { KeyValue } from "@angular/common";
import { UserRole } from "src/app/modules/shared/enums/user-role.enum";

export class UserTableModel {
    id!: number;
    firstName!: string;
    lastName!: string;
    email!: string;
    role!: KeyValue<UserRole, string>;
    canDelete!: boolean;
}
