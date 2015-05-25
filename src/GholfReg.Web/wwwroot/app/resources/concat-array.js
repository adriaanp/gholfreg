import _ from 'lodash';

export class ConcatArrayValueConverter {
    toView(value, member) {
        var names = _.map(value, item => item[member]);
        return names.join();
    }
};
