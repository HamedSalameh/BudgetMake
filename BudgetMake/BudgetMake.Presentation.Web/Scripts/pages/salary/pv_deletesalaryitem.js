(function () {
    "use strict";

    var bindEvents = function () {
        var alertBoxName = "interaction_alertBox";

        $("#btnCancel").on('click', function () {
            $("#interactionModal").modal('toggle');
        });

        $("#btnDeleteSalaryItem").on('click', function () {
            debugger;
            var BudgetItemId = $("#BudgetItemId").val();
            var MonthlyPlanId = $("#MonthlyPlanId").val();

            var form = $('#__deleteSalaryForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            var asyncCreate = function () {
                return $.ajax({
                    url: "/Salary/DeleteBudget",
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
                        modules.alerts.warning(alertBoxName, result);
                    } else {
                        modules.alerts.Danger(alertBoxName, result);
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