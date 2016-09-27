/*jshint node: true */
'use strict';

var WebDriver = require('selenium-webdriver'),
	axeBuilder = require('axe-webdriverjs');

module.exports = function (grunt) {
    grunt.registerMultiTask('axe-selenium', function () {

        var done = this.async(),
			count = this.data.length;

        var driver = new WebDriver.Builder()
			.forBrowser('chrome')
			.build();

        driver.manage().timeouts().setScriptTimeout(15000);

        var currentdate = new Date();
        var time = currentdate.getFullYear() + '_'
            + (currentdate.getMonth() + 1) + '_'
            + currentdate.getDate()
                     + currentdate.getHours()
                    + currentdate.getMinutes()
                    + currentdate.getSeconds();
        var resultFileName = '../Result/TestResult_' + time + '.html';

        this.data.forEach(function (testUrl) {
            driver.get(testUrl)
				.then(function () {
				    axeBuilder(driver)
                        //Choose which rules to test
                        .withRules([])
						.analyze(function (result) {
						    var oldContent = '';
						    if (grunt.file.exists(resultFileName)) {
						        oldContent = grunt.file.read(resultFileName);
						    } else {
						        oldContent = '<!DOCTYPE html><html><head><title>Test Result ' + time + '</title></head><body></body></html>';
						    }
						    var content = '<table rules=\"all\" style=\"border:2px solid black\">' + '<tr><td colspan=\"3\">Test URL: ' + result.url + '</td></tr><tr><th>Rule Id</th><th>Rule Description</th><th>Test Detail</th></tr>';

						    result.passes.forEach(function (pass) {
						        content += '<tr><td>' + pass.id + '</td><td>';
						        content += '<a href=\"' + pass.helpUrl + '\">' + pass.help.replace(/</g, '&lt;').replace(/>/g, '&gt;') + '</a></td><td>';
						        content += 'Passed</td></tr>';
						    });

						    result.violations.forEach(function (violation) {
						        content += '<tr><td>' + violation.id + '</td><td>';
						        content += '<a href=\"' + violation.helpUrl + '\">' + violation.help.replace(/</g, '&lt;').replace(/>/g, '&gt;') + '</a></td><td>' + violation.nodes.length.toString() + ' fragment(s) failed:<br/>';
						        var i = 1;
						        violation.nodes.forEach(function (node) {
						            var htmlText = (i++).toString() + '. ' + node.html.replace(/</g, '&lt;').replace(/>/g, '&gt;') + '<br>';
						            content += htmlText;
						        });
						        content += '</td></tr>';
						    });

						    content += '</table><br/></body></html>';
						    var finalContent = oldContent.replace('</body></html>', content);
						    grunt.file.write(resultFileName, finalContent);

						    if (!--count) {
						        done(result.violations.length === 0);
						    }
						});
				});
        });

        driver.quit();
    });
};
