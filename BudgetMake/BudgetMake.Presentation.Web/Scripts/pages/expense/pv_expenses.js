(function () {
    'use strict';

    var alertBoxName = "expensesListAlertBox";
    var monthlyPlanId = $("#MonthlyPlan").val();

    var bindEvents = function () {

        $(document).ready(function () {
            // check for errors from server
            modules.network.ServerData.CheckServerData("BaseResultData");
        });

        $("#expensesList").on('click', 'a[action="edit"]', function () {

            var BudgetItemId = this.getAttribute("budgetItemId");
            var url = "/Expense/EditBudgetItem";
            var data = { budgetItemId: BudgetItemId };
            var id = "edit";

            modules.ui.OpenPartialViewModal(url, data, id);

        });

        $("#expensesList").on('click', 'a[action="delete"]', function () {

            var BudgetItemId = this.getAttribute("budgetItemId");
            var url = "/Expense/DeleteBudgetItem";
            var data = { budgetItemId: BudgetItemId };
            var id = "edit";

            modules.ui.OpenPartialViewModal(url, data, id);

        });

        $("#expensesList").on('click', 'a[action="history"]', function () {
            var BudgetItemId = this.getAttribute("budgetItemId");
            var url = "/Expense/EntityHistory";
            var data = { budgetItemId: BudgetItemId };
            var modalNameForPartialView = "edit";

            modules.ui.OpenPartialViewModal(url, data, modalNameForPartialView);
        });

        $("#expensesList").on('click', 'span.item-display', function (event) {
            $(event.currentTarget)
                .hide()
                .next('span.item-field')
                .show()
                .find(':input:first')
                .focus()
                .select();
        })
        .on("focusout", "span.item-field", function (event) {
            modules.ui.ToggleDisplayEdit(event, 'span', 'item-display', true);
            return;
        })
        .on("keyup", "span.item-field", function (event) {

            var $field = $(event.currentTarget);
            var $display = $field.prev('span' + '.' + 'item-display');

            if (event.keyCode === 27) {
                // escape button. cancel changes
                modules.ui.ToggleDisplayEdit(event, 'span', 'item-display', true);
                return;
            } else if (event.keyCode !== 13)
                return;

            // stop propagation of event
            event.preventDefault();

            // handle the save event
            modules.ui.ToggleDisplayEdit(event, 'span', 'item-display');
            // Get anti forgery token from page
            var token = modules.security.GetAntiForgeryToken("expensesList");

            var _biAmount = $display.html();
            var _biId = $display.parent().parent().attr('data');
            var _mpId = $("#MonthlyPlan").val();
            var _propertyName = $field.attr('propertyName');

            var formData = {
                budgetItemId: _biId,
                PropName: _propertyName,
                PropValue: _biAmount
            };

            // Create data object with XSRF token included
            var xDataObj = new xdataObject(token, formData);
            // Call server with xDataObj
            modules.network.ServerCall("/Expense/QuickEditBudgetItem", "POST", xDataObj, redirect, showAlert);
        });

        var redirect = function (url) {
            if (typeof url === 'undefined' || url === '') {
                url = window.location.href;
            };
            location.href = url;
        };

        var showAlert = function () {
            
        }

    };

    bindEvents();

})();