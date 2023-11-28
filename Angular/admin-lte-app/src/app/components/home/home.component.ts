import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { GenericModule } from 'src/app/common/modules/generic.module';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [GenericModule],
  providers: [DatePipe],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export default class HomeComponent implements OnInit {
  personels: Personel[] = []
  dateInput: string;
  image: any;
  addModel = new Personel();
  constructor(
    private http: HttpClient,
    private date: DatePipe,
    private router: Router
  ){
    this.dateInput = this.date.transform(new Date(), 'yyyy-MM-dd');
  }
  ngOnInit(): void {
   this.getAllPersonels();
  }

  getAllPersonels(){
    const responseString: any= localStorage.getItem("response");
    const response: any = JSON.parse(responseString);
    const token = response.token;

    this.http.get<Personel[]>("https://localhost:7198/api/Personels/GetAll", {
      headers: {
        "Authorization": "Bearer " + token
      }
    })
    .subscribe({
      next: (res)=> {
        this.personels = res;
        
      },
      error: (err: HttpErrorResponse)=> {
        console.log(err);
        
      }
    })
  }

  getImage(event: any){
      this.image = event.target.files[0];
  }

  closeModal(modalId: string){
    const modalName = modalId + "_close";
    const el:any = document.getElementById(modalName);
    el.click();
  }

  save(form: NgForm){
    if(form.valid){
      const responseString: any= localStorage.getItem("response");
      const response: any = JSON.parse(responseString);
      const token = response.token;

      
      const formData = new FormData();
      formData.append("name",this.addModel.name);
      formData.append("lastName",this.addModel.lastName);
      formData.append("profession",this.addModel.profession);
      formData.append("salary",this.addModel.salary.toString());
      formData.append("startingDate",this.dateInput);
      formData.append("image",this.image, this.image.name);

      this.http.post("https://kik.ecnorow.com/api/Personels/CreateWithImage", formData, {
        headers: {
          "Authorization": "Bearer " + token
        }
      })
      .subscribe({
        next: (res)=> {
          this.getAllPersonels();
          this.addModel = new Personel();
          this.image = undefined;
          const closeEl = document.getElementById("addModal_close");  
          closeEl.click();      
        },
        error: (err: HttpErrorResponse)=> {
          console.log(err);
          
        }
      })
    }
  }
  

  removeById(data: Personel){
    Swal.fire({
      title: "Sil?",
      text: `${data.name} ${data.lastName} silmek istiyor musunuz?`,
      icon: "question",
      showConfirmButton: true,
      confirmButtonText : "Sil",
      showCancelButton: true,
      cancelButtonText : "Vazgeç"
    }).then(res=> {
      const responseString: any= localStorage.getItem("response");
      const response: any = JSON.parse(responseString);
      const token = response.token;


      if(res.isConfirmed){
        console.log("Silme işlemi yapılabilir");
        this.http.get("https://localhost:7198/api/Personels/RemoveById/" + data.id, {
        headers: {
          "Authorization": "Bearer " + token
        }
      })
        .subscribe(res=> {
          this.getAllPersonels();
        })
      }
    });
  }
}
 
export class Personel {
  name: string = "";
  lastName: string = "";
  profession: string = "";
  imageUrl: string = ""
  salary: number = 0;
  startingDate: string = ""
  leavingDate: string | null | undefined;
  leavingStatus: string | null | undefined;
  id: string = "";
}

