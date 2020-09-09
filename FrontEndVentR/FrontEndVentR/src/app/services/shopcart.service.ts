import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { ProductModel } from '../models/products.model';
import { ShopCartModel } from '../models/shopcart.model';


const endpoint = 'http://localhost:59292/api/CarritoCompras/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})
export class ShopcartService {

  

  constructor(private http: HttpClient) { }

  getByUserId(id:number){
    return this.http.get(endpoint + id).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getById'))
      );
  }

  addProductShopCart( shopCart:ShopCartModel){
    return this.http.post<any>(endpoint + shopCart.id_usuario , JSON.stringify(shopCart), httpOptions)
    .pipe(
      tap((user) => console.log('processing...')),
      catchError(this.handleError<any>('error login user')),
      map(resp =>{
        return resp;
      })
    );
  }

  deleteProductShopCart(id:number):Observable<any> {
    return this.http.delete(endpoint + id).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('no product deleted from shopcart'))
      );
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
