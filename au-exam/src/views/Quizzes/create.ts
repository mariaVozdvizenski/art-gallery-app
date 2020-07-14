import { IQuizCreate } from "domain/IQuizCreate";
import { QuizService } from "service/quiz-service";
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { QuizTypeService } from "service/quiz-type-service";
import { IQuizType } from "domain/IQuizType";
import { IListQuestion } from "domain/IListQuestion";
import { IListAnswer } from "domain/IListAnswer";
import { QuestionService } from "service/question-service";
import { IQuestionCreate } from "domain/IQuestionCreate";
import { IAnswerCreate } from "domain/IAnswerCreate";
import { AnswerService } from "service/answer-service";


@autoinject
export class QuizCreate {

    private _quiz: IQuizCreate | null = null;
    private _artistId = null;
    private _alert: IAlertData | null = null;
    private _quizTypes: IQuizType[] = null;
    private _quizType: IQuizType | null = null;
    private _quizTitle: string | null = null;
    private _questionString: string | null = null;
    private _question: IListQuestion | null = null;
    private _createdQuestions: IListQuestion[] = [];
    private _answerString: string | null = null;
    private _answer: IListAnswer | null = null;
    private _atleastOneQuestion: boolean = false;
    private _atleastTwoAnswersPerQuestion: boolean = false;

    constructor(private quizService: QuizService, private router: Router, private quizTypeService: QuizTypeService,
        private questionService: QuestionService, private answerService: AnswerService) {

    }

    attached() {
        this.quizTypeService.getQuizTypes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._quizTypes = response.data!;
                    console.log(this._quizTypes);
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

    addQuestionToList() {
        this._question = <IListQuestion>{
            content: this._questionString,
            answers: []
        }
        console.log(this._questionString);
        console.log(this._question);
        this._createdQuestions.push(this._question);
        this._questionString = null;
        console.log(this._createdQuestions);
    }

    addAnswerToQuestion(index: number, content: string) {
        console.log(content);

        this._answer = <IListAnswer>{
            content: content,
            correct: false
        }

        console.log(this._answer);
        this._createdQuestions[index].answers.push(this._answer);
        console.log(this._createdQuestions);
        this.checkForTwoAnswersPerQuesions();
    }

    deleteQuestionFromList(index: number) {
        if (this._createdQuestions) {
            this._createdQuestions.splice(index, 1);
        }
    }

    deleteAnswerFromQuestion(question: IListQuestion, index: number) {
        console.log(question, index);
        question.answers.splice(index, 1);
        this.checkForTwoAnswersPerQuesions();
    }

    makeOtherAnswersFalse(index: number, question: IListQuestion) {
        var arrayLength = question.answers.length;
        for (var i = 0; i < arrayLength; i++) {
            if (i != index) {
                question.answers[i].correct = false;
            }
        }
        return true;
    }

    onSubmit() {

        var quiz: IQuizCreate = <IQuizCreate>{
            title: this._quizTitle,
            quizTypeId: this._quizType!.id
        }

        this.quizService.createQuiz(quiz).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                console.log(response.data);
                this._createdQuestions.forEach(question => {
                     this.createQuestion(question, response.data);
                });
                this.router.navigateToRoute('quizzes');
            }
        });
    }

    checkForTwoAnswersPerQuesions() {
        this._createdQuestions.forEach(question => {
            if (question.answers){
                for (var i = 0; i < question.answers.length; i++) {
                    if (question.answers.length < 2) {
                        this._atleastTwoAnswersPerQuestion = false;
                        break;
                    } else {
                        this._atleastTwoAnswersPerQuestion = true;
                    }
                }
            } else {
                this._atleastTwoAnswersPerQuestion = false;
            }
        });
    }

    checkForAtleastOneQuestion() {
        if (this._createdQuestions && this._createdQuestions.length > 0){
             this._atleastOneQuestion = true;
        } else {
            this._atleastOneQuestion = false;
        } 
    }

    async createQuestion(question: IListQuestion, quizId: string){

        var questionCreate: IQuestionCreate = <IQuestionCreate>{
            quizId: quizId,
            content: question.content
        }

        await this.questionService.createQuestion(questionCreate).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                console.log(response.data);
                this.createAnswer(response.data, question);
            }
        });
    }

    async createAnswer(questionId: string, question: IListQuestion){
        question.answers.forEach(answer => {
            var answerCreate: IAnswerCreate = <IAnswerCreate>{
                content: answer.content,
                questionId: questionId,
                correct: answer.correct
            }
            this.answerService.createAnswer(answerCreate).then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log(response.data);
                }
            });
        });
    }
}