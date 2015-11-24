module.exports = function (grunt) {
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-less');
  grunt.loadNpmTasks('grunt-bower-task');

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
        banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' + '<%= grunt.template.today("dd.mm.yyyy") %> */\n'
      },
      dist: {
        src: ['Scripts/app.module.js', 'Scripts/**/*.module.js', 'Scripts/**/*.js', '!Scripts/*.config.js', '!Scripts/**/*.config.js'],
        dest: 'wwwroot/app/manager.js'
      },
      config: {
        src: ['Scripts/*.config.js', 'Scripts/**/*.config.js'],
        dest: 'wwwroot/app/manager.config.js'
      }
    },
    less: {
      dist: {
        options: {
        },
        src: ['Styles/main.less'],
        dest: 'wwwroot/css/main.css'
      }
    },
    uglify: {
      options: {
        banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' + '<%= grunt.template.today("dd.mm.yyyy") %> */\n'
      },
      dist: {
        src: ['wwwroot/app/manager.js'],
        dest: 'wwwroot/app/manager.min.js'
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
    }
  });

  grunt.registerTask('build', ['bower:install', 'jshint:all', 'concat:dist', 'concat:config', 'less:dist', 'uglify:dist']);
  grunt.registerTask('build-watch', ['build', 'watch']);
  grunt.registerTask('release', ['clean:js', 'clean:css', 'build']);
};