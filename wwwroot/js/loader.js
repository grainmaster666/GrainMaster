$('body').append("<div id=\"loading\"><img id=\"loading-image\" alt=\"Loading...\" src=\"../wwwroot/images/ajax-loader.gif\" /></div>");


$(window).on('load', function () {
    setTimeout(removeLoader, 500); //wait for page load PLUS two seconds.
});
function removeLoader() {
    $("#loading").fadeOut(500, function () {
        // fadeOut complete. Remove the loading div
        $("#loading").remove(); //makes page more lightweight 
    });
}