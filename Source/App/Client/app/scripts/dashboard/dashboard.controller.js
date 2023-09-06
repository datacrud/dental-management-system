angular.module("dentalApp")
    .controller("DashboardController", [
        "$scope", "$http", "LocalDataStorageService", "UrlService",
        function ($scope, $http, localDataStorageService, urlService) {
            "use strict";

            $scope.product = { Id: "", Code: "", Name: "", StartingInventory: 0, Received: 0, Shipped: 0, OnHand: 0, MinimumRequired: 0, UnitPrice: 0, SalePrice: 0, StatusId: 0 };

            $scope.user = localDataStorageService.getUserInfo();
            if ($scope.user !== null) {
                $scope.isAdminUser = ($scope.user.RoleNames[0] === "Admin") ? true : false;
                $scope.isSystemAdminUser = ($scope.user.RoleNames[0] === "SystemAdmin") ? true : false;
            }

            $scope.filters = [
                { Id: 0, Name: "All" },
                { Id: 1, Name: "In Stock" },
                { Id: 2, Name: "Out Of Stock" }
            ];            

            $scope.products = [];
            $scope.getProducts = function () {
                $http.get(urlService.ProductUrl + "/GetProductsIncludeStatus").then(
                    function (response) {
                        console.log(response);
                        $scope.products = response;
                        $scope.myData = $scope.products;
                    }, function (error) {
                        console.log(error);
                    });
            };
            $scope.getProducts();                        


            $scope.filterId = 0;
            $scope.filter = function(id) {
                $http.get(urlService.ProductUrl + "/Filter", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    if (response.data !== null) {
                        $scope.myData = response.data;
                    }
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.key = "";
            $scope.search = function () {
                $http.get(urlService.ProductUrl + "/Search", { params: { request: $scope.key } }).then(function(response) {
                    console.log(response);
                    if (response.data !== null) {
                        $scope.myData = response.data;
                    }
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.isKeyNull = function(key) {
                if (key === undefined)
                    $scope.getProducts();
                else
                    $scope.search();
            };           


            var columnDefs = [
                //{
                //    field: "Id",
                //    displayName: "Id",
                //    cellTemplate: "<div  ng-click=\"detail(row.entity)\" style=\"padding-left: 5px\" ng-bind=\"row.getProperty(col.field)\"></div>"
                //},
                { field: "Code", displayName: "Code" },
                { field: "Name", displayName: "Name" },
                { field: "StartingInventory", displayName: "Starting Inventory" },
                { field: "Received", displayName: "Total Received" },
                { field: "Shipped", displayName: "Total Shipped" },
                {
                    field: "OnHand", displayName: "On Hand",
                    cellTemplate: "<div  style=\"padding-left: 5px; background: #5cb85c;\" ng-bind=\"row.getProperty(col.field)\"></div>"
                },
                { field: "UnitPrice", displayName: "Unit Price (Tk)" },
                { field: "SalePrice", displayName: "Sale Price" },
                //{ field: "StatusId", displayName: "Status" },
                {
                    field: "Status.Name",
                    displayName: "Status"
                }
            ];

            $scope.gridOptions = {
                data: "myData",
                columnDefs: columnDefs,
                //enablePinning: true,
                multiSelect: false,
                selectedItems: $scope.mySelections,
                //enableCellSelection: true,
                enableRowSelection: true
            };

            $scope.detail = function(row) {
                if (row.State.Id === 3) {
                    $state.go("payslip.approved", { payslipId: row.Id });
                } else {
                    $state.go("payslip.detail", { payslipId: row.Id });
                }
            };


        }
    ]);