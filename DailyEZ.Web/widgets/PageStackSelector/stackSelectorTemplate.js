﻿define(["jquery"], function ($) {
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
                if (string.length("[%TITLE%]") > 12)
                    $("#dailyEZ-com-page-stack-selector-widther[%WIDGET-ID%] .areaTitle").css("font-size", "16pt");
                else {
                    $("#dailyEZ-com-page-stack-selector-widther[%WIDGET-ID%] .areaTitle").css("font-size", "18pt");
                }
            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-page-stack-selector-widget[%WIDGET-ID%]").html("<b>Error retreiving stack selector</b>");
            }
        });
    });

});
