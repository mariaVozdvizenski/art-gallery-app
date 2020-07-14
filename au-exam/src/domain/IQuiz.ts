import { IQuestion } from "./IQuestion";

export interface IQuiz {
    id: string;
    title: string;
    quizTypeId: string;
    quizType: string;
    howManyTimesDone: number;
    completelyCorrectAnswers: number;
    quizQuestionViews: IQuestion[];
    showStatistics?: boolean;
}