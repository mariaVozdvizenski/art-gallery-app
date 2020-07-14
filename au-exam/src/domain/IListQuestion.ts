import {IListAnswer} from 'domain/IListAnswer';

export interface IListQuestion {
    content: string;
    answers?: IListAnswer[];
}