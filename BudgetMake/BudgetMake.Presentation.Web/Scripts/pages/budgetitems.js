(function () {
    "use strict";

    var bindEvents = function () {
        $("#btnCreateNewExpenseItem").on('click', function () {

            var monthlyPlanId = $("#MonthlyPlan").val();

            var url = "/Expense/CreateBudgetItem";
            var data = { MonthlyPlanId: monthlyPlanId };
            var id = "create";

            modules.ui.OpenPartialViewModal(url, data, id);
        });
    };

    bindEvents();

})();