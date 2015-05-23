import {inject} from 'aurelia-framework';
import Api from 'api';

@inject(Api)
export class ListDays {
    constructor(api) {
        this.api = api;
    }

    activate() {
        return this.loadGolfDays();
    }

    loadGolfDays() {
        return this.api.getGolfDays()
        .then(days => {
            this.days = days;
        });
    }

    delete(id) {
        if (confirm('Sure you want to delete this golf day?'))
        {
            return this.api.deleteGolfDay(id)
            .then(this.loadGolfDays());
        }
    }

};
