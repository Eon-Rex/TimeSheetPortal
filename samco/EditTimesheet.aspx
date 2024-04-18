<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTimesheet.aspx.cs" Inherits="samco.CBol.EditTimesheet" Async ="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=10.0,initial-scale=1.0" />
    <title>Edit TimeSheet</title>
    <link href="css/blackberry1.css" rel="stylesheet" />
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
        <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
        <div class="navbar navbar-fixed-top">
            <nav class="navbar navbar-inverse navbar-custom" style="background-color:#041049;border-style:none;font-family:Arial">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand " style="color: #ffffff;">Edit MyTimesheet

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
        <div class="container" style="padding-top:70px;">
            <div class="timesheehboot">
                <asp:Label ID="Label2" runat="server"></asp:Label>
                <br />                
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Label ID="Label4" runat="server"></asp:Label>
            </div>
        </div>
       
        <div class="container">
            <div class="rowtimesheet1">
                Work Date <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
            </div>
            <div class="rowtimesheet11">
               <asp:TextBox ID="txtworkdate" class="form-control" runat="server"></asp:TextBox>               
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                TimeKeeper <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="txttimekeeper" class="form-control"  runat="server"></asp:TextBox>
            </div>
        </div>        
        <div class="container">
            <div class="rowtimesheet11ddshan">
                Project Id <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span> 
            </div>
            <div class="rowtimesheet11ddkhan" style="text-align: center;">               
               <asp:TextBox ID="DropDownList1" class="form-control"  runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                Customer Name <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="customername" class="form-control"  runat="server"></asp:TextBox>
            </div>
        </div>
        
        <div class="container">
            <div class="rowtimesheet11ddshan">
                Category <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
            </div>
            <div class="rowtimesheet11ddkhan">
                <asp:TextBox ID="DropDownList3" class="form-control"  runat="server"></asp:TextBox>
            </div>
        </div>
        
        <div class="container">
            <div class="rowtimesheet11ddshan">
                Line Property <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
            </div>
            <div class="rowtimesheet11ddkhan">
                <asp:TextBox ID="DropDownList4" class="form-control"  runat="server"></asp:TextBox>
            </div>
        </div>        
        
        <div class="container">
            <div class="rowtimesheet50">
                <div class="rowtimesheet501">
                    <div class="rowtimesheet12">
                        Hours <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
                    </div>
                    <div class="rowtimesheet112">
               <asp:TextBox ID="txthours" class="form-control"  runat="server"></asp:TextBox>                        
                    </div>
                </div>
                <div class="rowtimesheet502">
                    <div class="rowtimesheet12">
                        Minutes <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
                    </div>
                    <div class="rowtimesheet112">                        
                       <asp:TextBox ID="DropDownList2" class="form-control"  runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <%--<div class="rowtimesheet1">
                TimeKeeper NPG Code <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
            </div>--%>
            <div class="rowtimesheet11">               
               <asp:TextBox ID="npgcode" Visible="false" class="form-control"  runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="container">
            <%--<%--<div class="rowtimesheet1">
                TimeKeeper NPG Section Code <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
            </div>--%>
            <div class="rowtimesheet11">                
                <asp:TextBox ID="npgseccode" Visible="false" class="form-control"  runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                Description <span style="color: #ff0000; font-weight: bolder; font-size: large;">*</span>
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox CssClass="form-control"  ID="txtdis" Height="70px" TextMode="MultiLine" runat="server"></asp:TextBox>
            </div>
        </div>
         <div class="container">
            <div class="rowtimesheet1">
                
            </div>
            <div class="rowtimesheet11">
                <asp:Label ID="Label3" CssClass="btnreject" runat="server"></asp:Label>
            </div>
        </div> 
        
         <div class="container">
            <div class="rowtimesheet11">
                <div class="rowtimesheet50">
                    <div class="rowtimesheet501save">
                        <a href="Mytimesheetdash.aspx">
                            <img src="images/backbtn.jpg" border="0px" /></a>
                    </div>
                    <div class="rowtimesheet502">
                        <a href="#">                           
                            <asp:ImageButton ID="imgsave" runat="server" ImageUrl="images/editbtn.jpg"
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
