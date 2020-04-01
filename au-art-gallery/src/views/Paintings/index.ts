
import { autoinject } from 'aurelia-framework';
import { IPainting } from 'domain/IPainting';
import { PaintingService } from 'service/painting-service';

@autoinject
export class PaintingsIndex {

    private _paintings: IPainting[] | null = null;

    constructor(private paintingService: PaintingService) {

    }

    attached() {
        this.paintingService.getPaintings()
        .then(data => this._paintings = data);
    }
}