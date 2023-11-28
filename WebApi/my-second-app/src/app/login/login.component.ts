import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  template: `
  <h1>Login Page</h1>
  <button (click)="loginWithAnotherWebApi()">1. Siteden Giriş Yap</button>   
  `
})
export class LoginComponent {

  constructor(private http: HttpClient) {}

  loginWithAnotherWebApi(){
    this.http.post("https://localhost:7048/api/Auth/LoginWithProvide", {userName: "test"}).subscribe({
      next: (res:any)=> {
        console.log(res)
        this.http.post("https://localhost:7017/api/Auth/LoginWithProvider",res)
        .subscribe(data=> console.log(data))
      },
      error: (err: HttpErrorResponse)=> {
        console.log(err)
      }
    })
  }
}
