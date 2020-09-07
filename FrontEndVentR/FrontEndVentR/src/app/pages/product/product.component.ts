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

  product:any;
  shopcart:ShopCartModel;
  shopcarts:any=[];
  newProduct=true;
  userId:number;
  

  constructor(  private activatedRoute:ActivatedRoute, 
                private produtsService:ProdutsService,
                private shopcartService:ShopcartService,
                private router: Router
                ) {

    this.activatedRoute.params.subscribe( params =>{
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
    } )

  }
 
}
