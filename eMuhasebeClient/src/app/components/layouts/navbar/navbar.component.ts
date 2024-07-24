import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  constructor(
    private router: Router,
    public auth:AuthService
  ) { 
    this.auth.isAuthenticated();
  }

  logout() {
    localStorage.clear();
    this.router.navigateByUrl("/login");
  }
}
