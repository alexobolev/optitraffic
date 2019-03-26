<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontPage.aspx.cs" Inherits="optitraffic.FrontPage" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
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
        <div class="header container-fluid">
            <nav class="navbar-static-top">
                <div class="navbar-header col-lg-3 col-md-3 col-sm-3">
                    <a class="logo" href="#">OptiTraffic</a>
                </div>
            </nav>
        </div>
        <div class="content parent container">
            <div class="child">
                <div class="heading">
                    <span class="text_heading">What's the traffic load in...</span>
                </div>
                <div class="btn-group" role="group">
                    <div class="input-group-prepend">
                        <input class="input_location"type="text" placeholder="Search for a location..." name="search" />
                        <a href="ResultPage.aspx" class="search input-group-text" id="btnGroup"><i class="fa fa-search"></i></a>
                    </div>
                </div>
            </div>
        </div>
        <footer>
            <div class="dropdown">
                <div class="dropdown-content" aria-labelledby="btnGroupDrop1">
                    <a href="#">Fin</a>
                    <a href="#">Eng</a>
                </div>
                <button id="btnGroupDrop1" class="dropbtn btn dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Choose language <i class="fa fa-globe"></i>
                </button>

            </div>
        </footer>
    </div>
        <form id="SearchForm" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
