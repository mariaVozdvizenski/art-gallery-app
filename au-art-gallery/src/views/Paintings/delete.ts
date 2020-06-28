import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { PaintingService } from 'service/painting-service';
import { IPainting } from 'domain/IPainting';
import { IPaintingCategory } from 'domain/IPaintingCategory';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { PaintingCategoryService } from 'service/painting-category-service';
import { UploadsService } from 'service/uploads-service';

@autoinject
export class PaintingDelete {

    private _painting: IPainting | null = null;
    private _id = ""
    private _alert: IAlertData | null = null;

    constructor(private router: Router, private paintingService: PaintingService,
        private paintingCategoryService: PaintingCategoryService, private uploadsService: UploadsService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == "string") {
            this.getPainting(params.id);
        }
    }

    async onSubmit(event: Event) {

        await this.deletePaintingImage();

        await this.paintingService.deletePainting(this._painting!.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('paintings', {});
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

        event.preventDefault();
    }

    async deletePaintingImage() {

        await this.uploadsService.deleteUpload(this._painting!.imageName).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
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


    getPainting(id: string) {
        this.paintingService.getPainting(id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._painting = response.data!;
                        this._id = id;
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