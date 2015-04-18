import {HttpClient} from 'aurelia-http-client';
import {inject} from 'aurelia-framework';

@inject(HttpClient)
export class ListDays {
    constructor(http) {
        this.http = http;
    }

    activate() {
        return this.http.get('/api/day')
        .then(response => { 
            this.days = response.content; 
        });
    }

};