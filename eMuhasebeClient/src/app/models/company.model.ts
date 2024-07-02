export class CompanyModel {
    id: string = "";
    name: string = "";
    fullAddress: string = "";
    taxDepartment: string = "";
    taxNumber: string = "";
    isDeleted: boolean = false;
    database: DatabaseModel = new DatabaseModel();
}

export class DatabaseModel {
    server: string = "";
    databaseName: string = "";
    userId: string = "";
    password: string = "";
}