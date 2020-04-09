import { autoinject } from 'aurelia-framework';
import { CommentService } from 'service/comment-service';
import { IComment } from 'domain/IComment';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class CommentsIndex {
    private _comments: IComment[] = []

    private _alert: IAlertData | null = null;


    constructor(private commentService: CommentService) {

    }

    attached() {
        this.commentService.getComments().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._comments = response.data!;
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