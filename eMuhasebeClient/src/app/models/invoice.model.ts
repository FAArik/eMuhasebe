import { CustomerModel } from "./customer.model";
import { InvoiceDetailModel } from "./invoice-detail.model";

export class InvoiceModel {
  id: string = "";
  date: string = "";
  amount: number = 0;
  type: InvoiceType = new InvoiceType();
  typeValue: number = 0;
  customerId: string = "";
  customer: CustomerModel = new CustomerModel();
  details: InvoiceDetailModel[] = [];
  invoiceNumber:string="";
  productId: string = "";
  quantity: number = 0;
  price: number = 0;
}

export class InvoiceType {
  name: string = "";
  value: number = 0;
}
