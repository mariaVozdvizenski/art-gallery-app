import { AppState } from "state/app-state";
import {autoinject} from 'aurelia-framework';

@autoinject
export class HomeIndex{

    constructor(private appState: AppState){

    }
    attached() {
        console.log(this.appState.UserIsAdmin())       
    }  
}