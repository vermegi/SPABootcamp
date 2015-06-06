var gulp = require('gulp'),
    del = require('del'),
    concat = require('gulp-concat'),
    jshint = require('gulp-jshint'),
    rename = require('gulp-rename'),
    uglify = require('gulp-uglify'),
    annotate = require('gulp-ng-annotate');

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

gulp.task('default', ['copy'], function () {
    gulp.start('appscripts');
});