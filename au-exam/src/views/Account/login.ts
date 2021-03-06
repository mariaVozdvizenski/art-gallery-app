import {autoinject} from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import {RouterConfiguration, Router} from 'aurelia-router';


@autoinject
export class AccountLogin{

    private _email: string = "";
    private _password: string = "";
    private _errorMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    onSubmit(event: Event) {
        event.preventDefault();

        this.accountService.login(this._email, this._password).then(
            response => {
                if (response.statusCode == 200){
                    this.appState.jwt = response.data!.token;
                    this.appState.userName = response.data!.userName;
                    this.appState.userRoles = response.data!.userRoles;
                    this.appState.appUserId = response.data!.appUserId;
                    this.router!.navigateToRoute('quizzes');
                } else {
                    this._errorMessage = response.statusCode.toString() + ' ' + response.errorMessage!
                    if (response.statusCode == 404 || response.statusCode == 400){
                        this._errorMessage = "Incorrect email or password."
                    }
                }
            }
        )
    }
}