import { AppState } from "state/app-state";
import { autoinject } from 'aurelia-framework';
import { UploadsService } from "service/uploads-service";
import { IAlertData } from "types/IAlertData";
import { AlertType } from "types/AlertType";
import { PaintingService } from "service/painting-service";
import { IPainting } from "domain/IPainting";

@autoinject
export class HomeIndex {

    private _paintings: IPainting[] | null = null;
    private _alert: IAlertData | null = null;

    constructor(private paintingService: PaintingService, private appState: AppState, private uploadsService: UploadsService){

    }

     attached() {
         this.paintingService.getPaintings(null, null, null).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._paintings = response.data!;
                    console.log("attached");
                    this.getImages();
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

    async getImages(){
        this._paintings!.forEach(painting => {
              this.uploadsService.getUpload(painting.imageName).then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    painting.imageUrl = URL.createObjectURL(response.data);
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        });
    }
}