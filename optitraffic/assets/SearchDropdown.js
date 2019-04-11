$(document).ready(function () {

    $(document).on("click", function (event) {
        var attr = $(event.target).attr("data-name");
        var clickedOnHint = typeof attr !== typeof undefined && attr !== false;

        if (clickedOnHint) {
            $("#LocationName").val($(event.target).attr("data-name"));
            $("#searchOptions").hide();
        } else if ($(event.target).attr("id") != "LocationName") {
            $("#searchOptions").hide();
        }
    });

    $("#SearchForm").on("submit", function (event) {
        if ($("#LocationName").val() == "") {
            event.preventDefault();
        }
    });

    $("#SearchForm").on("focusin", function (event) {
        if ($("#LocationName").val().length > 0) {
            $("#searchOptions").show();
        }
    });


    var selectedHintIdx = -1;
    var selectableHints = 0;

    $("#LocationName").on("keydown", function (event) {
        if (event.which == 13) {

            if (selectedHintIdx != -1) {
                $("#searchOptions>ul>li").eq(selectedHintIdx).click();
            }

            if (selectableHints == 1) {
                $("#searchOptions>ul>li").eq(0).click();
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
        var inVal = $("#LocationName").val();


        $("#searchOptions").show();

        if (inVal.length < 2) {
            selectableHints = 0;
            $("#searchOptions>ul").empty();
            $("#searchOptionsHint").show();
            return;
        }

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
                            '<li data-name="' + this.Name + '">' + this.Name + '</li>'
                        );

                        if (this.Name.toLowerCase() == $("#LocationName").val().toLowerCase()) {
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
});