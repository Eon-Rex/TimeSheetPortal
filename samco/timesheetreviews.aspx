<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="timesheetreviews.aspx.cs" Inherits="samco.CBol.timesheetreviews" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=10.0,initial-scale=1.0" />
    <title>Timesheet for Reviews</title>
    <link href="css/blackberry1.css" rel="stylesheet"/>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
     <script type="text/javascript">
$(window).load(function() {
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
        .navbar-custom {
            color: white;
            background-color: #01204f;
            position:fixed;
            min-width:100%;
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
        <div class="navbar">
                    <nav class="navbar navbar-inverse navbar-custom" style="background-color:#041049;border-style:none;font-family:Arial">
                        <div class="container">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                </button>
                                <a class="navbar-brand " style="color: #ffffff;" href="Home.aspx">My Timesheet Dashboard
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
        <div class="container">
            <div class="rowtimesheet11dash">
                <a href="timesheetbymatter.aspx">
                <div class="rowtimesheet11dash1"><img src="images/imgtop.jpg" border="0px" /></div>
                <div class="rowtimesheet11dash2">Timesheet By Matter</div></a>
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet11dash">
                <a href="timesheetbytimekeeper.aspx">
                <div class="rowtimesheet11dash1"><img src="images/imgtop.jpg" border="0px" /></div>
                <div class="rowtimesheet11dash2">Timesheet By TimeKeeper</div></a>
            </div>
        </div> 
        <div class="loader"></div>
        <div class="container">
            <div class="rowtimesheet11">
                <div class="rowtimesheet50">
                    <div class="rowtimesheet501saver">
                        <a href="Mytimesheetdash.aspx">
                     <img src="images/backbtn.jpg" border="0px" /></a>
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
