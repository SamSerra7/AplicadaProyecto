import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../../models/products.model';
import { ProdutsService } from '../../services/produts.service';
import { Router } from '@angular/router';
import { ShopcartService } from '../../services/shopcart.service';
import Swal from 'sweetalert2';
import { ShopCartModel } from '../../models/shopcart.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-shopcart',
  templateUrl: './shopcart.component.html',
  styleUrls: ['./shopcart.component.css']
})
export class ShopcartComponent implements OnInit {

  shopcart:any[]=[];
  products:any[];
  subTotal:number=0;
  iva:number=0;
  total:number=0;
  userId:number;

  constructor(  private router: Router,
                private produtsService:ProdutsService,
                private shopcartService:ShopcartService) {
    this.userId= parseInt(localStorage.getItem("userId"));
    this.getShopcartProducts();
  }

  ngOnInit(): void {
    
  }

  buy(userId:number){

    Swal.fire({
      title: 'Confirmar pago',
      text: "El monto total de la factura es de " + this.total,
      icon: 'warning',
      showCancelButton: true,
      cancelButtonText: '¡No, cancelar!',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: '¡Pagar!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.shopcartService.registerSale(userId)
        .subscribe(resp=>{
          if(resp){
            Swal.fire(
              '¡Pago exitoso!',
              'Facturagenerada por ' + resp,
              'success'
            )
            this.getShopcartProducts();
          }else{
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: '¡Algo pasó!',
              footer: 'Intenta otro vez'
            })
          }
        });
      }
    })


  
  }

  lessProduct(shopcart:any){

    let newCartShop:ShopCartModel = new ShopCartModel();
    newCartShop.id_usuario = this.userId;
    newCartShop.idProducto = shopcart.productos.idProducto;
    newCartShop.cantidad = shopcart.cantidad_Solicitada;

    if(newCartShop.cantidad == 1){

      Swal.fire({
        title: '¿Esta seguro(a)?',
        text: "¡El producto sera eliminado del carrito!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Si, borrar!'
      }).then((result) => {
        if (result.isConfirmed) {
          this.shopcartService.deleteProductShopCart(newCartShop)
      .subscribe( resp =>{
        if(resp){   
          Swal.fire(
            '¡Borrado!',
            'El producto ha sido borrado',
            'success'
          )   
          this.getShopcartProducts();
          this.getCalculations();
        }
      });
        }
      })



      
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

  plusProduct(shopcart:any){

    if(shopcart.cantidad_Solicitada >= shopcart.productos.cantidad){
     
      Swal.fire(
        'Cantidad insuficiente',
        'Lo sentimos, solo hay '+ shopcart.productos.cantidad +' '+shopcart.productos.nombre + ' disponible(s)' ,
        'question'
      )

    }else{

      let newCartShop:ShopCartModel = new ShopCartModel();
      newCartShop.id_usuario = this.userId;
      newCartShop.idProducto = shopcart.productos.idProducto;

      this.shopcartService.plusProductsCartShop(newCartShop)
      .subscribe( resp =>{
        if(resp){      
          this.getShopcartProducts();
          this.getCalculations();
        }
      });
    }
    
  }

  getAllProducts():any{
    this.produtsService.getAll()
    .subscribe(resp =>{
      this.products = resp;
    })
  }

  available(shopcartItem:any){


    this.getAllProducts();

    console.log('2:' +  this.products );
    for(let product of  this.products){
      console.log('3:' + product);
      if(product.idProducto == shopcartItem.productos.idProducto){
        if(product.cantidad >= shopcartItem.cantidad_Solicitada){
          console.log('4:' + shopcartItem.cantidad_Solicitada);
          return true;
        }else{
          return true;
        }
      }
    }
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
    this.total =  this.subTotal;


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
