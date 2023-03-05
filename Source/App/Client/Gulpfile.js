var old = "1.0.0";
var current = "1.0.0";

var oldVersionNo = "v" + old;
var newVersionNo = "v" + current;

var footerOldVersion = "Version: " + old;
var footerNewVersion = "Version: " + current;

var localServerBaseUrl = "http://localhost:62262/";
var productionServerBaseUrl = "https://api.dental.com/";

var templateUrlDevelopmentDirectory = "app/views/";
var templateUrlProductionDirectory = "dist/" + newVersionNo + "/views/";


var gulp = require("gulp"),

    changed = require("gulp-changed"),
    imagemin = require("gulp-imagemin"),
    notify = require("gulp-notify"),

    minifyHTML = require("gulp-minify-html"),

    stripDebug = require("gulp-strip-debug"),
    jshint = require("gulp-jshint"),
    plumber = require("gulp-plumber"),
    concat = require("gulp-concat"),
    uglify = require("gulp-uglify"),

    autoprefixer = require("gulp-autoprefixer"),
    minifyCSS = require("gulp-minify-css"),

    livereload = require("gulp-livereload"),
    del = require("del"),

    inject = require("gulp-inject"),
    debug = require('gulp-debug'),

    replace = require("gulp-replace"),
    gutil = require('gulp-util'),
    pump = require('pump');



// minify new images
gulp.task("images", function (done) {
    var imgSrc = "./app/images/**/*",
        imgDst = "./dist/" + newVersionNo + "/images";

    gulp.src(imgSrc)
        .pipe(changed(imgDst))
        .pipe(debug())
        .pipe(imagemin())
        .pipe(gulp.dest(imgDst))
        //.pipe(notify({ message: 'images task complete' }))
        ;

    done();
});

// minify new or changed HTML pages
gulp.task("htmls", function (done) {
    var htmlSrc = ["./app/views/**/*.html"],
        htmlDst = "./dist/" + newVersionNo + "/views/";

    gulp.src(htmlSrc)
        .pipe(changed(htmlDst))
        //.pipe(minifyHTML())
        .pipe(debug())
        .pipe(replace("app/views/", "dist/" + newVersionNo + "/views/"))
        .pipe(gulp.dest(htmlDst))
        //.pipe(notify({ message: 'htmls task complete' }))
        ;

    gulp.src(["./index.html"])
        .pipe(replace(oldVersionNo, newVersionNo))
        .pipe(gulp.dest("./"));

    done();
});



// JS concat & uglify 
gulp.task("scripts", function (done) {

    gulp.src([
        "./app/scripts/app.config.js",
        "./app/scripts/**/*.config.js",

        "./app/scripts/app.directive.js",
        "./app/scripts/**/*.directive.js",

        "./app/scripts/app.service.js",
        "./app/scripts/**/*.service.js",

        "./app/scripts/app.controller.js",
        "./app/scripts/**/*.controller.js"
    ])
        .pipe(jshint())
        .pipe(jshint.reporter("default"))
        .pipe(plumber())
        .pipe(debug())
        .pipe(concat("app-scripts.min.js"))
        .pipe(replace("./assets/layouts/layout2/img", "./dist/theme/img"))
        .pipe(replace("./assets/", "./dist/assets/"))
        .pipe(replace(templateUrlDevelopmentDirectory, templateUrlProductionDirectory))
        .pipe(replace(localServerBaseUrl, productionServerBaseUrl))
        .pipe(debug())
        .pipe(uglify())
        .pipe(uglify().on('error',
            function (err) {
                gutil.log(gutil.colors.red('[Error]'), err.toString());
                console.log(err);
            }))
        .on('error', function (err) { gutil.log(gutil.colors.red('[Error]'), err.toString()); })
        .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"))
        .pipe(notify({ message: 'config scripts task complete' }));



    gulp.src(["./app/scripts/app.controller.js"])
        .pipe(replace(footerOldVersion, footerNewVersion))
        .pipe(gulp.dest("./app/scripts/"));

    done();
});


