import { Component, OnInit } from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';
import {AccountService, Account} from '../../../services/classes/account-service.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NotifBarService } from 'src/app/services/utils/notif-bar.service';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/services/utils/client-service.service';

@Component({
  selector: 'front-dialog-virement',
  templateUrl: './dialog-virement.component.html',
  styleUrls: ['./dialog-virement.component.css']
})
export class DialogVirementComponent implements OnInit {

  accountvar : any;
  account : AccountService;
  public compteForm : FormGroup;
  public ribForm : FormGroup;
  public montantForm : FormGroup;
  public loading: boolean;
  AccountID : any;

  constructor(
      public dialogRef: MatDialogRef<DialogVirementComponent>,
      private accountService : AccountService,
      private http : HttpClient, 
      private notif : NotifBarService,private router : Router,
      private clientService : ClientService,) { }

  ngOnInit() {
    this.getAccountConnected();
    /**
     * Init du compteForm
     */
    this.compteForm = new FormGroup({
      compteDebit : new FormControl('', [Validators.required])
    });

    /**
     * Init du ribForm
     */
    this.ribForm = new FormGroup({
      ribReceiver : new FormControl('',[Validators.required, Validators.minLength(26), Validators.maxLength(26)])
    });

    /**
     * Init du montantForm
     */
    this.montantForm = new FormGroup({
      montantSend : new FormControl('',[Validators.required, Validators.pattern("^[0-9]+(\.[0-9]{1,2})?$")])
    })

  }

  /**
   * Afficher l'erreur lors de non sélection du compte
   */
  public compteErrorForm = (controlName : string, errorName : string)=> {
    return this.compteForm.controls[controlName].hasError(errorName);
  }

  /**
   * Afficher l'erreur lors de non saisie du rib du client
   */
  public ribErrorForm = (controlName : string, errorName : string) => {
    return this.ribForm.controls[controlName].hasError(errorName);
  }

  /**
   * Afficher l'erreur lors de non saisie du compte
   */
  public montantErrorForm = (controlName : string, errorName : string) => {
    return this.montantForm.controls[controlName].hasError(errorName);
  }

  public montantNotNumber(data){
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  getAccountConnected(){
    this.account = this.accountService.getAccount();
  }

  getRib(account : Account){
    return `${account.BankCode}-${account.BranchCode}-${account.AccountNumber}-${account.Key}`;
  }

  invokeVirement(compte,rib,montant){
    this.loading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Access-Control-Allow-Origin' : 'http://127.0.0.1:4200',
        'Vary': 'Origin'
      })
    };
    this.http.get(`http://localhost:51588/api/Clients?rib=${compte.compteDebit}`)
    .subscribe
    ( 
          data => 
          {
              this.AccountID = data;
              console.log(this.AccountID);
              let dataSend = {
                "id" : this.AccountID,
                "rib" : rib.ribReceiver,
                "montantOperation" : montant.montantSend
              }
              this.http.put(`http://localhost:51588/api/Clients/PutTransfert/
              ${this.AccountID}/${rib.ribReceiver}/${montant.montantSend}`, httpOptions)
              .subscribe
              (
                  data => 
                  {
                    console.table(this.clientService);
                    this.http.get<AccountService>
                    (`http://localhost:51588/api/Clients?firstname=${this.clientService.firstName}&lastname=${this.clientService.lastName}`)
                    .subscribe( data => {
                
                      this.accountvar = data; // Ne pas écraser la valeur du service AccountService
                
                      this.accountService.setAccount(data);
                      this.accountService.isUpdated = true;
                      console.log(this.accountService);
                    });
                    console.warn("Redirection vers la liste des comptes");
                    this.onNoClick(); //Pour fermer la fenêtre modale 
                    this.router.navigate(['/Clients/ListeComptes']);
                    this.notif.openSnackBar("Virement effectué.","Fermer");
                  },
                  err => 
                  {
                      // console.log(err)
                      this.notif.openSnackBar(err.error.Message,"Fermer");
                  }
              ),
    this.loading = false;
          }
    );
  }

}

