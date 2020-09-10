import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../../models/products.model';
import { ProdutsService } from '../../services/produts.service';
import { Router } from '@angular/router';
import { ShopcartService } from '../../services/shopcart.service';
import Swal from 'sweetalert2';
import { ShopCartModel } from '../../models/shopcart.model';

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

  constructor(  private router: Router,
                private shopcartService:ShopcartService) {
    this.userId= parseInt(localStorage.getItem("userId"));
    this.getShopcartProducts();
  }

  ngOnInit(): void {
    
  }

  buy(userId:number){
    console.log('Pagando:' + userId)
  }

  lessProduct(shopcart:any){

    let newCartShop:ShopCartModel = new ShopCartModel();
    newCartShop.id_usuario = this.userId;
    newCartShop.idProducto = shopcart.productos.idProducto;
    newCartShop.cantidad = shopcart.cantidad_Solicitada;

    if(newCartShop.cantidad == 0){
      this.shopcartService.deleteProductShopCart(newCartShop)
      .subscribe( resp =>{
        if(resp){      
          this.getShopcartProducts();
          this.getCalculations();
        }
      });
    }else{
      this.shopcartService.lessProductsCartShop(newCartShop)
      .subscribe( resp =>{
        if(resp){      
          this.getShopcartProducts();
          this.getCalculations();
        }
      });
    }  
  }

  plusProduct(id:number){

    let newCartShop:ShopCartModel = new ShopCartModel();
    newCartShop.id_usuario = this.userId;
    newCartShop.idProducto = id;

    this.shopcartService.plusProductsCartShop(newCartShop)
    .subscribe( resp =>{
      if(resp){      
        this.getShopcartProducts();
        this.getCalculations();
      }
    });
  }

  getShopcartProducts(){
    this.shopcartService.getByUserId(this.userId)
    .subscribe(productsResp =>{
      this.shopcart = productsResp;
      this.getCalculations();
    })
  }

  getCalculations(){

    
    this.subTotal=0;

    for(let newShopcart of this.shopcart ){
      this.subTotal += newShopcart.productos.precio.value * newShopcart.cantidad_Solicitada;
    }

    this.iva = this.subTotal * 0.13;
    this.total = this.iva + this.subTotal;

  }

  delete(id:number){

    let newCartShop:ShopCartModel = new ShopCartModel();
    newCartShop.id_usuario = this.userId;
    newCartShop.idProducto = id;



    
    this.shopcartService.deleteProductShopCart(newCartShop)
    .subscribe( resp => {
      if(resp){

        console.log('Borrado:'+ resp)

      }
    })

    

  }

  viewProduct(id:number){
    this.router.navigate(['product',id]);
  }  

}
