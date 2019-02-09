angular.module("dentalApp")
    .controller("StockController", [
        "$scope", "$http", "AppService", "$uibModal", "$log","UrlService",
        function ($scope, $http, appService, $uibModal, $log, urlService) {
            "use strict";

            $scope.init = function() {
                $scope.stock = { ProductId: 0, CashMemoNo: "", OnHand: 0, ReceivedOrShippedQuantity: 0, StatusId: 0, Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString() };
            };
            $scope.init();

            $scope.dateFilter = { From: new Date(), To: new Date() };

            $scope.daysFilter = [
                { Id: 7, Name: "Last 7 Days" },
                { Id: 15, Name: "Last 15 Days" },
                { Id: 30, Name: "Last 1 Month" }
            ];
            $scope.daysFilterId = 7;

            $scope.isUpdateMode = false;


            $scope.productsName = [];
            $scope.getProductsName = function () {
                $http.get(urlService.ProductUrl + "/GetProductsName").success(function (response) {
                    console.log(response);
                    $scope.productsName = response;
                }).error(function (error) {
                    console.log(error);
                });
            };
            $scope.getProductsName();

            $scope.filterByDateRange = function() {

                if ($scope.stock.ProductId === 0) {
                    alert("You haven't select any product yet. Please select a product first.");
                } else {
                    $http.get(url + "Search", { params: { from: $scope.dateFilter.From, to: $scope.dateFilter.To, productId: $scope.stock.ProductId } }).then(function(response) {
                        console.log(response);
                        $scope.productInventoryHistory = response.data;
                    }, function(error) {
                        console.log(error);
                    });
                }
            };
            $scope.productName = "";
            $scope.getProductName = function(id) {
                for (var i = 0; i < $scope.productsName.length; i++) {
                    if ($scope.productsName[i].Id === id) {
                        $scope.productName = $scope.productsName[i].Name;
                        return $scope.productsName[i].Name;
                    }
                }
            };

            $scope.product = {};
            $scope.getProduct = function(id) {
                $http.get(urlService.ProductUrl + "/GetById?request=" + id).success(function(response) {
                    console.log(response);
                    $scope.product = response;
                    $scope.stock.OnHand = response.OnHand;
                }).error(function(error) {
                    console.log(error);
                    $scope.product = {};
                });
            };


            $scope.productInventoryHistory = [];
            $scope.totalReceived = 0;
            $scope.totalShipped = 0;
            $scope.getProductInventoryHistory = function() {
                if ($scope.stock.ProductId !== 0) {
                    var id = $scope.stock.ProductId;
                    $scope.getProduct(id);

                    var request = { Id: id, DaysFilterId: $scope.daysFilterId };
                    $http.get(urlService.InventoryUrl + "/GetProductHistory", { params: { request: JSON.stringify(request) } }).then(function(response) {
                        console.log(response);
                        $scope.productInventoryHistory = response.data;

                        $scope.totalReceived = 0;
                        $scope.totalShipped = 0;
                        for (var i = 0; i < $scope.productInventoryHistory.length; i++) {
                            if ($scope.productInventoryHistory[i].Status.Id == 3)
                                $scope.totalReceived = parseInt($scope.totalReceived) + parseInt($scope.productInventoryHistory[i].ReceivedOrShippedQuantity);

                            if ($scope.productInventoryHistory[i].Status.Id == 4)
                                $scope.totalShipped = parseInt($scope.totalShipped) + parseInt($scope.productInventoryHistory[i].ReceivedOrShippedQuantity);
                        }

                    }, function(error) {
                        console.log(error);
                    });
                } else {
                    alert("Please Select a Product First.");
                    $scope.daysFilterId = 7;
                }

            };
            
            String.prototype.toCapitalize = function () {
                return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
            };

            $scope.save = function() {
                $scope.stock.CashMemoNo = $scope.stock.CashMemoNo.toCapitalize();

                if ($scope.stock.StatusId == 4 && $scope.stock.ReceivedOrShippedQuantity > $scope.product.OnHand) {
                    alert("You are trying to shipped more then your stock");
                } else {
                    $http.post(urlService.InventoryUrl + "/Create", JSON.stringify($scope.stock)).then(function(response) {
                        console.log(response);

                        if ($scope.stock.StatusId == 3) {
                            $scope.product.OnHand += $scope.stock.ReceivedOrShippedQuantity;
                            $scope.product.Received += $scope.stock.ReceivedOrShippedQuantity;
                        }

                        if ($scope.stock.StatusId == 4) {
                            $scope.product.OnHand -= $scope.stock.ReceivedOrShippedQuantity;
                            $scope.product.Shipped += $scope.stock.ReceivedOrShippedQuantity;
                        }

                        $scope.updateProduct();

                    }, function(error) {
                        console.log(error);
                    });
                }
            };

            $scope.updateProduct = function() {
                if ($scope.product.OnHand <= 0) {
                    $scope.product.StatusId = 2;
                } else if ($scope.product.OnHand > 0) {
                    $scope.product.StatusId = 1;
                }

                $scope.product.LastUpdate = new Date().toLocaleString();

                $http.put(urlService.ProductUrl + "/Update", JSON.stringify($scope.product)).then(function(response) {
                    console.log(response);
                    $scope.stock.CashMemoNo = "";
                    $scope.stock.ReceivedOrShippedQuantity = 0;
                    $scope.stock.StatusId = 0;
                    $scope.getProductInventoryHistory($scope.stock.ProductId);
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.getInventoryById = function(id) {
                $http.get(urlService.InventoryUrl + "/GetById", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    $scope.stock = response.data;
                    $scope.isUpdateMode = true;
                }, function(error) {
                    console.log(error);
                    $scope.isUpdateMode = true;
                });
            };

            $scope.edit = function(id) {
                $scope.getInventoryById(id);
            };

            $scope.update = function() {

                $http.put(urlService.InventoryUrl + "/Update", JSON.stringify($scope.stock)).then(function(response) {
                    console.log(response);
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.delete = function(id) {

            };


            //print instance model
            $scope.animationsEnabled = true;
            $scope.open = function (size) {
                var modalInstance = $uibModal.open({
                    animation: $scope.animationsEnabled,
                    templateUrl: "inventoryHistoryReportModal.html",
                    controller: "InventoryHistoryReportModalInstanceCtrl",
                    size: size,
                    resolve: {
                        reports: function() {
                            return $scope.productInventoryHistory;
                        },
                        dateRange: function() {
                            return $scope.dateFilter;
                        },
                        totalReceived: function() {
                            return $scope.totalReceived;
                        },
                        totalShipped: function() {
                            return $scope.totalShipped;
                        },
                        productName: function() {
                            return $scope.productName;
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
    .controller("InventoryHistoryReportModalInstanceCtrl", [
        "$scope", "$uibModalInstance", "reports", "dateRange", "totalReceived", "totalShipped", "productName",
        function ($scope, $uibModalInstance, reports, dateRange, totalReceived, totalShipped, productName) {

            $scope.reports = reports;
            $scope.dateRange = dateRange;
            $scope.totalReceived = totalReceived;
            $scope.totalShipped = totalShipped;
            $scope.productName = productName;
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