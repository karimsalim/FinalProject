import { Component, OnInit } from '@angular/core';
import {AccountService, Account} from '../../../services/classes/account-service.service';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { DialogVirementComponent } from '../dialog-virement/dialog-virement.component';

@Component({
  selector: 'front-list-deposit',
  templateUrl: './list-deposit.component.html',
  styleUrls: ['./list-deposit.component.css']
})
export class ListDepositComponent implements OnInit {

  account : AccountService;

  constructor(private accountService : AccountService, private dialog: MatDialog) { }

  ngOnInit() {
    this.getAccountConnected();
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "100%";
    dialogConfig.maxWidth="none";
    
    this.dialog.open(DialogVirementComponent, dialogConfig)}

  ngDoCheck(): void {
    if(this.accountService.isUpdated)
    {
      this.getAccountConnected();
      console.warn("Rechargement après modifications");
      // this.accountService.isUpdated = false;
    }
  }


  getAccountConnected(){
    this.account = this.accountService.getAccount();
  }

  getRib(account : Account){
    return `${account.BankCode}-${account.BranchCode}-${account.AccountNumber}-${account.Key}`;
  }

  printRib(){
    alert("En cours de développement.");
  }
}
