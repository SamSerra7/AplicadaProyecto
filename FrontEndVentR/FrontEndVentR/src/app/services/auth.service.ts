import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { UserModel } from '../models/user.model';
import { UsersService } from './users.service';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _url = '';

  userToken: string;
  date: any;
  user: any;

  constructor( private usersService: UsersService, private http: HttpClient) {
    this.user = new UserModel() ;
    this.readToken();
    this.getUser();
  }

  private extractData(res: Response) {
    let body = res;
    return body || { };
  }

  getUser(){
    this.usersService.getById(parseInt(this.userToken))
    .subscribe( resp=>{
      this.user=resp;
    });
  }

  logout(){
    localStorage.removeItem('token')
  }

  login( user: UserModel){
    return this.http.post<any>(this._url + 'login/', JSON.stringify(user), httpOptions)
    .pipe(
      tap((user) => console.log('processing...')),
      catchError(this.handleError<any>('error login user')),
      map(resp =>{
        if(resp){
          this.saveToken(resp.usersId);
        }
        return resp;
      })
    );
   }

   private saveToken(idUser: string){
      this.userToken = idUser;
      localStorage.setItem('token', this.userToken);
   }

   readToken(){
     
    if(localStorage.getItem('token')){
      this.userToken = localStorage.getItem('token');
    }else{
      this.userToken = '';
    }
    return this.userToken;
   }

  adduser( user: UserModel){

    return this.http.post<any>(this._url, JSON.stringify(user), httpOptions)
    .pipe(
      tap((user) => console.log('added user')),
      catchError(this.handleError<any>('error add user')),
      map(resp =>{
        return resp;
      })
    );

   }

  isLogin(): boolean{
    return parseInt( this.userToken) > 0;
  }

  haveRole(role:string): boolean{
    if(role === this.user.password){
      return true;
    }
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
