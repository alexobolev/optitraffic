﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontPage.aspx.cs" Inherits="optitraffic.FrontPage" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml" runat="server">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <title><%= LocaleRes.GetString("SiteTitle") %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="assets/css/main.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <link href="https://fonts.googleapis.com/css?family=ZCOOL+QingKe+HuangYou" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/js-cookie@2.2.0/src/js.cookie.min.js"></script>

    <script src="assets/SearchDropdown.js"></script>
    <script src="assets/SetCookies.js"></script>

    <!--[if lt IE 9]>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.3/html5shiv.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="wrapper">
        <header class="navbar navbar-inverse navbar-fixed-top navbar-themed">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span class="sr-only"><%= LocaleRes.GetString("MobileNavToggle") %></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="FrontPage.aspx"><%= LocaleRes.GetString("SiteTitle") %></a>
                </div>
            </div>
        </header>
        <div class="parent container">
            <div class="row" runat="server">
                <div class="col-lg-12">
                    <div class="heading">
                        <%= LocaleRes.GetString("UserPrompt") %>
                    </div>
                    <form id="SearchForm" action="ResultPage.aspx" method="get" runat="server" autocomplete="off">
                        <div class="search-box">
                            <div class="input-group search-group" role="group">
                                <asp:TextBox id="LocationName" class="form-control" placeholder="" name="LocationName" runat="server"></asp:TextBox>
                                <asp:TextBox id="LocationCode" style="display: none; " name="LocationName" runat="server"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton id="SubmitBtn" class="btn btn-default" type="submit" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                                </span>
                            </div>
                            <div class="search-results" id="searchOptions" runat="server" style="display: none;">
                                <ul>
                                    <%--<li data-val="Porvoo">Porvoo</li>
                                    <li data-val="Espoo">Espoo</li>
                                    <li data-val="Vantaa">Vantaa</li>
                                    <li data-val="Helsinki">Helsinki</li>--%>
                                </ul>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <footer>
            <div class="container">
                <div class="row">
                    <div class="col-md-3">

                    </div>
                    <div class="col-md-6 credits">
                        <span class="authors">
                            <%= LocaleRes.GetString("ProjectDesc") %>
                        </span>
                        <span class="year">
                            2019
                        </span>
                    </div>
                    <div class="col-md-3 language">
                        <div class="dropup">
                            <button id="langDropdown" class="btn dropdown-toggle dropbtn" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <%= LocaleRes.GetString("ChooseLang") %>
                                <i class="fa fa-globe"></i>
                            </button>
                            <ul id="LangDropdownOptions" class="dropdown-menu dropdown-lang-options" aria-labelledby="langDropdown">
                                <li><a href="#" data-locale="fi">Suomi</a></li>
                                <li><a href="#" data-locale="en">English</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    </div>
</body>
</html>
