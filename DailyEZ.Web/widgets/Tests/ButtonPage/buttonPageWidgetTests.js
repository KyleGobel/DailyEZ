define(["jquery", "../../ButtonPage/buttonPage", 'bootstrap'], function ($, widgetLib) {
    module("Button Page Widget");
    var w = new widgetLib.Widgets.ButtonPage();
    w.pageId = 19206;
    w.buttonColor = "btn-info";
    w.favIcons = false;
    w.widgetId = 1;
    QUnit.asyncTest("run returns true", function(){
        expect(1);
        w.run(function (success) {
            ok(success, "run returned true");
            start();
        });
    });

    QUnit.asyncTest("correct button colors", function () {

        w.run(function (success) {
            $("#dailyEZ-com-button-page-widget1 button").each(function () {
                ok($(this).hasClass(w.buttonColor), "button has correct color class");
            });
            QUnit.start();
        });
    });
    
    QUnit.asyncTest("favIcons works when turned on", function () {
        w.favIcons = true;
        w.run(function (success) {
            $("#dailyEZ-com-button-page-widget1 button").each(function () {
                var html = $(this).html();
                ok(html.indexOf("<img ") >= 0, "found img tag within button");
            });
            start();
        });
      
    });
    
    QUnit.asyncTest("favIcons not present when turned off", function () {
        w.favIcons = false;
        w.run(function () {
            $("#dailyEZ-com-button-page-widget1 button").each(function () {
                var html = $(this).html();
                ok(html.indexOf("<img ") == -1, "img tag not found within button");
            });
            start();
        });
    });
    
    QUnit.asyncTest("invalid page id displays nothing", function () {
        expect(2);
        w.pageId = 0;
        w.run(function (success) {
            var buttons = $("#dailyEZ-com-button-page-widget1 button");
            ok(buttons.length == 0, "0 buttons displayed");
           
            var titleHtml = $("#dailyEZ-com-button-page-widget1 h4").html();
            ok(titleHtml.length == 0, "title h4 is empty");
            start();
        });
    });
});