import {autoinject} from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';
import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { CommentService } from 'service/comment-service';
import { ICommentCreate } from 'domain/ICommentCreate';
import {IBasket} from 'domain/IBasket';
import { AppState } from 'state/app-state';
import { BasketItemService } from 'service/basket-item-service';
import { BasketService } from 'service/basket-service';
import { IBasketItemCreate } from 'domain/IBasketItemCreate';
import { UploadsService } from 'service/uploads-service';
import { IComment } from 'domain/IComment';


@autoinject
export class PaintingDetails {
    private _painting: IPainting | null = null;
    private _comment: ICommentCreate | null = null;
    private _id = ""
    private _success: string | null = null;
    private _alert: IAlertData | null = null;
    private _imgSource: string | null = null;
    private _quantity: string | null = null;
    private _file: Blob | null = null;
    private _editComment: boolean = false;
    private _tempCommentBody: string | null = null;

    constructor(private paintingService: PaintingService, private commentService: CommentService, private router: Router, private appState: AppState,
        private basketItemService: BasketItemService, private basketService: BasketService, private uploadsService: UploadsService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string") {
            this.paintingService.getPainting(params.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._painting = response.data!;
                        this.getPicture();
                        this.formatDate();
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

    formatDate() {
        if (this._painting!.comments) {
            this._painting!.comments.forEach(comment => {
                comment.createdAtString = new Date(comment.createdAt).toLocaleDateString()
            });
        }
    }

    editComment(comment: IComment, edit: boolean) {
        if (edit) {
            comment.edit = true
            this._tempCommentBody = comment.commentBody
        } else {
            comment.edit = false
            this._tempCommentBody = null
        }
    }

    updateComment(comment: IComment) {
        if (this._tempCommentBody != null && this._tempCommentBody.length > 0) {
            let updateComment: IComment = <IComment> {
                commentBody: this._tempCommentBody,
                id: comment!.id,
                paintingId: this._painting!.id
            }
            this.commentService.updateComment(updateComment)
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
        }
    }

    onSubmit(event: Event){

        this._comment!.paintingId = this._painting!.id

        this.commentService.createComment(this._comment!)
        .then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                this._comment = null;
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

    getPicture() {
        this.uploadsService.getUpload(this._painting?.imageName!).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._file = response.data!;
                this._alert = null;
                this.createImgSource();
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,
                }
            }
        });
    }

    createImgSource() {
        var objectURL = URL.createObjectURL(this._file);
        this._imgSource = objectURL;
    }

    addToCart(itemQuantity: string){

        let basket : IBasket
        this.basketService.getBaskets().then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                this._alert = null;
                basket = response.data![0]
                let basketItem : IBasketItemCreate = <IBasketItemCreate>{
                    basketId: basket.id,
                    paintingId: this._painting!.id,
                    quantity: Number(this._quantity)
                }
                this.basketItemService.createBasketItem(basketItem).then((response) => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('paintingDetails', {id: this._painting!.id}, {replace: true});
                        this._success = "Item(s) added to Cart!";
                    } else {
                        this._alert = {
                            message: response.errorMessage!.toString(),
                            type: AlertType.Danger,
                            dismissable: true,
        
                        }
                    }
                });
            } else {
                this._alert = {
                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                    type: AlertType.Danger,
                    dismissable: true,

                }
            }
        });
    }
}