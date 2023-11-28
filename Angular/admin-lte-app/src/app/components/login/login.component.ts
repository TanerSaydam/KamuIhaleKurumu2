import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http'
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export default class LoginComponent {

constructor(
  private router: Router,
  private http: HttpClient,
  private spinner: NgxSpinnerService
){
  
}

  signIn(form: NgForm){
    this.spinner.show();
    this.http.post<any>("https://kik.ecnorow.com/api/Auth/Login", form.value)
    .subscribe({
      next: (res)=> {
        localStorage.setItem("response", JSON.stringify(res));        
        document.location.href = "/";
        this.spinner.hide();
      },
      error: (err: HttpErrorResponse)=> {
        console.log(err);
        this.spinner.hide();
        
      }
    })
  }
}
