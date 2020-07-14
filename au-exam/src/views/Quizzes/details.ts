import { IQuiz } from "domain/IQuiz";
import { IAlertData } from "types/IAlertData";
import { QuizService } from "service/quiz-service";
import {autoinject} from 'aurelia-framework';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import { IAnswer } from "domain/IAnswer";
import { IQuestion } from "domain/IQuestion";
import { QuizResultService } from "service/quiz-result-service";
import { IQuizResult } from "domain/IQuizResult";
import { IQuizResultCreate } from "domain/iQuizResultCreate";

@autoinject
export class QuizDetails {

    private _quiz: IQuiz | null = null;
    private _alert: IAlertData | null = null;
    private _chosenAnswers: IAnswer[] = [];
    private _imgUrl: string = "36601.png"
    private _imgPollUrl: string = "cio_poll.png"
    private _allQuestionsAnswered: boolean = false;

    constructor(private quizService: QuizService,  private router: Router, private appState: AppState,
         private quizResultService: QuizResultService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string") {
            this.quizService.getQuiz(params.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._quiz = response.data!;
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

    containsObject(answer: IAnswer, list: IAnswer[]): [boolean, number] {
        var i;
        for (i = 0; i < list.length; i++) {
            if (list[i] === answer) {
                return [true, i];
            }
        }
        return [false, 0];
    }

    addAnswerToQuestion(answer: IAnswer, question: IQuestion){
        question.chosenAnswer = answer;
        console.log(question.chosenAnswer);
        this.checkIfAllQuestionsAnswered();
        console.log(this._allQuestionsAnswered);
        return true;
    }

    checkIfAllQuestionsAnswered() {
        this._allQuestionsAnswered = !this._quiz.quizQuestionViews.some((question) => question.chosenAnswer == null)
    }

    calculateCorrectAnswers(): number{
        var correctAnswers = 0;

        this._quiz.quizQuestionViews.forEach(question => {
            if (question.chosenAnswer.correct) {
                correctAnswers += 1;
            }
        });

        return correctAnswers;
    }

    onSubmit() {
        var quizResult: IQuizResultCreate = <IQuizResultCreate>{
            correctAnswers: this.calculateCorrectAnswers(),
            quizId: this._quiz.id
        };

        console.log(quizResult);

        this.quizResultService.createQuizResult(quizResult).then((response) => {
           if (response.statusCode >= 200 && response.statusCode < 300) {
               if (this._quiz.quizType == 'Quiz'){
                this.router.navigateToRoute('completed', {id: response.data});
               } else {
                this.router.navigateToRoute('completedPoll', {id: response.data});
               }
           }
        });
    }
}