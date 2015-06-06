import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';
import Api from 'api';
import Day from './day';

@inject(Api, Router)
export class Add {
    constructor(api, router) {
        this.api = api;
        this.day = new Day();
        this.router = router;
    }

    activate() {

    }

    get hasError () { return this._error; }

    get canSave() {
        return this.day.name && !this.api.isRequesting;
    }

    save() {
        this._error = false;
        if (this.day.name.length == 0) {
            this._error = true;
            return;
        }

        this.api.createGolfDay(this.day)
        .then(() => this.router.navigate('/'));
    }
};
