import { Component, OnInit } from '@angular/core';
import { ClientService } from 'src/app/services/utils/client-service.service';
import { AccountService} from 'src/app/services/classes/account-service.service';
import { FormGroupDirective, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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
        private http: HttpClient
        ) { }
  
  ngOnInit() {
    
    this.getAccountConnected();
    if(this.account){
      this.date = new Date(this.account.Client[3]);
      this.ClientForm = new FormGroup({
        LastName : new FormControl(this.account.Client[1],[Validators.required]),
        FirstName : new FormControl(this.account.Client[2],[Validators.required]),
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
    this.loading = true;
    let client = {
      "LastName" : clientData.LastName,
      "FirstName" : clientData.FirstName,
      "DateOfBirth" : clientData.DateOfBirth,
      "Street" : clientData.Street,
      "Zip" : clientData.Zip,
      "City" : clientData.City
    }
    console.warn(client);
    console.log(JSON.stringify(client));
    let httpconfig = { 
      header: new HttpHeaders({
      'Content-Type' : 'application/json'
    })}
    this.http.put(`http://127.0.0.1:51588/Clients/PutClient/${this.client.idClient}`,
        JSON.stringify(client)).subscribe(data => {
            console.log(data);
            this.loading = false;
          },
          err => {
            console.error(err);
            this.loading = false;
          }
          )

  }

}
