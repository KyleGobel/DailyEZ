
//relies on jquery plugin jquery-bbq
define(["jquery"], function ($) {
    $(document).ready(function () {
        $("#dailyEZ-com-button-stack-selector-widget[%WIDGET-ID%]").html("<img src='widgets/weather/images/loading.gif'/>");
        $.ajax({
            type: "POST",
            data: {
                stacks: "[%STACKS%]",
                title: "[%TITLE%]",
                widgetID: "[%WIDGET-ID%]",
                buttonColor: "[%BUTTON-COLOR%]"
            },
            url: "widgets/ButtonStackSelector/ButtonStackSelectorWorker.ashx",
            success: function (response) {
                $("#dailyEZ-com-button-stack-selector-widget[%WIDGET-ID%]").html(response);

                $("#dailyEZ-com-button-stack-selector-widget[%WIDGET-ID%] button").click(function () {
                    var ret = $.param.fragment(window.location.href, "mId=" + $(this).attr("stack-id"));
                    window.location.href = ret;
                    $(".button-page-template button").removeClass("active");
                    $(this).addClass("active");
                });
            },
            error: function (ajaxObj, statusText, somethingElse) {
                $("#dailyEZ-com-button-stack-selector-widget[%WIDGET-ID%]").html("<b>Error retreiving stack selector</b>");
            }
        });
    });
});
