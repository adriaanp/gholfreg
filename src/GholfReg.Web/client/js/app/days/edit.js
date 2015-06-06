import Api from 'api';
import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';

@inject(Api, Router)
export class Edit {
    constructor(api, router) {
        this.api = api;
        this.router = router;
    }

    activate(parms) {
        return this.api.getGolfDay(parms.id)
        .then(day => this.day = day)
        .catch(() => alert("Could not load golf day."));
    }

    get canSave() {
        return this.day.name && !this.api.isRequesting;
    }

    save() {
        this.api.saveGolfDay(this.day)
        .then(() => this.router.navigate('/'));
    }
}
