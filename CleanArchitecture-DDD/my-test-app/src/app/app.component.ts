import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { TodoPipe } from './todo.pipe';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, TodoPipe, FormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isTrue: boolean = true;
  todos: any[] = [];
  search: string = "";
  constructor(
    private http:HttpClient
 ){
  //this.getAll();
 }

 getAll(){
  this.http.get("https://jsonplaceholder.typicode.com/todos")
  .subscribe((res:any)=> {
    this.todos = res;
  })
 }
}
