define(["require", "exports"], function(require, exports) {
    /// <reference path="../../js/typed/jquery.d.ts" />
    (function (Widgets) {
        var PageStackSelector = (function () {
            function PageStackSelector(url, title, widgetId, buttonColor) {
                this.url = url;
                this.title = title;
                this.widgetId = widgetId;
                this.buttonColor = buttonColor;
                this.rootUrl = "";
                var root = document.getElementById("ApplicationRoot");
                if (typeof root != 'undefined' && root != null)
                    this.rootUrl = root.href;
            }
            PageStackSelector.prototype.run = function (callback) {
                var widgetDomId = "#dailyEZ-com-page-stack-selector-widget" + this.widgetId;
                var widget = $(widgetDomId);

                widget.html("<img src='" + this.rootUrl + "widgets/weather/images/loading.gif'/>");
                $.ajax({
                    type: "POST",
                    data: {
                        url: this.url,
                        title: this.title,
                        widgetID: this.widgetId,
                        buttonColor: this.buttonColor
                    },
                    url: this.rootUrl + "widgets/PageStackSelector/PageStackSelectorWorker.ashx",
                    success: function (response) {
                        widget.html(response);
                        $(widgetDomId + " .areaTitle").css("font-size", "18pt");
                        if (typeof callback == 'function')
                            callback(true);
                    },
                    error: function (err) {
                        widget.html("<b>Error retreiving stack selector</b>");
                        if (typeof callback == 'function')
                            callback(false);
                    }
                });
            };
            return PageStackSelector;
        })();
        Widgets.PageStackSelector = PageStackSelector;
    })(exports.Widgets || (exports.Widgets = {}));
    var Widgets = exports.Widgets;
});
