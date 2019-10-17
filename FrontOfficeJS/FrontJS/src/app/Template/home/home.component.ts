import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import {MenuService} from '../../services/menu-service.service';
import {ClientService} from '../../services/client-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  menuItems : MenuService;
  clientConnected : ClientService;

  constructor(private menu : MenuService, private client : ClientService) { }

  @Output() public sidenavToggle = new EventEmitter();

  ngOnInit() {
    this.getMenuItems();
    this.getClientConnected();
  }

  getMenuItems(){
    this.menuItems = this.menu.getMenu();
  }

  getClientConnected(){
    this.clientConnected = this.client.getClientConnected();
  }

  public onToggleSidenav = () => {
    this.sidenavToggle.emit();
  }
  

}
