define(["jquery", "../../PageStackSelector/pageStackSelector"], function ($, widgetLibrary) {
    module("Page Stack Selector Widget");
    var w = new widgetLibrary.Widgets.PageStackSelector();
    
    QUnit.asyncTest("run", function () {
        expect(1);
        w.run(function (success) {
            ok(success, "success returned from run with value of true");
            start();
        });
    });
});