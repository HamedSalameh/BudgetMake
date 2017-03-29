(function () {
    "use strict";

    var bindEvents = function () {
        var alertBoxName = "edit";

        $("#btnCancel").on('click', function () {
            $("#editModal").modal('toggle');
        });

        $("#btnEditSalaryItem").on('click', function () {
            debugger;
            var formData = $("#_InnerForm_EditSalaryItem").serialize();
            var monthlyPlanId = $("#hdnMonthlyPlanId").val();

            var asyncCreate = function () {
                return $.ajax({
                    url: "/Salary/EditBudgetItem",
                    data: formData,
                    type: "POST"
                });
            };

            asyncCreate().done(function (result) {
                var res = modules.network.ServerResponse.IsSuccess(result);
                if (res == true) {
                    // all went ok!
                    location.href = "/Monthly/" + monthlyPlanId;
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