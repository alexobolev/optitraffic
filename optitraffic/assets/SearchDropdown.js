$(document).ready(function () {

    $("#LocationName").on("input", function (event) {
        var inVal = $("#LocationName").val();

        if (inVal.length == 1) {
            return
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

                    $("#searchOptions>ul").empty();
                    $.each(res.d, function () {
                        $("#searchOptions>ul").append(
                            '<li data-code="' + this.Code + '" data-name="' + this.Name + '">' + this.Name + '</li>'
                        );
                        $("#searchOptions").show();
                    });

                },
                failure: function (res) {
                    console.log(res.d);
                },
                error: function (res) {
                    console.log(res.d);
                }
            });
        }, 250);
    });

    $(document).on("click", ".search-results > ul > li", function () {
        $("#LocationCode").val($(this).attr("data-code"));
        $("#LocationName").val($(this).attr("data-name"));
        $("#searchOptions").hide();
    });

});