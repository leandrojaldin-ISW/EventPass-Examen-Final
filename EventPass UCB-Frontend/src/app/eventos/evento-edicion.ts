import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { Evento } from './eventos';
import { FormsModule } from '@angular/forms';
import { ApiClient } from '../core/http/api-client';

@Component({
  selector: 'app-evento-edicion',
  imports: [RouterLink, FormsModule],
  templateUrl: './evento-edicion.html',
})
export class EventoEdicion implements OnInit {
  evento: Evento = { id: 0, nombre: '', ubicacion: '', precioEntrada: 0 };

  private api = inject(ApiClient);
  private url = 'http://localhost:5056/Eventos'; // OJO: Mismo puerto que usaste en la lista

  private router = inject(Router);
  private route = inject(ActivatedRoute);

  ngOnInit() {
    const idParam = this.route.snapshot.paramMap.get('id');
    // Si el ID no es "nuevo", entonces vamos a C# a buscar los datos para editarlos
    if (idParam && idParam !== 'nuevo') {
      const id = Number(idParam);
      if (id > 0) {
        this.api.get<Evento>(this.url + '/' + id).subscribe({
          next: data => this.evento = data,
          error: error => console.error('Error al obtener evento', error)
        });
      }
    }
  }

  guardar() {
    if (this.evento.id === 0) {
      // Si el ID es 0, es un evento nuevo -> Hacemos POST (Crear)
      this.api.post<Evento>(this.url, this.evento).subscribe({
        next: () => this.router.navigate(['/eventos']),
        error: error => console.error('Error al crear evento', error)
      });
    } else {
      // Si ya tiene ID, es un evento existente -> Hacemos PUT (Actualizar)
      this.api.put(this.url + '/' + this.evento.id, this.evento).subscribe({
        next: () => this.router.navigate(['/eventos']),
        error: error => console.error('Error al actualizar evento', error)
      });
    }
  }
}
