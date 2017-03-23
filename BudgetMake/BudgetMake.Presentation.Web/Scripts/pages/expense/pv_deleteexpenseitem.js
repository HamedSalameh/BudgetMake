﻿(function () {
    "use strict";

    var bindEvents = function () {
        var alertBoxName = "edit";

        $("#btnCancel").on('click', function () {
            $("#editModal").modal('toggle');
        });

        $("#btnDeleteExpenseItem").on('click', function () {
            debugger;
            var BudgetItemId = $("#BudgetItemId").val();
            var MonthlyPlanId = $("#MonthlyPlanId").val();

            var form = $('#__deleteExpenseForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            var asyncCreate = function () {
                return $.ajax({
                    url: "/Expense/DeleteBudget",
                    data: {
                        __RequestVerificationToken: token,
                        budgetItemId: BudgetItemId
                    },
                    type: "POST"
                });
            };

            asyncCreate().done(function (result) {
                var res = modules.network.ServerResponse.IsSuccess(result);
                if (res == true) {
                    // all went ok!
                    location.href = "/Monthly/" + MonthlyPlanId;
                } else {
                    // something went wrong
                    var res = modules.network.ServerResponse.IsFailure(result);
                    if (res == true) {
                        alerts.warning(alertBoxName, result);
                    } else {
                        alerts.danger(alertBoxName, result);
                    }
                }

            }).fail(function (result) {
                // general ajax failure
                console.log(result);
            });

        });
    }

    bindEvents();

})();