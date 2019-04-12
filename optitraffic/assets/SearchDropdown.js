$(document).ready(function () {

    var elementHasAttribute = function (elem, attrName) {
        var attr = $(elem).attr(attrName);
        return typeof attr !== typeof undefined && attr !== false;
    }

    var selectedHintIdx = -1;
    var loadedHintsNum = 0;

    var requestAddress = "FrontPage.aspx/GetMunicipalitiesByInput";
    var requestDelay = 150;
    var requestMaxHints = 7;

    var hintsSelectionClass = "sel";
    var hintsAttrName = "data-name";

    var SearchForm = "#SearchForm";
    var SearchInput = "#LocationName";
    var SearchSumbitBtn = "#SubmitBtn";
    var SearchDropdown = "#searchOptions";
    var SearchDropdownList = SearchDropdown + ">ul";
    var SearchDropdownItems = SearchDropdownList + ">li";
    var SearchDropdownHint = "#searchOptionsHint";

    var selectedSearchDropdownItem = false;



    $(document).on("click", function (event) {
        if (elementHasAttribute($(event.target), hintsAttrName)) {
            selectedSearchDropdownItem = true;
            $(SearchInput).val($(event.target).attr(hintsAttrName));
            $(SearchDropdown).hide();
        } else if ($(event.target).attr("id") != $(SearchInput).attr("id")) {
            $(SearchDropdown).hide();
        } else {
            selectedSearchDropdownItem = false;
        }
    });

    $(SearchForm).on("submit", function (event) {
        if ($(SearchInput).val().trim().length == 0)
            event.preventDefault();
    });

    $(SearchForm).on("focusin", function (event) {
        if ($(SearchInput).val().length > 0 && !selectedSearchDropdownItem)
            $(SearchDropdown).show();
    });

    $(SearchInput).on("keydown", function (event) {
        if (event.which == 13) {

            if (loadedHintsNum == 1)
                $(SearchDropdownItems).eq(0).click();

            if (selectedHintIdx != -1)
                $(SearchDropdownItems).eq(selectedHintIdx).click();

            if ($(SearchInput).val().length > 0)
                $(SearchForm).submit();
            
            return false;
        }

        if (loadedHintsNum > 0) {
            if (event.which == 40) {
                event.preventDefault();

                if (selectedHintIdx + 1 < loadedHintsNum)
                    selectedHintIdx += 1;
                else
                    selectedHintIdx = 0;

            } else if (event.which == 38) {
                event.preventDefault();

                if (selectedHintIdx - 1 >= 0)
                    selectedHintIdx -= 1;
                else
                    selectedHintIdx = loadedHintsNum - 1;
            }

            $(SearchDropdownItems).removeClass(hintsSelectionClass);
            if (selectedHintIdx != -1)
                $(SearchDropdownItems).eq(selectedHintIdx).addClass(hintsSelectionClass);

        } else {
            selectedHintIdx = -1;
        }

    });

    $(SearchInput).on("input", function (event) {
        selectedSearchDropdownItem = false;
        $(SearchDropdown).show();

        if ($(SearchInput).val().length < 2) {
            loadedHintsNum = 0;
            $(SearchDropdownList).empty();
            $(SearchDropdownHint).show();
            return;
        }

        setTimeout(function () {
            event.stopImmediatePropagation();

            $.ajax({
                type: "POST",
                url: requestAddress,
                data: JSON.stringify({
                    inputValue: $(SearchInput).val(),
                    maxNum: requestMaxHints
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (res) {
                    if (res.d == null)
                        return;

                    $(SearchDropdownHint).hide();
                    $(SearchDropdownList).empty();

                    loadedHintsNum = 0;

                    $.each(res.d, function () {
                        loadedHintsNum += 1;

                        $(SearchDropdownList).append(
                            '<li data-name="' + this.Name + '">' + this.Name + '</li>'
                        );

                        if (this.Name.toLowerCase() == $(SearchInput).val().toLowerCase()) {
                            selectedSearchDropdownItem = true;
                            $(SearchDropdown).hide();
                        } else {
                            $(SearchDropdown).show();
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
        }, requestDelay);

        selectedHintIdx = -1;
        loadedHintsNum = 0;
    });
});