define(["jquery", "../../LinkPage/linkPageWidget", "bootstrap"], function ($, widgetLibrary) {

    var w = new widgetLibrary.Widgets.LinkPage(1, 19206, 'randomColor');
    
    module("Link Page Widget");
    QUnit.asyncTest("run", function () {
        w.run(function (success) {
            ok(success, "run completed with true success variable");
            start();
        });
    });
});