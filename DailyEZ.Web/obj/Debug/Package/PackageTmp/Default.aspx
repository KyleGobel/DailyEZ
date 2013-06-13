<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DailyEZ.Web.Default" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content runat="server" ID="scriptsContent" ContentPlaceHolderID="headContent">

 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div id="leftStack" class="span4"><%=LeftStack() %> </div>
	
    <div id="middleStack" class="span4"><%=MiddleStack() %> </div>
	
    <div class="span4"><%=RightStack() %> </div>
</asp:Content>
