import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Necesario para el formulario
import { ApiClient } from '../core/http/api-client';

@Component({
  selector: 'app-comentario-lista',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './comentario-lista.html',
})
export class ComentarioLista implements OnInit {
  comentarios: any[] = [];

  // Objeto temporal para el formulario
  nuevoComentario = {
    idEvento: 20, // Por defecto apuntamos a un evento existente para pruebas
    autor: 'Javi',
    texto: ''
  };

  private api = inject(ApiClient);

  ngOnInit() {
    this.cargarComentarios();
  }

  cargarComentarios() {
    this.api.get<any[]>('http://localhost:5056/api/Comentarios/lista')
      .subscribe({
        next: (data) => this.comentarios = data,
        error: (err) => console.error('Error al cargar comentarios', err)
      });
  }

  publicarComentario() {
    if (!this.nuevoComentario.texto) {
      alert("El comentario no puede estar vacío");
      return;
    }

    this.api.post('http://localhost:5056/api/Comentarios/crear', this.nuevoComentario)
      .subscribe({
        next: () => {
          alert('¡Comentario publicado!');
          this.nuevoComentario.texto = ''; // Limpiamos el input
          this.cargarComentarios(); // Recargamos la tabla para ver el nuevo comentario
        },
        error: (err) => {
          console.error('Error al publicar', err);
          alert('Asegúrate de que el backend de Comentarios esté listo.');
        }
      });
  }
}
