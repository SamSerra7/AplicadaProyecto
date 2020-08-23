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

  products:any[]=[];

  constructor(  private activatedRoute:ActivatedRoute,
                private produtsService:ProdutsService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      
      this.products = this.produtsService.findProduct(params['text']);

      console.log(this.products);
      
    }
    )
  }

}
