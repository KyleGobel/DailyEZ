/// <reference path="../../js/typed/jquery.d.ts" />
/// <reference path="../../js/typed/jquery.bbq.d.ts" />

//i guess by default this is attached to the window object
//so it's automatically available to anything that just icludes it

declare var $: JQueryStatic;
export module Widgets {
    export class ButtonStackSelector {
        private rootUrl: string = "";
        constructor(public stacks: string, public title: string, public widgetId: number, public buttonColor: string) {
            var root = <any>document.getElementById("ApplicationRoot")
            if (root != 'undefined' && root != null)
                this.rootUrl = root.href;

        }

        public run(callback: any): any
        {
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
                    buttonColor: this.buttonColor,
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
        }
    
    }
}
