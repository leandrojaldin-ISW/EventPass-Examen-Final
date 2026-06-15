import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiClient } from '../core/http/api-client';
import { Evento } from '../eventos/eventos'; // Ajusta esta ruta si es diferente

@Component({
  selector: 'app-inscripcion',
  standalone: true,
  imports: [CommonModule, FormsModule], // Importante para usar [(ngModel)]
  templateUrl: './inscripcion.html',
  styleUrl: './inscripcion.css'
})
export class Inscripcion implements OnInit {
  // Inicializamos un evento vacío para que Angular no dé error al cargar la página
  evento: Evento = {
    id: 0, nombre: '', descripcion: '', fecha: '', ubicacion: '', tipoEvento: 'Gratuito', precioEntrada: 0
  };

  // Variables para la inscripción
  cantidad: number = 1;

  private api = inject(ApiClient);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  ngOnInit() {
    // 1. Capturamos el ID del evento desde la URL (ej. /inscripcion/20)
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      const id = Number(idParam);
      // 2. Traemos los datos de ese evento desde C#
      this.api.get<Evento>(`http://localhost:5056/api/Eventos/buscar/${id}`).subscribe({
        next: (data) => {
          this.evento = data;
        },
        error: (err) => console.error('Error al cargar el evento', err)
      });
    }
  }

  confirmarInscripcion() {
    // 1. Rescatamos el ID del usuario
    const usuarioId = Number(localStorage.getItem('usuarioId'));

    // 2. Armamos el paquete de datos exacto que espera tu clase en C#
    const datosInscripcion = {
      idUsuario: usuarioId,
      idEvento: this.evento.id,
      cantidadPersonas: this.cantidad,
      totalPagado: this.cantidad * this.evento.precioEntrada // Calculamos el total
    };

    // 3. Enviamos los datos reales al backend
    this.api.post('http://localhost:5056/api/Inscripciones/crear', datosInscripcion)
      .subscribe({
        next: () => {
          // Si el backend dice "OK", mostramos el mensaje de éxito y volvemos al catálogo
          alert('¡Inscripción registrada con éxito! Ya tienes tu lugar asegurado.');
          this.router.navigate(['/catalogo']);
        },
        error: (err) => {
          console.error('Error al inscribirse', err);
          alert('Hubo un error al procesar tu inscripción.');
        }
      });
  }

  cancelar() {
    this.router.navigate(['/catalogo']);
  }
}
