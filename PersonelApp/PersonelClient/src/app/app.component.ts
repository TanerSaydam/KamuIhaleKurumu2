import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UserPipe } from './user.pipe';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, FormsModule, UserPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  search: string = "";
  users: UserModel[] = [];

  constructor(private http: HttpClient){
    this.getAll();
  }

  getAll(){
    this.http.get<UserModel[]>("https://localhost:7298/api/Users/GetAll")
    .subscribe(res=> {
      this.users = res;
    })
  }
}

export class UserModel{
  name: string = "";
  lastname: string = "";
  email: string = "";
  phoneNumber: string = "";
  country: string = "";
  city: string = "";
  postalCode: string = "";
  fullAddress: string = "";
}
