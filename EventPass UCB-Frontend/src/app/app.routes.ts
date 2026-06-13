import { Routes } from '@angular/router';
import { EventoLista } from './eventos/evento-lista';
import { EventoEdicion } from './eventos/evento-edicion';
//COMENTARIOS
import { ComentarioLista} from './comentarios/comentario-lista';
export const routes: Routes = [
  { path: '', redirectTo: 'eventos', pathMatch: 'full' },
  { path: 'eventos', component: EventoLista },
  // ¡Esta es la ruta que hace que tu botón funcione!
  { path: 'eventos/editar/:id', component: EventoEdicion },
  //Esta es la nueva ruta en el mapa
  {path: 'comentarios', component: ComentarioLista}
];
