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
            if (utility.getCookie('registered') != 'true') {
                $("#registration-dialog").dialog({ height: 220, draggable: false, width: 500, closeOnEscape: false, resizable: false, modal: true });
              
                $(".ui-dialog-titlebar-close").css("display", "none");
            }
               

            $("#registration-dialog button").click(function () {
                if ($('#tbZip').val().length < 5) {
                    alert('Please enter your Zipcode.');
                    return;
                }
                //set our shit
                var eDate = new Date();
                eDate.setDate(eDate.getDate() + 800);

                utility.setFullCookie("registered", "true", eDate, "/", false, false);

                utility.setFullCookie("zip", $('#tbZip').val(), eDate, "/", false, false);

                //pageTracker._setCustomVar(
                //          3,                        // This custom var is set to slot #1
                //          "Zipcode",                // The name of the custom variable
                //          $('#tbZip').val(),        // The value of the custom variable 
                //          1                         // Sets the scope to visitor-level
                //    );
                //pageTracker._trackPageview();
                $("#registration-dialog").dialog('close');
                // $.get("TrackView.aspx?pageID=" + gHomeIndex);
                //   $.get("TrackAdView.aspx");
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