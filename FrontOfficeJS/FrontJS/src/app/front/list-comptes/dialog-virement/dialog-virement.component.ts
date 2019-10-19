import { Component, OnInit } from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';
import {AccountService, Account} from '../../../services/classes/account-service.service';

@Component({
  selector: 'front-dialog-virement',
  templateUrl: './dialog-virement.component.html',
  styleUrls: ['./dialog-virement.component.css']
})
export class DialogVirementComponent implements OnInit {

  account : AccountService;

  constructor(
      public dialogRef: MatDialogRef<DialogVirementComponent>,
      private accountService : AccountService) { }

  ngOnInit() {
    this.getAccountConnected();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  getAccountConnected(){
    this.account = this.accountService.getAccount();
    console.warn(this.account);
  }

  getRib(account : Account){
    return `${account.BankCode}-${account.BranchCode}-${account.AccountNumber}-${account.Key}`;
  }

}

