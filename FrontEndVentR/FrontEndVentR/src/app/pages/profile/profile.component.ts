import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user:any;

  constructor( private usersService:UsersService) {

    this.usersService.getById(parseInt(localStorage.getItem("userId")))
    .subscribe(user => {
      this.user = user;
      console.log(this.user);
    });
   }

  ngOnInit(): void {
  }

}