// CSS concat, auto-prefix and minify
gulp.task("styles", function (done) {
    gulp.src(["./app/styles/**/*.css"])
        .pipe(concat("style.min.css"))
        .pipe(autoprefixer("last 2 versions"))
        .pipe(debug())
        .pipe(minifyCSS())
        .pipe(gulp.dest("./dist/" + newVersionNo + "/styles/"))
        //.pipe(notify({ message: 'styles task complete' }))
        ;

    done();
});


//replace tasks
gulp.task("replace-templateurl", function (done) {
    gulp.src(["./dist/" + newVersionNo + "/scripts/**/*.min.js"])
        .pipe(replace(templateUrlDevelopmentDirectory, templateUrlProductionDirectory))
        .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"));

    done();
});

gulp.task("replace-serverurl", function (done) {
    gulp.src(["./dist/" + newVersionNo + "/scripts/**/*.min.js"])
        .pipe(replace(localServerBaseUrl, productionServerBaseUrl))
        .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"));

    done();
});

gulp.task("replace-index", function (done) {
    gulp.src(["./index.html"])
        .pipe(replace(oldVersionNo, newVersionNo))
        .pipe(gulp.dest("./"));

    //gulp.src(["./index.html"])
    //  .pipe(replace(indexTemplateUrlOldDirectory, indexTemplateUrlNewDirectory))
    //  .pipe(gulp.dest("./"));

    done();
});

gulp.task("replace-footer-version", function (done) {
    gulp.src(["./dist/" + newVersionNo + "/scripts/**/*.min.js"])
        .pipe(replace(footerOldVersion, footerNewVersion))
        .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"));

    gulp.src(["./app/scripts/app.controller.js"])
        .pipe(replace(footerOldVersion, footerNewVersion))
        .pipe(gulp.dest("./app/scripts/"));

    done();
});


// Clean
gulp.task("clean", function (cb) {
    del(["dist", ".temp"], cb, { dryRun: true });
    console.log("clean task finished");
    return new Promise(function (resolve, reject) {
        console.log("HTTP Server Started");
        resolve();
    });
});


// Watch
gulp.task("watch", function (done) {

    // Watch .css files
    gulp.watch("./app/styles/**/*.css", ["styles"]);

    // Watch .js files
    gulp.watch("./app/scripts/**/*.config.js", ["scripts"]);
    gulp.watch("./app/scripts/**/*.directive.js", ["scripts"]);
    gulp.watch("./app/scripts/**/*.service.js", ["scripts"]);
    gulp.watch("./app/scripts/**/*.controller.js", ["scripts"]);

    // Watch .html files
    gulp.watch("./app/**/*.html", ["htmls"]);

    // Watch images files
    gulp.watch("./app/images/**/*", ["images"]);

    // Create LiveReload server
    livereload.listen();

    // Watch any files in dist/, reload on change
    gulp.watch(["./dist/**"]).on("change", livereload.changed);

    done();
});


gulp.task("command", function () {
    console.log("clean");
    console.log("default");
    console.log("urls");
    console.log("baseurl");
    console.log("index");
    console.log("footer");
});



// copy dist vendors start

gulp.task("dist-vendor", function (done) {
    gulp.src(getVendorCssSources())
        .pipe(concat("vendor-style.min.css"))
        .pipe(autoprefixer("last 2 versions"))
        .pipe(debug())
        .pipe(minifyCSS())
        .pipe(gulp.dest("./dist/vendors/css/"))
        .pipe(notify({ message: 'vendor style task complete' }))
        ;

    gulp.src(getVendorJsSources())
        //.pipe(changed(htmlDst))
        //.pipe(minifyHTML())
        .pipe(gulp.dest("./dist/vendors/js/"))
        .pipe(jshint())
        .pipe(jshint.reporter("default"))
        .pipe(plumber())
        .pipe(debug())
        .pipe(concat("vendor-scripts.min.js"))
        .pipe(debug())
        //.pipe(uglify().on('error',
        //    function (err) {
        //        gutil.log(gutil.colors.red('[Error]'), err.toString());
        //        console.log(err);
        //    }))
        .pipe(gulp.dest("./dist/vendors/js/"))
        .pipe(notify({ message: 'vendor scripts task complete' }))
        ;

    done();
});



