import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { UserModel } from '../models/user.model';
import { UsersService } from './users.service';

const endpoint = 'http://localhost:59292/api/Usuario/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  userTokenEmail: string;
  userTokenId: number;
  date: any;
  user: any;
  responseStatus: number;
  users:any=[];

  constructor(  private usersService: UsersService,
                private http: HttpClient) {
    this.user = new UserModel() ;
    this.readToken();
  }

  private extractData(res: Response) {
    let body = res;
    return body || { };
  }

  login( user: UserModel){
    return this.http.post<any>(endpoint + 'VerificarUsuario/', JSON.stringify(user), httpOptions)
    .pipe(
      tap((user) => console.log('processing auth...')),
      catchError(this.handleError<any>('error login user')),
      map(resp =>{
        console.log("Login service:"+resp);
        if(resp){
          //save email an id in token
          this.saveToken(user.correo);
        }
        return resp;
      })
    );
   }

   saveUserId(){
    this.usersService.getAll()
    .subscribe(resp=>{
      this.users=resp;      
      for(let user of this.users){
        if( user.correo == localStorage.getItem("token")){
          localStorage.setItem('userId',user.id_Usuario);
          return this.user=user;
        }
      }
    })
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    /*localStorage.removeItem('shopcart');*/
    
  }

   private saveToken(correo: string){
      this.userTokenEmail = correo;
      localStorage.setItem('token', this.userTokenEmail);
      this.saveUserId();
   }

   readToken(){     
    if(localStorage.getItem('token')){
      this.userTokenEmail = localStorage.getItem('token');
    }else{
      this.userTokenEmail = '';
    }
    return this.userTokenEmail;
   }
  
   /*
   adduser( user: UserModel){
     return this.http.post(endpoint, JSON.stringify(user), httpOptions)
     .pipe(
       map((resp:any) => {
        console.log("Map");
        return resp;
       }),
       catchError((err: any)=>{
        return err.error.text;
       })
     );
   } 
   */
   
  adduser( user: UserModel){
    return this.http.post<any>(endpoint, JSON.stringify(user), httpOptions).pipe(
      tap((user) => console.log('added user')),
      catchError(this.handleError<any>('error add user')),
      map(resp =>{
        return resp;
      })
    );
   }
   

  isLogin(): boolean{
    
    if(this.userTokenEmail){
      return true;
    }
    return false;    
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
