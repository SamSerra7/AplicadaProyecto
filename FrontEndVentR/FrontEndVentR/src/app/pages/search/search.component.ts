import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor( private router: Router ) { }

  ngOnInit(): void {
  }
  
  viewProduct(id:number){
    this.router.navigate(['product',id]);
  }
}
