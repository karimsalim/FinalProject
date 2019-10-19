import { Component, OnInit } from '@angular/core';
import {AccountService, Account} from '../../../services/classes/account-service.service';

@Component({
  selector: 'front-list-deposit',
  templateUrl: './list-deposit.component.html',
  styleUrls: ['./list-deposit.component.css']
})
export class ListDepositComponent implements OnInit {

  account : AccountService;

  constructor(private accountService : AccountService) { }

  ngOnInit() {
    this.getAccountConnected();
  }

  getAccountConnected(){
    this.account = this.accountService.getAccount();
  }

  getRib(account : Account){
    return `${account.BankCode}-${account.BranchCode}-${account.AccountNumber}-${account.Key}`;
  }
}
