const publishDir = "//Vm-mvr-eau2-ap1.dev.local/sppteau/01.DEV/03 WebApplications/UI/WebHelp";

const gulp = require('gulp');

const gulpSass = require('gulp-sass');

const gulpAutoprefixer = require('gulp-autoprefixer');

const gulpCleanCSS = require('gulp-clean-css');

const gulpSourcemaps = require('gulp-sourcemaps');

const browserSync = require('browser-sync').create();

const del = require('del');

function sass() {
    return gulp.src(['./src/scss/custom_header_footer.scss', 
    './src/scss/custom_topic.scss'])
        .pipe(gulpSourcemaps.init())
        .pipe(gulpSass().on('error', gulpSass.logError))
        .pipe(gulpAutoprefixer({
            cascade: false
        }))
        //.pipe(gulpCleanCSS())
        .pipe(gulpSourcemaps.write('./'))
        .pipe(gulp.dest('./build/WebHelp/includes/css/'))
        .pipe(browserSync.stream())
}

function build() {

    del(['./build/WebHelp/includes/css/custom_header_footer.css.map', 
    './build/WebHelp/includes/css/custom_topic.css.map']);

	return gulp.src(['./src/scss/custom_header_footer.scss', 
    './src/scss/custom_topic.scss'])
        .pipe(gulpSass().on('error', gulpSass.logError))
        .pipe(gulpAutoprefixer({
            cascade: false
        }))
        // .pipe(gulpCleanCSS())
        .pipe(gulp.dest('./build/WebHelp/includes/css/'));
}

function watch() {    
    gulp.watch('./src/scss/**/*.scss', sass);
    gulp.watch("./build/**/*.html").on('change', browserSync.reload);
}


function publish(cb) {

    del.sync([publishDir],{force: true});

    gulp.src(['./build/**/*','!./build/**/*.map','!./build/**/old*/**'])
    .pipe(gulp.dest(publishDir));

    console.log('\x1b[41m', 'Folder replaced from local source!'); 
    console.log(publishDir, '\x1b[40m'); 

    cb();
}


exports.sass = sass;        // Компилира style.scss без да е минимизиран. Добавя map файл.
exports.build = build;      // Компилира style.scss. Не включва map файл.
exports.watch = watch;      // Следи за промени в scss файловете и компилира с команда sass

exports.publish = gulp.series(build, publish);      // Компилира и копира проекта в конфигурираната папка

exports.default = build;