<%@ Page Language="C#" AutoEventWireup="true" EnableSessionState="ReadOnly" CodeBehind="ResultPage.aspx.cs" Inherits="optitraffic.ResultPage" %>

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
    <!--[if IE]>
        <div id="IEAlert" class="ie-alert alert bg-danger alert-dismissable">
            <button id="CloseIEAlert" type="button" class="close" aria-label="Close" data-dismiss="alert"><span aria-hidden="true">&times;</span></button>
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <h2><%= LocaleRes.GetString("IEAlertHeader") %></h2>
                        <p><%= LocaleRes.GetString("IEAlertP1") %></p>
                        <p><%= LocaleRes.GetString("IEAlertP2") %></p>
                        <p><%= LocaleRes.GetString("IEAlertP3") %></p>
                    </div>
                </div>
            </div>
        </div>
    <![endif]-->
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
                        <!-- Traffic volume chart -->
                        <div class="row charts">
                            <div class="col-sm-12 col-md-8 col-lg-8">
                                <div class="chart">
                                    <canvas id="TrafficVolumeChart" class="chart-body"></canvas>
                                </div>
                                <script>
                                    var ctx = document.getElementById('TrafficVolumeChart').getContext('2d');
                                    var myChart = new Chart(ctx, {
                                        type: 'line',

                                        // The data for our dataset
                                        data: {
                                            labels: [
                                                '<%= LocaleRes.GetString("ChartPt3") %>',
                                                '<%= LocaleRes.GetString("ChartPt4") %>'
                                            ],
                                            datasets: [{
                                                label: '<%= LocaleRes.GetString("TRFVLMMeasurementName") %>',
                                                fill: false,
                                                borderColor: '#534BAE',
                                                data: <%= GetVehiclesMeasurementsStr() %>
                                            }]
                                        },
                                        options: {
                                            responsive: true,
                                            maintainAspectRatio: true,
                                            aspectRatio: 1.5,
                                            title: {
                                                display: true,
                                                text: '<%= LocaleRes.GetString("TRFVLMMeasurementName") %> (<%= LocaleRes.GetString("TRFVLMMeasurementUnit") %>)'
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
                                                        suggestedMin: <%= GetVehiclesMinBound() %>,
                                                        suggestedMax: <%= GetVehiclesMaxBound() %>,
                                                    }
                                                }]
                                            }
                                        }
                                    });
                                </script>
                            </div>
                            <div class="textarea col-sm-12 col-md-4 col-lg-4">
                                <p><%= LocaleRes.GetString("TRFVLMMeasurementDesc") %></p>
                            </div>
                        </div>

                        <!-- Avg speed chart -->
                        <div class="row charts">
                            <div class="textarea col-sm-12 col-md-4 col-lg-4">
                                <p><%= LocaleRes.GetString("AVGSPDMeasurementDesc") %></p>
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
                                                '<%= LocaleRes.GetString("ChartPt3") %>',
                                                '<%= LocaleRes.GetString("ChartPt4") %>'
                                            ],
                                            datasets: [{
                                                label: '<%= LocaleRes.GetString("AVGSPDMeasurementName") %>',
                                                fill: false,
                                                borderColor: '#FF6659',
                                                data: <%= GetAvgSpeedMeasurementsStr() %>
                                            }]
                                        },
                                        options: {
                                            responsive: true,
                                            maintainAspectRatio: true,
                                            aspectRatio: 1.5,
                                            title: {
                                                display: true,
                                                text: '<%= LocaleRes.GetString("AVGSPDMeasurementName") %> (<%= LocaleRes.GetString("AVGSPDMeasurementUnit") %>)'
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
                                                        suggestedMin: <%= GetAvgSpeedMinBound() %>,
                                                        suggestedMax: <%= GetAvgSpeedMaxBound() %>,
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
                                                               { %> <br /><%= LocaleRes.GetString("ErrReason") %>: <%= ErrorReason %> <% } %>
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
