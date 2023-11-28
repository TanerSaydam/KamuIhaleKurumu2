import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private _auth: AuthService,
    private http: HttpClient
  ) { }

  apiUrl: string = "https://localhost:7048/api/TodoPost";
  apiRequest(methodName: ApiMethodName, data:any, callBack: (res:any)=> void, parametre: string = ""){

    this._auth.checkAuthorize();

    const responseString:any = localStorage.getItem("response");
    const response: any = JSON.parse(responseString);
    const token = response.token;
    
    this.http.post(`${this.apiUrl}/${methodName}/${parametre}`, data,{
      headers: {
        "Authorization": "Bearer " +token
      }
    })
    .subscribe(res=> {
      callBack(res)
    })
  }


  
}
type ApiMethodName = "Create" | "Update" | "DeleteById" | "GetAll"