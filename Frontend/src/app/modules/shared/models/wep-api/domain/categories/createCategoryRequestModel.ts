export class CreateCategoryRequestModel {
    companyId!: number;
    parentId: number | undefined;
    name!: string;
}