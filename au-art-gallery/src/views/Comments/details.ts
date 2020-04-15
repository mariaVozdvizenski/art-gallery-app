import { autoinject } from 'aurelia-framework';
import { CommentService } from 'service/comment-service';
import { IComment } from 'domain/IComment';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';



@autoinject
export class CommentsDetails {
    private _comment: IComment | null = null;
    private _alert: IAlertData | null = null;


    constructor(private commentService: CommentService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id != null && typeof(params.id) == "string"){
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

        

}