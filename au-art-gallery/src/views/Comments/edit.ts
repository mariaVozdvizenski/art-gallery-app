import {autoinject} from 'aurelia-framework';
import { CommentService } from 'service/comment-service';
import { IComment } from 'domain/IComment';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { ICommentEdit } from 'domain/ICommentEdit';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class CommentEdit{
    private _comment: IComment | null = null;
    private _id = "";
    private _alert: IAlertData | null = null;


    constructor(private commentService: CommentService, private router: Router) {

    }

    attached() {  
    }
    
    activate (params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.commentService.getComment(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._comment = response.data!;
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

    onSubmit(event: Event){
        console.log(this._id);

        let comment: IComment = <IComment> {
            commentBody: this._comment!.commentBody,
            id: this._comment!.id,
            createdAt: this._comment!.createdAt,
            createdBy: this._comment!.createdBy,
            paintingId: this._comment!.paintingId
        }

        this.commentService.updateComment(comment)
        .then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.router.navigateToRoute('paintingDetails', {id: this._comment!.paintingId});
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,

                }
            }
        });

        event.preventDefault();
    }

}