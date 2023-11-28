import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { AuthService } from './services/auth.service';
import { SkipComponent } from './components/skip/skip.component';

export const routes: Routes = [
    {
        path: "login",
        loadComponent: ()=> import("./components/login/login.component")
    },
    {
        path: "",
        loadComponent: ()=> import("./components/layouts/layouts.component"),
        canActivateChild: [()=> inject(AuthService).checkAuth()],
        children: [
            {
                path: "",
                loadComponent: ()=> import("./components/home/home.component")
            },
            {
                path: "about",
                loadComponent: ()=> import("./components/about/about.component")
            },
            {
                path: "contact",
                loadComponent: ()=> import("./components/contact/contact.component")
            },
            {
                path: "skip",
                component: SkipComponent
            }
        ]
    }
];
