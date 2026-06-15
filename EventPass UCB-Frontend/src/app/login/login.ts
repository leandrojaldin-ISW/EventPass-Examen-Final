import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule], // Importante para leer los inputs
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  correo: string = '';
  password: string = '';
  mensajeError: string = '';

  // Inyectamos las herramientas para llamar a C# y para cambiar de pantalla
  constructor(private http: HttpClient, private router: Router) {}

  iniciarSesion() {
    this.mensajeError = '';

    const datosLogin = {
      correo: this.correo,
      password: this.password
    };

    this.http.post<any>('http://localhost:5056/api/Usuarios/login', datosLogin)
      .subscribe({
        next: (respuesta) => {
          // Guardamos los datos de Javi o Leandro en la memoria del navegador
          localStorage.setItem('usuarioId', respuesta.id);
          localStorage.setItem('usuarioNombre', respuesta.nombre);

          // ¡Redirección a tu tabla de eventos!
          this.router.navigate(['/eventos']);
        },
        error: (err) => {
          this.mensajeError = 'Correo o contraseña incorrectos.';
        }
      });
  }
}
