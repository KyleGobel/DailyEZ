<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StackFrame.aspx.cs" Inherits="DailyEZ.Web.StackFrame" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Stack Frame</title>
    <link href="//netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-combined.min.css" rel="stylesheet" />
    
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">           
            <!--js files-->
            <%:
                Scripts.Render(
                     "~/bundles/jquery",
                     "~/bundles/jsextlibs",
                     "~/js/vendor/require.js",
                     "~/bundles/jsapplibs"
                 ) 
             %>
        </asp:PlaceHolder>
    
    <script type="text/javascript">
        define('jquery', [], function () { return this.jQuery; });
        define('toastr', [], function () { return this.toastr; });
    </script>
    <style type="text/css">
        html,body {
            margin:0;
            padding:0;
            width:364px;
        }
    </style>
    <base target="_parent" />
</head>
<body>
   <%=GetStack() %>
</body>
</html>
