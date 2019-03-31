$(document).ready(function () {

    $("#searchOptions>ul>li").click(function () {
        $("#LocationName").val($(this).attr("data-val"));
        //$("#searchOptions").hide();
    });

});