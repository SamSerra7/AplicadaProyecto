import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProdutsService } from '../../services/produts.service';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products:any=[];
  textTofind:string;
  @ViewChild( CdkVirtualScrollViewport) viewport: CdkVirtualScrollViewport;
  loading=true;

  constructor(private activatedRoute:ActivatedRoute,
              private produtsService:ProdutsService,
              private router: Router) {
    this.loadProducts();
  }

  ngOnInit(): void {
  }

  loadProducts(){
    
    this.activatedRoute.params.subscribe(params => {    
      this.textTofind = params['text'];      
      if(this.textTofind){
        this.products = this.produtsService.findProduct(this.textTofind);  
        if(this.products){
          this.loading=false;
        }
      }else{
        this.produtsService.getAll()
        .subscribe(resp =>{
          this.products=resp;
          if(this.products){
            localStorage.setItem('products', JSON.stringify(this.products));
            this.loading=false;
          }
        }); 
      }
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
