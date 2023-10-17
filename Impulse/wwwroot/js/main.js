$(document).ready(function () {
    $('.vacancy-link').click(function () {
        window.location.href = $(this).attr('href'); // Redirect to the href specified in the anchor
    });
});
