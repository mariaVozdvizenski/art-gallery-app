
import { autoinject } from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { AppState } from 'state/app-state';

@autoinject
export class PaintingsIndex {

    private _paintings: IPainting[] | null = null;
    private _alert: IAlertData | null = null;
    private imageURL: string = "tangerines.jpg";

    constructor(private paintingService: PaintingService, private appState: AppState) {

    }

    attached() {
        this.paintingService.getPaintings().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._paintings = response.data!;
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