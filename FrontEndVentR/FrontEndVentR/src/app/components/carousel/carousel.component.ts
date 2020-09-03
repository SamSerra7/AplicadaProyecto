import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  bestSellerProducts:any=[];
  userId:number;
  
  constructor(private userService:UsersService,
              private router:Router) { 
    this.userId= parseInt(localStorage.getItem("userId"));
    this.loadProducts();
  }

  ngOnInit(): void {
  }

  loadProducts(){
    this.userService.getBestSellerProducts(this.userId)
    .subscribe( resp =>{
      this.bestSellerProducts=resp;
    })
   
  }

  viewProduct(id:number){
    this.router.navigate(['product',id]);
  }


}
