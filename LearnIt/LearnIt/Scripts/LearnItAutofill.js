
function LearnItAutocomplete
   (myInputId,
    myModelSearchField,
    jsonModel,
) {
    $(document).ready(() => {
            let modelLenght = jsonModel.length;
            let names = [];
            let modelValueSearch = myModelSearchField;
            for (let i = 0; i < modelLenght; i += 1) {
                names.push(jsonModel[i][modelValueSearch]);
            }
            $(myInputId).autocomplete({
                source: names
            });
        });
};
function LearnItAutocompleteAndSearch 
            (myInputId,
            myModelSearchField,
            jsonModel,
            idHiddenDivAndA,
            urlSearchName = "?username="){
        $(document).ready(() => {
            let urlSearchNameLenght = urlSearchName.length;
            let modelLenght = jsonModel.length;
            let names = [];
            let modelValueSearch = myModelSearchField;
            for (let i = 0; i < modelLenght; i += 1) {
                names.push(jsonModel[i][modelValueSearch]);
            }
            $(myInputId).autocomplete({
                source: names
            });
            $(window).keydown(function (e) {
                if (e.which == 13) {
                    $(myInputId).keyup(function (e) {
                        if (e.which == 13 &&
                            !(names.indexOf($(myInputId).val()) === -1)) {

                            let cleanUrl = $(idHiddenDivAndA)
                                .attr('href')
                                .substring(0, $(idHiddenDivAndA)
                                    .attr('href')
                                    .indexOf(urlSearchName) + urlSearchNameLenght);

                            let searchUrl = cleanUrl + $(myInputId).val();
                            $(idHiddenDivAndA).attr("href", searchUrl)
                            $(idHiddenDivAndA).click();
                        }
                    });
                }
            });
        });
}