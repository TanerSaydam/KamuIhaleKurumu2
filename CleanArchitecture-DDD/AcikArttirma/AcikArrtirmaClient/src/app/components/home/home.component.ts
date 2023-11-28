import { Component, ElementRef, OnInit, ViewChild, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeLoaderComponent } from '../home-loader/home-loader.component';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { log } from 'console';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, HomeLoaderComponent, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  isLoaded = signal(false)
  data: Notice[] = []
  addModel: Notice = new Notice();
  @ViewChild("addModalCloseBtn") addModelCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  constructor(private http: HttpClient){}

  ngOnInit(): void {
   this.getAll();
  }

  add(form: NgForm){
    if(form.valid){
      this.http.post("https://localhost:7295/api/Notices/Add", this.addModel)
      .subscribe({
        next: ()=> {
          this.getAll();
          // const el = document.getElementById("addModalCloseBtn");
          // el?.click();
          this.addModelCloseBtn?.nativeElement.click();
          this.addModel = new Notice();
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);  
        }
      })
    }
  }

  getAll(){
    this.isLoaded.set(false);
    this.http.post<Notice[]>("https://localhost:7295/api/Notices/GetAll", {})
    .subscribe({
      next: (res)=> {
        this.data = res;
        this.isLoaded.set(true);
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);  
        this.isLoaded.set(true);    
      }
    })
  }
}

export class Notice{
  id: string = "";
  title: string = "";
  description: string = "";
  startingFee: Money = new Money();
  isCompleted: boolean = false;
  expireTime: number = 1;
  createdDate: string = "";
}

export class Money{
  amount: number = 0;
  currency:string = "TRY";
}
