import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { FileUploadModule } from 'primeng/fileupload';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast'
import { ButtonModule } from 'primeng/button';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, FileUploadModule, ToastModule, ButtonModule],
  providers: [MessageService],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  uploadedFiles: any[] = [];

  http = inject(HttpClient);

    constructor(
      private messageService: MessageService,
      private _http: HttpClient) {}

    onUpload(event:any) {
        for(let file of event.files) {
            this.uploadedFiles.push(file);
        }

        this.messageService.add({severity: 'info', summary: 'File Uploaded', detail: ''});
    }


    upload(){
      const formData = new FormData();

      for (let i = 0; i < this.uploadedFiles.length; i++) {
        const element = this.uploadedFiles[i];
        formData.append("files",element,element.name);
        
      }

      this.http.post("https://localhost:7066/api/Values/Save", formData)
      .subscribe(res=> {
        
      });      
    }
}
