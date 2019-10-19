import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class AccountService {

   public Deposit : DepositService[];
   public Saving : SavingService[];
   public Client : string[];
   public Conseiller : Conseiller;

   public data: any;
   
   public getAccount(){
        return this.data;
   }

   public setAccount(value){
     this.data = value;
   }

   public getRib(account : Account){
     return `${account.BankCode}-${account.BranchCode}-${account.AccountNumber}-${account.Key}`;
   }
   
}

/*************************************************************/
/*************************************************************/
/*************************************************************/
/*
 * interfaces utilitaires pour l'API
 */

/*
 * interface account 
 */
export class Account{
     AccountId : number;
     AccountNumber : string;
     Balance : number;
     BankCode : string;
     BIC : string;
     BranchCode : string;
     IBAN : string;
     Key : string;
}

/*
  * interface Deposit 
  */
export class DepositService extends Account{
     AutorizedOverdraft : number;
     CreationDate : Date;
     FreeOverdraft : number;
     OverdraftChargeRate : number;
}

/*
 * interface Saving 
 */
export class SavingService extends Account{
      InterestRate : number;
      MaximumAmount : number;
      MaximumDate : Date;
      MinimumAmount : number;
}

/**
 * Interface Client
 */
// export interface Client{
//       PersonID : number;
//       LastName : string;
//       FirstName : string;
//       DateOfBirth : Date;
//       Street : string;
//       ZipCode : string;
//       City : string;
// }

/**
 * Interface Conseiller
 */
export class Conseiller{
    LastName : string;
    FirstName : string;
}