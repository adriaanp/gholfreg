import 'bootstrap';
import 'bootstrap/css/bootstrap.css!';

export class App {
    configureRouter(config, router) {
        config.title = 'Gholf Registrasies';
        config.map([
            { route : '', moduleId: './days/list-days', nav: true, title: 'Gholf Registrasies' },
            { route: '/days/add', moduleId: './days/add', nav: false, title: 'Add Golf day' },
            { route: '/days/edit/:id', moduleId: './days/edit', nav: false, title: 'Edit golf day'},
            { route: '/players/:id', moduleId: './players/list', nav: false, title: 'Golf day players'},
            { route: '/players/add/:id', moduleId: './players/add', nav: false, title: 'Edit fourball'}
            ]);

        this.router = router;
    }
};
