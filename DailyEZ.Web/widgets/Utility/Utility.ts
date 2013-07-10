//This is used to grab query string values from a javascript file
//pass the filename (without .js) into the constructor, then use
//GetValue(name) to find the query string values
export module Utility {
    export class QueryStringHelper {
        names: string[] = [];
        values: string[] = [];
        constructor(public fileName: string) {
            this.getQueryStringNameAndValues();
        }
        // GetValue(queryStringName: string) => number;

        GetValue(queryStringName: string): string {
            var i = this.names.indexOf(queryStringName);
            if (i == -1)
                return undefined;
            else {
                if (this.values.length > i)
                    return this.values[i];
            }
        }
        getQueryStringNameAndValues(): void {
            var doc: HTMLDocument = document;
            var scriptQuery: string = '';

            // Look for the <script> node that loads this script to get its parameters.
            // This starts looking at the end instead of just considering the last
            // because deferred and async scripts run out of order.
            // If the script is loaded twice, then this will run in reverse order.
            // take from Google Prettify
            for (var scripts = doc.scripts, i = scripts.length; --i >= 0;) {
                var script = <HTMLScriptElement>scripts[i];
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
            scriptQuery.replace(
                /[?&]([^&=]+)=([^&]+)/g,
                function (_, name, value) {
                    value = decodeURIComponent(value);
                    name = decodeURIComponent(name);
                    that.names.push(name);
                    that.values.push(value);
                    return "";
                });
        }
    }
}
