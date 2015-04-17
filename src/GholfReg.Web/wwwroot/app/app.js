import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';
import 'bootstrap';
import 'bootstrap/css/bootstrap.css!';

@inject(Router)
export class App {
    constructor(router) {
        this.router = router;
        this.router.configure(config => {

            config.title = 'Gholf Registrasies';
            config.map([
                { route : '', moduleId: './days/list-days', nav: true, title: 'Gholf Registrasies' },
                { route: '/days/add', moduleId: './days/add', nav: false }
                ]);

        });
    }
};