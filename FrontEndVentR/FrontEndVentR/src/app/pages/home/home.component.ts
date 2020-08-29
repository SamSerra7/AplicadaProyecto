import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';


//Services
import { ProdutsService } from '../../services/produts.service';
import { ProductModel } from '../../models/products.model';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  //variables
  products:any=[];
  loading=true;
  @ViewChild( CdkVirtualScrollViewport) viewport: CdkVirtualScrollViewport;

  constructor(  public produtsService: ProdutsService,
                private route: ActivatedRoute,
                private router: Router) { 

    this.getAllProducts();
    this.products = new ProductModel();
  }

  ngOnInit(): void {
  }

  getAllProducts(){

    this.produtsService.getAll().subscribe((data:{})=>{
      this.products=data;
      localStorage.setItem('products', JSON.stringify(this.products));
      this.loading=false;
    });
  }

  viewProduct(id:number){

    this.router.navigate(['product',id]);
  }

  goStart(){
    this.viewport.scrollToIndex( 0 );
  }

  goMedium(){
    this.viewport.scrollToIndex( this.products.length / 2 );
  }

  goEnd(){
    this.viewport.scrollToIndex( this.products.length );
  }

}
