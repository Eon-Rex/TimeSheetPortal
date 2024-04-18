<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="samco.CBol._default" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" id="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=10.0,initial-scale=1.0" />
    <title>SAM & Co.</title>
    <link href="css/blackberry.css" rel="stylesheet" />    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="Bootstrap/bootstrap.min.css" rel="stylesheet" />

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
         .input-lg {
            height: 46px;
            padding: 10px 16px;
            font-size: 18px;
            line-height: 1.3333333;
            border-radius: 6px;
        }

    </style>

    <link rel="shortcut icon" href="images/Imgcon.ico" type="image/x-icon" />

     <script type="text/javascript">

         function NumberAssign() {
             var AddNo = parseInt(document.getElementById('<%=lblNo1.ClientID%>').innerText) + parseInt(document.getElementById('<%=lblNo2.ClientID%>').innerText);
               var txtTotal = document.getElementById("txtTotal").value;

               if (AddNo == txtTotal) {
                   return true;
               }
               else {
                   alert('Invalid Captcha');
                   return false;
               }
           }

    </script>
</head>
<body style="background-repeat: no-repeat; background-size: cover">
    <form id="form1" runat="server">

      <div id="loginModal" class="modal show" tabindex="-1" role="dialog" aria-hidden="true">
             <div class="modal-dialog">
                <div class="modal-content">

                     <div class="modal-header" style="background-color: #041049">
                            <table>
                                <tr>
                                    <td style="width:89%; vertical-align: middle; text-align:left;">
                                      <span style="color:#9B6839;text-align: left; font-family: Baskerville, 'Palatino Linotype', Palatino, 'Century Schoolbook L', 'Times New Roman', serif; font-size: 23px;"> Timesheet Portal </span>
                                    </td>
                                <td style="width:30%">
                                    <asp:Image ID="imgIcon" runat="server" ImageUrl="~/images/SAMLogo.png" style="width:144px;height: 71px;" />
                          </td>
                                    <td style="width:20%">
                              &nbsp;&nbsp;&nbsp;&nbsp;
                          </td>
                      </tr>
                    </table>
                     </div>

                     <div class="modal-body">
                        <div class="form col-md-12 center-block">
                            <div class="form-group">
                                 <p style="margin:30px"></p>
                                 <asp:TextBox ID="txtusername" placeholder="UserName"  CssClass="form-control input-lg" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="txtpass" TextMode="Password" placeholder="Password" CssClass="form-control input-lg" runat="server"></asp:TextBox>
                           </div>

                            <div class="form-group">
                                      <table >
                                        <tr>
                                         <td>&nbsp;
                                             <asp:Label ID="lblNo1" style="color:#041049;font-size: 20px;font-style:normal;" runat="server" ></asp:Label>  
                                            <span style="color:#041049;font-size:20px;font-style:normal;"> + </span>   
                                            <asp:Label ID="lblNo2" style="color:#041049;font-size: 20px;font-style:normal;" runat="server" ></asp:Label> 
                                            <span style="color:#041049;font-size:20px;font-style:normal;"> = </span> &nbsp;
                                            <asp:TextBox ID="txtTotal" placeholder="Captcha Value"  CssClass="form-control input-lg" style="width: 62%;display: table-row;"  runat="server"></asp:TextBox>
                                            </td>
                                             
                                    </tr>
                                   
                                </table>
                           </div>

                            <div class="form-group">
                                 <asp:Button ID="BtnLogin" runat="server" Text="Log In" style="background-color:#9B6839;border-style:none" CssClass="btn btn-primary btn-lg btn-block" OnClick="BtnLogin_Click" OnClientClick="return NumberAssign();" />
                            </div>
                            </div>
                        </div>

                     <div class="modal-footer" style="border-top: 0px solid #fff;">
                               <div class="col-md-12">
                                  <asp:Label ID="LblMessage" runat="server"></asp:Label>
                            </div>
                           </div>
                </div>
             </div>
        </div>
    </form>
</body>
    <noscript>
    <div style="display:block; border: 1px solid black;color:red; text-align:center;">
    You don't have javascript enabled.Please Enable Before Login.
    </div>

  <META  CONTENT="0;URL=Login.aspx">
</noscript>
</html>
