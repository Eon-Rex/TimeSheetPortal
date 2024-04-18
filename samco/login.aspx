<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="samco.login" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=10.0,initial-scale=1.0" />
    <title>SAM & Co.</title>
    <link href="css/blackberry.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <style type="text/css">
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: #01204f;
            background: url('images/page-loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
</head>
<body style=" font-family:Arial;">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
        <div id="outerlogin">
            <div class="topheaderout">
                <div class="topheadtxtb">
                    SAM & Co.
                </div>
            </div>
            <div class="firsttxtrow">
                <asp:TextBox ID="txtusername" placeholder="Enter UserName" CssClass="txttextbox" runat="server"></asp:TextBox>
            </div>
            <div class="firsttxtrow1">
                <asp:TextBox ID="txtpass" TextMode="Password" placeholder="Enter Password" CssClass="txttextbox" runat="server"></asp:TextBox>
            </div>
            <div class="firsttxtrow1">
                <asp:Label ID="LblMessage" runat="server"></asp:Label>
            </div>
            <div class="loader"></div>           
            <div class="firsttxtrow2th">
                <div class="button_area">                                           
                        <asp:LinkButton ID="LinkButton1" class="button"  runat="server" OnClick="LinkButton1_Click">Log In</asp:LinkButton>                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>

