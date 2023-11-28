
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { TodoModel } from './models/todo.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  todos:TodoModel[] = [];
  todo: TodoModel = new TodoModel();


  constructor(
    private http: HttpClient
  ){}

  getAll(){
    this.http.get<TodoModel[]>("https://jsonplaceholder.typicode.com/todos")
    .subscribe(res=> {
      console.log(res);
      this.todos = res;
    })
  }

  hasCompleted(completed: boolean){
    if(completed) return 'green'
    else return 'red'
  }

  isTwoMode(id: number = 0,userId: number = 0){
    const result = (id + userId);

    if(result % 2) return true;
    else return false;
  }
}
