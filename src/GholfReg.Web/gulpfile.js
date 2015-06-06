//https://github.com/luisrudge/generator-simple-aspnet/blob/master/app/templates/src/gulpfile.js
var gulp = require('gulp'),
    del = require('del'),
    sass = require('gulp-sass'),
    runSequence = require("run-sequence"),
    plumber = require("gulp-plumber"),
    rename = require("gulp-rename"),
    jshint = require("gulp-jshint"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    babel = require('gulp-babel'),
    changed = require('gulp-changed'),
    sourcemaps = require('gulp-sourcemaps'),
    browserSync = require('browser-sync'),
    exec = require('child_process'),
    jspm = require('jspm');

var project = require('./project.json');
var reload = browserSync.reload;

var opts = {
    buildFolder: './' + project.webroot + '/',
    client: {
        sass: {
            files: './client/sass/**/*.scss',
            destFilename: 'style.css'
        },
        js: {
            folder: './client/js/',
            files: './client/js/**/*.js',
            destFilename: 'app.js'
        },
        html: {
            files: './client/js/**/*.html'
        }
    },
    server: {
        cs: {
            files: '../**/*.cs'
        },
        cshtml: {
            files: '../**/*.cshtml'
        },
        dnx: {
            command: 'kestrel',
            options: {
                restore: true,
                build: false,
                run: true,
                cwd: './'
            }
        }
    },
    babel: {
        modules: 'system',
        moduleIds: false,
        comments: false,
        compact: false,
        stage:2,
        optional: [
          "es7.decorators",
          "es7.classProperties"
        ]
    }
};

var paths = {
    webroot: "./" + project.webroot + "/"
};


opts.lib = opts.buildFolder + "lib/"
opts.js = opts.buildFolder + "js/";
opts.css = opts.buildFolder + "css/";



gulp.task('clean', function(cb) {
    del([opts.lib, opts.js, opts.css], cb);
});

gulp.task('sass', function () {
    return gulp.src(opts.client.sass.files)
        .pipe(sass())
        .pipe(gulp.dest(opts.css))
        .pipe(browserSync.stream());
});

gulp.task('js', function() {
    return gulp.src(opts.client.js.files)
        .pipe(plumber())
        .pipe(changed(opts.js, {extension: '*.js'}))
        .pipe(sourcemaps.init())
        .pipe(babel(opts.babel))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(opts.js))
        .pipe(browserSync.stream());

});

gulp.task('html', function() {
    return gulp.src(opts.client.html.files)
        .pipe(changed(opts.js, {extension: '.html'}))
        .pipe(gulp.dest(opts.js))
        .pipe(browserSync.stream());
});

gulp.task('build-client', function(cb) {
    runSequence('clean', ['sass', 'js', 'html'], cb);
});

gulp.task('watch', ['build-client'], function(cb) {
    gulp.watch(opts.client.sass.files, ['sass'], browserSync.reload);
    gulp.watch(opts.client.js.files, ['js'], browserSync.reload);
    gulp.watch(opts.client.html.files, ['html'], browserSync.reload);
});

gulp.task('bs-reload', function(cb) {
    browserSync.reload();
});

gulp.task('serve', ['build-client'], function(cb) {
    //TODO: need to start dnxmon
    browserSync.init({
        proxy: 'localhost:5001'
    });

    runSequence('watch');
});

//gulp.task('dnx', dnx(opts.server.dnx.command, opts.server.dnx.options));

gulp.task('default', ['watch'], function() {
    return;
});


gulp.task("pre-publish", ['build-client'], function () {
    //TODO: minimize, uglify, jspm bundle
});

gulp.task('jspm:bundle', function (done) {

  jspm.bundle(
    [
      'app/*',
      'aurelia-bootstrapper',
      'aurelia-http-client',
      'aurelia-dependency-injection',
      'aurelia-router',
      'moment',
      'lodash',
      //'babel',
      //'npm:core-js@0.4.10',
      //these  below wont be needed in near future as Aurelia team will fix it
      'github:aurelia/history-browser@0.4.0',
      'github:aurelia/templating-router@0.12.0',
      'github:aurelia/templating-resources@0.11.1',
      'github:aurelia/templating-binding@0.11.0',
      'github:aurelia/loader-default@0.7.0'
    ].join(' + '),
    'wwwroot/build.js',
    {inject: false}
  ).then(done);
});
