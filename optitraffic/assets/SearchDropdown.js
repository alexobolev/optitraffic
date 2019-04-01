$(document).ready(function () {

    $("#LocationName").on("input", function (e) {
        $.ajax({
            type: "POST",
            url: "AjaxSearchSuggestions.asmx/GetSuggestions",
            data: "value=" + "La",
            dataType: "html",
            success: function (res) {
                $("#searchOptions").html(res);
                console.log(res);
            },
            error: function (res) {
                //$("#searchOptions").html("<h1>Bye world</h1>");
                console.log(res);
            }
        });
    });

    $("#searchOptions>ul>li").click(function () {
        $("#LocationName").val($(this).attr("data-val"));
        //$("#searchOptions").hide();
    });

});