import { Component, OnInit } from '@angular/core';
import {AccountService, Account} from '../../../services/classes/account-service.service';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { DialogVirementComponent } from '../dialog-virement/dialog-virement.component';

@Component({
  selector: 'front-list-saving',
  templateUrl: './list-saving.component.html',
  styleUrls: ['./list-saving.component.css']
})
export class ListSavingComponent implements OnInit {

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

  getAccountConnected(){
    this.account = this.accountService.getAccount();
  }

  ngDoCheck(): void {
    if(this.accountService.isUpdated)
    {
      this.getAccountConnected();
      console.log("Rechargement après modifications");
      this.accountService.isUpdated = false;
    }
  }

  getRib(account : Account){
    return `${account.BankCode}-${account.BranchCode}-${account.AccountNumber}-${account.Key}`;
  }
  

}
