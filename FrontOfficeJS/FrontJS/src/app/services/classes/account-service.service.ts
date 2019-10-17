export interface AccountService {

   Deposit : DepositService[];
   Saving : SavingService[];
   Client : Client;
   Conseiller : Conseiller;

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
export interface Account{
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
export interface DepositService extends Account{
     AutorizedOverdraft : number;
     CreationDate : Date;
     FreeOverdraft : number;
     OverdraftChargeRate : number;
}

/*
 * interface Saving 
 */
export interface SavingService extends Account{
      InterestRate : number;
      MaximumAmount : number;
      MaximumDate : Date;
      MinimumAmount : number;
}

/**
 * Interface Client
 */
export interface Client{
      LastName : string;
      FirstName : string;
      DateOfBirth : Date;
      Street : string;
      ZipCode : string;
      City : string;
}

/**
 * Interface Conseiller
 */
export interface Conseiller{
    LastName : string;
    FirstName : string;
}