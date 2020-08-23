import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

//Services
import { ProdutsService } from '../../services/produts.service';
//Models
import { ProductModel } from '../../models/products.model';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  product:any={};

  constructor(  private activatedRoute:ActivatedRoute, 
                private produtsService:ProdutsService) {

    this.activatedRoute.params.subscribe( params =>{
      console.log(params['id']);
      produtsService.getById(params['id']).subscribe((data:{})=>{
        this.product=data;
      });
    });
   }

  ngOnInit(): void {
  }

}
