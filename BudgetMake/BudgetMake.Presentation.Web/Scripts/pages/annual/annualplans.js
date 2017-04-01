(function () {

    var bindEvents = function () {

        $(document).ready(function () {
            // check for errors from server
            modules.network.ServerData.CheckServerData("BaseResultData");
        });

        $("#annualPlans").on('click', 'a[action="edit"]', function () {

            debugger;
            var annualPlanId = this.getAttribute("annualPlanId");
            var url = "/AnnualBudget/Edit";
            var data = { AnnualPlandId: annualPlanId };
            var id = "edit";

            modules.ui.OpenPartialViewModal(url, data, id);

        });

        $("#annualPlans").on('click', 'a[action="delete"]', function () {

            var annualPlanId = this.getAttribute("annualPlanId");
            var url = "/AnnualBudget/Delete";
            var data = { AnnualPlanId: annualPlanId };
            var id = "edit";

            modules.ui.OpenPartialViewModal(url, data, id);

        });

        document.getElementById("btnCreateNewAnnualBudgetPlan").addEventListener("click", function () {

            var annualPlanId = $("#AnnualPlanId").val();

            var url = "/AnnualBudget/CreatePlan";
            var data = { AnnualPlanId: annualPlanId };
            var id = "edit";

            modules.ui.OpenPartialViewModal(url, data, id);
        });

    }

    var init = function () {
    }

    init();
    bindEvents();

})();

