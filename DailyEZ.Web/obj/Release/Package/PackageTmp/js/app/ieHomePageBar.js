define("ieHomePageBar", ["jquery", "utility"], function ($, utility) {
    return {
        sureButtonClick: function () {
            document.body.style.behavior = 'url(#default#homepage)';

            document.body.setHomePage(utility.getBaseUrl());
            $('#top-bar').slideUp('slow', function () { });
            $('#invis-top-bar').slideUp('slow', function () { });
            utility.setCookie('askedForHomepage', 'true', 60);
        },
        noThanksButtonClick: function () {
            $('#top-bar').slideUp('slow', function () { });
            $('#invis-top-bar').slideUp('slow', function () { });

            utility.setCookie('askedForHomepage', 'true', 30);
        },
        xButtonClick: function () {
            $('#top-bar').slideUp('slow', function () { });
            $('#invis-top-bar').slideUp('slow', function () { });
        }

    };
});