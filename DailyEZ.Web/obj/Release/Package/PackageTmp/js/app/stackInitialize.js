//depends on jquery plugin jquey.ba-bbq.js
define("stackInitialize",
    ["jquery", "utility"],
    function ($, utility) {
        
    var loadMiddleStack = function () {
        //extract the hash
        var hash = $.param.fragment();
        //get the stackID from the hash

        var match = hash.match("mId=([^&]*)");
        if (match == null)
            return;

        //check to see if the middle stack is the same as it was before
        //if (Modernizr.sessionstorage) {
        //    if (sessionStorage.middleStack) {
        //        if (sessionStorage.middleStack == match[1])
        //            return;
        //        else {
        //            sessionStorage.middleStack = match[1];
        //        }
        //    } else {
        //        sessionStorage.middleStack = match[1];
        //    }
        //} else {
        //    //if we don't have session storage, use the jquery solution, this will first try what we tried above, and when that doesn't
        //    //work it will use cookies..hopefully..don't know how to test this
        //    alert("Your browser doesn't support some of the features required for the DailyEZ.  Consider upgrading.  http://www.browsehappy.com\n\nFeature: sessionStorage");
            
        //}
        var stackID = match[1];

        //attempt to find a button with this stack ID on it so we can get the height
        var stackHeight = $('button[stack-id="' + stackID + '"]').attr("stack-height");
        

        //SET DEFAULT HEIGHT TO THE STACK FRAME IF WE CANT FIND IT ON PAGE
        if (!utility.isNumber(stackHeight))
            stackHeight = "1050";

        //save it as our cookie so when people come back to page it displays right
        utility.setCookie("middleStackOverride", stackID, null);
        //load up the correct page if stackID > 0
        
        if ($("#middleStackFrame").length) {
            if (stackID > 0) {
                $("#middleStackFrame").attr('src', 'StackFrame.aspx?stackID=' + stackID);
                $("#middleStackFrame").css("height", stackHeight + "px");
            }
        } else {
            if (stackID > 0)
                $("#middleStack").html("<iframe class='stackFrame' id='middleStackFrame' frameborder='0' scrolling='no' style='height:" + stackHeight + "px' src='StackFrame.aspx?stackID=" + stackID + "'></iframe>");
        }
        

    };
    
    var init = function () {
        loadMiddleStack();
        $(window).bind('hashchange', loadMiddleStack);
    };
    return {
        init: init
    };
});