import { Component, inject, OnInit } from '@angular/core';
import { ApiClient } from '../core/http/api-client';
import { RouterLink } from '@angular/router';
import { Evento } from './eventos';

@Component({
  selector: 'app-eventos',
  imports: [RouterLink],
  templateUrl: './evento-lista.html',
})
export class EventoLista implements OnInit {
  private api = inject(ApiClient);

  // ¡OJO AQUÍ! Cambia el '5056' por el puerto exacto en el que corre tu C# en Rider
  private url = 'http://localhost:5056/api/Eventos';

  eventos: Evento[] = [];

  ngOnInit() {
    this.cargarEventos();
  }

  ///Esta es una de las partes mas importatnes PEDIMOS LOS DATOS
  private cargarEventos() {
    console.log('Buscando eventos en el backend...');
    this.api.get<Evento[]>(this.url + '/lista').subscribe({// <-- Aquí está el problema
      next: data => this.eventos = data,
      error: error => console.error('Error al obtener eventos', error)
    });
  }

  eliminar(id: number): void {
    if(confirm('¿Estás seguro de eliminar este evento?')) {
      this.api.delete(this.url + '/borrar/' + id).subscribe({
        next: () => this.cargarEventos(),
        error: error => console.error('Error al eliminar evento', error)
      });
    }
  }
}
