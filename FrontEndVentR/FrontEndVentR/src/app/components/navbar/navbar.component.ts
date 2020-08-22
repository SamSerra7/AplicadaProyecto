import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

//Services
import { AuthService } from '../../services/auth.service';
import { UsersService } from '../../services/users.service';

//Models
import { UserModel } from '../../models/user.model';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  cantItems:number=0;
  isLogin = false;
  user: any;
  userId: number;

  constructor(private auth: AuthService, private router:Router,private profile: UsersService) {
    this.user = new UserModel();   
    this.loadProfile();    
   }

  ngOnInit(): void {   
  }

   logout(){
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  loadProfile(){

    this.userId = parseInt(localStorage.getItem('token'));
    console.log(this.userId);

    if(this.userId > 0){
      this.isLogin == true;
    }
    this.user.email="admin@admin.com";
    this.user.name="Admin";

    console.log(this.user);

    /*
    this.profile.getById(parseInt(localStorage.getItem('token')))
    .subscribe( resp=>{
      this.user=resp;
    });
*/
  }

}
