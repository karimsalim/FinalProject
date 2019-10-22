import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AccueilClientComponent} from './front/accueil-client/accueil-client.component';
import {LoginClientComponent} from './front/login-client/login-client.component';
import {ListComptesComponent} from './front/list-comptes/list-comptes.component';
import { FormulaireComponent } from './front/formulaire/formulaire.component';


const routes: Routes = [
  {
    path:'Clients', component : AccueilClientComponent,
    children: [
      {
        path:'ListeComptes',
        component : ListComptesComponent
      },
      {
        path:'DonneesClients',
        component : FormulaireComponent
      }
    ]
  },
  { path: 'Accueil', component: LoginClientComponent},
  { path: '', redirectTo: 'Accueil', pathMatch: 'full'}
];

RouterModule.forRoot([
  
])

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
