
export class FourBall {

    constructor() {
        this.players = [];
        this.primaryContact = '';
        this.contactNumber = '';
        this.contactEmail = '';
    }

    addPlayer(player) {
        this.players.push(player);
    }

    removePlayer(player) {
        var index = this.players.indexOf(player);
        this.players.splice(index, 1);
    }
};

export class Player {
    constructor () {
        this.name = '';
        this.handicap = 18;
    }
};
