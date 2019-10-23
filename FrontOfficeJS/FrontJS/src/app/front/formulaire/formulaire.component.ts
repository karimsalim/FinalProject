import { Component, OnInit } from '@angular/core';
import { ClientService } from 'src/app/services/utils/client-service.service';
import { AccountService, Account} from 'src/app/services/classes/account-service.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { NotifBarService } from 'src/app/services/utils/notif-bar.service';

@Component({
  selector: 'app-formulaire',
  templateUrl: './formulaire.component.html',
  styleUrls: ['./formulaire.component.css']
})
export class FormulaireComponent implements OnInit{

  public ClientForm : FormGroup;

  account : AccountService;
  modifFormulaire = true;
  date : Date;
  client : ClientService;
  loading = false;

  constructor(
        private clientService : ClientService,
        private accountService : AccountService,
        private router : Router,
        private http: HttpClient,
        private notif : NotifBarService
        ) { }
  
  ngOnInit() {
    
    this.getAccountConnected();
    if(this.account){
      this.date = new Date(this.account.Client[3]);
      this.ClientForm = new FormGroup({
        LastName : new FormControl(this.account.Client[2],[Validators.required]),
        FirstName : new FormControl(this.account.Client[1],[Validators.required]),
        DateOfBirth : new FormControl(this.date,Validators.required),
        Street : new FormControl(this.account.Client[4],Validators.required),
        Zip : new FormControl(this.account.Client[5],Validators.required),
        City : new FormControl(this.account.Client[6],Validators.required)
      })
    }
    

    //Récupérer le client connecté
    this.getClientConnected();
    //Récupérer le compte du client
    // this.getAccountConnected();
  }

  getAccountConnected(){
    this.account = this.accountService.getAccount();
  }

  getClientConnected(){
    this.client = this.clientService.getClientConnected();
  }

  public hasError = (controlName : string, errorName:string) =>
  {
    return this.ClientForm.controls[controlName].hasError(errorName);
  }

  returnHome() : void{
    this.router.navigate(['Clients/ListeComptes'])
  }

  onSubmit(clientData){

    console.log(this.account);

    this.loading = true;

    let client = {
      "PersonID" : this.clientService.idClient,
      "FirstName" : clientData.FirstName,
      "LastName" : clientData.LastName,
      "DateOfBirth" : clientData.DateOfBirth,
      "Street" : clientData.Street,
      "ZipCode" : clientData.Zip,
      "City" : clientData.City
    }

    const headers = new HttpHeaders({'Content-Type': 'application/json'});

    this.http.put(`http://localhost:51588/api/Clients/PutClient/${this.client.idClient}`,
    JSON.stringify(client), {headers : headers})
     .subscribe(data => {
          this.http.get<AccountService>
          (`http://localhost:51588/api/Clients?firstname=${clientData.FirstName}&lastname=${clientData.LastName}`)
            .subscribe( data => {
            this.loading = false;

            this.accountService.setAccount(data);
            this.account.isUpdated = false;

            this.clientService
              .setClientConnected(
              this.account.Client[2], 
              this.account.Client[1], 
              parseInt(this.account.Client[0]));

            this.router.navigate(['/Clients/ListeComptes'])
            this.notif.openSnackBar("Modifications enregistrées.","Fermer");
          })},
          err => {
            console.error(err);
            this.notif.openSnackBar("Une erreur est survenue. Veuillez réessayer plus tard.","Fermer");
            this.loading = false;
          }
      )

  }

}
