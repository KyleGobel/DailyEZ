function loadUp() {

    $("#accChangeComm > li > div").click(function () {

        if (false == $(this).next().is(':visible')) {
            $('#accChangeComm ul').slideUp(300);
            $("#accChangeComm > li > div").css('background-image', "url(http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_default.png), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");
            $(this).css('background-image', "url('http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_open.png'), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");

        } else {
            $(this).css('background-image', "url(http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_default.png), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");
        }

        $(this).next().slideToggle(300);
    });
    /*$("#accordion > li > div").click(function () {

        if (false == $(this).next().is(':visible')) {
            var mainParent = $(this).parent();

            if (false == mainParent.parent('#accordion ul').is(':visible'))
                $('#accordion ul').slideUp(300);
            $("#accordion > li > div").css('background-image', "url(http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_default.png), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");
            $(this).css('background-image', "url('http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_open.png'), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");

        } else {
            $(this).css('background-image', "url(http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_default.png), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");
        }

        $(this).next().slideToggle(300);
    });
    */

    $('#AdHelper').offset({ top: $('.divAd').offset().top - 13, left: $('.divAd').offset().left });

    $(".divAd").find('a').attr({ target: "_blank", rel: "nofollow" });

    //take borders off ie ad graphics
    $(".divAd").find('img').addClass("noBorder");
    $(".divAd").find('a').addClass("noUnderline");

    $.ajax({
        type: "POST",
        url: "http://static.dailyez.com/ajax/recordPageView.aspx",
        data: {}
    });
}
$(document).ready(loadUp);