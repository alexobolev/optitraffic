<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultPage.aspx.cs" Inherits="optitraffic.ResultPage" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml" runat="server">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <title>OptiTraffic</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="assets/css/main.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <link href="https://fonts.googleapis.com/css?family=ZCOOL+QingKe+HuangYou" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <!--[if lt IE 9]>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.3/html5shiv.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="wrapper">
        <header class="navbar navbar-inverse navbar-static-top navbar-themed">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="FrontPage.aspx">OptiTraffic</a>
                </div>
            </div>
        </header>
        <div class="content container result">
            <div class="box container">
                <div class="left col-sm-12 col-md-12 col-lg-12">
                    <div class="location">
                        <h2><%= Subject.Name %></h2>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="TrafficBar" class="traffic_bar">
                                <div class="volume" style="<%= GetTrafficBarStyleString() %>"></div>
                            </div>
                            <div class="volume_text">
                                <span><%= GetTrafficLevelString() %></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="static col-sm-12 col-md-6 col-lg-6">
                            <img src="assets/images/graph.jpg" alt="static" />
                            <p>According to www.SOMEAMAZINGDATASOURCE.org</p>
                        </div>
                        <div class="textarea col-sm-12 col-md-6 col-lg-6">
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                        </div>
                    </div>
                </div>
                <div class="right col-sm-12 col-md-4 col-lg-4">

                </div>
                <div class="col-sm-12 col-md-12 col-lg-12 container">
                    <a href="FrontPage.aspx" class="back-btn"><i class="fa fa-chevron-left"></i> Return back</a>
                </div>
            </div>
        </div>
    </div>
    <form id="ResultForm" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
