import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../../models/products.model';
import { ShopCartModel } from '../../models/shopcart.model';
import { ProdutsService } from '../../services/produts.service';
import { Router } from '@angular/router';
import { ShopcartService } from '../../services/shopcart.service';

@Component({
  selector: 'app-shopcart',
  templateUrl: './shopcart.component.html',
  styleUrls: ['./shopcart.component.css']
})
export class ShopcartComponent implements OnInit {

  shopcart:any[]=[];
  products:ProductModel[]=[];
  subTotal:number=0;
  iva:number=0;
  total:number=0;
  userId:number;

  constructor(  private productService:ProdutsService,
                private router: Router,
                private shopcartService:ShopcartService) {

    this.userId= parseInt(localStorage.getItem("userId"));
    this.getShopcartProducts();
    this.getProducts();
    
  }

  ngOnInit(): void {
    
  }

  getShopcartProducts(){
    this.shopcartService.getByUserId(this.userId)
    .subscribe(productsResp =>{
      this.shopcart = productsResp;
      console.log(this.shopcart);
      this.getCalculations();
    })

    /*this.shopcart = JSON.parse(localStorage.getItem('shopcart'));*/
  }

  getCalculations(){
    for(let newShopcart of this.shopcart ){
      this.subTotal += newShopcart.productos.precio.value * newShopcart.productos.cantidad;
    }

    this.iva = this.subTotal * 0.13;
    this.total = this.iva + this.subTotal;

  }
  getProducts(){

    this.productService.getAll()
    .subscribe( products => {
      this.products = products;
    })

  }

  delete(id:number){
    console.log(id);
  }

  viewProduct(id:number){
    this.router.navigate(['product',id]);
  }
  

}
