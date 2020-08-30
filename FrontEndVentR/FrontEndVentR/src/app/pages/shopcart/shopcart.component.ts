import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../../models/products.model';
import { ShopCartModel } from '../../models/shopcart.model';
import { ProdutsService } from '../../services/produts.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shopcart',
  templateUrl: './shopcart.component.html',
  styleUrls: ['./shopcart.component.css']
})
export class ShopcartComponent implements OnInit {

  shopcart:ShopCartModel[]=[];
  products:ProductModel[]=[];
  subTotal:number=0;
  iva:number=0;
  total:number=0;

  constructor(  private productService:ProdutsService,
                private router: Router) {

    this.shopcart = JSON.parse(localStorage.getItem('shopcart'));
    this.getProducts();
    this.getCalculations();
  }

  ngOnInit(): void {
  }

  getCalculations(){

    for(let newShopcart of this.shopcart ){
      this.subTotal += newShopcart.precio * newShopcart.cantidad;
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
