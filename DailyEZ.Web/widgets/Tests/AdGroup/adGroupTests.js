define(["jquery", "../../adGroup/adGroup"], function ($, widgetLibrary) {
    var w = new widgetLibrary.Widgets.AdGroup();
    w.adGroup = 19;
    w.widgetId = 1;
    
    module("Ad Group Widget");
    QUnit.test("auto rotate is false by default", function () {
        ok(w.autoRotate == false);
    });
    QUnit.asyncTest("run returns true", function () {
        expect(1);
        w.run(function (success) {

            ok(success, "run completed with value of true");
            start();
        });
    });
    QUnit.asyncTest("removes img borders (required for IE)", function () {
        expect(1);
        w.run(function (success) {
            var borderCss = $(".divAd img").css("border");
            ok(borderCss.indexOf("none") > 0, "img border is none");
            start();
        });
    });
    QUnit.asyncTest("all links target is _blank", function () {
        w.run(function (s) {
            $(".divAd a").each(function () {
                equal($(this).attr("target"), "_blank", "link target equals blank");
            });
            start();
        });
    });
    QUnit.asyncTest("all links rel set to 'nofollow'", function () {
        w.run(function (s) {
            $(".divAd a").each(function () {
                equal($(this).attr("rel"), "nofollow", "link rel set to 'nofollow'");
            });
            start();
        });
    });

});