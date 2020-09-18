/// <binding AfterBuild='default' Clean='clean' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require("gulp");
var del = require("del");
var ts = require("gulp-typescript");
var tsProject = ts.createProject("tsconfig.json");

var paths = {
    scripts: ["wwwroot/js/src/**/*.js", "wwwroot/js/src/**/ *.ts", "wwwroot/js/src/**/*.map"],
};

gulp.task("clean", async function () {
    return del(["wwwroot/js/dist/*"]);
});

//gulp.task('default', function () {
//    return gulp.src(paths.scripts)
//        .pipe(tsProject())
//        .pipe(gulp.dest('wwwroot/js/dist'));
//});