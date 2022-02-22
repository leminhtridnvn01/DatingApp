import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  users: User[] =[];
  constructor(private httpClient: HttpClient) { }
  ngOnInit(): void {
    this.fetchUsers();
  }
  title = 'dating-app';

  fetchUsers(){
    this.httpClient
      .get('https://localhost:5001/api/Users')
      .subscribe((restponse) =>{
        this.users = restponse as User[];
      });
  }
}
