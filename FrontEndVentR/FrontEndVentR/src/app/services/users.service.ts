import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

import { UserModel } from '../models/user.model';

const endpoint = 'http://localhost:59292/api/Usuario/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  private extractData(res: Response) {
    let body = res;
    return body || { };
  }

  getAll(): Observable<any> {
    return this.http.get(endpoint).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getAll'))
      );
  }


  getById(id): Observable<any> {
    return this.http.get(endpoint + id).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('no user by id'))
      );
  }
  
  getMostSearchedProducts(userId:number){
    if(!userId){
      userId = -1;
    }
    return this.http.get(endpoint + userId +'/producto').pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getById'))
      );
  }



  /*----------------------------------------------*/
  updateuser(users: UserModel): Observable<any>{
    return this.http.put<any>(endpoint + 'put/', JSON.stringify(users), httpOptions).pipe(
      tap((inquiry) => console.log('updated user')),
      catchError(this.handleError<any>('error update user'))
    );
  } 

  delete(id): Observable<any> {
    return this.http.delete(endpoint+ 'delete/' + id).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('no user deletedby id'))
      );
  }


   /*----------------------------------------------*/ 
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
