import { Component, inject , OnInit } from '@angular/core';
import { ApiClient } from '../core/http/api-client';
import { Comentario } from './comentario';

@Component({
  selector: 'app-comentarios',
  imports: [],
  templateUrl: './comentario-lista.html',
})

export class ComentarioLista implements OnInit {
  private api = inject(ApiClient);
  private url = 'http://localhost:5056/Comentarios';

  comentarios : Comentario[] = [];

  //El boton de arranque automatico
  ngOnInit() {
    this.cargarComentarios();
  }

  private cargarComentarios (){
    this.api.get<Comentario[]>(this.url).subscribe({
      next: data => this.comentarios = data,
      error: error => console.error("Error al obtener comentarios", error)
    });
  }
}

