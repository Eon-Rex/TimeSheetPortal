<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rejecttimesheet.aspx.cs" Inherits="samco.rejecttimesheet" Async="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=10.0,initial-scale=1.0" />
    <title>Reject TimeSheet</title>
    <link href="css/blackberry1.css" rel="stylesheet" />
     <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <style>
        
        .modal {
            display: none; 
            position: fixed;
            z-index: 1; 
            padding-top: 100px;
            left: 0;
            top: 0;
            width: 100%; 
            height: 100%;
            overflow: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.4); 
        }

        
        .modal-content {
            background-color: #fefefe;
            margin: auto;           
            padding: 5px;            
            width: 80%;
             border-right-color: #ff8800;
    border-right-style: solid;
    border-right-width: 2px;
    border-left-color:#ff8800;
    border-left-style: solid;
    border-left-width: 2px;
    border-bottom-color:#ff8800;
    border-bottom-style: solid;
    border-bottom-width:2px;
    border-top-color:#ff8800;
    border-top-style: solid;
    border-top-width:2px;
        }

        
        .close {
            color: #ff8800;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #ff8800;
                text-decoration: none;
                cursor: pointer;
            }
    </style>
    <link rel="shortcut icon" href="images/Imgcon.ico" type="image/x-icon" />
</head>
<body style=" font-family:Arial;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
        <div class="timesheetheadrow">
           <asp:Label ID="Label2" runat="server"></asp:Label><br />
            <asp:Label ID="Label4" runat="server"></asp:Label>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                Work Date
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="txtworkdate" class="form-control"  runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                TimeKeeper
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="txttimekeeper" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                Project Id
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="DropDownList1" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                Customer Name
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="customername" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                Category
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="DropDownList3" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                Line Property
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="DropDownList4" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet50">
                <div class="rowtimesheet501">
                    <div class="rowtimesheet12">
                        Hours
                    </div>
                    <div class="rowtimesheet112">
                        <asp:TextBox ID="txthours" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="rowtimesheet502">
                    <div class="rowtimesheet12">
                        Minutes
                    </div>
                    <div class="rowtimesheet112">
                        <asp:TextBox ID="DropDownList2" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

            </div>

        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                TimeKeeper NPG Code
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="npgcode" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                TimeKeeper NPG Section Code
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="npgseccode" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="rowtimesheet">
            <div class="rowtimesheet1">
                Description
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox class="form-control" ID="txtdis" Height="70px" TextMode="MultiLine" runat="server"></asp:TextBox>
            </div>
        </div>       
        <div class="rowtimesheet">
            <div class="rowtimesheet11">
                <div class="rowtimesheet50">
                    <div class="rowtimesheet501save">
                        <a href="timesheetreviews.aspx">
                     <img src="images/backbtn.jpg" border="0px" /></a>
                    </div>
                     <div class="rowtimesheet502">
                         <a href="#">
                             <div id="myBtn">
                     <img src="images/rejbtn1.jpg" border="0px" /></div></a>
                    </div>
                </div>                
            </div>
        </div>
        <div id="myModal" class="modal">            
            <div class="modal-content">
                <span class="close">×</span>
                <p style="color:#ff8800;font-size:18px;">Reject Reason</p>
                <div class="firsttxtrow1">
                    <asp:TextBox ID="TextBox2" placeholder="Reason for Rejection" TextMode="MultiLine"  CssClass="txttextboxpop" runat="server"></asp:TextBox>
                </div>
                <div class="firsttxtrow2">
                    <div class="lgtout" style="padding-top: 5px;">
                        <a href="#">                            
                            <asp:Button ID="Button1" class="btnlast" OnClick="Button1_Click"  runat="server"  style="width:100%;background-color:#337ab7;border-radius: 10px; padding-bottom:5px; border:0px;color:#ffffff;cursor:pointer;" Text="Submit" />
                        </a>
                    </div>
                </div>
            </div>
            <script>               
                var modal = document.getElementById('myModal');

                
                var btn = document.getElementById("myBtn");

                
                var span = document.getElementsByClassName("close")[0];

                
                btn.onclick = function () {
                    modal.style.display = "block";
                }

               
                span.onclick = function () {
                    modal.style.display = "none";
                }

                
                window.onclick = function (event) {
                    if (event.target == modal) {
                        modal.style.display = "none";
                    }
                }
            </script>
        </div>
    </form>
</body>
</html>
