const publishFolder = "//ui.dev.local/www/UI/MVR_EAU_Backoffice(PR026198,15.11.2025)/PublicPortal";
const reactFolder = "../../EAU.Web.Portal.App/client-app/src/assets/";
const IdentityServerFolder = "../../EAU.Web.IdentityServer/wwwroot/";

const sourceFolder = "./src/scss/";
const sourceFile = "style.scss";
const destinationFolder = "./mockups/";


const gulp = require('gulp');

const gulpSass = require('gulp-sass')(require('node-sass'));

const gulpAutoprefixer = require('gulp-autoprefixer');

const gulpCleanCSS = require('gulp-clean-css');

const gulpSourcemaps = require('gulp-sourcemaps');

const browserSync = require('browser-sync').create();

const rimraf = require('rimraf');

const tasksConfig = require('./gulp-tasks-config.json');

function sass() {
    return gulp.src([sourceFolder+sourceFile, sourceFolder+'print.scss'])
        .pipe(gulpSourcemaps.init())
        .pipe(gulpSass().on('error', gulpSass.logError))
        .pipe(gulpAutoprefixer({
            cascade: false
        }))
        //.pipe(gulpCleanCSS())
        .pipe(gulpSourcemaps.write('./'))
        .pipe(gulp.dest(destinationFolder+'css/'))
        .pipe(browserSync.stream())
}

function build() {

    rimraf(destinationFolder+'css/*.map',{},()=>{});

	return gulp.src([sourceFolder+sourceFile, sourceFolder+'print.scss'])
        .pipe(gulpSass().on('error', gulpSass.logError))
        .pipe(gulpAutoprefixer({
            cascade: false
        }))
        // .pipe(gulpCleanCSS())        
        .pipe(gulp.dest(destinationFolder+'css/'));
}

function copyToReact() {
    //  копира необходимите файлове (картинки, css, шрифтове) в папката на react приложението
    return gulp.src([destinationFolder+'css/style.css', destinationFolder+'fonts/**/*.*', destinationFolder+'images/**/*.*'], { base: destinationFolder })
      .pipe(gulp.dest(reactFolder))
      .pipe(gulp.dest(IdentityServerFolder));
}

function watch() {    
    gulp.watch(sourceFolder+'**/*.scss', sass);
    gulp.watch(destinationFolder+'**/*.html').on('change', browserSync.reload);
}

function serv() {
    browserSync.init({
        notify: false,
        server: {
            baseDir: destinationFolder,
            directory: true
        },
        ghostMode: false
    });

    watch();
}

function publishToPath(path) {
    rimraf.sync(path,{},()=>{});
    gulp.src([destinationFolder+'**/*','!'+destinationFolder+'**/*.map','!'+destinationFolder+'**/old*/**'])
    .pipe(gulp.dest(path));

    console.log('\x1b[42m', 'Folder replaced from local source!'); 
    console.log(path, '\x1b[40m');
}

function publish(cb) {
    publishToPath(publishFolder);
    cb();
}

function publishPersonal(cb) {
    if (tasksConfig.user != null) {
        let dirName = publishFolder.substring(publishFolder.lastIndexOf('/') + 1);
        let path = publishFolder.substring(0, publishFolder.lastIndexOf('/'));
        publishToPath(path + "/work/" + tasksConfig.user + "/" + dirName);    
    } else {
        console.log('\x1b[41m','User name not set in gulp-tasks-config.json');
        console.log('\x1b[40m');
    }
    cb();
}

exports.sass = sass;                                                // Компилира style.scss без да е минимизиран. Добавя map файл.
exports.build = build;                                              // Компилира style.scss. Не включва map файл.
exports.watch = watch;                                              // Следи за промени в scss файловете и компилира с команда sass
exports.serv = serv;                                                // Стартира web server. При промяна в scss и html файлове презарежда съдържанието на прозорците отворени през web server-a. Компилира scss с команда sass.
exports.copyToReact = gulp.series(build, copyToReact);              // Компилира style.scss. Копира го в папката на react app.

exports.publish = gulp.series(build, publish);                      // Компилира style.scss. Копира проекта в конфигурираната папка. 
exports.publishPersonal = gulp.series(build, publishPersonal);      // Компилира и копира проекта в конфигурираната папка в подпапка на конфугурирания потребител

exports.default = build;