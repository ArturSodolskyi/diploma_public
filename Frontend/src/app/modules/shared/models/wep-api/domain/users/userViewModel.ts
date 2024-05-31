import { CompanyRole } from "src/app/modules/shared/enums/company-role.enum";
import { UserRole } from "src/app/modules/shared/enums/user-role.enum";

export class UserViewModel {
    id!: number;
    firstName!: string;
    lastName!: string;
    role!: UserRole;
    companyId: number | undefined;
    companyRole: CompanyRole | undefined;
}
