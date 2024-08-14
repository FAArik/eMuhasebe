import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/home/home.component';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { UsersComponent } from './components/users/users.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { CompaniesComponent } from './components/companies/companies.component';
import { CashRegistersComponent } from './components/cash-registers/cash-registers.component';
import { CashRegisterDetailsComponent } from './components/cash-register-details/cash-register-details.component';
import { BanksComponent } from './components/banks/banks.component';

export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "confirm-email/:email",
        component: ConfirmEmailComponent
    },
    {
        path: "",
        component: LayoutsComponent,
        canActivateChild: [() => inject(AuthService).isAuthenticated()],
        children: [
            {
                path: "",
                component: HomeComponent
            },
            {
                path: "users",
                component: UsersComponent
            },
            {
                path: "companies",
                component: CompaniesComponent
            },
            {
                path: "cash-registers",
                children: [
                    {
                        path: "",
                        pathMatch: "full",
                        component: CashRegistersComponent
                    },
                    {
                        path:"details/:id",
                        component:CashRegisterDetailsComponent
                    }
                ]
            },
            {
                path: "banks",
                children: [
                    {
                        path: "",
                        pathMatch: "full",
                        component: BanksComponent
                    },
                    {
                        path:"details/:id",
                        component:CashRegisterDetailsComponent
                    }
                ]
            },

        ]
    }
];
