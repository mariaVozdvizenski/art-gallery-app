import { IAlertData } from "types/IAlertData";
import { QuizService } from "service/quiz-service";
import {autoinject} from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import { QuizResultService } from "service/quiz-result-service";
import { IQuizResult } from "domain/IQuizResult";

@autoinject
export class CompletedIndex{
    private _quizResult: IQuizResult | null = null;
    private _alert: IAlertData | null = null;

    constructor(private quizService: QuizService,  private router: Router, private appState: AppState,
         private quizResultService: QuizResultService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string") {
            this.quizResultService.getQuizResult(params.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._quizResult = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                }
            );
        }
    }
}