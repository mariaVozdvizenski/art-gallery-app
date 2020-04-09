import {autoinject} from 'aurelia-framework';
import { AccountService } from 'service/account-service';

@autoinject
export class AccountRegister{

    private _email: string = "";
    private _password: string = "";
    private _errorMessage: string | null = null;

    constructor(private accountService: AccountService){
        
    }

    onSubmit(event: Event){
        console.log(event)
    }

}