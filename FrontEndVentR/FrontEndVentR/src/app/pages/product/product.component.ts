import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

//Services
import { ProdutsService } from '../../services/produts.service';
//Models
import { ProductModel } from '../../models/products.model';
import { ShopcartService } from '../../services/shopcart.service';

import Swal from 'sweetalert2';
import { ShopCartModel } from '../../models/shopcart.model';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  productList:any;
  product:ProductModel = new ProductModel();
  shopcart:ShopCartModel;
  shopcartList:any;
  newProduct=true;
  userId:number;
  

  constructor(  private activatedRoute:ActivatedRoute, 
                private produtsService:ProdutsService,
                private shopcartService:ShopcartService,
                private router: Router
                ) {

    this.activatedRoute.params.subscribe( params =>{
      produtsService.getById(params['id']).subscribe((data:ProductModel)=>{
        this.product = data;
      });
    });
    this.userId = parseInt(localStorage.getItem("userId"));
    this.shopcart=new ShopCartModel();
   }

  ngOnInit(): void {
  }

  getShopcartProducts(){
    this.shopcartService.getByUserId(this.userId)
    .subscribe(resp =>{
      this.shopcartList = resp;
    })
  }

  getAllProducts(){
    this.produtsService.getAll()
    .subscribe(resp =>{
      this.productList = resp;
    })
  }

  add(shopcart:ShopCartModel){
    this.shopcartService.addProductShopCart(shopcart)
          .subscribe( resp => {
            if(resp){
              Swal.fire(
                'Agregado al carrito',
                'OK para continuar',
                'success'
              )
              this.router.navigateByUrl('/shopcart');             
            }else{
              Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Algo salio mal; intenta otra vez',
              })
            }
          })
  }

   addShopcart(idProduct:number){

    this.shopcart.id_usuario=this.userId;
    this.shopcart.idProducto=idProduct;

    this.shopcartService.getByUserId(this.userId)
    .subscribe( resp => {

      if(resp.length != 0){
        this.shopcartList = resp;
        for(let shopcart of this.shopcartList){
         if(shopcart.productos.idProducto == idProduct){
           let apply = shopcart.cantidad_Solicitada + 1;
           let available = shopcart.productos.cantidad
           if(apply > available){
             Swal.fire({
               icon: 'error',
               title: 'Ya tienes la cantidad máxima en el carrito',
               text: 'Solo hay: ' + available + ' en stock',
             })
           }else{
             this.add(this.shopcart);
             break;
           }
         }else{
           this.add(this.shopcart);
           this.shopcartService.cantItemsControl(1);
           break;
         }
        }
      }else{
        this.add(this.shopcart);
      }
    
    });
  }
}
