define(["jquery"], function ($) {
    $(document).ready(function () {
        /******* Load CSS *******/
        var cssLink = $("<link>", {
            rel: "stylesheet",
            type: "text/css",
            href: "widgets/LinkPage/style.css"
        });
        cssLink.appendTo('head');

        $("#dailyEZ-com-link-page-widget[%WIDGET-ID%]").html("<img src='widgets/weather/images/loading.gif'/>");
        $.ajax({
            type: "POST",
            data: {
                pageID: "[%PAGE-ID%]",
                wellColor: "[%WELL-COLOR%]"
            },
            url: "widgets/LinkPage/GetLinkWidgetWorker.ashx",
            success: function (response) {

                $("#dailyEZ-com-link-page-widget[%WIDGET-ID%]").html(response);
                $("#dailyEZ-com-link-page-widget[%WIDGET-ID%] a").tooltip();
                $("#dailyEZ-com-link-page-widget[%WIDGET-ID%]").trigger("widget-loaded");


            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-link-page-widget[%WIDGET-ID%]").html("<b>Error retreiving link page</b>");
            }
        });
    });
});

