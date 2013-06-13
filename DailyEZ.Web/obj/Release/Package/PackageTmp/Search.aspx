<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="DailyEZ.Web.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
         <br /><a style="color:Blue; text-decoration:none; font-size:12pt;" href="javascript:history.go(-1);">&lt;&lt; Back</a>
            <div id='searchHeader'>
           <h3><asp:Literal ID="litSearchHeader" runat="server" /></h3>
          
               
            </div>
            <div id='searchResultsContainer' style="line-height:30px;">
                <asp:Literal ID="litSearchResults" runat="server" />

                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
            <br/><br/><br/><br/>
</asp:Content>
