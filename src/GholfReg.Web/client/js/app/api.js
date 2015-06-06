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

    deleteGolfDay(id) {
        this.isRequesting = true;
        return this.http.delete(`/api/day/${id}`)
        .then(response => {
            this.isRequesting = false;
            return response;
        });
    }

    getFourballs(id) {
        return [{
            primaryContact: 'Primary Contact',
            contactNumber : '123-3456',
            players: [
                { name: 'Player 1', handicap: 24},
                { name: 'Player 2', handicap: 14},
                { name: 'Player 3', handicap: 17},
                { name: 'Player 4', handicap: 18}
                ]
        }];
        this.isRequesting = true;
        return this.http.get(`/api/fourballs/${id}`)
        .then(response => {
            this.isRequesting = false;
            return response.content;
        });
    }

    addFourball(id, fourball) {
        this.isRequesting = true;
        return this.http.createRequest(`/api/players/${id}`)
            .withHeader('Content-Type', 'application/json')
            .asPost()
            .withContent(fourball)
            .send()
            .then(response => {
                this.isRequesting = false;
            });
    }
}
