import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

//Services
import { AuthService } from '../../services/auth.service';
import { UsersService } from '../../services/users.service';

//Models
import { UserModel } from '../../models/user.model';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {

  cantItems:number=0;
  isLogin = false;
  user: any;
  userId: number;

  constructor(private auth: AuthService, 
              private router:Router,
              private profile: UsersService) {
    this.user = new UserModel();   
    this.loadProfile();    
   }

  ngOnInit(): void {   
  }

  findProduct(textToFind:string){    
    this.router.navigate(['/results',textToFind]);
  }

   logout(){
    this.auth.logout();
    this.router.navigate(['/sign-in']);
  }

  loadProfile(){
    this.isLogin =  this.auth.isLogin();    
    if(this.isLogin){
      this.user.correo = localStorage.getItem('token');
    }    

  }
}
