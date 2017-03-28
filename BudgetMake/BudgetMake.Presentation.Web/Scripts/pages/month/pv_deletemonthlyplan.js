(function () {
    "use strict";

    var bindEvents = function () {
        var alertBoxName = "edit";

        $("#btnCancel").on('click', function () {
            $("#editModal").modal('toggle');
        });

        $("#btnDeleteMonthlyPlan").on('click', function () {
            debugger;
            var MonthlyPlanId = $("#hdnMonthlyPlanId").val();
            var annualPlanId = $("#hdnAnnualPlanId").val();

            var form = $('#__deleteExpenseForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            var asyncCreate = function () {
                return $.ajax({
                    url: "/MonthlyPlans/Delete",
                    data: {
                        __RequestVerificationToken: token,
                        monthlyPlanId: MonthlyPlanId
                    },
                    type: "POST"
                });
            };

            asyncCreate().done(function (result) {
                var res = modules.network.ServerResponse.IsSuccess(result);
                if (res == true) {
                    // all went ok!
                    location.href = "/Annual/" + annualPlanId;
                } else {
                    // something went wrong
                    var res = modules.network.ServerResponse.IsFailure(result);
                    if (res == true) {
                        modules.alerts.Warning(alertBoxName, result);
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