import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

import { UserModel } from '../models/user.model';
import { UserRoleModel } from '../models/user-role.model';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private _url = '';
  private _urlrole = '';
  constructor(private http: HttpClient) { }

  private extractData(res: Response) {
    let body = res;
    return body || { };
  }

  approveUser(userrole: UserRoleModel): Observable<any>{
    return this.http.post<any>(this._urlrole , JSON.stringify(userrole), httpOptions).pipe(
      tap((inquiry) => console.log('approved student')),
      catchError(this.handleError<any>('error approved student'))
    );
  }
  
  addTeacher(teacher: UserModel): Observable<any>{
    return this.http.post<any>(this._url + 'teacher/', JSON.stringify(teacher), httpOptions).pipe(
      tap((inquiry) => console.log('added teacher')),
      catchError(this.handleError<any>('error add teacher'))
    );
  }

  updateuser(users: UserModel): Observable<any>{
    return this.http.put<any>(this._url + 'put/', JSON.stringify(users), httpOptions).pipe(
      tap((inquiry) => console.log('updated user')),
      catchError(this.handleError<any>('error update user'))
    );
  }

  getById(id): Observable<any> {
    return this.http.get(this._url + 'id/' + id).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('no user by id'))
      );
  }

  delete(id): Observable<any> {
    return this.http.delete(this._url + 'delete/' + id).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('no user deletedby id'))
      );
  }

  deleteRole(id): Observable<any> {
    return this.http.delete(this._urlrole + 'delete/' + id).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('no user deletedby id'))
      );
  }

  getAll(): Observable<any> {
    return this.http.get(this._url + 'getAll').pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getAll'))
      );
  }

  getNewUsers(): Observable<any> {
    return this.http.get(this._url + 'getNewUser').pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getNewUser'))
      );
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
