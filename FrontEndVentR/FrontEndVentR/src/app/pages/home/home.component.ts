import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor( private router: Router) {
  }

  ngOnInit(): void {
  }

  viewProduct(id:number){
    this.router.navigate(['product',id]);
  }
}
