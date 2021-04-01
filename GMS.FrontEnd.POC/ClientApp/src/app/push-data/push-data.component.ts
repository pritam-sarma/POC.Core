import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Users} from '../Models/Users';


@Component({
  selector: 'app-push-data',
  templateUrl: './push-data.component.html',
  styleUrls: ['./push-data.component.css']
})
export class PushDataComponent implements OnInit {

  currentUser: Users;
  constructor(private http: HttpClient) {
  }  

  ngOnInit() {
    this.http
      .get("http://localhost:31154/api/Values")
      .subscribe(res => {
        let user = res as Users
        this.currentUser = user;
      });
  }
}
