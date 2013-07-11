define(["require", "exports"], function(require, exports) {
    /// <reference path="../../js/typed/bootstrap.d.ts" />
    /// <reference path="../../js/typed/jquery.d.ts" />
    (function (Widgets) {
        var ButtonPage = (function () {
            function ButtonPage(pageId, favIcons, buttonColor, widgetId) {
                this.pageId = pageId;
                this.favIcons = favIcons;
                this.buttonColor = buttonColor;
                this.widgetId = widgetId;
                if (typeof favIcons == 'undefined')
                    favIcons = false;

                var root = document.getElementById("ApplicationRoot");
                if (typeof root != 'undefined' && root != null)
                    this.rootUrl = root.href;
            }
            ButtonPage.prototype.run = function (callback) {
                var widgetDomId = "#dailyEZ-com-button-page-widget" + this.widgetId;
                var widget = $(widgetDomId);

                widget.html("<img src='" + this.rootUrl + "widgets/weather/images/loading.gif'/>");

                $.ajax({
                    type: "POST",
                    data: {
                        pageID: this.pageId,
                        favIcons: this.favIcons,
                        buttonColor: this.buttonColor
                    },
                    url: this.rootUrl + "widgets/ButtonPage/GetButtonWidgetWorker.ashx",
                    success: function (response) {
                        widget.html(response);
                        $(widgetDomId + " a").tooltip();
                        widget.trigger("widget-loaded");
                        if (typeof callback == 'function')
                            callback(true);
                        return true;
                    },
                    error: function (err) {
                        widget.html("<b>Error retreiving link page</b>");
                        if (typeof callback == 'function')
                            callback(false);
                        return false;
                    }
                });
            };
            return ButtonPage;
        })();
        Widgets.ButtonPage = ButtonPage;
    })(exports.Widgets || (exports.Widgets = {}));
    var Widgets = exports.Widgets;
});
