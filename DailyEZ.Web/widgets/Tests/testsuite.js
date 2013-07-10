(function () {
    var testModules = [
        "ButtonStackSelector/ButtonStackSelectorTests"
    ];
    
    require(testModules, function () {
        QUnit.load();
        QUnit.start();
    });

}());