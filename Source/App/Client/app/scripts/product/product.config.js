angular.module("dentalApp")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterProvider, $stateProvider) {
            "use strict";

            $stateProvider
                .state("root.product", {
                    url: "/product",
                    views: {
                        "" : {
                            templateUrl: "app/views/product/product.tpl.html",
                            controller: "ProductController"
                        }
                    }
                });
            //state("product-edit", {
            //    url: "/product/{Id:[a-zA-Z0-9-]}",
            //    templateUrl: "views/product/product.tpl.html",
            //    controller: "ProductController"
            //});
        }
    ]);