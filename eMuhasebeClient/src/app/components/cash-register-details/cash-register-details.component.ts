import { Component, ElementRef, ViewChild } from '@angular/core';
import { CashRegisterModel } from '../../models/cash-register.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';
import { CashRegisterDetailModel } from '../../models/cash-register-detail.model';
import { ActivatedRoute } from '@angular/router';
import { SharedModule } from '../../modules/shared.module';
import { CashRegisterDetailPipe } from '../../pipes/cash-register-detail.pipe';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-cash-register-details',
  standalone: true,
  imports: [SharedModule, CashRegisterDetailPipe],
  templateUrl: './cash-register-details.component.html',
  styleUrl: './cash-register-details.component.css',
  providers: [DatePipe]
})
export class CashRegisterDetailsComponent {
  cashRegister: CashRegisterModel = new CashRegisterModel();
  cashregisters: CashRegisterModel[] = [];
  cashRegisterId: string = "";
  search: string = "";
  startDate: string = "";
  endDate: string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel: CashRegisterDetailModel = new CashRegisterDetailModel();
  updateModel: CashRegisterDetailModel = new CashRegisterDetailModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private activated: ActivatedRoute,
    private date: DatePipe
  ) {
    this.activated.params.subscribe((params) => {
      this.cashRegisterId = params["id"];
      this.startDate = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
      this.endDate = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
      this.createModel.date = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
      this.createModel.cashRegisterId = this.cashRegisterId;
      this.getAll();
      this.getAllCashRegisters();
    });
  }

  getAll() {
    this.http.post<CashRegisterModel>("CashRegisterDetails/GetAll", { cashRegisterId: this.cashRegisterId, startDate: this.startDate, endDate: this.endDate }, (res) => {
      this.cashRegister = res;
    });
  }
  getAllCashRegisters() {
    this.http.post<CashRegisterModel[]>("CashRegister/GetAll", {}, (res) => {
      this.cashregisters = res.filter(x => x.id != this.cashRegisterId);
    });
  }

  create(form: NgForm) {
    if (form.valid) {
      this.createModel.amount = +this.createModel.amount;
      this.createModel.oppositeAmount = +this.createModel.oppositeAmount;
      
      if (this.createModel.recordType === 0) {
        this.createModel.oppositeCashRegisterId = null;
      }

      if(this.createModel.oppositeAmount ===0) this.createModel.oppositeAmount =this.createModel.amount

      this.http.post<string>("CashRegisterDetails/Create", this.createModel, (res) => {
        this.swal.callToast(res);
        this.createModalCloseBtn?.nativeElement.click();
        form.resetForm(
          {
            date: this.date.transform(new Date(), "yyyy-MM-dd") ?? "",
            cashRegisterId: this.cashRegisterId,
            type: 0,
            recordType: 0,
          }
        );
        this.getAll();
      });
    }
  }

  deleteById(model: CashRegisterDetailModel) {
    this.swal.callSwal("Kasa Hareketini Sil?", `${model.date} tarihteki ${model.description} açıklamalı hareketi silmek istiyor musunuz?`, () => {
      this.http.post<string>("CashRegisterDetails/DeleteById", { id: model.id }, (res) => {
        this.getAll();
        this.swal.callToast(res, "info");
      });
    })
  }

  get(model: CashRegisterDetailModel) {
    this.updateModel = { ...model };
    this.updateModel.amount = this.updateModel.depositAmount + this.updateModel.withdrawalAmount;
    this.updateModel.type = this.updateModel.depositAmount > 0 ? 0 : 1;
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("CashRegisterDetails/Update", this.updateModel, (res) => {
        this.swal.callToast(res, "info");
        this.updateModalCloseBtn?.nativeElement.click();
        form.resetForm();
        this.getAll();
      });
    }
  }

  changeCurrencyNameToSymbol(name: string) {
    switch (name) {
      case "TL":
        return "₺";
      case "USD":
        return "$";
      case "EUR":
        return "€";
      default:
        return "";
    }
  }
  setOppositeCashRegister() {
    const cash = this.cashregisters.find(x => x.id == this.createModel.oppositeCashRegisterId);
    if (cash) {
      this.createModel.oppositeCashRegister = cash;
    }
  }
}