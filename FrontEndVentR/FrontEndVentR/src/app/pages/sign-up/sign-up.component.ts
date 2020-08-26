import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import Swal from 'sweetalert2';

import { UserModel } from '../../models/user.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  user: UserModel;
  form: FormGroup;
 

  constructor( private authService: AuthService, private fb: FormBuilder, private router: Router) {
    this.doForm();
    this.user = new UserModel();
  }

  ngOnInit(): void {}

  doForm(){

    this.form = this.fb.group({
      lastname: ['', [Validators.required, Validators.minLength(1), Validators.pattern('[a-zA-ZñÑáéíóúÁÉÍÓÚ]{2,254}')]],
      name:     ['', [Validators.required, Validators.minLength(1), Validators.pattern('[a-zA-ZñÑáéíóúÁÉÍÓÚ]{2,254}')]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.pattern('[a-zA-Z1234567890]{2,254}')]],
      email:    ['', [Validators.required, Validators.email]]
    });
  }

  get lastnameValid(){
    return this.form.get('lastname').invalid && this.form.get('lastname').touched;
  }
  get nameValid(){
    return this.form.get('name').invalid && this.form.get('name').touched;
  }
  get passwordValid(){
    return this.form.get('password').invalid && this.form.get('password').touched;
  }
  get emailValid(){
    return this.form.get('email').invalid && this.form.get('email').touched;
  }

  onSubmit(){

    if (this.form.invalid){
      return Object.values(this.form.controls).forEach(control=>{
        control.markAllAsTouched();
      });
    }else{

      Swal.fire({
        text: 'Espere por favor..',
        icon: 'info',
        allowOutsideClick: false
      });
  
      this.user.id_Usuario = 0;
      this.user.correo = this.form.value.email;
      this.user.contrasennia = this.form.value.password;
      this.user.id_Rol = 1;

      this.authService.adduser(this.user)
      .subscribe(resp=>{

        console.log(resp);
         if(resp){
           
          Swal.close();
          Swal.fire(
            'Registro exitoso',
            'Tu solicitud está en trámite',
            'success'
          )
          this.router.navigate(['/sign-in']);
        }else{
          Swal.fire({
            text: 'Correo no válido...',
            icon: 'error',
            title: 'Error al registrar'
         });
          
        }
      })
    }
  }

}
