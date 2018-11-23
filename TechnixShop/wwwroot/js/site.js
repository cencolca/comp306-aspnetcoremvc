// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on("click", "ul[thumb] li img", function (event) {
    event.preventDefault();

    var src = $(this).attr("src");
    var image = $(this).closest("ul").find("li:first-child img");
    
    $(this).attr("src", image.attr("src"));
    image.attr("src", src);
})
