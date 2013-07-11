/// <reference path="../../js/typed/bootstrap.d.ts" />
/// <reference path="../../js/typed/jquery.d.ts" />
export module Widgets {
    export class LinkPage {
        private rootUrl: string = "";
        constructor(public widgetId: number, public pageId: number, public wellColor: string) {
            var root = <any>document.getElementById("ApplicationRoot");
            if (typeof root != 'undefined' && root != null)
                this.rootUrl = root.href;
        }

        public run(callback): any {
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
        }
    }
}