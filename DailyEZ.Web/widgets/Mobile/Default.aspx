<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DailyEZ.Web.widgets.Mobile.Default" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="DailyEZ.Web.Code" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title></title>
  
     <asp:PlaceHolder ID="PlaceHolder1" runat="server">
            <!--modernizr-->
            <%: Scripts.Render("~/bundles/modernizr") %>
                       
            
            <!--js files-->
            <%:
                Scripts.Render(
                     "~/bundles/jquery"
                 )
             %>
   
        </asp:PlaceHolder>
    
    <asp:Literal runat="server" ID="litIcons"></asp:Literal>
    <link rel="stylesheet" type="text/css" href="http://static.dailyez.com/css/redmond/jquery-ui-1.7.2.custom.css" />
    <script type="text/javascript" src="script.js"></script>
      <script type="text/javascript">
          function getParameterByName(name) {
              name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
              var regexS = "[\\?&]" + name + "=([^&#]*)";
              var regex = new RegExp(regexS);
              var results = regex.exec(window.location.search);
              if (results == null)
                  return "";
              else
                  return decodeURIComponent(results[1].replace(/\+/g, " "));
          }
          function getWeather() {
              $.ajax({
                  type: "GET",
                  url: "../../ajax/getWeather.aspx?zipcode=" + getParameterByName("zipcode")
              }).done(function (msg) {
                  $("#weatherTR").html(msg);
              }).fail(function (e) {
              });
          }
          function isNumber(value) {
            if ((undefined === value) || (null === value)) {
                return false;
            }

            if (typeof value == 'number') {
                return true;
            }
            return !isNaN(value - 0);
        }
          function getHtmlForLink(linkObject) {
              var html = "";

              if (isNumber(linkObject.URL)) {
                  //check to see if it's a heading
                  if (linkObject.URL.length == 0) {
                      html = "<li><span style=\"font-weight:bold;\">" + linkObject.Title + "</span></li>";
                      return html;
                  }
                  html = "<li><div pageID=\"" + linkObject.URL + "\" onclick=\"divClickHandler(" + linkObject.URL +")\">" + linkObject.Title + "</div><ul pageID=\"" + linkObject.URL + "\" id=\"accordion\"><li id=\"list"+ linkObject.ID + "\"></li></ul>";
                  return html;
              } else {
                   var target = "_blank";
                  if (linkObject.URL.toLowerCase().indexOf("megdl.com") > 0)
                      target = "_self";
                  else {
                      target = "_blank";
                  }
                 
                  var style = "";
                  var extra = "";
                if (linkObject.Title.toLowerCase().indexOf("[content]") > 0)
                    style += "font-weight:normal;";
                if (linkObject.Title.toLowerCase().indexOf("[bold]") > 0 || linkObject.Title.toLowerCase().indexOf("*bold*") > 0)
                    style += "font-weight:bold;";
                if (linkObject.Title.toLowerCase().indexOf("[break]") > 0)
                    extra += "<br/>";
                  if (linkObject.Title.toLowerCase().indexOf("[button]") > 0) {
                      linkObject.Title = linkObject.Title.replace("[button]", "");
                      html = "<li><div onclick=\"specialButtonHandler('" + linkObject.URL + "')\">" + linkObject.Title + "</div></li>";
                      return html;
                  }
                linkObject.Title =
                    linkObject.Title.replace("*BOLD*", "").replace("[CONTENT]", "").replace("[BOLD]", "").replace(
                        "*bold*", "")
                        .replace("[content]", "").replace("[bold]", "").replace("[BREAK]", "").replace("[break]", "");
                  html = "<li><a style=\"" + style + "\" href=\"" + linkObject.URL + "\" target=\"" + target +"\">" + linkObject.Title + "<a/></li>" + extra;
                  return html;
              }
          }

           function divClickHandler(pageId) {

               var mainDiv = $("div[pageID=" + pageId + "]");
               var accordion = $("ul[pageID=" + pageId + "]");
               


                  //if next element is not visible we need to load links and open it
                  if (false == mainDiv.next().is(':visible')) {
                      var mainParent = mainDiv.parent();

                      //if the parents accordion is not visible slide this up
                      //this will slide up all other accordions
                      if (false == mainParent.parent('#accordion ul').is(':visible'))
                          $('#accordion ul').slideUp(300);
                      //set the background images of all divs in the accordion to default closed
                      $("#accordion > li > div").css('background-image', "url(http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_default.png), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");
                      //set this divs accoridon background image to open
                      mainDiv.css('background-image', "url('http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_open.png'), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");

                      loadLinks(accordion, pageId);

                  } else {
                      //this is already open, close it now
                      mainDiv.css('background-image', "url(http://static.dailyez.com/widgets/Mobile/images/aside_accordion_indicator_default.png), -webkit-gradient(linear, 0 0%, 0 100%, from(#F7F7F7), to(#EDEDED))");
                  }

                  mainDiv.next().slideToggle(300);
           }
           function specialButtonHandler(url, tar) {
               
               var target = tar;
               if (url.toLowerCase().indexOf("megdl.com") > 0)
                   target = "_self";
                window.open(url, target);
          }
          function loadLinks(elementToAppend, pageId) {
              
              //Set some loading text
              elementToAppend.html("<center><img src=\"http://static.dailyez.com/widgets/Mobile/images/ajax-loader.gif\" alt=\"Loading\"/></center>");
              $.ajax({
                   type: "POST",
                   url: "getLinks.aspx",
                   data: { pageId: pageId }
               }).done(function (msg) {
                   elementToAppend.html("");
                   $.each(msg, function(index, link) {
                       elementToAppend.append(getHtmlForLink(link));
                   });
           
               });
          }
         

          $(document).ready(function () {
              getWeather();
              setInterval(function () {
                  getWeather();
              },
              1000 * 60 * 10);
              $("#c").val(<asp:Literal runat="server" ID="clientIDLiteral"/>);

              //set hidden field to clientId

          });
          
    
      </script>
    <link rel="stylesheet" href="styles.css" type="text/css"/>
    <style type="text/css">
        .ui-widget {
            font-family: Lucida Grande, Lucida Sans, Arial, sans-serif;
            font-size: 10pt;
        }
    </style>
