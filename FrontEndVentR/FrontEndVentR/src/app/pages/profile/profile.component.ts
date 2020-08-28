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

    usersService.getById(parseInt(localStorage.getItem("userId")))
    .subscribe(user => {
      this.user = user;
    });
   }

  ngOnInit(): void {
  }

}
