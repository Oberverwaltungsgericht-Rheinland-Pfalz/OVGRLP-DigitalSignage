/// <binding BeforeBuild='build' />
module.exports = function (grunt) {
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-less');
  grunt.loadNpmTasks('grunt-bower-task');
  grunt.loadNpmTasks('grunt-ts');

  grunt.initConfig({
    ts: {
      default: {
        src: ['Scripts/**/*.ts']
      }
    },
    bower: {
      install: {
        options: {
          targetDir: "wwwroot/lib",
          layout: "byComponent",
          cleanTargetDir: true
        }
      }
    },
    less: {
      dev: {
        options: {

        },
        files: {
          "wwwroot/css/main.css": "Styles/main.less"
        }
      },
      prod: {
        options: {
        },
        files: {
          "wwwroot/css/main.css": "Styles/main.less"
        }
      }
    },
    uglify: {
      my_target: {
        files: { 'wwwroot/app/ds-infoscreen.min.js': ['Scripts/ds-infoscreen.module.js', 'Scripts/**/*.module.js', 'Scripts/**/*.js'] }
      }
    },
    watch: {
      scripts: {
        files: ['Scripts/**/*js'],
        tasks: ['uglify']
      }
    }
  });

  grunt.registerTask('default', ['bower:install', 'less:dev', 'uglify', 'watch']);
  grunt.registerTask('build', ['bower:install', 'less:dev', 'uglify']);
};