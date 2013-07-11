(function () {
    var testModules = [
        "ButtonStackSelector/ButtonStackSelectorTests",
        "AdGroup/adGroupTests",
        "ButtonPage/buttonPageWidgetTests",
        "LinkPage/linkPageTests"
    ];
    
    require(testModules, function () {
        QUnit.load();
        QUnit.start();
    });

}());