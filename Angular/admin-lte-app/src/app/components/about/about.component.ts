import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlankComponent } from 'src/app/common/components/blank/blank.component';
import { SectionComponent } from 'src/app/common/components/section/section.component';

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [CommonModule, BlankComponent, SectionComponent],
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export default class AboutComponent {

}
