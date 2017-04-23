(function () {
    "use strict";

    var bindEvents = function () {
        var alertBoxName = "interaction_alertBox";

        $("#btnCancel").on('click', function () {
            $("#interactionModal").modal('toggle');
        });

        $("#btnCreateExpenseItem").on('click', function () {
            debugger;
            var formData = $("#_InnerForm_CreateExpenseItem").serialize();
            var monthlyPlanId = $("#hdnMonthlyPlanId").val();


            var asyncCreate = function () {
                return $.ajax({
                    url: "/Expense/CreateBudgetItem",
                    data: formData,
                    type: "POST"
                });
            };

            asyncCreate().done(function (result) {
                var res = modules.network.ServerResponse.IsSuccess(result);
                if (res === true) {
                    // all went ok!
                    location.href = "/Monthly/" + monthlyPlanId;
                } else {
                    // something went wrong
                    res = modules.network.ServerResponse.IsFailure(result);
                    if (res === true) {
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
    };

    bindEvents();

})();