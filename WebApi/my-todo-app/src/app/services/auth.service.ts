import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private router: Router,
    private http: HttpClient,
  ) { 
    this.roles = this.getUserRoles();
  }

  checkAuthorize() {
    const response:any = localStorage.getItem("response");
  
    if (!response || !JSON.parse(response).token) {
      this.router.navigateByUrl("/login");
    }
  
    try {
      const token = JSON.parse(response).token;
      const decoded:any = jwtDecode(token);
      if (Math.floor(new Date().getTime() / 1000) > decoded.exp) {
        const refreshToken = JSON.parse(response).refreshToken;
        const refreshTokenExpires = JSON.parse(response).refreshTokenExpires;
        if(new Date().getTime() / 1000 > refreshTokenExpires){
          this.router.navigateByUrl("/login");
        }else{
          this.http.post("https://localhost:7048/api/Auth/CreateTokenByRefreshToken", {refreshToken: refreshToken}).subscribe(res=> {
            localStorage.setItem("response",JSON.stringify(res));
            this.checkAuthorize();
          })
        }
         
      }
    } catch {
      this.router.navigateByUrl("/login");
    }
  }

  getUserRoles(){
    const response = localStorage.getItem("response");
    if(response !== null){
      const responseJson = JSON.parse(response);
      const roles = responseJson.roles;

      return roles;
    }
  }
  roles: {id: string, name: string}[] = [];

  isAuthorized(name: string){
    const result = this.roles.filter(p=> p.name === name);

    
    if(result.length > 0) return true;
    else return false;
  }
}
