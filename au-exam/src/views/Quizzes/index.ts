import { autoinject } from 'aurelia-framework';
import { IQuiz } from 'domain/IQuiz';
import { QuizService } from 'service/quiz-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';
import { IQuizType } from 'domain/IQuizType';
import { QuizTypeService } from 'service/quiz-type-service';

@autoinject
export class QuizzesIndex {

    private _quizzes: IQuiz[] | null = null;
    private _alert: IAlertData | null = null;
    private _categoryNames: string[] = [];
    private _quizTypes: IQuizType[] | null = null;

    constructor(private quizService: QuizService, private appState: AppState, private quizTypeService: QuizTypeService) {

    }

    viewStatisticsForQuiz(quiz: IQuiz) {
        quiz.showStatistics = true;
        console.log(quiz.showStatistics);
    }

    hideStatisticsForQuiz(quiz: IQuiz) {
        quiz.showStatistics = false;
    }

    filter() {
        this.attached();
    }

    async attached() {
        await this.quizService.getQuizzes(this._categoryNames).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._quizzes = response.data!;
                    this.hideStatistics();
                    console.log(this._quizzes[0].showStatistics)
                    this.getQuizTypes();
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

    getQuizTypes(){
        this.quizTypeService.getQuizTypes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._quizTypes = response.data;
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

    hideStatistics(){
        this._quizzes.forEach(quiz => {
            quiz.showStatistics = false;
        });
    }
}