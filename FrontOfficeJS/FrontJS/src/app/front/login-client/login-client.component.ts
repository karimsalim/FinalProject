import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {ClientService} from '../../services/utils/client-service.service';
import { AccountService } from 'src/app/services/classes/account-service.service';
import {Router} from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NotifBarService } from 'src/app/services/utils/notif-bar.service';

@Component({
  selector: 'app-login-client',
  templateUrl: './login-client.component.html',
  styleUrls: ['./login-client.component.css']
})
export class LoginClientComponent implements OnInit {

  public loginForm : FormGroup;
  public loading: boolean;
  accountvar : any;

  constructor(
    private router: Router,
    private clientService : ClientService,
    private http : HttpClient, 
    private notif : NotifBarService,
    private account : AccountService) { }

  ngOnInit() {
    this.loginForm = new FormGroup({
        LastName : new FormControl('', [Validators.required]),
        FirstName : new FormControl('', [Validators.required])
    });
  }

  public hasError = (controlName : string, errorName:string) =>
  {
    return this.loginForm.controls[controlName].hasError(errorName);
  }

  /* Appel A L'API pour véfifier la connexion */
  public onSubmit(clientData)
  {
    this.loading = true;
    this.http.get<AccountService>
    (`http://localhost:51588/api/Clients?firstname=${clientData.FirstName}&lastname=${clientData.LastName}`)
    .subscribe( data => {

      this.accountvar = data; // Ne pas écraser la valeur du service AccountService

      this.account.setAccount(data);

      this.clientService
        .setClientConnected(
        this.accountvar.Client[1], 
        this.accountvar.Client[2], 
        parseInt(this.accountvar.Client[0]));

        this.router.navigate(['/Clients/ListeComptes']);
    },
        err => {
          this.loading = false;
          if(err.status == 404)
          {
            this.notif.openSnackBar("On ne vous reconnait pas ! Veuillez vérifier les informations saisies","Fermer");
          }
          else
          {
            this.notif.openSnackBar("Un problème est survenu. Veuillez réessayer plus tard.","Fermer");
          }
        }
      )

    
  }

}
