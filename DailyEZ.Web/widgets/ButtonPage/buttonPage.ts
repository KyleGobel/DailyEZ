/// <reference path="../../js/typed/bootstrap.d.ts" />
/// <reference path="../../js/typed/jquery.d.ts" />
export module Widgets {
    export class ButtonPage {
        private rootUrl: string;
        constructor(public pageId: number, public favIcons: boolean, public buttonColor: string, public widgetId: number) {
            if (typeof favIcons == 'undefined')
                favIcons = false;

            var root = <any>document.getElementById("ApplicationRoot");
            if (typeof root != 'undefined' && root != null)
                this.rootUrl = root.href;

        }

        public run(callback: any): any {
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
        }
    }
}