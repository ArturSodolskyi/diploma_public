import { CompanyRole } from "src/app/modules/shared/enums/company-role.enum";

export class CompanyViewModel {
    id!: number;
    name!: string;
    isCreatedByCurrentUser!: boolean;
    role!: CompanyRole;
}