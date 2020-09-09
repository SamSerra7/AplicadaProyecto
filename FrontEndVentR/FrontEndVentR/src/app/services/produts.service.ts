import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { ProductModel } from '../models/products.model';

const endpoint = 'http://localhost:59292/api/producto/';
const endpointUserProduct = 'http://localhost:59292/api/usuario/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ProdutsService {

  products:any=[];


  constructor(private http: HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get(endpoint).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getAll'))
      );
  }

  getById(productId): Observable<any> {
    let userId = localStorage.getItem('userId');
    if(userId){
      return this.http.get(endpointUserProduct + userId +'/producto/'+ productId).pipe(
        map(this.extractData),
        catchError(this.handleError<any>('getById'))
        );  
    }
    return this.http.get(endpoint +  productId).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getById'))
      );
  }

  
  findProduct(textToFind:string){
    this.products =  JSON.parse(localStorage.getItem('products'));
    if(!textToFind){
      return this.products;
    }
    textToFind = textToFind.toLowerCase();
    let newProducts:any[]=[];
    for(let product of this.products){
      let nombre = product.nombre.toLowerCase();
      let detalle = product.detalle.toLowerCase();
      if( nombre.indexOf(textToFind) >= 0 || detalle.indexOf(textToFind) >= 0 ){
        newProducts.push(product)
      }
    }
    return newProducts;
  }

  private extractData(res: Response) {
    let body = res;
    return body || { }; 
  }
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
