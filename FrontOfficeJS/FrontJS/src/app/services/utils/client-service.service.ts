import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
      public lastName : string;
      public firstName : string;
      public idClient : number;
      public isConnected : boolean;

      /*Enregistrer le client connecté */
      setClientConnected(nom:string, prenom:string, idclient : number)
      {
        this.lastName = nom;
        this.firstName = prenom;
        this.idClient = idclient;
        this.isConnected = true;
      }

      /*Récupérer le nom et prénom du client*/
      getClientNom(){
        return this.lastName + " " + this.firstName;
      }

      getClientConnected()
      {
        return this;
      }

      /*Réinitialiser un client */
      initClient(){
        this.lastName = "";
        this.firstName = "";
        this.isConnected = false;
        this.idClient = null;
        return this;
      }

      constructor() { }
}
