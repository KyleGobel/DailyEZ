define('ButtonStackSelectorTests', ['jquery', '../ButtonStackSelector/buttonStackSelector', "jquery.ba-bbq"], function ($, widgetLibrary) {
    module("Button Stack Selector Tests");
    var widget = new widgetLibrary.Widgets.ButtonStackSelector();
    widget.stacks = "220,221,219,213,214,212,216,217";
    widget.title = "Monona, WI";
    widget.widgetId = 1;
    widget.buttonColor = "btn-primary";


    QUnit.test("jquery defined", function () {
        ok(typeof $ != 'undefined', 'jquery is defined');
    });

    QUnit.test("stack property setter", function () {
        equal(widget.stacks, "220,221,219,213,214,212,216,217", "widget.stacks equals expected value");
    });
    QUnit.test("title property setter", function () {
        equal(widget.title, "Monona, WI", "widget.title equals expected value");
    });
    QUnit.test("widgetId property setter", function () {
        equal(widget.widgetId, 1, "widget.widgetId equals expected value");
    });
    QUnit.test("buttonColor property setter", function () {
        equal(widget.buttonColor, "btn-primary", "widget.buttonColor equals expected value");
    });
    QUnit.asyncTest("run returns true success code", function () {
        QUnit.expect(1);
        widget.run(function (success) {
            ok(success, "Run completed successfully");
            QUnit.start();
        });
    });
    QUnit.asyncTest("number of buttons", function () {
        expect(1);
        widget.run(function (success) {
            equal($("#dailyEZ-com-button-stack-selector-widget1 button").length, 8, "Correct number of Buttons returned");
            QUnit.start();
        });
    });
    QUnit.asyncTest("correct button colors", function () {
        expect(8);
        widget.run(function (success) {
            $("#dailyEZ-com-button-stack-selector-widget1 button").each(function () {
                ok($(this).hasClass(widget.buttonColor), "button has correct color class");
            });
            QUnit.start();
        });
    });
  
    QUnit.asyncTest("clicking button sets class to active", function () {
        widget.run(function (success) {
            $("#dailyEZ-com-button-stack-selector-widget1 button").each(function () {
                $(this).trigger('click');
                //check that this button has an active class
                ok($(this).hasClass('active'), "Active button has active class");

                //check that siblings do not have active class
                $(this).siblings().each(function () {
                    ok(!$(this).hasClass("active"), "siblings don't have active class");
                });
            });
            QUnit.start();
        });
    });
    QUnit.asyncTest("clicking button sets hash to mId=[button's stack-id attribute]", function () {
        widget.run(function (success) {
            $("#dailyEZ-com-button-stack-selector-widget1 button").each(function () {
                $(this).trigger('click');
                //check that the hash was set on the location url
                var expectedStackId = $(this).attr("stack-id");
                
                var hash = $.param.fragment();
                //get the stackID from the hash
                var match = hash.match("mId=([^&]*)");
                var actualStackId = match[1];
                
                equal(actualStackId, expectedStackId, "hash set correctly");
            });
            QUnit.start();
        });

    });
});




