import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

//Services
import { AuthService } from '../../services/auth.service';
import { UsersService } from '../../services/users.service';

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
  user: any;
  userId: number;
  shopcarts:any=[];

  constructor(private auth: AuthService, 
              private router:Router,
              private profile: UsersService,
              private shopcartService:ShopcartService) {
    this.userId= parseInt(localStorage.getItem("userId"));
    this.user = new UserModel();   
    this.loadProfile();  
    this.countProducts();
   }

  ngOnInit(): void {   
  }

  countProducts(){

    this.shopcartService.getByUserId(this.userId)
    .subscribe(productsResp =>{

      console.log(productsResp);

      if(!productsResp){
        return false;
      }
      for(let shopcart of productsResp){
        this.cantItems += shopcart.cantidad_Solicitada;    
      } 
    })

    /*this.shopcarts =  JSON.parse(localStorage.getItem('shopcart'));*/

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
