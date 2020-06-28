import { autoinject } from 'aurelia-framework';
import { ArtistService } from 'service/artist-service';
import { IArtist } from 'domain/IArtist';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { UploadsService } from 'service/uploads-service';
import { IPainting } from 'domain/IPainting';



@autoinject
export class ArtistsDetails {

    private _artist?: IArtist | null;
    private _id = ""
    private _alert: IAlertData | null = null;

    constructor(private artistService: ArtistService, private uploadsService: UploadsService) {

    }

    attached() {
    }

    async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == "string") {
            await this.artistService.getArtist(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._artist = response.data!;
                        this._artist.dateOfBirthString = new Date(this._artist.dateOfBirth).toLocaleDateString();
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
            await this.getImages();
        }
    }

    async getImages() {
        if (this._artist!.paintings) {
            this._artist!.paintings!.forEach(painting => {
                this.uploadsService.getUpload(painting.imageName).then((response) => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.createImgSource(painting, response.data!)
                    } else {
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                })
            });
        }
    }

    createImgSource(painting: IPainting, file: Blob) {
        var objectURL = URL.createObjectURL(file);
        painting.imageUrl = objectURL;
    }
}
