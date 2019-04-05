<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultPage.aspx.cs" Inherits="optitraffic.ResultPage" %>

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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.3/Chart.bundle.js"></script>

    <!--[if lt IE 9]>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.3/html5shiv.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="wrapper">
        <header class="navbar navbar-inverse navbar-fixed-top navbar-themed">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="FrontPage.aspx"><%= LocaleRes.GetString("SiteTitle") %></a>
                </div>
            </div>
        </header>
        <div class="content result">
            <div class="box container">
                <% if (!IncompleteData)
                    { %>
                <div class="row data-row">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="row location">
                            <div class="col-lg-12">
                                <h2><%= Subject.Name %></h2>
                            </div>
                        </div>
                        <div class="row traffic-level">
                            <div class="col-lg-12">
                                <div id="TrafficBar" class="traffic-bar">
                                    <div class="volume" style="<%= GetTrafficBarStyleString() %>"></div>
                                </div>
                                <div class="volume-text">
                                    <span><%= LocaleRes.GetString(GetTrafficLevelIdentifier()) %></span>
                                </div>
                            </div>
                        </div>
                        <!-- Some very important measurement chart -->
                        <div class="row charts">
                            <div class="col-sm-12 col-md-8 col-lg-8">
                                <div class="chart">
                                    <canvas id="SomeVeryImportantChart" class="chart-body"></canvas>
                                </div>
                                <script>
                                    var ctx = document.getElementById('SomeVeryImportantChart').getContext('2d');
                                    var myChart = new Chart(ctx, {
                                        type: 'line',

                                        // The data for our dataset
                                        data: {
                                            labels: [
                                                '<%= LocaleRes.GetString("ChartPt0") %>',
                                                '<%= LocaleRes.GetString("ChartPt1") %>',
                                                '<%= LocaleRes.GetString("ChartPt2") %>',
                                                '<%= LocaleRes.GetString("ChartPt3") %>',
                                                '<%= LocaleRes.GetString("ChartPt4") %>'
                                            ],
                                            datasets: [{
                                                label: '<%= LocaleRes.GetString("GenericMeasurementName") %>',
                                                fill: false,
                                                borderColor: '#534BAE',
                                                data: <%= GetRandomMeasurementsStr(40, 100) %>
                                                //data: [74, 63, 68, 80, 88]
                                            }]
                                        },
                                        options: {
                                            responsive: true,
                                            maintainAspectRatio: true,
                                            aspectRatio: 1.5,
                                            title: {
                                                display: true,
                                                text: '<%= LocaleRes.GetString("SVIMeasurementName") %>'
                                            },
                                            legend: {
                                                display: false,
                                                position: 'bottom',
                                                labels: {
                                                    padding: 0
                                                }
                                            },
                                            scales: {
                                                scaleLabel: {
                                                    fontSize: 10
                                                },
                                                xAxes: [{
                                                    display: true,
                                                    scaleLabel: {
                                                        display: true
                                                    }
                                                }],
                                                yAxes: [{
                                                    display: true,
                                                    scaleLabel: {
                                                        display: false,
                                                        labelString: 'Value'
                                                    },
                                                    ticks: {
                                                        suggestedMin: 40,
                                                        suggestedMax: 100,
                                                    }
                                                }]
                                            }
                                        }
                                    });
                                </script>
                            </div>
                            <div class="textarea col-sm-12 col-md-4 col-lg-4">
                                <p><%= LocaleRes.GetString("SVIMeasurementDesc") %></p>
                                <p>Usually, the very important measurement in this municipality is high, but it has seen some additional rise lately.</p>
                            </div>
                        </div>

                        <!-- Avg speed chart -->
                        <div class="row charts">
                            <div class="textarea col-sm-12 col-md-4 col-lg-4">
                                <p><%= LocaleRes.GetString("AVGSPDMeasurementDesc") %></p>
                                <p>Usually, the local average speed in this municipality is high, but it has seen some additional rise lately.</p>
                            </div>
                            <div class="col-sm-12 col-md-8 col-lg-8">
                                <div class="chart">
                                    <canvas id="AvgSpeedChart" class="chart-body"></canvas>
                                </div>
                                <script>
                                    var ctx = document.getElementById('AvgSpeedChart').getContext('2d');
                                    var myChart = new Chart(ctx, {
                                        type: 'line',

                                        data: {
                                            labels: [
                                                '<%= LocaleRes.GetString("ChartPt0") %>',
                                                '<%= LocaleRes.GetString("ChartPt1") %>',
                                                '<%= LocaleRes.GetString("ChartPt2") %>',
                                                '<%= LocaleRes.GetString("ChartPt3") %>',
                                                '<%= LocaleRes.GetString("ChartPt4") %>'
                                            ],
                                            datasets: [{
                                                label: '<%= LocaleRes.GetString("AVGSPDMeasurementName") %>',
                                                fill: false,
                                                borderColor: '#FF6659',
                                                data: <%= GetRandomMeasurementsStr(20, 50) %>
                                                //data: [35, 39, 37, 40, 39]
                                            }]
                                        },
                                        options: {
                                            responsive: true,
                                            maintainAspectRatio: true,
                                            aspectRatio: 1.5,
                                            title: {
                                                display: true,
                                                text: '<%= LocaleRes.GetString("AVGSPDMeasurementName") %>'
                                            },
                                            legend: {
                                                display: false,
                                                position: 'bottom',
                                                labels: {
                                                    padding: 0
                                                }
                                            },
                                            scales: {
                                                scaleLabel: {
                                                    fontSize: 10
                                                },
                                                xAxes: [{
                                                    display: true,
                                                    scaleLabel: {
                                                        display: true
                                                    }
                                                }],
                                                yAxes: [{
                                                    display: true,
                                                    scaleLabel: {
                                                        display: false,
                                                        labelString: 'Value'
                                                    },
                                                    ticks: {
                                                        suggestedMin: 20,
                                                        suggestedMax: 50,
                                                    }
                                                }]
                                            }
                                        }
                                    });
                                </script>
                            </div>
                        </div>
                    </div>
                </div>
                <% }
                    else
                    { %>
                <div class="row error-row">
                    <div class="col-lg-12">
                        <div class="alert alert-danger" role="alert">
                            <%= LocaleRes.GetString("ErrGeneric") %> <% if (ErrorReason.Length != 0)
                                                               { %> <br />Error reason: <%= ErrorReason %> <% } %>
                        </div>
                    </div>
                </div>
                <% } %>
                <div class="row backlink-row">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <a href="FrontPage.aspx" class="back-btn"><i class="fa fa-chevron-left"></i><%= LocaleRes.GetString("ReturnToMainPage") %></a>
                    </div>
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
