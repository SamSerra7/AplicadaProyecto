import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

//Services
import { ProdutsService } from '../../services/produts.service';
import { ProductModel } from '../../models/products.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  //variables
  products:any=[];

  constructor( public produtsService: ProdutsService,private route: ActivatedRoute, private router: Router) { 

    this.getAllProducts();
    this.products = new ProductModel();
  }

  ngOnInit(): void {
  }

  getAllProducts(){

    this.produtsService.getAll().subscribe((data:{})=>{
      this.products=data;
    });

  }

  viewProduct(id:number){

    this.router.navigate(['product',id]);
  }

}
