import { Component } from '@angular/core';
import { ControlSidebarComponent } from './control-sidebar/control-sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { MainSidebarComponent } from './main-sidebar/main-sidebar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { PreloaderComponent } from './preloader/preloader.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-layouts',
  standalone: true,
  imports: [
      ControlSidebarComponent, 
      FooterComponent,
      MainSidebarComponent,
      NavbarComponent,
      PreloaderComponent,
      RouterOutlet],
  templateUrl: './layouts.component.html',
  styleUrls: ['./layouts.component.css']
})
export default class LayoutsComponent {
 
}
