<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRequire.aspx.cs" Inherits="DailyEZ.Web.TestRequire1" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
          <asp:PlaceHolder ID="PlaceHolder1" runat="server">
             <%: 
             Scripts.Render(
                 "~/bundles/jquery",
                 "~/bundles/jsextlibs",
                 "~/js/vendor/require.js",
                 "~/bundles/jsapplibs",
                 "~/js/main.js"
             ) 
             %>
        </asp:PlaceHolder>
</head>
<body>
       <div id="top-bar">
            <div id="bar-wrapper">
                <div id="visit-often">Visit this DailyEZ often?  Make this DailyEZ your homepage.</div>
                <div id="sure-button">Sure</div>
                <div id="no-thanks-button">No Thanks</div>
            </div>
            <div id="x-button">X&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        </div>
    
    
    <div id="leftStack">
        
        <!--nomodify--><div style="margin-top:10px"></div>
        <script>require(["../widgets/ButtonStackSelector/ButtonStackSelectorWidget.ashx?title=&widgetID=1&buttonColor=btn-info&stacks=206,6,13,178,12,210,"]);</script>
        <div id="dailyEZ-com-button-stack-selector-widget1"></div>
 
      <!--nomodify--><div style="margin-top:8px"></div>
        <script>require(["../widgets/Weather/WeatherWidget.ashx?zipcode=53716&extendedWeatherURL=&stackID=175"]);</script>
        <div id="dailyEZ-com-weather-widget"></div>
        
        <script>require(["../widgets/StackSelector/StackSelectorWidget.ashx?stacks=208,&widgetID=2&title=Monona%2c+WI&buttonColor=btn-replace-oj"]);</script>
        <div id="dailyEZ-com-stack-selector-widget2"></div>
        
        <script>require(["../widgets/ButtonStackSelector/ButtonStackSelectorWidget.ashx?title=Monona%2c+WI+Link+Menus&widgetID=3&buttonColor=btn-info&stacks=2,209,3,169,7,165,"]);</script>
        <div id="dailyEZ-com-button-stack-selector-widget3"></div>
        
        <script>require(["../widgets/ButtonStackSelector/ButtonStackSelectorWidget.ashx?title=South+Central+Wisconsin+Link+Menus&widgetID=4&buttonColor=btn-info&stacks=221,219,213,214,212,215,216,217,"]);</script>
        <div id="dailyEZ-com-button-stack-selector-widget4"></div>
        <script>require(["../widgets/ButtonStackSelector/ButtonStackSelectorWidget.ashx?title=Best+of+the+Web+Link+Menus&widgetID=5&buttonColor=btn-info&stacks=218,220,222,223,"]);</script>
        <div id="dailyEZ-com-button-stack-selector-widget5"></div><!--nomodify--><div style="margin-top:10px"></div>
        <script>require(["../widgets/AdGroup/AdGroupWidget.ashx?adGroup=16&widgetID=5&autoRotate="]);</script>
        <div id="dailyEZ-com-ad-group5"></div>
        
        
        

    </div>
   <div id="middleStack" class="span4">
       <!--nomodify--><div style="margin-top:15px"></div>
       <script>require(["../widgets/ButtonPage/ButtonPageWidget.ashx?pageID=21313&widgetID=21&buttonColor=&favIcons=True"]);</script>
       <div id="dailyEZ-com-button-page-widget21"></div>
       <!--nomodify--><div style="margin-top:10px"></div>
<script>require(["../widgets/AdGroup/AdGroupWidget.ashx?adGroup=&widgetID=22&autoRotate=false"]);</script>
       <div id="dailyEZ-com-ad-group22"></div>
       <!--nomodify--><div style="margin-top:-15px"></div>
<script>require(["../widgets/ButtonPage/ButtonPageWidget.ashx?pageID=21311&widgetID=23&buttonColor=&favIcons="]);</script>
       <div id="dailyEZ-com-button-page-widget23"></div><script>    require(["../widgets/ButtonPage/ButtonPageWidget.ashx?pageID=21309&widgetID=24&buttonColor=&favIcons=True"]);</script>
       <div id="dailyEZ-com-button-page-widget24"></div><script>    require(["../widgets/ButtonPage/ButtonPageWidget.ashx?pageID=21310&widgetID=25&buttonColor=&favIcons=True"]);</script>
       <div id="dailyEZ-com-button-page-widget25"></div><script>    require(["../widgets/ButtonPage/ButtonPageWidget.ashx?pageID=21314&widgetID=26&buttonColor=&favIcons=True"]);</script>
       <div id="dailyEZ-com-button-page-widget26"></div> 
   </div>
	

    



</body>
</html>
