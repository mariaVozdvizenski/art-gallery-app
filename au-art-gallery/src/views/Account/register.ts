import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { ValidationRules, ValidationController, ControllerValidateResult } from 'aurelia-validation';
import { AppState } from 'state/app-state';
import { RouterConfiguration, Router } from 'aurelia-router';


@autoinject
export class AccountRegister {

    private _email: string = "";
    private _password: string = "";
    private _confirmPassword: string = "";
    private _errorMessage: string | null = null;


    constructor(private accountService: AccountService, private validationController: ValidationController,
        private appState: AppState, private router: Router) {

    }

    onSubmit(event: Event) {
        this.validateReg()
            .then(result => {
                if (result.valid) {
                    this.accountService.register(this._email, this._password).then(response => {
                        console.log(response);
                        if (response.statusCode == 200) {
                            this.appState.jwt = response.data!.token;
                            this.appState.userName = response.data!.userName;
                            this.appState.userRoles = response.data!.userRoles;
                            this.appState.appUserId = response.data!.appUserId;
                            this.router!.navigateToRoute('home');
                            console.log(this.appState);
                        } else {
                            this._errorMessage = response.statusCode.toString() + ' ' + response.errorMessage!
                        }
                    });

                } else {
                    console.log("Invalid")
                }
            });
    }

    validateReg(): Promise<ControllerValidateResult> {
        const registrationRules = ValidationRules

            .ensure(p => this._email).displayName('Email')
            .required()
            .email()

            .ensure(p => this._password).displayName('Password')
            .required()
            .minLength(6)
            .maxLength(100)
            .satisfies((value: string, o: unknown) => {
                if (value.match(/[A-Z]/)) {
                    return true;
                }
                return false;
            }).withMessage('The password should contain an uppercase letter!')

            .satisfies((value: string, o: unknown) => {
                if (value.match(/\s/g)) {
                    return false;
                }
                return true;
            }).withMessage('The password shouldnt contain spaces!')

            .satisfies((value: string, o: unknown) => {
                if (value.match(/\d/g)) {
                    return true;
                }
                return false;
            }).withMessage('The password should contain atleast one number!')

            .satisfies((value: string, o: unknown) => {
                if (value.match(/\W/)) {
                    return true;
                }
                return false;
            }).withMessage('The password should contain atleast one non alphanumeric character!')


            .ensure(p => this._confirmPassword).displayName('Confirm password')
            .required()
            .equals(this._password)
            .withMessage(`The password and confirmation password do not match.`)
            .on(this);

        return this.validationController.validate();
    }

}