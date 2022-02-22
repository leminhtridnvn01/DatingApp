import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserLogin } from '../_models/userLogin';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  httpOptions = {
    headers: new HttpHeaders({  'Content-Type': 'application/json'  }),
  };
  baseUrl = 'https://localhost:5001/api/accounts'
  constructor(private httpClient: HttpClient) { }

  login(userLogin: UserLogin) : Observable<any>{
    
    return this.httpClient
      .post<any>(`${this.baseUrl}/Login`, userLogin, this.httpOptions);
  }
}
