$(document).ready(function () {

    $(document).on("click", function (event) {
        var attr = $(event.target).attr("data-code");
        var clickedOnHint = typeof attr !== typeof undefined && attr !== false;

        if (clickedOnHint) {
            $("#LocationCode").val($(event.target).attr("data-code"));
            $("#LocationName").val($(event.target).attr("data-name"));
            $("#searchOptions").hide();
        } else if ($(event.target).attr("id") != "LocationName") {
            $("#searchOptions").hide();
        }
    });

    $("#SearchForm").on("submit", function (event) {
        if ($("#LocationCode").val() == "") {
            event.preventDefault();
        }
    });

    $("#SearchForm").on("focusin", function (event) {
        if ($("#LocationName").val().length > 0) {
            $("#searchOptions").show();
        }
    });

    //$("#SearchForm").on("focusout", function (event) {
    //    $("#searchOptions").hide();
    //});

    var selectedHintIdx = -1;
    var selectableHints = 0;

    $("#LocationName").on("keydown", function (event) {
        if (event.which == 13) {

            if (selectedHintIdx != -1) {
                $("#searchOptions>ul>li").eq(selectedHintIdx).click();
            }

            $("#SearchForm").submit();
            return false;
        }

        if (selectableHints > 0) {
            if (event.which == 40) {
                event.preventDefault();

                if (selectedHintIdx + 1 < selectableHints) {
                    selectedHintIdx += 1;
                } else {
                    selectedHintIdx = 0;
                }
            } else if (event.which == 38) {
                event.preventDefault();

                if (selectedHintIdx - 1 >= 0) {
                    selectedHintIdx -= 1;
                } else {
                    selectedHintIdx = selectableHints - 1;
                }
            }

            $("#searchOptions>ul>li").removeClass("sel");
            $("#searchOptions>ul>li").eq(selectedHintIdx).addClass("sel");

        } else {
            selectedHintIdx = -1;
        }

    });

    $("#LocationName").on("input", function (event) {
        $("#LocationCode").val("");

        var inVal = $("#LocationName").val();


        $("#searchOptions").show();

        if (inVal.length < 2) {
            selectableHints = 0;
            $("#searchOptions>ul").empty();
            $("#searchOptionsHint").show();
            return;
        }

        $("#searchOptions>ul").empty();

        setTimeout(function () {
            event.stopImmediatePropagation();

            $.ajax({
                type: "POST",
                url: "FrontPage.aspx/GetMunicipalitiesByInput",
                data: JSON.stringify({
                    inputValue: $("#LocationName").val(),
                    maxNum: 7
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (res) {
                    if (res.d == null)
                        return;

                    $("#searchOptionsHint").hide();
                    $("#searchOptions>ul").empty();

                    selectableHints = 0;

                    $.each(res.d, function () {
                        selectableHints += 1;

                        $("#searchOptions>ul").append(
                            '<li data-code="' + this.Code + '" data-name="' + this.Name + '">' + this.Name + '</li>'
                        );

                        if (this.Name.toLowerCase() == $("#LocationName").val().toLowerCase()) {
                            $("#LocationCode").val(this.Code);
                            $("#searchOptions").hide();
                        } else {
                            $("#searchOptions").show();
                        }
                    });

                },
                failure: function (res) {
                    console.log(res.d);
                },
                error: function (res) {
                    console.log(res.d);
                }
            });
        }, 150);
    });

    //$(document).on("click", ".search-results > ul > li", function (event) {
    //    $("#LocationCode").val($(this).attr("data-code"));
    //    $("#LocationName").val($(this).attr("data-name"));
    //    $("#searchOptions").hide();
    //});

});