import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {autoinject} from 'aurelia-framework';
import { PaintingService } from 'service/painting-service';
import { IPainting } from 'domain/IPainting';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class PaintingDelete{

    private _painting: IPainting | null = null;
    private _id = ""
    private _alert: IAlertData | null = null;

    constructor(private router: Router, private paintingService: PaintingService){

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string") {
            this.paintingService.getPainting(params.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._painting = response.data!;
                        this._id = params.id;
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
        console.log(event);
        this.paintingService.deletePainting(this._painting!.id)
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

}