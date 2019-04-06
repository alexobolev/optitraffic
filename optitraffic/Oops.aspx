<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Oops.aspx.cs" Inherits="optitraffic.Oops" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml" runat="server">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <title>Error - OptiTraffic</title>
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
        <header class="navbar navbar-inverse navbar-fixed-top navbar-themed">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="FrontPage.aspx">OptiTraffic</a>
                </div>
            </div>
        </header>
        <div class="content result">
            <div class="box container">
                <div class="row error-row">
                    <div class="col-lg-12">
                        <div class="alert alert-danger" role="alert">
                            <strong>System error happened.</strong> Contact administrators or try again later.
                        </div>
                    </div>
                </div>
                <div class="row backlink-row">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <a href="FrontPage.aspx" class="back-btn"><i class="fa fa-chevron-left"></i>Back to front page</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
