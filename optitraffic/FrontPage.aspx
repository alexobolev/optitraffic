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
        <header class="navbar navbar-inverse navbar-static-top navbar-themed">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">OptiTraffic</a>
                </div>
            </div>
        </header>
        <div class="parent container">
            <div class="row">
                <div class="heading">
                    <span class="heading__text">What's the traffic load in...</span>
                </div>
                <div class="input-group search-group" role="group">
                    <input type="text" class="form-control" placeholder="Search for a location..." name="search" />
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>
                    </span>
                </div>
            </div>
        </div>
        <footer>
            <div class="container language-selection">
                <div class="dropup">
                    <button id="langDropdown" class="btn dropdown-toggle dropbtn" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Choose language 
                        <i class="fa fa-globe"></i>
                    </button>
                    <ul class="dropdown-menu" aria-labelledby="langDropdown">
                        <li><a href="#">Suomi</a></li>
                        <li><a href="#">English</a></li>
                    </ul>
                </div>
            </div>
        </footer>
    </div>
    <form id="SearchForm" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
