//dependency on bootstrap
define(["jquery"], function ($, bootstrap) {
    $(document).ready(function (){

        /*--------------Load CSS-----------------*/
        var cssLink = $("<link>", {
            rel: "stylesheet",
            type: "text/css",
            href: "widgets/ButtonPage/style.css"
        });
        cssLink.appendTo('head');

        $("#dailyEZ-com-button-page-widget[%WIDGET-ID%]").html("<img src='widgets/weather/images/loading.gif'/>");
        $.ajax({
            type: "POST",
            data: { pageID: "[%PAGE-ID%]",
                favIcons: "[%FAVICONS%]",
                buttonColor: "[%BUTTON-COLOR%]"
            },
                
            url: "widgets/ButtonPage/GetButtonWidgetWorker.ashx",
            success: function (response) {

                $("#dailyEZ-com-button-page-widget[%WIDGET-ID%]").html(response);
                $("#dailyEZ-com-button-page-widget[%WIDGET-ID%] a").tooltip();
                $("#dailyEZ-com-button-page-widget[%WIDGET-ID%]").trigger("widget-loaded");  

            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-button-page-widget[%WIDGET-ID%]").html("<b>Error retreiving link page</b>");
            }
        });
    });
});

