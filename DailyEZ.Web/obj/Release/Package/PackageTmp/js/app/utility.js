define("utility", ["jquery"], {
    
    //usingIE
    //------------
    //returns true if we're using interent explorer else false
    
    usingIE: function () {
        var browserDetect = {
            init: function () {
                this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
            },
            searchString: function (data) {
                for (var i = 0; i < data.length; i++) {
                    var dataString = data[i].string;
                    var dataProp = data[i].prop;
                    this.versionSearchString = data[i].versionSearch || data[i].identity;
                    if (dataString) {
                        if (dataString.indexOf(data[i].subString) != -1)
                            return data[i].identity;
                    }
                    else if (dataProp)
                        return data[i].identity;
                }
            },
            dataBrowser: [
                {
                    string: navigator.userAgent,
                    subString: "MSIE",
                    identity: "Explorer",
                    versionSearch: "MSIE"
                }]
        };
        browserDetect.init();
        if (browserDetect.browser == "Explorer")
            return true;
        else {
            return false;
        }
    },
    
    //getCookie
    //------------
    //gets a cookie by name from current domain and returns the value
    
    getCookie: function (cName) {
        if (document.cookie.length > 0) {
            var cStart = document.cookie.indexOf(cName + "=");
            if (cStart != -1) {
                cStart = cStart + cName.length + 1;
                var cEnd = document.cookie.indexOf(";", cStart);
                if (cEnd == -1) cEnd = document.cookie.length;
                return unescape(document.cookie.substring(cStart, cEnd));
            }
        }
        return "";
    },
    

    //isNumber
    //-------------
    //simple utility function to tell us if the input is a number or not
    
    isNumber: function (nguid) {
        return !isNaN(parseFloat(nguid)) && isFinite(nguid);
    },
    
    //setCookie
    //-------------
    //params: 
    //   --name: name of the cookie to save
    //   --value: value you want to save into the cookie
    //   --expiredays: how many days to store the cookie
    
    setCookie: function (c_name, value, expiredays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + expiredays);
        document.cookie = c_name + "=" + escape(value) +
    ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
    },
    
    //getBaseURL
    //-------------
    //Gets the URL that is in the address bar
    
    getBaseUrl: function () {
        var port = 0;
        if (location.port > 0)
        port = ":" + location.port;
        return location.protocol + "//" + location.hostname + port + "/";
    },
});