define(["jquery"], function ($) {
    $(document).ready(function () {


        $("#dailyEZ-com-page-stack-selector-widget[%WIDGET-ID%]").html("<img src='widgets/weather/images/loading.gif'/>");
        $.ajax({
            type: "POST",
            data: {
                url: "[%URL%]",
                title: "[%TITLE%]",
                widgetID: "[%WIDGET-ID%]",
                buttonColor: "[%BUTTON-COLOR%]"
            },
            url: "widgets/PageStackSelector/PageStackSelectorWorker.ashx",
            success: function (response) {
                $("#dailyEZ-com-page-stack-selector-widget[%WIDGET-ID%]").html(response);
            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-page-stack-selector-widget[%WIDGET-ID%]").html("<b>Error retreiving stack selector</b>");
            }
        });
    });

});
