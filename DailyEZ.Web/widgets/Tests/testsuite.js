(function () {
    var testModules = [
        "ButtonStackSelector/ButtonStackSelectorTests",
        "AdGroup/adGroupTests",
        "ButtonPage/buttonPageWidgetTests"
    ];
    
    require(testModules, function () {
        QUnit.load();
        QUnit.start();
    });

}());