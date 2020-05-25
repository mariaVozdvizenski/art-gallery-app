import {autoinject} from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { CommentService } from 'service/comment-service';
import { ICommentCreate } from 'domain/ICommentCreate';
import { AppState } from 'state/app-state';


@autoinject
export class PaintingDetails {
    private _painting: IPainting | null = null;
    private _comment: ICommentCreate | null = null;
    private _id = ""
    private _alert: IAlertData | null = null;
    private imageURL: string = "tangerines.jpg";

    constructor(private paintingService: PaintingService, private commentService: CommentService, private router: Router, private appState: AppState) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string") {
            this.paintingService.getPainting(params.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._painting = response.data!;
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

        console.log(event);

        this._comment!.paintingId = this._painting!.id

        this.commentService.createComment(this._comment!)
        .then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this.router.navigateToRoute('paintingDetails', {id: this._painting!.id}, {replace: true});
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