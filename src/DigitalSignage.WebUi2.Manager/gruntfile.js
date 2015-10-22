module.exports = function (grunt) {

  grunt.initConfig({
    bower: {
      install: {
        options: {
          targetDir: "wwwroot/lib",
          layout: "byComponent",
          cleanTargetDir: false
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
        files: { 'wwwroot/app/app.min.js': ['Scripts/app.js', 'Scripts/**/*.module.js', 'Scripts/**/*.js'] }
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

  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-less');
  grunt.loadNpmTasks('grunt-bower-task');
};