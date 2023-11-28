import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlankComponent } from '../components/blank/blank.component';
import { SectionComponent } from '../components/section/section.component';
import { ModalComponent } from '../components/modal/modal.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BlankComponent,
    SectionComponent,
    ModalComponent,
    FormsModule
  ],
  exports: [
    CommonModule,
    BlankComponent,
    SectionComponent,
    ModalComponent,
    FormsModule
  ]
})
export class GenericModule { }
