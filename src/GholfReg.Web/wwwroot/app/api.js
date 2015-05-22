import {HttpClient} from 'aurelia-http-client';
import {inject} from 'aurelia-framework';

@inject(HttpClient)
export default class Api {
    constructor(http) {
        this.http = http;
    }

    getGolfDays() {
        this.isRequesting = true;
        return this.http.get('/api/day')
        .then(response => {
            this.isRequesting = false;
            return response.content;
        });
    }

    getGolfDay(id) {
        this.isRequesting = true;
        return this.http.get(`/api/day/${id}`)
        .then(response => {
            this.isRequesting = false;
            return response.content;
        });
    }

    createGolfDay(golfDay) {
        this.isRequesting = true;
        return this.http.createRequest('/api/day')
            .withHeader('Content-Type', 'application/json')
            .asPost()
            .withContent(golfDay)
            .send()
            .then(response => {
                this.isRequesting = false;
                return response.content;
            });
    }

    saveGolfDay(golfDay) {
        this.isRequesting = true;
        return this.http.createRequest(`/api/day/${golfDay.id}`)
            .withHeader('Content-Type', 'application/json')
            .asPut()
            .withContent(golfDay)
            .send()
            .then(response => {
                this.isRequesting = false;
            });
    }
}
