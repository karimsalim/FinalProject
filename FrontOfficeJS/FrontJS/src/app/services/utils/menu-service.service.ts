import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  public itemMenu = [
    {
      "link" : "Clients/ListeComptes",
      "display" : "Liste des comptes",
      "icon" : "list"
    },
    {
      "link" : "Clients/DonneesClients",
      "display" : "Informations personnelles",
      "icon" : "account_circle"    
    }];
    // ,"Informations personnelles","Créer un virement"];
  public subItemMenu = ["Liste des comptes courants","Liste des comptes épargne"];
  constructor() {}

  getMenu(){
    return this;
  }
}
