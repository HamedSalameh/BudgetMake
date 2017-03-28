(function () {

    var bindEvents = function () {

        $(document).ready(function () {
            // check for errors from server
            modules.network.ServerData.CheckServerData("BaseResultData");
        });

        $("#plansList").on('click', 'a[action="edit"]', function () {

            var monthlyPlanId = this.getAttribute("monthlyPlanId");
            var url = "/MonthlyPlans/Edit";
            var data = { MonthlyPlanId: monthlyPlanId };
            var id = "edit";

            modules.ui.OpenPartialViewModal(url, data, id);

        });

        $("#plansList").on('click', 'a[action="savetemplate"]', function () {
            var monthlyPlanId = this.getAttribute("id");
            document.getElementById("planId").value = monthlyPlanId;
            $("#SaveAsTemplate").modal();
        });

        document.getElementById("btnCreateNewMonthlyPlan").addEventListener("click", function () {
            location.href = " @Url.Action('CreatePlan', 'MonthlyPlans', new { AnnualPlanId = TempData['AnnualPlan'] })";
        });

        document.getElementById("ddlMonthlyPlanTemplates").addEventListener("change", function () {

            var templateId = document.getElementById("ddlMonthlyPlanTemplates").value;
            if (templateId != null && templateId != "") {
                document.getElementById("btnCreateFromTemplate").disabled = false;
            } else {
                document.getElementById("btnCreateFromTemplate").disabled = true;
            }

        });

        document.getElementById("btnSave").addEventListener("click", function () {
            
            var asyncSave = function (templateInfo) {
                return $.ajax({
                    url: '/MonthlyPlans/SaveAsTemplate',
                    type: "POST",
                    data: templateInfo,
                });
            }

            var planId = document.getElementById("planId").value;
            if (planId) {
                planId = planId.slice(3, planId.length);
            }
            var templateName = document.getElementById("templateName").value;

            var templateInfo = {};
            templateInfo.PlanId = planId;
            templateInfo.TemplateName = templateName;

            if (planId != null && planId > 0) {
                asyncSave(templateInfo)
                    .done(function (result) {
                        var response = result;

                    })
                    .fail(function (result) {
                        var response = result;
                    });
            }

        });

        document.getElementById("btnCreateFromTemplate").addEventListener("click", function () {
            var templateId = document.getElementById("ddlMonthlyPlanTemplates").value;

            var templateInfo = {};
            templateInfo.PlanId = templateId;
            templateInfo.TemplateName = "";

            var asyncCreate = function () {
                return $.ajax({
                    url: "/MonthlyPlans/CreateFromTemplateByTemplateInfoPlanId",
                    data: templateInfo,
                    type: "POST"
                });
            };

            asyncCreate().done(function (result) {
                var res = modules.network.ServerResponse.IsSuccess(result);
                if (res == true) {
                    // all went ok!
                    // redirect to edit plan
                    location.href = "/MonthlyPlans/Edit?MonthlyPlanId=" + result.Value;
                } else {
                    // something went wrong
                    var res = modules.network.ServerResponse.isFailure(result);
                    if (res == true) {
                        alerts.warning("alertBox", result.Message);
                    } else {
                        alerts.danger("alertBox", result.Message);
                    }
                }

            }).fail(function (result) {
                // general ajax failure
                console.log(result);
            });

        });
    }

    var init = function () {
        document.getElementById("btnCreateFromTemplate").disabled = true;
    }

    init();
    bindEvents();

})();