gulp.task("dist-theme", function (done) {

    gulp.src(getThemeCssSources())
        //.pipe(changed(htmlDst))
        //.pipe(minifyHTML())
        .pipe(concat("theme-style.min.css"))
        .pipe(autoprefixer("last 2 versions"))
        .pipe(debug())
        .pipe(minifyCSS())
        .pipe(gulp.dest("./dist/theme/css/"))
        .pipe(notify({ message: 'theme style task complete' }))
        ;

    gulp.src(getThemeJsSources())
        //.pipe(changed(htmlDst))
        //.pipe(minifyHTML())
        .pipe(gulp.dest("./dist/theme/js/"))
        .pipe(jshint())
        .pipe(jshint.reporter("default"))
        .pipe(plumber())
        .pipe(debug())
        .pipe(concat("theme-scripts.min.js"))
        .pipe(debug())
        //.pipe(uglify().on('error',
        //    function (err) {
        //        gutil.log(gutil.colors.red('[Error]'), err.toString());
        //        console.log(err);
        //    }))
        .pipe(gulp.dest("./dist/theme/js/"))
        .pipe(notify({ message: 'theme scripts task complete' }))
        ;



    done();
});


var getVendorCssSources = function () {
    return [        
        "./Content/ng-grid.min.css",
        "./Content/angular-busy.css",       
    ];
};



var getVendorJsSources = function () {
    return [
        "./Scripts/angular/angular.min.js",
        "./Scripts/angular-ui/angular-ui-router.min.js",
        "./Scripts/angular-ui/ui-bootstrap.min.js",
        "./Scripts/angular-ui/ui-bootstrap-tpls.min.js",
        "./Scripts/ng-grid.min.js",
        "./Scripts/angular-busy.min.js",
        "./Scripts/checklist-model.js",
    ];
};


var getThemeCssSources = function () {
    return [
        "./Content/bootstrap/bootstrap.min.css",       
    ];
};

var getThemeJsSources = function () {
    return [
        "./Scripts/jquery-2.2.3.min.js",
        "./Scripts/bootstrap.min.js",  
    ];
};


// copy dist vendors end



//gulp.task('build', function (cb) {
//    pump([
//        gulp.src('app/**/*.js'),
//        uglify(),
//        gulp.dest('./dist/')
//    ], cb);
//});


gulp.task("prod",
    gulp.series(gulp.parallel("scripts", "styles", "images", "htmls", "dist-vendor", "dist-theme")));


gulp.task("dist",
    gulp.series(gulp.parallel("scripts", "styles", "images", "htmls", "dist-vendor", "dist-theme")));


gulp.task("build",
    gulp.series(gulp.parallel("scripts", "styles", "images", "htmls", "dist-vendor", "dist-theme")));




//=======gulp 3 setting start=========
// Default task
//gulp.task("default",
//    [
//        "scripts", "styles", "images", "htmls", "dist-vendor", "dist-theme"
//    ]);

//gulp.task("urls", ["replace-templateurl"]);
//gulp.task("baseurl", ["replace-serverurl"]);
//gulp.task("index", ["replace-index"]);
//gulp.task("footer", ["replace-footer-version"]);
//=======gulp 3 settings end==========



/*
 *
 npm install --save gulp gulp-plumber gulp-changed gulp-minify-html gulp-autoprefixer gulp-minify-css gulp-uglify gulp-imagemin gulp-rename gulp-concat gulp-strip-debug gulp-notify gulp-livereload del gulp-inject gulp-jshint@2.x gulp-replace
 *
 */

/*
 *
 npm uninstall gulp gulp-plumber gulp-changed gulp-minify-html gulp-autoprefixer gulp-minify-css gulp-uglify gulp-imagemin gulp-rename gulp-concat gulp-strip-debug gulp-notify gulp-livereload del gulp-inject gulp-jshint@2.x gulp-replace
 *
 */
