define(["jquery", "jquery.jfeed.pack", "toastr"], function ($, k,toastr) {

    var getDay = function () {
        var dt = new Date();
        dt.getDay();

        switch (dt.getDay()) {
            case 0:
                return "Sunday";
            case 1:
                return "Monday";
            case 2:
                return "Tuesday";
            case 3:
                return "Wednesday";
            case 4:
                return "Thursday";
            case 5:
                return "Friday";
            case 6:
                return "Saturday";
            default:
                return "Invalid Day";
        }
    };
    var updateDateTimeString = function () {
        var currentTime = new Date();
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();

        var hours = currentTime.getHours();
        var minutes = currentTime.getMinutes();
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;

        $("#weatherDate").html(month + "/" + day + "/" + year + " " + hours + ":" + minutes + " " + ampm);

        setTimeout(updateDateTimeString, 1000 * 60);
    };

    var checkForAlerts = function () {
        $.getFeed({
            url: "widgets/weather/Proxy.ashx?http://alerts.weather.gov/cap/wwaatmget.php?x=WIC025&y=0",
            success: function (feed) {
                var items = feed.items;

                //dont' know better way to see if there isn't a warning
                if (items[0].title.indexOf("There are no active watches, warnings or advisories") >= 0)
                    return;

                //alert
                $("#dailyEZ-com-weather-widget table img").attr("src", "widgets/weather/images/weather-severe-alert.png");
                $("#conditionsContainer").html("<span style='color:yellow; font-weight:bold'>NWS Weather Alert!</span>");
                //if we got here this is a warning
                toastr.options.positionClass = "toast-top-left";
                toastr.options.timeOut = 30000;



                toastr.error("<a href='" + items[0].link + "' target='_blank'>" + items[0].title + "</a>");

            },
            error: function (er, statusText, otherObj) {
                alert(statusText);
            },


        });

    };
    var getWeather = function () {
        $("#dailyEZ-com-weather-widget").html("<img src='widgets/weather/images/loading.gif'/>");
        $.ajax({
            type: "POST",
            data: {
                zipcode: "[%ZIPCODE%]",
            },
            url: "widgets/Weather/GetWeatherWorker.ashx",
            success: function (response) {
                response = response.replace("[%DAY%]", getDay());

                var stackID = "[%STACK-ID%]";


                $("#dailyEZ-com-weather-widget").html(response);

                checkForAlerts();
                updateDateTimeString();

                if (stackID > 0) {
                    $.ajax({
                        type: "POST",
                        data: { stackID: stackID },
                        url: "widgets/Weather/GetStackHeight.ashx",
                        success: function (response) {
                            $("#dailyEZ-com-weather-widget").click(function () {
                                var ret = $.param.fragment(window.location.href, "mId=" + stackID);
                                window.location.href = ret;
                            });
                        },
                        error: function (ajaxObj, statusText, somethingElse) {
                            $("#dailyEZ-com-weather-widget").html("<b>Error retreiving Stack Height</b>");
                        }

                    });

                } else {
                    $("#dailyEZ-com-weather-widget").click(function () {
                        window.open("[%WEATHERURL%]", '_blank');
                        return false;
                    });
                }

            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-weather-widget").html("<b>Error retreiving Weather</b>");
            }
        });
        setTimeout(getWeather, 1000 * 60 * 10);

    };
    $(document).ready(function () {
        /******* Load CSS *******/
        var weatherCss = $("<link>", {
            rel: "stylesheet",
            type: "text/css",
            href: "widgets/Weather/style.css"
        });
        weatherCss.appendTo('head');
        
        var toastrCss = $("<link>", {
            rel: "stylesheet",
            type: "text/css",
            href: "css/vendor/toastr.min.css"
        });
        toastrCss.appendTo('head');
        getWeather();
    });
});
