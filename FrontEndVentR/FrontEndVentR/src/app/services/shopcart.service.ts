import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { ShopCartModel } from '../models/shopcart.model';
import { AuthService } from './auth.service';

const endpoint = 'http://localhost:59292/api/';



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

  getByUserId(userId:number):Observable<any>{
    return this.http.get(endpoint + 'CarritoCompras/' + userId).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getById'))
      );
  }

  addProductShopCart( shopCart:ShopCartModel){
    return this.http.post<any>(endpoint + 'CarritoCompras/' + shopCart.id_usuario , JSON.stringify(shopCart), httpOptions)
    .pipe(
      tap((user) => console.log('processing...')),
      catchError(this.handleError<any>('error login user')),
      map(resp =>{
        return resp;
      })
    );
  } 

  plusProductsCartShop(shopCart:ShopCartModel):Observable<any>{

   // this.cantItemsControl(1);
    return this.http.get( endpoint + 'usuario/' + shopCart.id_usuario + '/Producto/' + shopCart.idProducto + '/Agregarcantidad').pipe(
      map(this.extractData),
      catchError(this.handleError<any>('plus product...'))
      );

  }

  lessProductsCartShop(shopCart:ShopCartModel):Observable<any>{

  //  this.cantItemsControl(-1);
    return this.http.get(
      endpoint + 'usuario/' + shopCart.id_usuario + '/Producto/' + shopCart.idProducto + '/DisminuirCantidad').pipe(
      map(this.extractData),
      catchError(this.handleError<any>('less product...'))
      );
    
  }

  cantItemsControl(cantiItems:number){
    let newCantItems:number = parseInt(localStorage.getItem("cantItems"));
    newCantItems += cantiItems;
    localStorage.setItem("cantItems", newCantItems.toString());

  }

  deleteProductShopCart(shopCart:ShopCartModel):Observable<any> {
    this.cantItemsControl(-1);
    return this.http.get(endpoint +'usuario/'+ shopCart.id_usuario + '/Producto/' + shopCart.idProducto + '/borrarDelCarrito').pipe(
      map(this.extractData),
      catchError(this.handleError<any>('no product deleted from shopcart'))
      );
  }

  registerSale(userId:number){
    return this.http.get(endpoint +'usuario/venta/' + userId).pipe(
      tap((user) => console.log('added sale')),
      catchError(this.handleError<any>('error add sale')),
      map(resp =>{
        return resp;
      })
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
