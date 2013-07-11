(function () {
    var testModules = [
        "ButtonStackSelector/ButtonStackSelectorTests",
        "AdGroup/adGroupTests",
        "ButtonPage/buttonPageWidgetTests",
        "LinkPage/linkPageTests",
        "PageStackSelector/pageStackSelectorTests",
        "Rss/rssTests"
    ];
    
    require(testModules, function () {
        QUnit.load();
        QUnit.start();
    });

}());