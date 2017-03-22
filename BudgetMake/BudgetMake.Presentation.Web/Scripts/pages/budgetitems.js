(function () {
    "use strict";

    var bindEvents = function () {
        $("#btnCreateNewExpenseItem").on('click', function () {

            var monthlyPlanId = $("#MonthlyPlan").val();

            $.ajax({
                url: "/Expense/CreateBudgetItem",
                data: { MonthlyPlanId: monthlyPlanId },
                type: "GET",
                success: function (response) {
                    $("#createFormBody").html(response);
                    $("#createModal").modal();
                },
                failure: function (response) {
                    console.log("Server call failed: ", response);
                },
                error: function (response) {
                    console.log("Server call returned with error(s): ", response);
                }
            });
        });
    };

    bindEvents();

})();