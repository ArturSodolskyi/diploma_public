import { CompanyRole } from "src/app/modules/shared/enums/company-role.enum";

export class UserViewModel {
    id!: number;
    firstName!: string;
    lastName!: string;
    email!: string;
    role!: CompanyRole;
}