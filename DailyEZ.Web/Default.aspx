<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DailyEZ.Web.Default" %>

<asp:Content runat="server" ID="scriptsContent" ContentPlaceHolderID="headContent">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div id="leftStack" class="span4"><%=DefaultViewModel.LeftStackHtml() %> </div>
	
    <div id="middleStack" class="span4"><%=DefaultViewModel.MiddleStackHtml() %> </div>
	
    <div class="span4"><%=DefaultViewModel.RightStackHtml() %> </div>
</asp:Content>
