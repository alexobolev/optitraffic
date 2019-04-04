$(document).ready(function () {

    $("#LangDropdownOptions>li>a").click(function () {
        var loc = $(this).attr("data-locale");

        console.log("Setting locale to: " + loc + "\n");

        Cookies.set('lang', loc);
        location.reload(true);
    });

});