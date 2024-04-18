<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="timesheetbytimekeeper.aspx.cs" Inherits="samco.CBol.timesheetbytimekeeper" Async="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" id="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=10.0,initial-scale=1.0" />
    <title>TimeSheet By Timekeeper</title>
    <link href="css/blackberry1.css" rel="stylesheet" />
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
    background-color:#01204f;
	background: url('images/page-loader.gif') 50% 50% no-repeat rgb(249,249,249);
}
    </style>
    <style type="text/css">
        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #FFFFBF;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id*=gvbidhistory] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/ishrar.css" rel="stylesheet" />
    <script src="js/needish.js"></script>
    <script src="js/ishrar.js"></script>
    <style type="text/css">
        .navbar-custom {
            color: white;
            background-color: #01204f;
        }

        .dropdown-color {
            background-color: whitesmoke;
        }
    </style>
    <style type="text/css">
        .ul {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 250px;
            background-color: white;
            text-decoration: none;
            width: 100%;
        }

        li a {
            display: block;
            color: #000;
            padding: 8px 0 8px 16px;
            text-decoration: none;
        }

            /* Change the link color on hover #555 */
            li a:hover {
                background-color: darkgrey;
                color: white;
                text-decoration: none;
            }

        .accounts {
        }

        .Appointments {
        }

        .Cases {
        }

        .Leads {
        }
    </style>
    <link rel="shortcut icon" href="images/Imgcon.ico" type="image/x-icon" />
</head>
<body style=" font-family:Arial;">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="navbar navbar-fixed-top">
            <nav class="navbar navbar-inverse navbar-custom" style="background-color:#041049;border-style:none;font-family:Arial">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand " style="color: #ffffff; " href="Home.aspx">My Timesheet Dashboard
                        </a>
                    </div>
                    <div class="collapse navbar-collapse  " id="myNavbar">
                        <ul class="nav navbar-nav">
                            <li class="dropdown">
                                <a class="dropdown-toggle" data-toggle="dropdown" href="#"><span class=""></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#"></a></li>
                                    <li><a href="#"></a></li>
                                    <li><a href="#"></a></li>
                                </ul>
                            </li>
                            <li><a href="Mytimesheetdash.aspx" style="color: #ffffff;font-size:15px">Home</a></li>
                           <li><a href="#" style="color:#ffffff; padding:0px;overflow:hidden;"><asp:LinkButton ID="lbkSignOut" OnClick="lbkSignOut_Click" CssClass="btnlogin" style="color:#ffffff;font-size:15px" runat="server">Sign Out</asp:LinkButton></a></li>
                         
                        </ul>
                    </div>
                </div>
            </nav>
        </div>
        <div class="container" style="padding-top: 70px;">
            <div class="timesheehboot">
                Timesheet By TimeKeeper<br />
                <asp:Label ID="Label4" runat="server"></asp:Label>
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
               From Date
            </div>
            <div class="rowtimesheet11">
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtworkdate" TargetControlID="txtworkdate"></asp:CalendarExtender>
                <asp:TextBox ID="txtworkdate" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
        </div>
         <div class="container">
            <div class="rowtimesheet1">
                To Date
            </div>
            <div class="rowtimesheet11">
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtTodate" TargetControlID="txtTodate"></asp:CalendarExtender>
                
                <asp:TextBox ID="txtTodate" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                Search TimeKeeper
            </div>
            <div class="rowtimesheet11">              
                <asp:DropDownList ID="DropDownList1" CssClass="selectpicker" Style="background-color: #eeeeee" data-live-search="true" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="loader"></div>
        <div class="container" style="padding-top:20px;">
            <asp:GridView ID="gvbidhistory" Width="100%" runat="server" AutoGenerateColumns="False"
                OnRowDataBound="gvbidhistory_RowDataBound"
                OnPageIndexChanging="gvbidhistory_PageIndexChanging" BackColor="White" AllowPaging="false"
                PageSize="10" ForeColor="Black" Font-Size="10px" OnRowCommand="gvbidhistory_RowCommand" OnSelectedIndexChanged="gvbidhistory_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="ProjectNo" HeaderStyle-Width="25%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("transationId") %>'
                                CommandName="Status" ForeColor="Gray" Text='<%# Eval("projId")%>'></asp:LinkButton>
                            <asp:Label ID="lblCountry" Visible="false" ForeColor="White" runat="server" Text='<%# Eval("transationId") %>'></asp:Label>                            
                        </ItemTemplate>
                        <HeaderStyle Width="25%" Font-Names="Times New Roman" CssClass="text-center" HorizontalAlign="Center" />
                        <ItemStyle Width="25%" Font-Names="Times New Roman" CssClass="text-center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hours" HeaderStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("hour") %><bold>.</bold>
                            <%# Eval("minutes") %>
                        </ItemTemplate>
                        <HeaderStyle Width="25%" Font-Names="Times New Roman" CssClass="text-center" HorizontalAlign="Center" />
                        <ItemStyle Width="25%" Font-Names="Times New Roman" CssClass="text-center" />
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="LineProperty" DataField="projLineProtId">
                        <HeaderStyle Width="25%" Font-Names="Times New Roman" HorizontalAlign="Center" CssClass="text-center" />
                        <ItemStyle Width="25%" Font-Names="Times New Roman" HorizontalAlign="Center" CssClass="text-center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Category" DataField="categoryName">
                        <HeaderStyle Width="25%" Font-Names="Times New Roman" HorizontalAlign="Center" CssClass="text-center" />
                        <ItemStyle Width="25%" Font-Names="Times New Roman" HorizontalAlign="Center" CssClass="text-center" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle BackColor="#000000" ForeColor="White" Height="25px" Wrap="False" />
                <RowStyle Wrap="False" />
                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" Font-Bold="True"
                    Font-Size="12pt" ForeColor="White" />
                <RowStyle Height="20px" />
                <AlternatingRowStyle Height="20px" />
            </asp:GridView>

        </div>       
        <div class="container">
            <div class="rowtimesheet11">
                <div class="rowtimesheet50">
                    <div class="rowtimesheet501save">
                        <a href="timesheetreviews.aspx">
                            <img src="images/backbtn.jpg" border="0px" /></a>
                    </div>
                    <div class="rowtimesheet502">
                        <a href="#">
                            <asp:ImageButton ID="imgsave" runat="server" ImageUrl="images/search.jpg"
                                alt="save" border="0" OnClick="imgsave_Click" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <script src="js/bootstrap.js"></script>
        <script src="js/script1.js"></script>
        <script src="js/site.js"></script>
    </form>
</body>
</html>
