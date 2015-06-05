var gulp = require('gulp'),
    del = require('del');

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

gulp.task('default', ['copy'], function () {
    // place code for your default task here
});