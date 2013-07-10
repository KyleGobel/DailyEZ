define(["require", "exports"], function(require, exports) {
    //This is used to grab query string values from a javascript file
    //pass the filename (without .js) into the constructor, then use
    //GetValue(name) to find the query string values
    (function (Utility) {
        var QueryStringHelper = (function () {
            function QueryStringHelper(fileName) {
                this.fileName = fileName;
                this.names = [];
                this.values = [];
                this.getQueryStringNameAndValues();
            }
            // GetValue(queryStringName: string) => number;
            QueryStringHelper.prototype.GetValue = function (queryStringName) {
                var i = this.names.indexOf(queryStringName);
                if (i == -1)
                    return undefined; else {
                    if (this.values.length > i)
                        return this.values[i];
                }
            };
            QueryStringHelper.prototype.getQueryStringNameAndValues = function () {
                var doc = document;
                var scriptQuery = '';

                for (var scripts = doc.scripts, i = scripts.length; --i >= 0; ) {
                    var script = scripts[i];
                    var match = script.src.match("^[^?#]*\\/" + this.fileName + "\\.js(\\?[^#]*)?(?:#.*)?$");
                    if (match) {
                        scriptQuery = match[1] || '';

                        // Remove the script from the DOM so that multiple runs at least run
                        // multiple times even if parameter sets are interpreted in reverse
                        // order.
                        script.parentNode.removeChild(script);
                        break;
                    }
                }

                var that = this;
                scriptQuery.replace(/[?&]([^&=]+)=([^&]+)/g, function (_, name, value) {
                    value = decodeURIComponent(value);
                    name = decodeURIComponent(name);
                    that.names.push(name);
                    that.values.push(value);
                    return "";
                });
            };
            return QueryStringHelper;
        })();
        Utility.QueryStringHelper = QueryStringHelper;
    })(exports.Utility || (exports.Utility = {}));
    var Utility = exports.Utility;
});
