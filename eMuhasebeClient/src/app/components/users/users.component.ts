import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UserModel } from '../../models/user.model';
import { NgForm } from '@angular/forms';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { UserPipe } from '../../pipes/user.pipe';
import { SharedModule } from '../../modules/shared.module';
import { CompanyModel } from '../../models/company.model';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [SharedModule, UserPipe],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  users: UserModel[] = [];
  companies:CompanyModel[]=[];
  search: string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel: UserModel = new UserModel();
  updateModel: UserModel = new UserModel();

  constructor(
    private http: HttpService,
    private swal: SwalService
  ) { }

  ngOnInit(): void {
    this.getAll();
    this.getAllCompanies();
  }

  getAll() {
    this.http.post<UserModel[]>("Users/GetAll", {}, (res) => {
      this.users = res;
    });
  }
  getAllCompanies() {
    this.http.post<CompanyModel[]>("Companies/GetAll", {}, (res) => {
      this.companies = res;
    });
  }

  create(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("Users/CreateUser", this.createModel, (res) => {
        this.swal.callToast(res);
        this.createModel = new UserModel();
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: UserModel) {
    this.swal.callSwal("Kullanıcıyı Sil?", `${model.fullName} kullanıcısını silmek istiyor musunuz?`, () => {
      this.http.post<string>("Users/DeleteUserById", { id: model.id }, (res) => {
        this.getAll();
        this.swal.callToast(res, "info");
      });
    })
  }

  get(model: UserModel) {
    this.updateModel = { ...model };
    this.updateModel.companyIds=this.updateModel.companyUsers.map(x=>x.companyId);
  }

  update(form: NgForm) {
    if (form.valid) {
      if(this.updateModel.password=="") this.updateModel.password=null;
      this.http.post<string>("Users/UpdateUser", this.updateModel, (res) => {
        this.swal.callToast(res, "info");
        this.updateModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }
}
