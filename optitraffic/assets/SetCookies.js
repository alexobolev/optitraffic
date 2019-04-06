$(document).ready(function () {

    $("#LangDropdownOptions>li>a").click(function () {
        var loc = $(this).attr("data-locale");

        if (loc != "en" && loc != "fi") {
            alert("Overriding inexistent locale code...\nDid someone try to break me? :D");
            $(this).attr("data-locale", "en");
            loc = "en";
        }

        console.log("Setting locale to: " + loc + "\n");

        Cookies.set('lang', loc);
        location.reload(true);
    });

});