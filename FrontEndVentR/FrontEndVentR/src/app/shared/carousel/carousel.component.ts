import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../../services/users.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  bestSellerProducts:any=[];
  userId:number;
  
  constructor(private userService:UsersService,
              private authService:AuthService,
              private router:Router) { 
    this.userId= parseInt(localStorage.getItem("userId"));
    this.loadProducts();
  }

  ngOnInit(): void {
  }

  loadProducts(){
    if(this.authService.isLogin()){
      this.userService.getMostSearchedProducts(this.userId)
      .subscribe( resp =>{
        this.bestSellerProducts=resp;
      })
    }else{
      this.userService.getMostSearchedProducts(-1)
      .subscribe( resp =>{
        this.bestSellerProducts=resp;
      })
    }
 
   
  }

  viewProduct(id:number){
    this.router.navigate(['product',id]);
  }


}
