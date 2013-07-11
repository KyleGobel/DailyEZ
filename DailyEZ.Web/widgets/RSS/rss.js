define(["require", "exports"], function(require, exports) {
    /// <reference path="../../js/typed/jquery.d.ts" />
    (function (Widgets) {
        var Rss = (function () {
            function Rss(feedUrl, rssId, wellColor, title) {
                this.feedUrl = feedUrl;
                this.rssId = rssId;
                this.wellColor = wellColor;
                this.title = title;
                this.rootUrl = "";
                var root = document.getElementById("ApplicationRoot");
                if (typeof root != 'undefined' && root != null)
                    this.rootUrl = root.href;
            }
            Rss.prototype.getPrevNews = function () {
                var widgetDomId = "#dailyEZ-com-rss-widget" + this.rssId;
                var prevButton = $(widgetDomId + "-prev-button");
                var nextButton = $(widgetDomId + "-next-button");
                var widget = $(widgetDomId);

                $.ajax({
                    type: "POST",
                    data: {
                        feedUrl: this.feedUrl,
                        rssID: this.rssId,
                        action: "prev",
                        wellColor: this.wellColor,
                        title: this.title
                    },
                    url: this.rootUrl + "widgets/rss/GetRSSWorker.ashx",
                    success: function (response) {
                        widget.html(response);
                        widget.trigger("widget-refreshed");

                        prevButton.click(this.getPrevNews);
                        nextButton.click(this.getNextNews);
                    },
                    error: function (err) {
                        widget.html("<b>Error retreiving RSS Feed</b>");
                    }
                });
            };

            Rss.prototype.getNextNews = function () {
                var widgetDomId = "#dailyEZ-com-rss-widget" + this.rssId;
                var prevButton = $(widgetDomId + "-prev-button");
                var nextButton = $(widgetDomId + "-next-button");
                var widget = $(widgetDomId);

                $.ajax({
                    type: "POST",
                    data: {
                        feedUrl: this.feedUrl,
                        rssID: this.rssId,
                        wellColor: this.wellColor,
                        action: 'next',
                        title: this.title
                    },
                    url: this.rootUrl + "widgets/rss/GetRSSWorker.ashx",
                    success: function (response) {
                        widget.html(response);
                        widget.trigger("widget-refreshed");

                        prevButton.click(this.getPrevNews);
                        nextButton.click(this.getNextNews);
                    },
                    error: function (err) {
                        widget.html("<b>Error retreiving RSS Feed</b>");
                    }
                });
            };
            Rss.prototype.run = function (callback) {
                var widgetDomId = "#dailyEZ-com-rss-widget" + this.rssId;
                var prevButton = $(widgetDomId + "-prev-button");
                var nextButton = $(widgetDomId + "-next-button");
                var widget = $(widgetDomId);

                widget.html("<img src='" + this.rootUrl + "widgets/weather/images/loading.gif'/>");

                $.ajax({
                    type: "POST",
                    data: {
                        feedUrl: this.feedUrl,
                        rssID: this.rssId,
                        wellColor: this.wellColor,
                        action: 'next',
                        title: this.title
                    },
                    url: this.rootUrl + "widgets/rss/GetRSSWorker.ashx",
                    success: function (response) {
                        widget.html(response);
                        widget.trigger("widget-refreshed");

                        prevButton.click(this.getPrevNews);
                        nextButton.click(this.getNextNews);
                        if (typeof callback == 'function')
                            callback(true);
                    },
                    error: function (err) {
                        widget.html("<b>Error retreiving RSS Feed</b>");
                        if (typeof callback == 'function')
                            callback(false);
                    }
                });
            };
            return Rss;
        })();
        Widgets.Rss = Rss;
    })(exports.Widgets || (exports.Widgets = {}));
    var Widgets = exports.Widgets;
});
