import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpService } from '../services/http.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  todos: any = [];
  work: string = "";
  updateTodo: UpdateTodoModel = new UpdateTodoModel();
  isUpdateFormActive: boolean = false;

  constructor(
    private _http: HttpService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }  

  getAll() {
    this._http.apiRequest("GetAll",{},res=>this.todos = res);
  }


  create() {
    this._http.apiRequest("Create", {work: this.work}, res=>{
      this.work = "";
      this.getAll();
    })
  }

  deleteById(id: number) {
    this._http.apiRequest("DeleteById",{},(res=> this.getAll()),id.toString());
  }

  get(model: any){
    this.updateTodo.id = model.id;
    this.updateTodo.work = model.work;
    this.isUpdateFormActive = true;
  }

  update(){
    this._http.apiRequest("Update", this.updateTodo, res=> {
      this.getAll();
      this.cancel();
    });
  }

  cancel(){
    this.isUpdateFormActive = false;
  }
}

export class UpdateTodoModel{
  id: number = 0;
  work: string = "";
}