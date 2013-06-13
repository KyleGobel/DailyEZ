(function () {
    var root = this;

    requirejs.config({
        waitSeconds: 60
    });
   
    define3rdPartyModules();
    
    loadPluginsAndBoot();
    
    function define3rdPartyModules() {
        define('jquery', [], function () { return root.jQuery; });
        define('toastr', [], function () { return root.toastr; });
    };
    
    function loadPluginsAndBoot() {
        requirejs([
            "jquery.jfeed.pack"
        ], boot);

    };
    function boot() {
        require(['bootstrapper'], function (bs) {bs.run();});
    }
})();


