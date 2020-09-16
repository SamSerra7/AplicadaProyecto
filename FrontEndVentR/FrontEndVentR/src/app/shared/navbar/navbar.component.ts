import { Component } from '@angular/core';
import { Router } from '@angular/router';

//Services
import { AuthService } from '../../services/auth.service';

//Models
import { UserModel } from '../../models/user.model';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {

  cantItems:number = 0;
  isLogin = false;
  user: UserModel;
  shopcarts:any=[];

  constructor(private auth: AuthService, 
              private router:Router) {
    this.user = new UserModel();    
    this.loadProfile();  
   }

  findProduct(textToFind:string){    
    this.router.navigate(['/results',textToFind]);
  }

   logout(){     
    this.auth.logout();
    window.location.replace('/sign-in');
  }

   loadProfile(){
    this.user.id_Usuario = parseInt(localStorage.getItem("userId"));
    this.isLogin =  this.auth.isLogin(); 
    if(this.isLogin){
      this.user.correo = localStorage.getItem('token');
    }  
    this.cantItems = parseInt(localStorage.getItem("cantItems"));
  }
}