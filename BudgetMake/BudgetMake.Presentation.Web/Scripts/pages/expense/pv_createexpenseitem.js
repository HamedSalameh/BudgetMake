(function () {
    "use strict";

    var bindEvents = function () {
        var alertBoxName = "create_alertBox";

        $("#btnCancel").on('click', function () {
            $("#createModal").modal('toggle');
        });

        $("#btnCreateExpenseItem").on('click', function () {
            debugger;
            var formData = $("#_InnerForm_CreateExpenseItem").serialize();
            var monthlyPlanId = $("#hdnMonthlyPlanId").val();
            

            var asyncCreate = function () {
                return $.ajax({
                    url: "/Expense/CreateExpenseItem",
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