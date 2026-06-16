import { Routes } from '@angular/router';
import { EventoLista } from './eventos/evento-lista';
import { EventoEdicion } from './eventos/evento-edicion';
// COMENTARIOS
import { ComentarioLista} from './comentarios/comentario-lista';
// IMPORTAMOS EL LOGIN
import { Login } from './login/login';
import { Catalogo } from './catalogo/catalogo';
import { Inscripcion } from './inscripcion/inscripcion';
import { MisTickets } from './mis-tickets/mis-tickets';

export const routes: Routes = [
  // 1. Ahora la ruta vacía redirige primero al Login
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  // 2. Definimos el componente Login en el mapa
  { path: 'login', component: Login },

  { path: 'catalogo', component: Catalogo },
  // 3. El resto de tu plataforma (Solo se llega aquí si el login es exitoso)
  { path: 'eventos', component: EventoLista },
  // ¡Esta es la ruta que hace que tu botón funcione!
  { path: 'eventos/editar/:id', component: EventoEdicion },
  //Esta es la nueva ruta en el mapa
  { path: 'comentarios', component: ComentarioLista },

  { path: 'catalogo', component: Catalogo },

  //ruta nueva para la inscripcion
  { path: 'inscripcion/:id', component: Inscripcion },
  { path: 'mis-tickets', component: MisTickets },
];
