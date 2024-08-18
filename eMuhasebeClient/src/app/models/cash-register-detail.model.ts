import { CashRegisterModel } from "./cash-register.model";

export class CashRegisterDetailModel {
    id: string = ""
    cashRegisterId: string = "";
    date: string = "";
    type: number = 0;
    amount: number = 0;
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    cashRegisterDetailOppositeId: string = "";    
    bankDetailId: string = "";
    customerDetailId: string = "";
    oppositeCashRegisterId: string | any = "";    
    oppositeBankId:string | any = "";
    oppositeCustomerId: string | any = "";
    oppositeCashRegister: CashRegisterModel = new CashRegisterModel();
    description: string = "";
    oppositeAmount: number = 0;
    recordType: number = 0;
}