(function () {
    
    var testModules = [
        "ButtonStackSelectorTests"
    ];
 
    
    require(testModules, function () {
        QUnit.load();
        QUnit.start();
    });

}());