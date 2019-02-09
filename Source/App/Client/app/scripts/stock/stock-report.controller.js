angular.module("dentalApp")
    .controller("StockReportController", [
        "$scope",  "$http", "$uibModal", "$log","UrlService",
        function ($scope, $http, $uibModal, $log, urlService) {
            "use strict";

            $scope.productsName = [];
            $scope.status = [
                {Id: 0, Name: "All Status"},
                {Id: 1, Name: "In Stock"},
                {Id: 2, Name: "Out Of Stock"}
            ];
            $scope.daysFilter = [
                { Id: 7, Name: "Last 7 Days" },
                { Id: 15, Name: "Last 15 Days" },
                { Id: 30, Name: "Last 1 Month" }
            ];

            $scope.filter = { From: new  Date (), To: new Date(), ProductId: "00000000-0000-0000-0000-000000000000", StatusId: 0, DaysFilterId: 7 };
            $scope.inventoryReports = [];

            $scope.getProductsName = function() {
                $scope.productsName = [];
                $http.get(urlService.ProductUrl + "/GetProductsName").then(function(response) {
                    console.log(response);
                    $scope.productsName.push({ Id: 0, Name: "All Product", Code: "All" });
                    for (var p in response.data) {
                        if (response.data.hasOwnProperty(p)) {
                            $scope.productsName.push(response.data[p]);
                        }
                    }
                }, function(error) {
                    console.log(error);
                });
            };
            $scope.getProductsName();

            $scope.getReport = function() {
                $scope.inventoryReports = [];
                $http.get(urlService.InventoryReportUrl + "/GetReport", { params: { request: $scope.filter } }).then(function(response) {
                    console.log(response);
                    $scope.inventoryReports = response.data;
                }, function(error) {
                    console.log(error);
                });
            };
            $scope.getReport();


            //print instance model
            $scope.animationsEnabled = true;
            $scope.open = function (size) {
                var modalInstance = $uibModal.open({
                    animation: $scope.animationsEnabled,
                    templateUrl: "inventoryReportModal.html",
                    controller: "InventoryReportModalInstanceCtrl",
                    size: size,
                    resolve: {
                        reports: function () {
                            return $scope.inventoryReports;
                        },
                        dateRange: function () {
                            return $scope.filter;
                        }
                    }
                });

                modalInstance.result.then(function (selectedItem) {
                    $scope.selected = selectedItem;
                }, function () {
                    $log.info("Modal dismissed at: " + new Date());
                });
            };

        }
    ]);



angular.module("dentalApp")
    .controller("InventoryReportModalInstanceCtrl", [
        "$scope", "$uibModalInstance", "reports",  "dateRange",
        function ($scope, $uibModalInstance, reports, dateRange) {

            $scope.reports = reports;
            $scope.dateRange = dateRange;
            $scope.today = new Date();

            $scope.ok = function () {
                $uibModalInstance.close();
            };


            $scope.printDiv = function (divName) {
                var printContents = document.getElementById(divName).innerHTML;
                var originalContents = document.body.innerHTML;
                var popupWin;
                if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
                    popupWin = window.open('', '_blank', 'width=750,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
                    popupWin.window.focus();
                    popupWin.document.write('<!DOCTYPE html><html><head>' + '<link href="Content/bootstrap/bootstrap.min.css" type="text/css" rel="stylesheet"/>' + '</head><body onload="window.print()"><div style="width: 800px; height:auto;">' + printContents + '</div></html>');
                    popupWin.onbeforeunload = function (event) {
                        popupWin.close();
                        return '.n';
                    };
                    popupWin.onabort = function(event) {
                        popupWin.document.close();
                        popupWin.close();
                    };
                } else {
                    popupWin = window.open('', '_blank', 'width=750,height=600');
                    popupWin.document.open();
                    popupWin.document.write('<html><head><link href="../Content/bootstrap/bootstrap.min.css" type="text/css" rel="stylesheet"/></head><body onload="window.print()">' + printContents + '</html>');
                    popupWin.document.close();
                }
                popupWin.document.close();
                $scope.cancel();
                return true;
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss("cancel");
            };
        }
    ]);