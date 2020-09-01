import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

//Services
import { AuthService } from '../../services/auth.service';

//Models
import { UserModel } from '../../models/user.model';
import { ShopcartService } from '../../services/shopcart.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {

  cantItems:number=0;
  isLogin = false;
  user: UserModel;
  shopcarts:any=[];

  constructor(private auth: AuthService, 
              private router:Router,
              private shopcartService:ShopcartService) {
    this.user = new UserModel(); 
    this.user.id_Usuario = parseInt(localStorage.getItem("userId"));
    this.loadProfile();  
    this.countProducts();
   }

  ngOnInit(): void {   
  }

  countProducts(){
    if(this.user.id_Usuario){
      this.shopcartService.getByUserId(this.user.id_Usuario)
      .subscribe(productsResp =>{
        if(productsResp){
          for(let shopcart of productsResp){
            this.cantItems += shopcart.cantidad_Solicitada;    
          }
        }
      })
    }
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
