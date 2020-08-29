import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

//Services
import { ProdutsService } from '../../services/produts.service';
//Models
import { ProductModel } from '../../models/products.model';
import { ShopcartService } from '../../services/shopcart.service';
import { ShopCartModel } from 'src/app/models/shopcart.model';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  product:any;
  shopcart:ShopCartModel;
  shopcarts:any=[];
  newProduct=true;
  userId:number;

  constructor(  private activatedRoute:ActivatedRoute, 
                private produtsService:ProdutsService,
                private shopcartService:ShopcartService
                ) {

    this.activatedRoute.params.subscribe( params =>{
      console.log(params['id']);
      produtsService.getById(params['id']).subscribe((data:{})=>{
        this.product=data;
      });
    });
    this.userId = parseInt(localStorage.getItem("userId"));
    this.shopcart=new ShopCartModel();
   }

  ngOnInit(): void {
  }

  addShopcart(idProduct:number){

    this.shopcart.id_usuario=this.userId;
    this.shopcart.idProducto=idProduct;
    this.shopcartService.addProductShopCart(this.shopcart)
    .subscribe( resp => {
      if(resp){
        console.log("agregado");
      }else{
        console.log("NO agregado");

      }
    } )

  }
  /*
  addShopcart(idProduct:number){

    this.shopcart.precio=this.product.precio.value;
    this.shopcart.detalle = this.product.detalle;
    this.shopcart.nombre= this.product.nombre;
    this.shopcart.urlImg = this.product.urlImg;
    this.shopcart.id_producto=idProduct;
    this.shopcart.id_usuario=parseInt( localStorage.getItem("userId"));
    
    this.saveInCart(this.shopcart);
  }

  saveInCart(newShopcart:ShopCartModel){

    this.shopcarts =  JSON.parse(localStorage.getItem('shopcart'));
    let newShopcarts:any []=[];

    if(!this.shopcarts){
      newShopcarts.push(newShopcart);
      localStorage.setItem("shopcart",JSON.stringify(newShopcarts));
      return false;
    }

    for(let shopcart of this.shopcarts){
      //verifica si ya el producto existe
      if(shopcart.id_producto == newShopcart.id_producto){
        this.newProduct=false;
        shopcart.cantidad ++;
      }      
      //llena lista con productos ya existentes
      newShopcarts.push(shopcart);
    }

    if(this.newProduct == true){
      //carga nuevo producto a la lista
      newShopcarts.push(newShopcart);
    }
   
    localStorage.setItem("shopcart",JSON.stringify(newShopcarts));
    return true;
    
  }
  */

}
