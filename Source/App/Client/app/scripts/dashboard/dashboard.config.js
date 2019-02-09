angular.module("dentalApp")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterProvider, $stateProvider) {
            "use strict";

            $stateProvider
                .state("root.dashboard", {
                    url: "/",
                    views: {
                        "" : {
                            templateUrl: "app/views/dashboard/dashboard.tpl.html",
                            controller: "DashboardController"
                        }
                    }
                });
        }
    ]);