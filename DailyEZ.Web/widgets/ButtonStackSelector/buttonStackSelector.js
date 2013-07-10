define(["require", "exports"], function(require, exports) {
    (function (Widgets) {
        var ButtonStackSelector = (function () {
            function ButtonStackSelector(stacks, title, widgetId, buttonColor) {
                this.stacks = stacks;
                this.title = title;
                this.widgetId = widgetId;
                this.buttonColor = buttonColor;
                this.rootUrl = "";
                var root = document.getElementById("ApplicationRoot");
                if (root != 'undefined' && root != null)
                    this.rootUrl = root.href;
            }
            ButtonStackSelector.prototype.run = function (callback) {
                if (typeof this.rootUrl == 'undefined')
                    this.rootUrl = "";
                var widgetDomId = "#dailyEZ-com-button-stack-selector-widget" + this.widgetId;
                $(widgetDomId).html("<img src='" + this.rootUrl + "widgets/weather/images/loading.gif'/>");
                $.ajax({
                    type: "POST",
                    data: {
                        stacks: this.stacks,
                        title: this.title,
                        widgetID: this.widgetId,
                        buttonColor: this.buttonColor
                    },
                    url: this.rootUrl + "widgets/ButtonStackSelector/ButtonStackSelectorWorker.ashx",
                    success: function (response) {
                        $(widgetDomId).html(response);

                        $(widgetDomId + " button").click(function () {
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
                        $(widgetDomId).html("<b>Error retreiving stack selector</b>");
                        if (typeof callback == 'function')
                            callback(false);
                        return false;
                    }
                });
            };
            return ButtonStackSelector;
        })();
        Widgets.ButtonStackSelector = ButtonStackSelector;
    })(exports.Widgets || (exports.Widgets = {}));
    var Widgets = exports.Widgets;
});
