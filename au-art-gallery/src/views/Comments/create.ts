import {autoinject} from 'aurelia-framework';
import { CommentService } from 'service/comment-service';
import { ICommentCreate } from 'domain/ICommentCreate';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';


@autoinject
export class CommentCreate{

    private _commentBody = "";
    private _paintingId = "";
    private _paintings: IPainting[] | null = null;

    private _alert: IAlertData | null = null;

    constructor(private commentService: CommentService, private router: Router, private paintingService: PaintingService){
    }

    attached(){
        this.paintingService.getPaintings()
        .then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._paintings = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });

    }

    onSubmit(event: Event){
        
        this.commentService
        .createComment({
            commentBody: this._commentBody, paintingId: this._paintingId})
        .then(response => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null,
                this.router.navigateToRoute('comments', {});
            } else {

                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,

                }
                console.log(this._alert);
            }
        });

        event.preventDefault();
    }
}