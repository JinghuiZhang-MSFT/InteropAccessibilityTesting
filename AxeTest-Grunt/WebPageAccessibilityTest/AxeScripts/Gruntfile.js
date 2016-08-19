module.exports = function (grunt) {
	'use strict';
	grunt.loadTasks('build/tasks');

	grunt.initConfig({
	    pkg:grunt.file.readJSON('package.json'),
		'axe-selenium': {
			urls:[]
		}
	});

	grunt.registerTask('test', ['axe-selenium']);
};
