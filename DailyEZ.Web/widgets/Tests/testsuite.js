(function () {
    var testModules = [
        "ButtonStackSelector/ButtonStackSelectorTests",
        "AdGroup/adGroupTests",
        "ButtonPage/buttonPageWidgetTests",
        "LinkPage/linkPageTests",
        "PageStackSelector/pageStackSelectorTests"
    ];
    
    require(testModules, function () {
        QUnit.load();
        QUnit.start();
    });

}());