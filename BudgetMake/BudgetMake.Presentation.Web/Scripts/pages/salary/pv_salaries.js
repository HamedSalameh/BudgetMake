(function () {
    'use strict';

    var alertBoxName = "expensesListAlertBox";
    var monthlyPlanId = $("#MonthlyPlan").val();

    var bindEvents = function () {

        $(document).ready(function () {
            // check for errors from server
            modules.network.ServerData.CheckServerData("BaseResultData");
        });

        $("#salariesList").on('click', 'a[action="edit"]', function () {

            var BudgetItemId = this.getAttribute("budgetItemId");
            var url = "/Salary/EditBudgetItem";
            var data = { budgetItemId: BudgetItemId };
            var id = "edit";

            modules.ui.OpenPartialViewModal(url, data, id);

        });

        var redirect = function (url) {
            if (typeof url === 'undefined' || url === '') {
                url = window.location.href;
            };
            location.href = url;
        };

        var showAlert = function () {

        }

    };

    bindEvents();

})();