import {inject} from 'aurelia-framework';
import Api from 'api';
import {FourBall, Player} from './fourball';
import {Router} from 'aurelia-router';

@inject(Api, Router)
export class AddFourball {
    constructor (api, router) {
        this.api = api;
        this.router = router;
        this.fourball = new FourBall();
        this.currentPlayer = new Player();
    }

    activate (parms) {
        this.dayId = parms.id;
        return this.api.getGolfDay(this.dayId)
        .then(day => this.day = day)
        .catch(() => alert("could not load golf day and or fourballs"));
    }

    get hasError () { return this._error; }

    get canSave() {
        return this.fourball.primaryContact.length > 0 && !this.api.isRequesting;
    }

    get canAddPlayer() {
        return this.fourball.players.length < 4;
    }

    addPlayer() {
        this.fourball.addPlayer(this.currentPlayer);
        this.currentPlayer = new Player();
    }

    removePlayer(player) {
        this.fourball.removePlayer(player);
    }

    save () {
        this._error = false;
        if (this.fourball.primaryContact.length == 0) {
            this._error = true;
            return;
        }

        this.api.addFourball(this.dayId, this.fourball)
        .then(() => this.router.navigate(`/players/${this.dayId}`))
        .catch(() => alert("could not save fourball"));
    }
}
