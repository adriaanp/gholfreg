import Api from 'api';
import {inject} from 'aurelia-framework';

@inject(Api)
export class List {
    constructor(api) {
        this.api = api;
    }

    activate(parms) {
        return this.api.getGolfDay(parms.id)
        .then(day => this.day = day)
        .then(() => this.api.getFourballs(parms.id))
        .then(fourballs => this.fourballs = fourballs)
        .catch(() => alert('could not load fourballs'));
    }
}
