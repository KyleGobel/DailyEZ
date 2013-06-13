define(["jquery"], function ($) {
    function getPrevNews() {

        $.ajax({
            type: "POST",
            data: {
                feedUrl: "[%FEED-URL%]",
                rssID: "[%RSS-ID%]",
                action: "prev",
                wellColor: "[%WELL-COLOR%]",
                title: "[%TITLE%]"
            },
            url: "widgets/rss/GetRSSWorker.ashx",
            success: function (response) {

                $("#dailyEZ-com-rss-widget[%RSS-ID%]").html(response);
                $("#dailyEZ-com-rss-widget[%RSS-ID%]").trigger("widget-refreshed");

                $("#dailyEZ-com-rss-widget[%RSS-ID%]-prev-button").click(getPrevNews);
                $("#dailyEZ-com-rss-widget[%RSS-ID%]-next-button").click(getNextNews);


            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-rss-widget[%RSS-ID%]").html("<b>Error retreiving RSS Feed</b>");
            }
        });
    }
    function getNextNews() {
        $.ajax({
            type: "POST",
            data: {
                feedUrl: "[%FEED-URL%]",
                rssID: "[%RSS-ID%]",
                action: "next",
                wellColor: "[%WELL-COLOR%]",
                title: "[%TITLE%]"
            },
            url: "widgets/rss/GetRSSWorker.ashx",
            success: function (response) {

                $("#dailyEZ-com-rss-widget[%RSS-ID%]").html(response);
                $("#dailyEZ-com-rss-widget[%RSS-ID%]").trigger("widget-refreshed");

                $("#dailyEZ-com-rss-widget[%RSS-ID%]-prev-button").click(getPrevNews);
                $("#dailyEZ-com-rss-widget[%RSS-ID%]-next-button").click(getNextNews);


            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-rss-widget[%RSS-ID%]").html("<b>Error retreiving RSS Feed</b>");
            }
        });
    }
    
    $(document).ready(function () {

        $("#dailyEZ-com-rss-widget[%RSS-ID%]").html("<img src='widgets/weather/images/loading.gif'/>");
        $.ajax({
            type: "POST",
            data: {
                feedUrl: "[%FEED-URL%]",
                rssID: "[%RSS-ID%]",
                action: "next",
                wellColor: "[%WELL-COLOR%]",
                title: "[%TITLE%]"
            },
            url: "widgets/rss/GetRSSWorker.ashx",
            success: function (response) {

                $("#dailyEZ-com-rss-widget[%RSS-ID%]").html(response);
                $("#dailyEZ-com-rss-widget[%RSS-ID%]").trigger("widget-loaded");

                $("#dailyEZ-com-rss-widget[%RSS-ID%]-prev-button").click(getPrevNews);
                $("#dailyEZ-com-rss-widget[%RSS-ID%]-next-button").click(getNextNews);


            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-rss-widget[%RSS-ID%]").html("<b>Error retreiving RSS Feed</b>");
            }
        });
    });
});
