﻿(function () {
    "use strict";

    var bindEvents = function () {
        $("#btnCreateNewExpenseItem").on('click', function () {

            var monthlyPlanId = $("#MonthlyPlan").val();

            var url = "/Expense/CreateBudgetItem";
            var data = { MonthlyPlanId: monthlyPlanId };
            var id = "interaction";

            modules.ui.OpenPartialViewModal(url, data, id);
        });

        $("#btnCreateNewSalaryItem").on('click', function () {

            var monthlyPlanId = $("#MonthlyPlan").val();

            var url = "/Salary/CreateBudgetItem";
            var data = { MonthlyPlanId: monthlyPlanId };
            var id = "interaction";

            modules.ui.OpenPartialViewModal(url, data, id);
        });

        $("#btnCreateNewChequeItem").on('click', function () {

            var monthlyPlanId = $("#MonthlyPlan").val();

            var url = "/Cheque/CreateBudgetItem";
            var data = { MonthlyPlanId: monthlyPlanId };
            var id = "interaction";

            modules.ui.OpenPartialViewModal(url, data, id);
        });

        $("#btnCreateNewCreditCardItem").on('click', function () {

            var monthlyPlanId = $("#MonthlyPlan").val();

            var url = "/CreditCard/CreateBudgetItem";
            var data = { MonthlyPlanId: monthlyPlanId };
            var id = "interaction";

            modules.ui.OpenPartialViewModal(url, data, id);
        });
    };

    bindEvents();

})();