// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


(function () {
    const exactSearch = $("#exact-search");
    const partialSearch = $("#partial-search");

    exactSearch.show();
    partialSearch.hide();

    $(".toggle-search").click(e => {
        e.preventDefault();
        if (exactSearch.is(":visible")) {
            exactSearch.hide();
            exactSearch.find("input").val("");
            partialSearch.show();
        } else {
            exactSearch.show();
            partialSearch.hide();
            partialSearch.find("input").val("");
        }
    });
})();