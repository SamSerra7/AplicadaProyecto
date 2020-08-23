import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import Swal from 'sweetalert2';

import { AuthService } from '../../services/auth.service';
import { UserModel } from '../../models/user.model';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  user: UserModel = new UserModel();
  form: FormGroup;
  rememberme: boolean = false;

  constructor( private authService: AuthService, 
    private fb: FormBuilder, 
    private router: Router ) {
    
   }

  ngOnInit(): void {

    if( localStorage.getItem('email')){
      this.user.email = localStorage.getItem('email');
      this.rememberme = true;
    }
    this.doForm();
  }

  get emailValid(){
    return this.form.get('email').invalid && this.form.get('email').touched;
  }
  get passwordValid(){
    return this.form.get('password').invalid && this.form.get('password').touched;
  }
  

  doForm(){

    this.form = this.fb.group({
      password: ['', [Validators.required, Validators.minLength(8)]],
      email:    [this.user.email, [Validators.required, Validators.email]],
      rememberme: [this.rememberme]
    });
  }

  login( ){

    if (this.form.invalid){
      return Object.values(this.form.controls).forEach(control=>{
        control.markAllAsTouched();
      });
    }

    Swal.fire({
      text: 'Espere por favor..',
      icon: 'info',
      allowOutsideClick: false
    });

    Swal.showLoading();

    this.user.email = this.form.value.email;
    this.user.password = this.form.value.password;

    var resp = this.authService.login(this.user);
    
    if(resp){
      //if login
      Swal.close();        
      if(this.form.value.rememberme){
        //save remember email session 
        localStorage.setItem('email', this.user.email);
      }else{
        //delete remember email session
        localStorage.setItem('email', '');
      }
      this.router.navigateByUrl('/home');
    }else{
      //if no login
      Swal.fire({
        text: 'Datos incorrectos...',
        icon: 'error',
        title: 'Error al autenticar'
      });
    }    

    /*
    this.authService.login(this.user)
    .subscribe(resp=>{
      if(resp){
        //if login
        Swal.close();        
        if(this.form.value.rememberme){
          //save remember email session 
          localStorage.setItem('email', this.user.email);
        }else{
          //delete remember email session
          localStorage.setItem('email', '');
        }
        this.router.navigateByUrl('/home');
      }else{
        //if no login
        Swal.fire({
          text: 'Datos incorrectos...',
          icon: 'error',
          title: 'Error al autenticar'
        });
      }    
      */
  }
}

