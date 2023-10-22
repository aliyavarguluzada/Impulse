$(document).ready(function () {
    // Event handler for the filter button
    $("#search-button").click(function () {
        var jobCategory = $("#jobCategory").val();
        var jobType = $("#jobType").val();
        var workForm = $("#workForm").val();

        // Make an AJAX request to your controller action
        $.ajax({
            type: "POST",
            url: "/Vacancy/Index", // Update with the actual URL
            data: {
                jobCategoryId: jobCategory,
                jobTypeId: jobType,
                workFormId: workForm
            },
            dataType: "json",
            success: function (result) {
                if (result.status === 200) {
                    var vacancies = result.data;
                    // Generate HTML to display the filtered vacancies
                    var html = "";
                    vacancies.forEach(function (vacancy) {
                        // Generate vacancy HTML
                        // ...
                    });
                    // Update the vacancyList div with the generated HTML
                    $("#vacancyList").html(html);
                } else {
                    // Handle error
                    console.log("Error: " + result.status);
                }
            },
            error: function (xhr, status, error) {
                // Handle AJAX error
                console.log("AJAX error: " + error);
            }
        });
    });
});