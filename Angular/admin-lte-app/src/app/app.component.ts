import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgxSpinnerModule } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgxSpinnerModule],
  template: `
    <ngx-spinner bdColor = "rgba(0, 0, 0, 0.8)" size = "medium" color = "#fff" type = "timer" [fullScreen] = "true">
      <p style="color: white" > Loading... </p>
    </ngx-spinner> 
    <router-outlet></router-outlet>`
})
export class AppComponent { }
