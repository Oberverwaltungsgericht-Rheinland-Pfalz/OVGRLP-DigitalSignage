module.exports = function (grunt) {
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-watch');
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
      all: ['gruntfile.js', 'Scripts/*.js', 'Scripts/**/*.js', 'Demo/*.js', 'Demo/**/*.js']
    },
    concat: {
      options: {
        stripBanners: true,
        banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' + '<%= grunt.template.today("dd.mm.yyyy") %> */\n'
      },
      demo: {
        src: ['Demo/app.module.js', 'Demo/**/*.js'],
        dest: 'wwwroot/app/ds-demo.js'
      },
      dist: {
        src: ['Scripts/app.module.js', 'Scripts/**/*.module.js', 'Scripts/**/*.js'],
        dest: 'wwwroot/dist/ds-core.js'
      }
    },
    uglify: {
      options: {
        banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' + '<%= grunt.template.today("dd.mm.yyyy") %> */\n'
      },
      dist: {
        src: ['wwwroot/dist/ds-core.js'],
        dest: 'wwwroot/dist/ds-core.min.js'
      }
    },
    clean: {
      js: ['wwwroot/dist/*.js', 'wwwroot/dist/**/*.js', 'wwwroot/app/*.js']
    },
    watch: {
      scripts: {
        files: ['Scripts/**/*.js', 'Demo/**/*.js'],
        tasks: ['build']
      }
    },
  });

  grunt.registerTask('build', ['bower:install', 'jshint:all', 'concat:demo', 'concat:dist', 'uglify:dist']);
  grunt.registerTask('build-watch', ['build', 'watch']);
  grunt.registerTask('release', ['clean:js', 'build']);
};