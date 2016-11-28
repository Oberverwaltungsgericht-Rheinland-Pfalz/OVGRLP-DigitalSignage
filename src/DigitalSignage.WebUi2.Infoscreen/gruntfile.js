/// <binding BeforeBuild='build' />
module.exports = function (grunt) {
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-less');
  grunt.loadNpmTasks('grunt-bower-task');
  grunt.loadNpmTasks('grunt-browser-sync');

  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    bower: {
      install: {
        options: {
          install: true,
          copy: true,
          targetDir: "wwwroot/lib",
          layout: "byComponent",
          cleanTargetDir: true
        }
      }
    },
    jshint: {
      all: ['gruntfile.js', 'app/*.js', 'app/**/*.js']
    },
    concat: {
      options: {
        stripBanners: true,
        banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' + '<%= grunt.template.today("dd.mm.yyyy") %> */\n',
      }, 
      dist: {
        src: ['Scripts/app.module.js', 'Scripts/**/*.module.js', 'Scripts/**/*.js', '!Scripts/*.config.js', '!Scripts/**/*.config.js'],
        dest: 'wwwroot/app/infoscreen.js'
      },
      config: {
        src: ['Scripts/*.config.js', 'Scripts/**/*.config.js'],
        dest: 'wwwroot/app/infoscreen.config.js'
      }
    },
    less: {
      dist: {
        options: {
        },
        src: ['Styles/default.less'],
        dest: 'wwwroot/css/default.css'
      }
    },
    uglify: {
      options: {
        banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' + '<%= grunt.template.today("dd.mm.yyyy") %> */\n'
      },
      dist: {
        src: ['wwwroot/app/infoscreen.js'],
        dest: 'wwwroot/app/infoscreen.min.js'
      }
    },
    watch: {
      scripts: {
        files: ['Scripts/**/*js', 'Styles/*.less'],
        tasks: ['build']
      }
    },
    clean: {
      js: ['wwwroot/app/*.js', 'wwwroot/app/**/*.js'],
      css: ['wwwroot/css/*.*']
    },
    browserSync: {
      bsFiles: {
        src : './wwwroot'
      },
      options: {
        server: {
          baseDir: "./wwwroot"
        }
      }
    }
  });

  grunt.registerTask('build', ['bower:install', 'jshint:all', 'concat:dist', 'concat:config', 'less:dist', 'uglify:dist']);
  grunt.registerTask('build-watch', ['build', 'watch']);
  grunt.registerTask('release', ['clean:js', 'clean:css', 'build']);
  grunt.registerTask('serve', ['build', 'browserSync']);
};