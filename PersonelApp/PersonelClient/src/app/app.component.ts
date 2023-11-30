import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  search: string = "";
  orgUsers: UserModel[] = [];
  users: UserModel[] = [];
  pageNumbers: number[] = [];
  selectedPage: number = 1;
  pageCount: number = 0;
  totalCount: number = 0;
  ids: string[] = [];

  constructor(private http: HttpClient) {
    this.getAll();
  }

  selectOrUnselect(id: string){
    const index = this.ids.findIndex(p=> p == id);
    if(index < 0){
      this.ids.push(id)
    }else{
      this.ids.splice(index,1);
    }
  }

  sendEmail(){
    this.http.post("https://localhost:7298/api/Users/SendEmailSelectedUsers", this.ids)
    .subscribe(()=> {
      console.log("Gönderim başarılı");      
    })
  }

  getAll() {
    this.http.get<UserModel[]>("https://localhost:7298/api/Users/GetAll")
      .subscribe(res => {
        this.orgUsers = res;
        this.pageCount = Math.ceil(this.orgUsers.length / 5);
        this.users = this.orgUsers.slice(((this.selectedPage - 1) * 5), (this.selectedPage * 5));
        this.setPageNumbersFromList();
        this.totalCount = this.orgUsers.length;
      })
  }

  removeById(id: string){
    this.http.post("https://localhost:7298/api/Users/RemoveById", {id: id})
    .subscribe(()=> {
      this.getAll();
    })
  }

  setPageNumbersFromList() {
    const startPage = Math.max(1, this.selectedPage - 3);
    const endPage = Math.min(this.pageCount, this.selectedPage + 3);
    this.pageNumbers = [];
    for (let i = startPage; i < endPage; i++) {
      this.pageNumbers.push(i);
    }
  }

  changePage(pageNumber: number) {
    this.selectedPage = pageNumber;
    this.setPageNumbersFromList();

    this.users = this.orgUsers.slice(((this.selectedPage - 1) * 5), (this.selectedPage * 5));
  }

  searchFromList() {
    if (this.search === "") this.changePage(1);
    
    this.selectedPage = 1;

    this.users = this.orgUsers.filter(p =>
      p.name.toLowerCase().includes(this.search.toLowerCase()) ||
      p.lastname.toLowerCase().includes(this.search.toLowerCase()) ||
      p.email.toLowerCase().includes(this.search.toLowerCase()) ||
      p.phoneNumber.toLowerCase().includes(this.search.toLowerCase()) ||
      p.country.toLowerCase().includes(this.search.toLowerCase()) ||
      p.city.toLowerCase().includes(this.search.toLowerCase()) ||
      p.postalCode.toLowerCase().includes(this.search.toLowerCase()) ||
      p.fullAddress.toLowerCase().includes(this.search.toLowerCase()));

    this.pageCount = Math.ceil(this.users.length / 5);
    this.totalCount = this.users.length;
    this.users = this.users.slice(((this.selectedPage - 1) * 5), (this.selectedPage * 5));

    this.setPageNumbersFromList();
  }
}

export class UserModel {
  id: string = "";
  name: string = "";
  lastname: string = "";
  email: string = "";
  phoneNumber: string = "";
  country: string = "";
  city: string = "";
  postalCode: string = "";
  fullAddress: string = "";
}
