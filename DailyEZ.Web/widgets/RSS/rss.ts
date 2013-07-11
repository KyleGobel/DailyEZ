/// <reference path="../../js/typed/jquery.d.ts" />
export module Widgets {
    export class Rss {
        private rootUrl: string = "";

        constructor(public feedUrl:string, public rssId:number, public wellColor:string, public title:string) {
            var root = <any>document.getElementById("ApplicationRoot");
            if (typeof root != 'undefined' && root != null)
                this.rootUrl = root.href;
        }

        public getPrevNews() {
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
        }
 
        public getNextNews() {
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
        }
        public run(callback: any) {
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
        }

    }
}