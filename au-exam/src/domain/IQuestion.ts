import { IAnswer } from "./IAnswer";

export interface IQuestion {
    id: string;
    content: string;
    quizId: string;
    questionAnswers: IAnswer[];
    chosenAnswer: IAnswer;
}