define(["require", "exports"], function(require, exports) {
    /// <reference path="../../js/typed/bootstrap.d.ts" />
    /// <reference path="../../js/typed/jquery.d.ts" />
    (function (Widgets) {
        var LinkPage = (function () {
            function LinkPage(widgetId, pageId, wellColor) {
                this.widgetId = widgetId;
                this.pageId = pageId;
                this.wellColor = wellColor;
                this.rootUrl = "";
                var root = document.getElementById("ApplicationRoot");
                if (typeof root != 'undefined' && root != null)
                    this.rootUrl = root.href;
            }
            LinkPage.prototype.run = function (callback) {
                var widgetDomId = "#dailyEZ-com-link-page-widget" + this.widgetId;
                var widget = $(widgetDomId);
                widget.html("<img src='" + this.rootUrl + "widgets/weather/images/loading.gif'/>");
                $.ajax({
                    type: "POST",
                    data: {
                        pageID: this.pageId,
                        wellColor: this.wellColor
                    },
                    url: this.rootUrl + 'widgets/LinkPage/GetLinkWidgetWorker.ashx',
                    success: function (response) {
                        widget.html(response);
                        $(widgetDomId + " a").tooltip();
                        widget.trigger("widget-loaded");
                        if (typeof callback == 'function')
                            callback(true);
                    },
                    error: function (err) {
                        widget.html("<b>Error retreiving link page</b>");
                        if (typeof callback == 'function')
                            callback(false);
                    }
                });
            };
            return LinkPage;
        })();
        Widgets.LinkPage = LinkPage;
    })(exports.Widgets || (exports.Widgets = {}));
    var Widgets = exports.Widgets;
});
