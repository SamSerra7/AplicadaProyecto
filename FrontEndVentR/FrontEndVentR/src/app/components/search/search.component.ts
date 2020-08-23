import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


//services
import { ProdutsService } from '../../services/produts.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  products:any=[];
  textTofind:string;

  constructor(  private activatedRoute:ActivatedRoute,
                private produtsService:ProdutsService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {    
      this.textTofind = params['text'];
      this.products = this.produtsService.findProduct(this.textTofind);   
      console.log(this.products.length)   
    });
  }

}
