var gulp = require('gulp'),
    del = require('del'),
    concat = require('gulp-concat'),
    jshint = require('gulp-jshint'),
    rename = require('gulp-rename'),
    uglify = require('gulp-uglify'),
    annotate = require('gulp-ng-annotate'),
    karma = require('gulp-karma');

var karmaserver = require("karma").server;

var paths = {
    bower: "./bower_components/",
    lib: "./Content/lib/"
};

gulp.task('copy', function() {
    var bower = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
        "jquery": "jquery/dist/jquery*.{js,map}",
        "toastr": "toastr/toastr.*{js,css}",
        "angular": "angular/angular.js",
        "angularmocks": "angular-mocks/angular-mocks.js",
        "angularui": "angular-ui-bootstrap-bower/ui-bootstrap*.js",
        "smarttable": "angular-smart-table/dist/*.js",
        "moment": "moment/min/*.js*/", 
        "angularmoment": "angular-moment/angular*.js",
        "route": "angular-route/angular-route*.js"
    }

    for (var destinationDir in bower) {
        gulp.src(paths.bower + bower[destinationDir])
          .pipe(gulp.dest(paths.lib + destinationDir));
    }
});

gulp.task('appscripts', function() {
    gulp.src('./Content/app/**/*.js')
        .pipe(annotate())
        .pipe(jshint())
        .pipe(jshint.reporter('default'))
        .pipe(concat('eventplanner.js'))
        .pipe(gulp.dest('./Content/dist'))
        .pipe(rename({ suffix: '.min' }))
        .pipe(uglify())
        .pipe(gulp.dest('./Content/dist'));
});

gulp.task('watch', function () {
    gulp.watch('./Content/app/**/*.js', ['appscripts']);
});

var testfiles = [
    './Content/lib/angular/angular.js',
    './Content/lib/angularmocks/angular-mocks.js',
    './Content/lib/toastr/toastr.js',
    './Content/lib/angularui/ui-bootstrap.js',
    './Content/lib/angularui/ui-bootstrap-tpls.js',
    './Content/lib/smarttable/smart-table.js',
    './Content/lib/route/angular-route.js',
    './Content/dist/eventplanner.js',
    './Content/test/**/*.js'
];

gulp.task('test', function () {
    return gulp.src(testfiles)
        .pipe(karma({
            configFile: __dirname + '/my.conf.js',
            action: 'run'
        }));
});

gulp.task('runtests', function () {
    gulp.src(testfiles)
      .pipe(karma({
          configFile: 'my.conf.js',
          action: 'watch'
      }));
});

gulp.task('default', ['copy'], function () {
    gulp.start('appscripts');
});