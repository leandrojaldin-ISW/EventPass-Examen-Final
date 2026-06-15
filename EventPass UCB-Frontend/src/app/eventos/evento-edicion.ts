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
  // Objeto inicial sincronizado con C#
  evento: Evento = {
    id: 0,
    nombre: '',
    descripcion: '',
    fecha: '',
    ubicacion: '',
    tipoEvento: 'Pagado',
    precioEntrada: 0
  };

  private api = inject(ApiClient);
  private url = 'http://localhost:5056/api/Eventos';

  private router = inject(Router);
  private route = inject(ActivatedRoute);

  ngOnInit() {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam && idParam !== 'nuevo') {
      const id = Number(idParam);
      if (id > 0) {
        this.api.get<Evento>(this.url + '/buscar/' + id).subscribe({
          next: data => {
            this.evento = data;
            // Pequeño truco para que el select se llene bien al editar
            this.evento.tipoEvento = data.precioEntrada > 0 ? 'Pagado' : 'Gratuito';
          },
          error: error => console.error('Error al obtener evento', error)
        });
      }
    }
  }

  guardar() {
    // 1. Rescatamos el ID de la memoria del navegador y lo convertimos a número
    const miId = Number(localStorage.getItem('usuarioId'));

    // 2. Armamos el paquete final combinando los datos del formulario con el ID real
    const datosParaEnviar = {
      ...this.evento,        // Copia todos los campos (nombre, fecha, etc.)
      idOrganizador: miId    // Agrega la llave faltante para el backend
    };

    if (this.evento.id === 0) {
      // Enviamos "datosParaEnviar" en lugar de "this.evento"
      this.api.post<Evento>(this.url + '/crear', datosParaEnviar).subscribe({
        next: () => this.router.navigate(['/eventos']),
        error: error => console.error('Error al crear evento', error)
      });
    } else {
      // Lo mismo para editar
      this.api.put(this.url + '/editar?id=' + this.evento.id, datosParaEnviar).subscribe({
        next: () => this.router.navigate(['/eventos']),
        error: error => console.error('Error al actualizar evento', error)
      });
    }
  }
}
