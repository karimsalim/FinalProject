import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';

@Component({
  selector: 'app-login-client',
  templateUrl: './login-client.component.html',
  styleUrls: ['./login-client.component.css']
})
export class LoginClientComponent implements OnInit {

  public loginForm : FormGroup;

  constructor() { }

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

}
