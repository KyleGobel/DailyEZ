define(["jquery"], function ($) {
    $(document).ready(function () {
        //8693
        var widget = $("#dailyEZ-com-ad-group[%WIDGET-ID%]");
        widget.html("<img src='widgets/weather/images/loading.gif'/>");
        $.ajax({
            type: "POST",
            data: { adGroup: "[%AD-GROUP%]",
                autoRotate: "[%AUTO-ROTATE%]"
            },
            url: "widgets/AdGroup/AdGroupHtml.ashx",
            success: function (response) {

                widget.html(response);
                widget.trigger("widget-loaded");
                $(".divAd").css("margin", "auto");
                $(".divAd").find('a').attr({ target: "_blank", rel: "nofollow" });


                //take borders off ie ad graphics
                $(".divAd").find('img').css("border", "none");
                $(".divAd").find('a').css("text-decoration", "none");
                $(".divAd").css("line-height", "normal");
                $(".divAd").find('table').css("border-collapse", "seperate");

            },
            error: function (ajaxObj, statusText, somethingElse) {
                widget.html("<b>Error retreiving ad group</b>");
            }
        });

    });
});

