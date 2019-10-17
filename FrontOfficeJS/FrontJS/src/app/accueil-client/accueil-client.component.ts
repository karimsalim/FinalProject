import { Component, OnInit } from '@angular/core';
import {ClientService} from '../services/client-service.service';
import { MenuService } from '../services/menu-service.service';

@Component({
  selector: 'app-accueil-client',
  templateUrl: './accueil-client.component.html',
  styleUrls: ['./accueil-client.component.css']
})
export class AccueilClientComponent implements OnInit {

  client : ClientService;

  constructor(private clientService : ClientService) { }

  ngOnInit() {
    this.getClientConnected();
    // this.getMenuItems();
  }

  getClientConnected(){
    this.client = this.clientService.getClientConnected();
  }

  // getMenuItems(){
  //   this.menu = this.menuItem.getMenu();
  //   console.log(this.menu);
  // }

}
