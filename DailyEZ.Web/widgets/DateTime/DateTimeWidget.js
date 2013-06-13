define(["jquery"], function ($) {
    var updateTime = function () {
        var currentTime = new Date();

        var hours = currentTime.getHours();
        var minutes = currentTime.getMinutes();
        var numDay = currentTime.getDay();

        var dayString = "";
        switch (numDay) {
            case 0:
                dayString = "Sunday"; break;
            case 1:
                dayString = "Monday"; break;
            case 2:
                dayString = "Tuesday"; break;
            case 3:
                dayString = "Wednesday"; break;
            case 4:
                dayString = "Thursday"; break;
            case 5:
                dayString = "Friday"; break;
            case 6:
                dayString = "Saturday"; break;
        }

        if (minutes < 10) {
            minutes = "0" + minutes;
        }

        if (hours > 11) {
            minutes = minutes + " PM";
            if (hours != 12)
                hours = hours - 12;
        }
        else {
            minutes = minutes + " AM";
        }
        $("#spanDate").html(hours + ":" + minutes + "&nbsp; - &nbsp;" + dayString + " " + (currentTime.getMonth() + 1) + "/" + currentTime.getDate() + "/" + currentTime.getFullYear());
        setTimeout(updateTime, 10000);
    };

    $(document).ready(function () {
        //8693
        var widget = $("#dailyEZ-com-date-time");
        if (widget)
            widget.html("<h5><a href=\"[%CALENDAR-ID%]-Calendar\">Calendar</a>&nbsp;&nbsp; <span id=\"spanDate\"></span></h5>");
        updateTime();

    });
});

