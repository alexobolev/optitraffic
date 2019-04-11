$(document).ready(function () {

    var selectedHintIdx = -1;
    var selectableHints = 0;

    var maxHints = 7;
    var requestDelay = 150;

    var hintsSelectionClass = "sel";

    var SearchFormSelector = "#SearchForm";
    var SearchInputSelector = "#LocationName";
    var SearchDropdownSelector = "#searchOptions";
    var SearchDropdownListSelector = "#searchOptions>ul";
    var SearchDropdownItemsSelector = "#searchOptions>ul>li";
    var SearchDropdownHintSelector = "#searchOptionsHint";



    $(document).on("click", function (event) {
        var dataNameAttr = $(event.target).attr("data-name");

        if (typeof dataNameAttr !== typeof undefined && dataNameAttr !== false) {
            $(SearchInputSelector).val(dataNameAttr);
            $(SearchDropdownSelector).hide();
        } else if ($(event.target).attr("id") != $(SearchInputSelector).attr("id")) {
            $(SearchDropdownSelector).hide();
        }
    });

    $(SearchFormSelector).on("submit", function (event) {
        if ($(SearchInputSelector).val() == "") {
            event.preventDefault();
        }
    });

    $(SearchFormSelector).on("focusin", function (event) {
        if ($(SearchInputSelector).val().length > 0) {
            $(SearchDropdownSelector).show();
        }
    });

    $(SearchInputSelector).on("keydown", function (event) {
        if (event.which == 13) {

            if (selectedHintIdx != -1) {
                $(SearchDropdownItemsSelector).eq(selectedHintIdx).click();
            }

            if (selectableHints == 1) {
                $(SearchDropdownItemsSelector).eq(0).click();
            }

            $(SearchFormSelector).submit();
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

            $(SearchDropdownItemsSelector).removeClass(hintsSelectionClass);
            $(SearchDropdownItemsSelector).eq(selectedHintIdx).addClass(hintsSelectionClass);

        } else {
            selectedHintIdx = -1;
        }

    });

    $(SearchInputSelector).on("input", function (event) {
        var inputText = $(SearchInputSelector).val();


        $(SearchDropdownSelector).show();

        if (inputText.length < 2) {
            selectableHints = 0;
            $(SearchDropdownListSelector).empty();
            $(SearchDropdownHintSelector).show();
            return;
        }

        setTimeout(function () {
            event.stopImmediatePropagation();

            $.ajax({
                type: "POST",
                url: "FrontPage.aspx/GetMunicipalitiesByInput",
                data: JSON.stringify({
                    inputValue: $(SearchInputSelector).val(),
                    maxNum: maxHints
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (res) {
                    if (res.d == null)
                        return;

                    $(SearchDropdownHintSelector).hide();
                    $(SearchDropdownListSelector).empty();

                    selectableHints = 0;

                    $.each(res.d, function () {
                        selectableHints += 1;

                        $(SearchDropdownListSelector).append(
                            '<li data-name="' + this.Name + '">' + this.Name + '</li>'
                        );

                        if (this.Name.toLowerCase() == $(SearchInputSelector).val().toLowerCase()) {
                            $(SearchDropdownSelector).hide();
                        } else {
                            $(SearchDropdownSelector).show();
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
    });
});