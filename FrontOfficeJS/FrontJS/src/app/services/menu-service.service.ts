import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  public itemMenu = ["Liste des comptes","Informations personnelles","Créer un virement"];
  public subItemMenu = ["Liste des comptes courants","Liste des comptes épargne"];
  constructor() {}

  getMenu(){
    return this;
  }
}
