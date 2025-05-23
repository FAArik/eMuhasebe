import { Component, ElementRef, ViewChild } from '@angular/core';
import { CompanyModel } from '../../models/company.model';
import { NgForm } from '@angular/forms';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { SharedModule } from '../../modules/shared.module';
import { CompanyPipe } from '../../pipes/company.pipe';

@Component({
  selector: 'app-companies',
  standalone: true,
  imports: [SharedModule, CompanyPipe],
  templateUrl: './companies.component.html',
  styleUrl: './companies.component.css'
})
export class CompaniesComponent {
  companies: CompanyModel[] = [];
  search: string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel: CompanyModel = new CompanyModel();
  updateModel: CompanyModel = new CompanyModel();

  constructor(
    private http: HttpService,
    private swal: SwalService
  ) { }

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.http.post<CompanyModel[]>("Companies/GetAll", {}, (res) => {
      this.companies = res;
    });
  }
  migrateCompanyDatabases() {
    this.http.get<CompanyModel[]>("Companies/MigrateAll", (res: any) => {
    });
  }
  seedData() {
    this.swal.callSwal("Seed Data oluştur?", `Bu şirket için fake veri oluşturmak istiyor musunuz?`, () => {
      this.http.get<string>("SeedData/Create", (res: any) => {
        this.swal.callToast(res)
      });
    }, "Onayla")
  }

  create(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("Companies/CreateCompany", this.createModel, (res) => {
        this.swal.callToast(res);
        this.createModel = new CompanyModel();
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: CompanyModel) {
    this.swal.callSwal("Şirketi Sil?", `${model.name} şirketini silmek istiyor musunuz?`, () => {
      this.http.post<string>("Companies/DeleteCompanyById", { id: model.id }, (res) => {
        this.getAll();
        this.swal.callToast(res, "info");
      });
    })
  }

  get(model: CompanyModel) {
    this.updateModel = { ...model };
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("Companies/UpdateCompany", this.updateModel, (res) => {
        this.swal.callToast(res, "info");
        this.updateModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }
}
