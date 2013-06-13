define('bootstrapper',
    ["jquery", "utility", "ieHomePageBar", "stackInitialize"],
    function ($, utility, ieHomePageBar, stackInitializr) {
        var run = function () {
            //wire up some events
            //ieHomePageBar Buttons
            $("#sure-button").click(ieHomePageBar.sureButtonClick);
            $("#no-thanks-button").click(ieHomePageBar.noThanksButtonClick);
            $("#x-button").click(ieHomePageBar.xButtonClick);

            //google search wire up
            $("#google-search-form").submit(function () {
                window.open("http://www.google.com/search?q=" + $("#google-search-form input").val().replace(" ", "+").replace("\"", "%22"), "_blank");
                return false;
            });

            //local search wire up
            $("#local-search-form").submit(function () {
                window.open(utility.getBaseUrl() + "Search.aspx?q=" + $("#local-search-form input").val().replace(" ", "+").replace("\"", "%22"), "_self");
                return false;
            });

            //ie make home page top bar prompt
            if (utility.usingIE()) {
                if (utility.getCookie('askedForHomepage') != 'true') {
                    $("#top-bar").slideDown('slow');
                    $("#invis-top-bar").slideDown('slow');
                }
            }
    
            stackInitializr.init();
        };
        return {
            run: run
    };
});