import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

//services
import { ProdutsService } from '../../services/produts.service';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  products:any=[];
  textTofind:string;
  @ViewChild( CdkVirtualScrollViewport) viewport: CdkVirtualScrollViewport;

  constructor(  private activatedRoute:ActivatedRoute,
                private produtsService:ProdutsService,
                private router: Router) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {    
      this.textTofind = params['text'];
      this.products = this.produtsService.findProduct(this.textTofind);   
      console.log(this.products.length)   
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