</head>
<body>  
    <div>
        <br/>
        <center>
            <asp:Literal runat="server" ID="Ads"></asp:Literal>
        </center>
    </div>
    <div class="topHeader"><%=MainTitle %></div>
      
        <table style="font-size:10pt;margin:auto;">
            <tr id="weatherTR">
                
            </tr>
            <tr>
                <td> <asp:Literal runat="server" ID="litExtendedWeatherURL"> </asp:Literal></td>
            </tr>
        </table>
    
    <div class="menu" style="margin-top:-15px;">
    <%=ChangeCommunity %>
    </div>
     <div style="text-align:center;margin:auto;margin-top:10px;">
         <form action="Search.aspx" method="GET">
        <input id="q" name="q" class="searchBox" placeholder="search" required="required" type="text"/>
        <input id="c" name="c" type="hidden" value=""/>
        </form>
    </div>
   
    <div class="menu">
        <%=PagesTree %>
    </div>
 <div style="margin-bottom:20px; height:10px;"></div>
    <div id="AdHelper">
       <%if (AdGroup > 0) {  Response.Write("Advertisement"); } %>
    </div>
    
     <div id="regDialog" title="The DailyEZ Internet Directory" style='display:none; font-family:Arial;'>
        <p style='font-weight:bold; margin-top:0px; margin-bottom:0px; font-size:10pt; text-align:center;'>The DailyEZ is FREE for you to use.</p>
        <p style='text-align:center; font-size:10pt;'>Please enter your zipcode below.</p>
        
        <table style='margin:auto'>
        <tr>
            <td>Zip Code</td>
            <td><input type='text' id='tbZip' style='width:60px' /></td>
             <td><input type='button' value='Submit' onclick='javascript:register()'/></td>
        </tr>
        </table>   
    </div>
    <script type="text/javascript">
        var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
        document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
<script type="text/javascript">
    try {
        var pageTracker = _gat._getTracker("<asp:Literal ID="litAnalytics" runat="server"/>");
        pageTracker._trackPageview();
    } catch(err) {}</script>
</body>
</html>

   