import 'bootstrap';
import 'bootstrap/css/bootstrap.css!';

export class App {
    configureRouter(config, router) {
        config.title = 'Gholf Registrasies';
        config.map([
            { route : '', moduleId: './days/list-days', nav: true, title: 'Gholf Registrasies' },
            { route: '/days/add', moduleId: './days/add', nav: false }
            ]);

        this.router = router;
    }
};
