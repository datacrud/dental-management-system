angular.module("dentalApp")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterProvider, $stateProvider) {
            "use strict";

            $stateProvider
                .state("root.stock", {
                    url: "/inventory",
                    views: {
                        "" : {
                            templateUrl: "app/views/stock/stock.tpl.html",
                            controller: "StockController"
                        }
                    }
                })
                .state("root.stock-report", {
                    url: "/inventory/report",
                    views: {
                        "" : {
                            templateUrl: "app/views/stock/stock-report.tpl.html",
                            controller: "StockReportController"
                        }
                    }
                });
        }
    ]);