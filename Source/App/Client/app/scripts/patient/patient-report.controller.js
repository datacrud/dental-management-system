angular.module("dentalApp")
    .controller("PatientReportController", [
        "$scope", "UrlService", "$http", "$uibModal", "$log",
        function ($scope, urlService, $http, $uibModal, $log) {
            "use strict";

            $scope.filter = { From: new Date(), To: new Date() };
            $scope.patientPaymentsReports = [];
            $scope.totalServiceAmount = 0;

            $scope.loadPatientPaymentsReports = function() {
                $scope.patientPaymentsReports = [];

                $http.get(urlService.PatientReportUrl + "/GetPatientPaymentReport", { params: { request: $scope.filter } }).then(function(response) {
                    console.log(response);
                    $scope.patientPaymentsReports = response.data;
                    $scope.totalServiceAmount = 0;
                    for (var i = 0; i < $scope.patientPaymentsReports.length; i++) {
                        $scope.totalServiceAmount = parseInt($scope.totalServiceAmount) + parseInt($scope.patientPaymentsReports[i].Amount);
                    }
                }, function(error) {
                    console.log(error);
                    $scope.totalServiceAmount = 0;
                });
            };
            $scope.loadPatientPaymentsReports();


            //print instance model
            $scope.animationsEnabled = true;
            $scope.open = function (size) {
                var modalInstance = $uibModal.open({
                    animation: $scope.animationsEnabled,
                    templateUrl: "patientReportModal.html",
                    controller: "PatientReportModalInstanceCtrl",
                    size: size,
                    resolve: {
                        reports: function () {
                            return $scope.patientPaymentsReports;
                        },
                        totalAmount: function() {
                            return $scope.totalServiceAmount;
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
    .controller("PatientReportModalInstanceCtrl", [
        "$scope", "$uibModalInstance", "reports", "totalAmount", "dateRange",
        function ($scope, $uibModalInstance, reports, totalAmount, dateRange) {

            $scope.reports = reports;
            $scope.totalAmount = totalAmount;
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