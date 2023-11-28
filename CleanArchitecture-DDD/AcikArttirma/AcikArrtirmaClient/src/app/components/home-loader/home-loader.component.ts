import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home-loader',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home-loader.component.html',
  styleUrl: './home-loader.component.css'
})
export class HomeLoaderComponent {
 
  data: any[] = [
    1,2,3,4,5,6,7,8,9,10,11,12
  ]
}
