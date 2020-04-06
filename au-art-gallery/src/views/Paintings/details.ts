import {autoinject} from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';
import {RouteConfig, NavigationInstruction} from 'aurelia-router';


@autoinject
export class PaintingDetails {
    private _painting: IPainting | null = null;
    private _id = ""

    constructor(private paintingService: PaintingService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string") {
            this.paintingService.getPainting(params.id)
            .then(data => this._painting = data)
            this._id = params.id;
            console.log(this._id);
        }
    }
}