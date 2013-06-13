<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="DailyEZ.Web.widgets.Mobile.Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>Search</title>
    <% if (HttpContext.Current.IsDebuggingEnabled) { %>
        <script src="../../javascript/01-utility.debug.js" type="text/javascript"></script>
        <script src="../../javascript/02-loadCssFile.debug.js" type="text/javascript"></script>
        <script src="../../javascript/03-runSearch.debug.js" type="text/javascript"></script>
        <script src="../../javascript/04-frontPageNews.debug.js" type="text/javascript"></script>
        <script src="../../javascript/05-mainPage.debug.js" type="text/javascript"></script>
        <script src="../../javascript/06-trackPage.debug.js" type="text/javascript"></script>
        <link rel="stylesheet" type="text/css" href="../../css/import.css"/>
    <% }else{ %>
        <script src="http://static.dailyez.com/client/combined.<%= Version %>.min.js" type="text/javascript"></script>
        <link rel="stylesheet" type="text/css" <%=CssFile %> />
    <% } %>
    <link rel="Shortcut Icon" href="http://thedailyez.com/images/icons/daily_ez.ico" />
    <style type="text/css">
        /*Header Content Extra Literals */
        /********************************/
        /********************************/
        #menuBar
        {
            <asp:Literal ID="menuBarExtraStyle" runat="server"/>
        }
        
        #tableCommitmentRow
        {
        	<asp:Literal ID="litCommitmentRowExtraStyle" runat="server"/> 	
        }
        #mainTitle
        {
        	<asp:Literal ID="litMainTitleExtraStyle" runat="server"/>
        }
        .tabButton
        {
        	<asp:Literal ID="litTabButtonExtraStyle" runat="server"/>
        }
        /********************************/   
        #searchHeader
        {
        	font-family:Arial;
        	font-weight:bold;
        	font-size:14pt;
        	clear:both;
        	margin-top:20px;
        	margin-bottom:0px;
        	width:100%;
        }
        
        #searchResultscontainer .leftColumnContent
        {
        	width:700px;
        	overflow:hidden;
        }
        #searchResultsContainer .rightColumnContent
        {
        	width:400px;
        	border:none;
        }
        .spanLink
        {
        	color:Blue;
        	cursor:pointer;
        }
        .popupPanel
        {
        	position:absolute; 
        	display:none; 
        	width:325px; 
        	height:120px;
        	background-color: rgb(230, 237, 255); 
        	border:solid 1px gray; 
        	border-top:none; 
        	line-height:20px; 
        	padding-top:5px; 
        	font-family:Arial; 
        	font-size:10pt; 
        	padding-left:10px;
        }
        .popupPanel h3
        {
        	margin-bottom:0px;
        	margin-top:0px;
        }
        .rightColumnContent
        {
        /*	border:1px solid black;
        	background-color: rgb(230, 237, 255); */
        }
        #searchResultsContainer .rightColumnContent
        {
        	margin-top:-2px;
        	background-color: #E6EDFF;
        	border-left:1px solid black;
        	border-right:1px solid black;
        	border-bottom:1px solid black;
        	margin-right:2px;
        	width:403px;       	
        }
        .noBorder
        {
        	border:none;
        }
    </style>
    <script type="text/javascript">
        var gHomePage = "<asp:Literal ID="litUserFriendlyURL" runat="server"/>";

        var customGoogleSearch = null;
        function DailyEZDirectory()
        {
            <asp:Literal ID="litDailyEZDirectoryJavascript" runat="server"/>
        }
        function EZRevisit()
        {
            window.open('EZRevisit.aspx', '_self');
        }
        if (BrowserDetect.browser == "Explorer")
        {
            if (BrowserDetect.version == 7)
            {
                loadStyleSheet('css/ie7styles.css');
            }     
        }
        function findLocal()
        {
            window.open('<%=findLocal %>', '_blank');
        }

        function runGoogSearch()
        {
            customGoogleSearch.draw('searchResultsContainer');
            customGoogleSearch.execute(document.getElementById('siteSearchBox').value);
            $('#htmHeader').html("Search Results for '" + document.getElementById('siteSearchBox').value + "'");
        }
        function getParameterByName(name)
        {
          name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
          var regexS = "[\\?&]" + name + "=([^&#]*)";
          var regex = new RegExp(regexS);
          var results = regex.exec(window.location.href);
          if(results == null)
            return "";
          else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
        }



        <asp:Literal ID="litTabControlJavascript" runat="server"/>
    </script>
     <script type="text/javascript" src="http://www.google.com/jsapi"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script> 
    <script type="text/javascript" src="fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script type="text/javascript">
        //load jQuery, jQueryUI, and GoogleWebElements-News
        google.load("elements", "1", { packages: ["newsshow"] });

        google.load("jqueryui", "1.7.2");
        google.load('search', '1');
        //set main onLoad function - located in onLoad.js


        $(document).ready(function () {
            //init google search object
            customGoogleSearch = new google.search.CustomSearchControl('012735483916704295968:wrw7givo-um');
            //make all the links in the preview open in new window
            $(".divAd").find('a').attr({ target: "_blank" });
            //take borders off ie graphics
            $(".divAd").find('img').addClass("noBorder");
            $(".divAd").find('a').addClass("noUnderline");

            //replace each link to point AdsClick.aspx?placementID=placement&url=url
            $('.divAd a').each(function () {
                $(this).attr('href', '../AdsClick.aspx?placementID=' + $(this).parents('.divAd').attr('id').replace('placementID', '') + '&url=' + encodeURI($(this).attr('href')));
            });
            $("#siteSearchBox").keyup(function (event) {

                if (event.keyCode == 13) {
                    $("#bSiteSearch").click();
                }
            });

            if (getParameterByName('googleSearch') == 'true') {
                customGoogleSearch.draw('searchResultsContainer');
                customGoogleSearch.execute(getParameterByName('q'));
                document.getElementById('siteSearchBox').value = getParameterByName('q');
            }

        });


        </script>
</head>
<body>
    <div id="page">
        <div id="contentContainer">
        <br /><a style="color:Blue; text-decoration:none; font-size:12pt;" href="javascript:history.go(-1);">&lt;&lt; Back</a>
            <div id='searchHeader'>
            <table style='width:100%; margin-bottom:0px;'>
                <tr>
                    <td style='text-align:left;'><span id='htmHeader'><asp:Literal ID="litSearchHeader" runat="server" /></span></td>
                </tr>
            </table>
               
            </div>
            <div id='searchResultsContainer'>
                <asp:Literal ID="litSearchResults" runat="server" />

                <div class='clearContent'></div>
            </div>
            <div class='clearContent'></div>
        </div>
    </div>
    <div id="makeHomePage" class='popupPanel'></div>
    <div id="makeBookmark" class='popupPanel'></div>
   
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
