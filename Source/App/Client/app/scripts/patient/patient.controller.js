angular.module("dentalApp")
    .controller("PatientController", [
        "$scope", "$state", "$http", "AppService", "PatientService", "LocalDataStorageService", "UrlService",
        function ($scope, $state, $http, appService, patientService, localDataStorageService, urlService) {
            "use strict";

            $scope.user = localDataStorageService.getUserInfo();
            if ($scope.user !== null) {
                $scope.isAdminUser = ($scope.user.RoleNames[0] === "Admin") ? true : false;
                $scope.isSystemAdminUser = ($scope.user.RoleNames[0] === "SystemAdmin") ? true : false;
            }
            
            $scope.myData = [];
            $scope.filters = [
                { Id: 0, Name: "All" },
                { Id: 1, Name: "Due" },
                { Id: 2, Name: "Payment Complete" }
            ];
            $scope.filterId = 0;

            $scope.loadPatientGridData = function() {
                $http.get(urlService.PatientUrl + "/GetGridList").then(function(response) {
                    console.log(response);
                    $scope.myData = response.data;
                }, function(error) {
                    console.log(error);
                });
            };
            $scope.loadPatientGridData();

            $scope.key = "";
            $scope.search = function() {
                if ($scope.key === undefined) {
                    $scope.loadPatientGridData();
                } else {
                    var request = { SearchKey: $scope.key, FilterId: $scope.filterId };
                    $http.get(urlService.PatientUrl + "/Search", { params: { request: request } }).then(function (response) {
                        console.log(response);
                        $scope.myData = response.data;
                    }, function (error) {
                        console.log(error);
                    });
                }
            };

            
            var columnDefs = [
                {
                    field: "Code",
                    displayName: "Patient Id",
                    cellTemplate: "<a  ng-click=\"detail(row.entity)\" style=\"padding-left: 5px; \" ng-bind=\"row.getProperty(col.field)\"></a>"
                },
                { field: "Name", displayName: "Patient Name" },
                { field: "Phone", displayName: "Phone" },
                { field: "Age", displayName: "Age" },
                { field: "Gender", displayName: "Gender" },
                {
                    field: "LastVisitingDate", displayName: "Last Visiting Date",
                    cellTemplate: "<div style=\"padding-left: 5px\" ng-bind=\"row.getProperty(col.field) | date: 'dd MMMM yyyy'\"></div>"
                },
                { field: "TotalPayable", displayName: "Payable" },
                { field: "TotalPaid", displayName: "Paid" },
                { field: "TotalDue", displayName: "Due" }
            ];

            $scope.gridOptions = {
                data: "myData",
                columnDefs: columnDefs,                
                multiSelect: false,
                selectedItems: $scope.mySelections,                
                enableRowSelection: true,
                //enablePinning: true,
                //enableCellSelection: true,
            };

            $scope.detail = function(row) {
                //alert(row.Id);

                if (row.Code !== null) {
                    $state.go("root.patient-detail", { patientId: row.Code });
                } else {
                    $state.go("root.patient");
                }
            };


            $scope.toAddPatientView = function() {
                patientService.setPatientId(null);
                patientService.setPageName('new-patient');
                $state.go("root.patient-create");
            };

        }
    ]);