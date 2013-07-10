define(["require", "exports"], function(require, exports) {
    /// <reference path="../../js/typed/jquery.d.ts" />
    (function (Widgets) {
        var AdGroup = (function () {
            function AdGroup(adGroup, widgetId, autoRotate) {
                this.adGroup = adGroup;
                this.widgetId = widgetId;
                this.autoRotate = autoRotate;
                this.rootUrl = "";
                var root = document.getElementById("ApplicationRoot");
                if (typeof autoRotate == 'undefined')
                    this.autoRotate = false;
                if (typeof root != 'undefined' && root != null)
                    this.rootUrl = root.href;
            }
            AdGroup.prototype.run = function (callback) {
                if (typeof this.rootUrl == 'undefined')
                    this.rootUrl = "";
                var widget = $("#dailyEZ-com-ad-group" + this.widgetId);
                widget.html("<img src='" + this.rootUrl + "widgets/weather/images/loading.gif'/>");

                $.ajax({
                    type: "POST",
                    data: {
                        adGroup: this.adGroup,
                        autoRotate: this.autoRotate
                    },
                    url: this.rootUrl + "widgets/AdGroup/AdGroupHtml.ashx",
                    success: function (response) {
                        widget.html(response);
                        widget.trigger("widget-loaded");
                        $(".divAd").css("margin", "auto");
                        $(".divAd").find('a').attr({ target: "_blank", rel: "nofollow" });

                        //take borders off ie ad graphics
                        $(".divAd").find('img').css("border", "none");
                        $(".divAd").find('a').css("text-decoration", "none");
                        $(".divAd").css("line-height", "normal");
                        $(".divAd").find('table').css("border-collapse", "seperate");
                        if (typeof callback == 'function')
                            callback(true);
                    },
                    error: function (err) {
                        widget.html("<b>Error retreiving ad group</b>");
                        if (typeof callback == 'function')
                            callback(false);
                        return false;
                    }
                });
            };
            return AdGroup;
        })();
        Widgets.AdGroup = AdGroup;
    })(exports.Widgets || (exports.Widgets = {}));
    var Widgets = exports.Widgets;
});
