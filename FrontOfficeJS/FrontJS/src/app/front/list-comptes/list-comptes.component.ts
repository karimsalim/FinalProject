import { Component, OnInit } from '@angular/core';
// import {AccountService, Account} from '../../services/classes/account-service.service';
import {ClientService} from '../../services/utils/client-service.service';

@Component({
  selector: 'clients-list-comptes',
  templateUrl: './list-comptes.component.html',
  styleUrls: ['./list-comptes.component.css']
})
export class ListComptesComponent implements OnInit {
  // account : AccountService;private accountService : AccountService
  client : ClientService;
  constructor(
        private clientService : ClientService,
        ) { }
        
  ngOnInit() {
    //Récupérer le client connecté
    this.getClientConnected();
    //Récupérer le compte du client
    // this.getAccountConnected();
  }


  getClientConnected(){
    this.client = this.clientService.getClientConnected();
  }

  // getAccountConnected(){
  //   this.account = this.accountService.getAccount();
  //   if(this.account){
  //     console.error(this.account);
  //   }
  //   // console.table(this.account.Deposit);
  // }

  


}
