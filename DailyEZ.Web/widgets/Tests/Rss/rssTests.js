define(["jquery", "../../RSS/rss"], function ($, widgetLibrary) {
    var w = new widgetLibrary.Widgets.Rss();
    
    module("Rss Widget");
    QUnit.asyncTest("run", function () {
        expect(1);
        w.run(function (success) {
            ok(success, "run completed successfully");
            start();
        });
    });

});