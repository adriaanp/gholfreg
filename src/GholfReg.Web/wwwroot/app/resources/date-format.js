import moment from 'moment';

export class DateFormatValueConverter {
    toView(value, format) {
        format = format || 'D/M/YYYY';
        return moment(value).format(format);
    }
};