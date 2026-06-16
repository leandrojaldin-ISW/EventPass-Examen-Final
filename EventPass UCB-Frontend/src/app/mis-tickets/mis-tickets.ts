import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiClient } from '../core/http/api-client';

@Component({
  selector: 'app-mis-tickets',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mis-tickets.html',
  styleUrl: './mis-tickets.css'
})
export class MisTickets implements OnInit {
  // Usamos un array genérico para atrapar los datos rápido
  tickets: any[] = [];

  private api = inject(ApiClient);

  ngOnInit() {
    this.cargarTickets();
  }

  cargarTickets() {
    // 1. PRUEBA DE FUEGO: Forzamos el ID a 1 (Javi) temporalmente
    const usuarioId = 1;
    console.log("Buscando tickets para el Usuario ID:", usuarioId);

    this.api.get<any[]>(`http://localhost:5056/api/Inscripciones/usuario/${usuarioId}`)
      .subscribe({
        next: (data) => {
          // 2. Imprimimos exactamente lo que C# nos está enviando
          console.log("Datos crudos del backend:", data);
          this.tickets = data;
        },
        error: (err) => console.error('Error al cargar mis tickets', err)
      });
  }
}
