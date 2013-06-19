<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Community.aspx.cs" Inherits="DailyEZ.Web.Community" %>

<asp:Content runat="server" ContentPlaceHolderID="headContent">
    <asp:Literal runat="server" ID="litMeta"></asp:Literal>
    <asp:Literal runat="server" ID="litCanonicalLink"></asp:Literal>
    
    <style type="text/css">
        .header h2 {
            line-height:30px;
        }
        .span4 {
            line-height:30px;
        }
        .span8 {
            line-height:30px;
        }
        .span4 a {
            font-size:16px;
        }
    </style>
    <script type="text/javascript">
        
        $(document).ready(function () {
            $("#captchaText").bind("keyup", verifyCaptcha);
            $("#ButtonSubmitWebsite").attr("disabled", "disabled");
        });
        var verifyCaptcha = function () {
            var value = $("#captchaText").val();
            
            $.ajax({
                type: "POST",
                data: { textValue: value },
                url: "captcha/VerifyCaptcha.aspx",
                success: function (response) {
                    if (response == "true") {
                        $("#ButtonSubmitWebsite").removeAttr("disabled");
                    }
                    else {
                        $("#ButtonSubmitWebsite").attr("disabled", "disabled");
                    }
                }
            });
        }
    </script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
      <br /><a id="backButton" href="javascript:history.go(-1);">&lt;&lt; Back</a>
    <br/>
    <div id="pageHeader">
        <h1 style="font-size:18pt; display:inline"><asp:Literal ID='litPageHeader' runat='server' /></h1> - <a data-toggle="modal" style="cursor:pointer;" data-target="#myModal">Recommend a Website</a>
    </div>
    <br/>
    <asp:Literal runat="server" ID="litExtraHtml"></asp:Literal> 
    <div class="clearfix"></div>
                
                
    <asp:Literal ID="litLinkContent" runat="server" />

    <!-- google_ad_section_end -->
    <div id="adContainer" class="span3">
    <asp:Literal ID="litAds" runat="server" />
    </div>
    <div class="clearContent"></div>
    <asp:Literal ID="litFooterHtml" runat="server"></asp:Literal>
    
    
    
 
 
<div class="modal hide fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-header">
    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
    <h3 id="myModalLabel">Recommend a Website</h3>
    
  </div>
  <div class="modal-body">
       <form id="form1" runat="server">
    <div>
    <table style="width:500px">
    <tr>
    <td>Website Name</td><td>
        <asp:TextBox ID="tbName" Width="300px" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Website URL</td><td>
        <asp:TextBox ID="tbURL" Width="300px" runat="server">http://</asp:TextBox>

        </td>
    </tr>
        <tr>
        <td colspan="2" style="line-height:36px;">
           
        <asp:Literal ID="litResult" runat="server"></asp:Literal>
            Enter the numbers displayed in the green box in the textbox below<br/>
        <img src="captcha/Image.aspx" style="margin-top:-10px" alt="Enter the Number"/>
            <input type="text" id="captchaText"/>
            <br/>
            <br/>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="margin-top:10px;">
            <asp:Button ID="ButtonSubmitWebsite" ClientIDMode="Static" runat="server" onclick="ButtonSubmitWebsiteClick" 
                Text="Submit Website" CssClass="btn btn-primary btn-large" Width="200px" />
        </td>
    </tr>

    </table>
    </div>
    </form>
  </div>
</div>

</asp:Content>
