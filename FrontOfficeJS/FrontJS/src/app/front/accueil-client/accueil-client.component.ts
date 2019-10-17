import { Component, OnInit } from '@angular/core';
import {ClientService} from '../../services/utils/client-service.service';
import { Router } from '@angular/router';
import {NotifBarService} from '../../services/utils/notif-bar.service';

@Component({
  selector: 'app-accueil-client',
  templateUrl: './accueil-client.component.html',
  styleUrls: ['./accueil-client.component.css']
})

export class AccueilClientComponent implements OnInit {

  client : ClientService;

  constructor(
    private clientService : ClientService, 
    private router : Router,
    private notif : NotifBarService) { }

  ngOnInit() {
    this.getClientConnected();
    if(!this.client.isConnected){ /*Si le client n'est pas connecté redirigé vers l'accueil */
      this.router.navigate(['/Accueil']);
    }
    else{
      this.notif.openSnackBar("Bonjour " + this.client.getClientNom(),"Fermer");
    }
  }
  /*Récupérer le service du client connecté*/
  getClientConnected(){
    this.client = this.clientService.getClientConnected();
  }

  /**Bouton Déconnexion du client**/
  public Deconnexion(){
    this.client = this.clientService.initClient();
    this.router.navigate['/Accueil'];
  }

}
