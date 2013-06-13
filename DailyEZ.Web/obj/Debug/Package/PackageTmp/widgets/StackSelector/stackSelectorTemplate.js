define(["jquery", "utility"], function ($, utility) {
    $(document).ready(function () {
        $("#dailyEZ-com-stack-selector-widget[%WIDGET-ID%]").html("<img src='widgets/weather/images/loading.gif'/>");
        $.ajax({
            type: "POST",
            data: {
                stacks: "[%STACKS%]",
                title: "[%TITLE%]",
                widgetID: "[%WIDGET-ID%]",
                buttonColor: "[%BUTTON-COLOR%]"
            },
            url: "widgets/StackSelector/StackSelectorWorker.ashx",
            success: function (response) {
                $("#dailyEZ-com-stack-selector-widget[%WIDGET-ID%]").html(response);

                $("#stack-selector[%WIDGET-ID%] button[stack-id]").click(function () {
                    utility.setCookie("leftStackOverride", $(this).attr("stack-id"), 1);

                    window.location.reload();
                });
            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-stack-selector-widget[%WIDGET-ID%]").html("<b>Error retreiving stack selector</b>");
            }
        });
    });
});







