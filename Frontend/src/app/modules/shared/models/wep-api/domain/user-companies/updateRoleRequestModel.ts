import { CompanyRole } from "src/app/modules/shared/enums/company-role.enum";

export class UpdateRoleRequestModel {
    userId!: number;
    companyId!: number;
    role!: CompanyRole;
}