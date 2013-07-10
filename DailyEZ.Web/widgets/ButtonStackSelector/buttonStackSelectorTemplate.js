var Widgets;
(function (Widgets) {
    var ButtonStackSelector = (function () {
        function ButtonStackSelector() {
            //document.addEventListener("DOMContentLoaded", this.onReady);
        }
        ButtonStackSelector.prototype.onReady = function (callback) {
      
            var rootUrl = "";
            var root = document.getElementById("ApplicationRoot");
            if (root != null) {
                rootUrl = root.href;
            }
            $("#dailyEZ-com-button-stack-selector-widget[%WIDGET-ID%]").html("<img src='" + rootUrl + "widgets/weather/images/loading.gif'/>");
            $.ajax({
                type: "POST",
                data: {
                    stacks: "[%STACKS%]",
                    title: "[%TITLE%]",
                    widgetID: "[%WIDGET-ID%]",
                    buttonColor: "[%BUTTON-COLOR%]"
                },
                url: rootUrl + "widgets/ButtonStackSelector/ButtonStackSelectorWorker.ashx",
                success: function (response) {
                    $("#dailyEZ-com-button-stack-selector-widget[%WIDGET-ID%]").html(response);

                    $("#dailyEZ-com-button-stack-selector-widget[%WIDGET-ID%] button").click(function () {
                        var ret = $.param.fragment(window.location.href, "mId=" + $(this).attr("stack-id"));
                        window.location.href = ret;
                        $(".button-page-template button").removeClass("active");
                        $(this).addClass("active");
                    });
                    if (typeof callback == 'function')
                        callback(true);
                    return true;
                },
                error: function (ajaxObj, statusText, somethingElse) {
                    $("#dailyEZ-com-button-stack-selector-widget[%WIDGET-ID%]").html("<b>Error retreiving stack selector</b>" + statusText);
                    if (typeof callback == 'function')
                        callback(false);
                    return false;
                }
            });
        };
        return ButtonStackSelector;
    })();
    Widgets.ButtonStackSelector = ButtonStackSelector;
})(Widgets || (Widgets = {}));

