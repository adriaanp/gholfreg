var gulp = require('gulp');
var bower = require('gulp-bower');
var del = require('del');

var project = require('./project.json');
var lib = project.webroot + '/lib';

gulp.task('clean', function(done) {
    del(lib, done);
});

gulp.task('bower:install', ['clean'], function () {
    return bower({
        directory: lib
    });
});

gulp.task('default', ['bower:install'], function() {
    return;
});

// for future use
gulp.task('wiredep', function() {
    var wiredep = require('wiredep').stream;
    var options = {
        directory: lib,
        cwd: './',
        client: 'Views/Shared/_Layout.cshtml'
    };

    return gulp
        .src(options.client)
        .pipe(wiredep(options))
        .pipe(gulp.dest('Views/Shared'));
});