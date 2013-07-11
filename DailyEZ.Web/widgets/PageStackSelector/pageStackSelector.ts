/// <reference path="../../js/typed/jquery.d.ts" />
export module Widgets {
    export class PageStackSelector {
        private rootUrl: string = "";
        constructor(public url: string, public title: string, public widgetId: number, public buttonColor: string) {
            var root = <any>document.getElementById("ApplicationRoot");
            if (typeof root != 'undefined' && root != null)
                this.rootUrl = root.href;
           
        }
        public run(callback: any): any {
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
        }
    }
}