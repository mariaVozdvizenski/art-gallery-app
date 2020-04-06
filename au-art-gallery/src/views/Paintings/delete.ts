import {RouteConfig, NavigationInstruction, Router} from 'aurelia-router';
import {autoinject} from 'aurelia-framework';
import { PaintingService } from 'service/painting-service';
import { IPainting } from 'domain/IPainting';

@autoinject
export class PaintingDelete{

    private _painting: IPainting | null = null;
    private _id = ""

    constructor(private router: Router, private paintingService: PaintingService){

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof(params.id) == "string"){
            this.paintingService.getPainting(params.id).then(
                data => this._painting = data)
                this._id = params.id;
        }  
        
    }

    onSubmit(event: Event){
        console.log(event);
        this.paintingService.deletePainting(this._id)
        .then((response) => {
            console.log('redirect?', response);
            this.router.navigateToRoute('paintings');
        });
    }

}