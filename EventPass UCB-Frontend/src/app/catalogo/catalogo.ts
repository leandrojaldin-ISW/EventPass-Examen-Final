import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiClient } from '../core/http/api-client'; // Ajusta la ruta si es necesario
import { Evento } from '../eventos/eventos'; // Ajusta la ruta a tu interface Evento
import { Router } from '@angular/router';

@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './catalogo.html',
  styleUrl: './catalogo.css'
})
export class Catalogo implements OnInit {
  eventos: Evento[] = [];

  private api = inject(ApiClient);
  private router = inject(Router);
  private url = 'http://localhost:5056/api/Eventos/lista';

  ngOnInit() {
    this.cargarCatalogo();
  }

  cargarCatalogo() {
    // Usamos el mismo endpoint que tu tabla, ¡reutilización de código al máximo!
    this.api.get<Evento[]>(this.url).subscribe({
      next: (data) => {
        this.eventos = data;
      },
      error: (err) => console.error('Error al cargar el catálogo', err)
    });
  }

  irAInscripcion(eventoId: number) {
    // Esto navega a la ruta /inscripcion/20 (o el ID que corresponda)
    this.router.navigate(['/inscripcion', eventoId]);
  }

}
