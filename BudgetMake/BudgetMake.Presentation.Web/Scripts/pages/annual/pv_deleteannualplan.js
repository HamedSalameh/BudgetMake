(function () {
    "use strict";

    var bindEvents = function () {
        var alertBoxName = "edit";

        $("#btnCancel").on('click', function () {
            $("#editModal").modal('toggle');
        });

        $("#btnDeleteAnnualPlan").on('click', function () {
            debugger;
            var annualPlanId = $("#hdnAnnualPlanId").val();

            var form = $('#__deleteAnnualPlanForm');
            var token = $('input[name="__RequestVerificationToken"]', form).val();

            var asyncCreate = function () {
                return $.ajax({
                    url: "/AnnualBudget/Delete",
                    data: {
                        __RequestVerificationToken: token,
                        AnnualPlanId: annualPlanId
                    },
                    type: "POST"
                });
            };

            asyncCreate().done(function (result) {
                var res = modules.network.ServerResponse.IsSuccess(result);
                if (res === true) {
                    // all went ok!
                    location.href = "/AnnualPlans";
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