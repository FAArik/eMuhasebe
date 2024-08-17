import { Component, ElementRef, ViewChild } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SharedModule } from '../../modules/shared.module';
import { DatePipe } from '@angular/common';
import { BankDetailPipe } from '../../pipes/bank-detail.pipe';
import { BankDetailModel } from '../../models/bank-detail.model';
import { BankModel } from '../../models/bank.model';

@Component({
  selector: 'app-bank-details',
  standalone: true,
  imports: [SharedModule, BankDetailPipe],
  templateUrl: './bank-details.component.html',
  styleUrl: './bank-details.component.css',
  providers: [DatePipe]
})
export class BankDetailsComponent {
  bank: BankModel = new BankModel();
  banks: BankModel[] = [];
  bankId: string = "";
  search: string = "";
  startDate: string = "";
  endDate: string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel: BankDetailModel = new BankDetailModel();
  updateModel: BankDetailModel = new BankDetailModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private activated: ActivatedRoute,
    private date: DatePipe
  ) {
    this.activated.params.subscribe((params) => {
      this.bankId = params["id"];
      this.startDate = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
      this.endDate = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
      this.createModel.date = this.date.transform(new Date(), "yyyy-MM-dd") ?? "";
      this.createModel.bankId = this.bankId;
      this.getAll();
      this.getAllBanks();
    });
  }

  getAll() {
    this.http.post<BankModel>("BankDetails/GetAll", { bankId: this.bankId, startDate: this.startDate, endDate: this.endDate }, (res) => {
      this.bank = res;
    });
  }
  getAllBanks() {
    this.http.post<BankModel[]>("Bank/GetAll", {}, (res) => {
      this.banks = res.filter(x => x.id != this.bankId);
    });
  }

  create(form: NgForm) {
    if (form.valid) {
      this.createModel.amount = +this.createModel.amount;
      this.createModel.oppositeAmount = +this.createModel.oppositeAmount;
      if (this.createModel.recordType === 0) {
        this.createModel.oppositeBankId = null;
      }
      this.http.post<string>("BankDetails/Create", this.createModel, (res) => {
        this.swal.callToast(res);
        this.createModalCloseBtn?.nativeElement.click();
        form.resetForm(
          {
            date: this.date.transform(new Date(), "yyyy-MM-dd") ?? "",
            bankId: this.bankId,
            type: 0,
            recordType: 0,
          }
        );
        this.getAll();
      });
    }
  }

  deleteById(model: BankDetailModel) {
    this.swal.callSwal("Kasa Hareketini Sil?", `${model.date} tarihteki ${model.description} açıklamalı hareketi silmek istiyor musunuz?`, () => {
      this.http.post<string>("BankDetails/DeleteById", { id: model.id }, (res) => {
        this.getAll();
        this.swal.callToast(res, "info");
      });
    })
  }

  get(model: BankDetailModel) {
    this.updateModel = { ...model };
    this.updateModel.amount = this.updateModel.depositAmount + this.updateModel.withdrawalAmount;
    this.updateModel.type = this.updateModel.depositAmount > 0 ? 0 : 1;
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("BankDetails/Update", this.updateModel, (res) => {
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
  setOppositeBank() {
    const cash = this.banks.find(x => x.id == this.createModel.oppositeBankId);
    if (cash) {
      this.createModel.oppositeBank = cash;
    }
  }
}