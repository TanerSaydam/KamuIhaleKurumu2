import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent {
 
  
  constructor(
    public _auth: AuthService
  ){
   this._auth.checkAuthorize();
  }
  

  // checkAuthorize(){
  //   if(localStorage.getItem("response") === null){
  //     this.router.navigateByUrl("/login");
  //   }else{
  //     const response:any = localStorage.getItem("response");
  //     try {
  //       const responseJson = JSON.parse(response);
  //     if(responseJson.token === null){
  //       this.router.navigateByUrl("/login");
  //     }else{
  //       const token:string = responseJson.token;
  //       var decoded:any = jwt_decode(token);
  //       console.log(decoded);
  //       //debugger
  //       const now = Math.floor(new Date().getTime() / 1000);
  //       //const now2 = new Date().getTime() / 60;
  //       if(now > decoded.exp){
  //         this.router.navigateByUrl("/login");
  //       }
  //     }
  //     } catch (error) {
  //       this.router.navigateByUrl("/login");
  //     }
      
  //   }
  // }
}


