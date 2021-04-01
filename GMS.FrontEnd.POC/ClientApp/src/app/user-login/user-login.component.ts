import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { LoginModel } from '../Models/Users';


@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  loginModel: LoginModel;
  
  constructor(private router: Router, private http: HttpClient) { }
  
  ngOnInit() {
    this.loginModel = new LoginModel();
  }
  login() {  
      this.http
        .post("https://localhost:44371/api/Values/Login", this.loginModel)
        .subscribe(res => {
          console.log(res);
          this.router.navigate(['./home']);
        });
   
  }

  getResponseData(res: any) {
    debugger
    console.log(res);
  }

}

