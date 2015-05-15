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
    }

    for (var destinationDir in bower) {
        gulp.src(paths.bower + bower[destinationDir])
          .pipe(gulp.dest(paths.lib + destinationDir));
    }
});

gulp.task('default', ['copy'], function () {
    // place code for your default task here
});