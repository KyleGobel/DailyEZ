(function () {
    var testModules = [
        "ButtonStackSelector/ButtonStackSelectorTests",
        "AdGroup/adGroupTests"
    ];
    
    require(testModules, function () {
        QUnit.load();
        QUnit.start();
    });

}());