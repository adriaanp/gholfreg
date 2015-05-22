import {HttpClient} from 'aurelia-http-client';
import {inject} from 'aurelia-framework';

@inject(HttpClient)
export default class Api {
    constructor(http) {
        this.http = http;
    }

    getGolfDays() {
        return this.http.get('/api/day')
        .then(response => response.content);
    }

    createGolfDay(golfDay) {
        //return this.http.post('/api/day', golfDay);
        return this.http.createRequest('/api/day')
            .withHeader('Content-Type', 'application/json')
            .asPost()
            .withContent(golfDay)
            .send();
    }
}
