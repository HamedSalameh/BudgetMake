(function () {

    'use strict';

    var showHistoryEventDetails = function (reponseObject) {
        return new Promise(
            function (resolve, reject) {
                if (reponseObject && reponseObject.serverResponse) {
                    var _html = reponseObject.serverResponse;
                    $("#hlID_" + reponseObject.targetElement).html(_html);
                    // <tr><td> .. </td></tr>
                    resolve(reponseObject);
                } else {
                    reject(reponseObject);
                }
            });
    };

    var getHistoryEventDetails = function (serverURL, data, targetElement) {

        return new Promise(
                function (resolve, reject) {
                    // Object to hold sessionCache for data for every history detail object
                    var _cacheData = [];
                    // Represents the response object to be resolved or reject in the promise
                    var reponseObject = {
                        targetElement: targetElement,
                        serverResponse: ""
                    };

                    // Check if there is data on the sessionCache                    
                    if (sessionStorage.getItem("data") !== "") {
                        _cacheData = JSON.parse(sessionStorage.getItem("data"));
                    }
                    // Try locate the requested details in the sessionCache
                    for (var entry in _cacheData) {
                        if (_cacheData[entry].HistoryLogId === data.HistoryLogId) {
                            reponseObject.serverResponse = _cacheData[entry].responseObject.serverResponse;
                            resolve(reponseObject);
                        }
                    }
                    // before getting data from server, try get the data from local cache (sessionCache)
                    if (reponseObject.serverResponse === "") {
                        $.ajax({
                            url: serverURL,
                            data: data,
                            type: "GET"
                        })
                        .done(function (response) {
                            reponseObject.serverResponse = response;
                            //temp object to save data to sessionCache
                            var _newCache = {
                                HistoryLogId: data.HistoryLogId,
                                responseObject: {
                                    serverResponse : response
                                }
                            }
                            // add cache object to _cacheData
                            _cacheData.push(_newCache);
                            // Save the updated cahce data to the sessionCache
                            sessionStorage.setItem("data", JSON.stringify(_cacheData));
                            // all is good, resolve the promise
                            resolve(reponseObject);
                        })
                        .fail(function (response) {
                            reponseObject.serverResponse = response;
                            reject(reponseObject);
                        });
                    }
                }
            );
    };


    var bindEvents = function () {

        $(document).ready(function () {

            // Init cache
            sessionStorage.data = [];

            $("#historyData").off().on('click', 'tr', function () {

                var historyLogId = this.getAttribute("data");
                var url = "/Expense/HistoryEventDetails";
                var data = { HistoryLogId: historyLogId };

                getHistoryEventDetails(url, data, historyLogId)
                    .then(showHistoryEventDetails)
                    .catch(err => console.log("Unable to get detailed history for select event: " + err));
            });

        });

    };

    bindEvents();

})();