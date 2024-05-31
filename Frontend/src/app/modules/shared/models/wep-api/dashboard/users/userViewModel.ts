import { UserRole } from "src/app/modules/shared/enums/user-role.enum";

export class UserViewModel {
    id!: number;
    firstName!: string;
    lastName!: string;
    email!: string;
    role!: UserRole;
}
