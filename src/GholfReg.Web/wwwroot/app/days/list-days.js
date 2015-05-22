import {inject} from 'aurelia-framework';
import Api from 'api';

@inject(Api)
export class ListDays {
    constructor(api) {
        this.api = api;
    }

    activate() {
        return this.api.getGolfDays()
        .then(days => {
            this.days = days;
        });
    }

};
