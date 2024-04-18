<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="dropdownlist.aspx.cs" Inherits="samco.dropdownlist" Async="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" id="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=10.0,initial-scale=1.0" />
    <title>New TimeSheet</title>
    <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
 <script type="text/javascript">
     $(document).ready(function () {
         $.ajax({
             type: "POST",
             contentType: "application/json; charset=utf-8",

             url: "dropdownlist.aspx/PopulateDropDownList",
             data: "{}",
             dataType: "json",

             success: function (result) {
                 $('#DropDownList5').empty();
                 $('#DropDownList5').append("<option value='0'>--Select--</option>");
                 $.each(result.d, function (key, value) {
                     $("#DropDownList5").append($("<option></option>").val(value.LineName1).html(value.Value3));
                 });
             },
             error: function ajaxError(result) {
                 alert(result.status + ' : ' + result.statusText);
             }
         });
     });
    </script>   
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
                        <a class="navbar-brand " style="color: #ffffff;">My Timesheet Dashboard
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
                            <li><a href="#" style="color: #ffffff; padding: 0px; overflow: hidden;">
                                <asp:LinkButton ID="lbkSignOut" OnClick="lbkSignOut_Click" CssClass="btnlogin" Style="color: #ffffff;font-size:15px" runat="server">Sign Out</asp:LinkButton></a></li>
                            
                        </ul>
                    </div>
                </div>
            </nav>
        </div>
        <div class="container" style="padding-top: 70px;">
            
            <div class="timesheehboot">
                New TimeSheet
            <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Label ID="Label2" runat="server"></asp:Label>

            </div>
        </div>       

        <div class="container">
            <div class="rowtimesheet1">
                Work Date
            </div>
            <div class="rowtimesheet11">
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtworkdate" TargetControlID="txtworkdate"></asp:CalendarExtender>
                <asp:TextBox ID="txtworkdate" CssClass="form-control" runat="server"></asp:TextBox>                
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                TimeKeeper
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox ID="txttimekeeper" CssClass="form-control" Style="background-color: #eeeeee" runat="server"></asp:TextBox>
            </div>
        </div>


        
        <div class="container">
            <div class="rowtimesheet11ddshan">
                Project Id
            </div>
            <div class="rowtimesheet11ddkhan" style="text-align: center;">
                
                        <asp:DropDownList ID="DropDownList1" CssClass="selectpicker" Style="background-color: #eeeeee" data-live-search="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                Customer Name
            </div>
            <div class="rowtimesheet11">

                <asp:TextBox ID="customername" CssClass="form-control" Style="background-color: #eeeeee" runat="server"></asp:TextBox>

            </div>
        </div>
        
        <div class="container">
            <div class="rowtimesheet11ddshan">
                Category
            </div>
            <div class="rowtimesheet11ddkhan">
                <asp:DropDownList ID="DropDownList3" CssClass="selectpicker" Style="background-color: #eeeeee" data-live-search="true" runat="server"></asp:DropDownList>
            </div>
        </div>
        
        <div class="container">
            <div class="rowtimesheet11ddshan">
                Line Property
            </div>
            <div class="rowtimesheet11ddkhan">
                <asp:DropDownList ID="DropDownList4" CssClass="selectpicker" data-live-search="true" Style="background-color: #eeeeee" runat="server"></asp:DropDownList>
            </div>
        </div>
        
        <div class="container">
            <div class="rowtimesheet50">
                <div class="rowtimesheet501">
                    <div class="rowtimesheet12">
                        Hours
                    </div>
                    <div class="rowtimesheet112">

                        <asp:DropDownList ID="txthours" CssClass="selectpicker" data-live-search="true" Style="background-color: #eeeeee" runat="server">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                        </asp:DropDownList>
                        
                    </div>
                </div>
                <div class="rowtimesheet502">
                    <div class="rowtimesheet12">
                        Minutes
                    </div>
                    <div class="rowtimesheet112">
                        
                        <asp:DropDownList ID="DropDownList2" CssClass="selectpicker" data-live-search="true" Style="background-color: #eeeeee" runat="server">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                            <asp:ListItem>32</asp:ListItem>
                            <asp:ListItem>33</asp:ListItem>
                            <asp:ListItem>34</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>36</asp:ListItem>
                            <asp:ListItem>37</asp:ListItem>
                            <asp:ListItem>38</asp:ListItem>
                            <asp:ListItem>39</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>41</asp:ListItem>
                            <asp:ListItem>42</asp:ListItem>
                            <asp:ListItem>43</asp:ListItem>
                            <asp:ListItem>44</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>46</asp:ListItem>
                            <asp:ListItem>47</asp:ListItem>
                            <asp:ListItem>48</asp:ListItem>
                            <asp:ListItem>49</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>51</asp:ListItem>
                            <asp:ListItem>52</asp:ListItem>
                            <asp:ListItem>53</asp:ListItem>
                            <asp:ListItem>54</asp:ListItem>
                            <asp:ListItem>55</asp:ListItem>
                            <asp:ListItem>56</asp:ListItem>
                            <asp:ListItem>57</asp:ListItem>
                            <asp:ListItem>58</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                TimeKeeper NPG Code
            </div>
            <div class="rowtimesheet11">
               
                <asp:DropDownList ID="DropDownList5"  runat="server" CssClass="selectpicker" data-live-search="true" Style="background-color: #eeeeee"></asp:DropDownList>
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                TimeKeeper NPG Section Code
            </div>
            <div class="rowtimesheet11">
                
                <asp:DropDownList ID="DropDownList6" CssClass="selectpicker" data-live-search="true" Style="background-color: #eeeeee" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="container">
            <div class="rowtimesheet1">
                Description
            </div>
            <div class="rowtimesheet11">
                <asp:TextBox CssClass="form-control" ID="txtdis" Height="70px" TextMode="MultiLine" Style="background-color: #eeeeee" runat="server"></asp:TextBox>
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
                            <asp:ImageButton ID="imgsave" runat="server" ImageUrl="images/savebtn.jpg"
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
