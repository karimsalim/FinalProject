import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {ClientService} from '../services/client-service.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-login-client',
  templateUrl: './login-client.component.html',
  styleUrls: ['./login-client.component.css']
})
export class LoginClientComponent implements OnInit {

  public loginForm : FormGroup;

  constructor(private route: ActivatedRoute,
    private router: Router,private clientService : ClientService) { }

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

  public onSubmit(clientData)
  {
    this.clientService.setClientConnected(clientData.LastName, clientData.FirstName, 99);
    this.router.navigate(['/Clients']);
  }

}
