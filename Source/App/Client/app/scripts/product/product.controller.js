angular.module("dentalApp")
    .controller("ProductController", [
        "$scope", "$http", "AppService", "UrlService", "toaster",
        function ($scope, $http, appService, urlService, toaster) {
            "use strict";

            $scope.init = function() {
                $scope.product = { Code: "", Name: "", StartingInventory: 0, Received: 0, Shipped: 0, OnHand: 0, MinimumRequired: 0, UnitPrice: 0, SalePrice: 0, Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString(), StatusId: 0 };
            };
            $scope.init();
            $scope.isUpdateMode = false;            

            $scope.products = [];

            $scope.getProducts = function () {
                $http.get(urlService.ProductUrl + "/GetProducts").then(
                    function (response) {
                        console.log(response);
                        $scope.products = response.data;
                    }, function (error) {
                        console.log(error);
                    });
            };
            $scope.getProducts();

            String.prototype.toCapitalize = function () {
                return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
            };

            $scope.save = function() {
                $scope.product.Code = $scope.product.Code.toUpperCase();
                $scope.product.Name = $scope.product.Name.toCapitalize();
                $scope.product.Received = $scope.product.StartingInventory;
                $scope.product.OnHand = $scope.product.StartingInventory;
                $scope.product.StatusId = 1;

                $http.post(urlService.ProductUrl + "/Create", JSON.stringify($scope.product)).then(function(response) {
                        console.log(response);
                        $scope.getProducts();
                        $scope.init();
                    },
                    function(error) {
                        console.log(error);
                    }
                );
            };

            $scope.getProduct = function(id) {
                $scope.product = [];
                $http.get(urlService.ProductUrl + "/GetById?request=" + id).then(function(response) {
                    console.log(response);
                    $scope.product = response.data;
                    $scope.isUpdateMode = true;
                }, function(error) {
                    console.log(error);
                    $scope.isUpdateMode = false;
                });
            };

            $scope.edit = function(id) {
                $scope.getProduct(id);
            };

            $scope.update = function() {
                $scope.product.Code = $scope.product.Code.toUpperCase();
                $scope.product.Name = $scope.product.Name.toCapitalize();
                $scope.product.LastUpdate = new Date().toLocaleString();

                $http.put(urlService.ProductUrl + "/Update", JSON.stringify($scope.product)).then(function(response) {
                        console.log(response);
                        $scope.getProducts();
                        $scope.init();
                        $scope.isUpdateMode = false;
                    },
                    function(error) {
                        console.log(error);
                    });
            };

            $scope.delete = function (id) {
                $http.get(urlService.ProductUrl + "/GetById?request=" + id).then(function (product) {
                    console.log(product);

                    var bool = confirm("Are you sure, you want to delete " + product.Name + " ?");
                    if (bool === true) {
                        $http.delete(urlService.ProductUrl + "/Delete?request=" + id).then(function (response) {
                            console.log(response);
                            $scope.getProducts();
                        }, function (error) {
                            console.log(error);
                        });
                    }
                }, function (error) {
                    console.log(error);
                    toaster.pop("error","Failed to delete this product! Please try again.");
                });
            };

            $scope.key = "";

            $scope.search = function () {
                $http.get(urlService.ProductUrl + "/SearchProduct", { params: { request: $scope.key } }).then(
                    function (response) {
                        console.log(response);
                        $scope.products = response.data;
                    }, function (error) {
                        console.log(error);
                    });
            };

            $scope.isKeyNull = function(key) {
                if (key === undefined)
                    $scope.getProducts();
                else
                    $scope.search();
            };
        }
    ]);