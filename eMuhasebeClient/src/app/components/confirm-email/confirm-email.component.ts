import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-confirm-email',
  standalone: true,
  imports: [],
  template: `
  <div style="height: 90vh; display:flex; align-items:center; justify-content:center;flex-direction: column;">
    <h1>{{response}}</h1>
    <a href="/login" class="btn btn-primary">Giriş sayfasına dön</a>
  </div>
  `
})
export class ConfirmEmailComponent {
  email: string = "";
  response: string = "";
  constructor(private activated: ActivatedRoute, private http: HttpService) {
    this.activated.params.subscribe(params => {
      this.email = params["email"];
      this.confirmEmail();
    });
  }
  confirmEmail() {
    this.http.post("Auth/ConfirmEmail", { email: this.email }, (res: any) => {
      this.response = res;
    });
  }
}
