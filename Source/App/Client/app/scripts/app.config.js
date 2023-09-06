angular.module("dentalApp", ["ui.router", "ngGrid", "ui.bootstrap", "checklist-model", "cgBusy", "toaster", "ngAnimate"])
    .run([
        "$rootScope", "$state", "$stateParams", "AuthService",
        function ($rootScope, $state, $stateParams, authService) {
            "use strict";

            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;

            $rootScope.$on("$stateChangeStart", function(event, toState, toStateParams) {

                var isLogin = toState.name === "root.login";
                if (isLogin) return;

                var isAccessDenied = toState.name === "root.access-denied";
                if (isAccessDenied) return;


                var authzSuccessCallback = function(isAuthorized) {
                    if (!isAuthorized) {
                        event.preventDefault();
                        $state.go("root.access-denied");
                    }
                };
                var authzErrorCallback = function(error) {
                    console.log(error);
                    event.preventDefault();
                    $state.go("root.access-denied");
                };
                var isAuthorized = function() {
                    authService.authorize(toState.name).then(authzSuccessCallback, authzErrorCallback);
                };


                var authnSuccessCallback = function(isAuthenticated) {
                    if (isAuthenticated) {
                        isAuthorized();
                    } else {
                        event.preventDefault();
                        $state.go("root.login");
                    }
                };
                var authnErrorCallback = function(error) {
                    console.log(error);
                    event.preventDefault();
                    $state.go("root.login");
                };
                authService.authenticate().then(authnSuccessCallback, authnErrorCallback);
               

            });
        }
    ])
    .config([
        "$urlRouterProvider", "$stateProvider", "$httpProvider", "$qProvider",
        function ($urlRouterProvider, $stateProvider, $httpProvider, $qProvider) {
            "use strict";

            //$qProvider.errorOnUnhandledRejections(false);

            $httpProvider.interceptors.push('tokenInterceptor');

            $urlRouterProvider.otherwise("/patient");

            $stateProvider
                .state("root", {
                    abstract: true,
                    url: "",
                    template: "<div ui-view class=\"container-fluid slide\"></div>",
                    //controller: "AppController"
                })
                .state("root.about", {
                    url: "/about",
                    views: {
                        "": {
                            templateUrl: "app/views/about/about.tpl.html",
                            controller: "AboutController"
                        }
                    }
                })
                .state("root.contact", {
                    url: "/contact",
                    views: {
                        "": {
                            templateUrl: "app/views/contact/contact.tpl.html",
                            controller: "ContactController"
                        }
                    }
                });
        }
    ]);


angular.module("dentalApp")
    .factory("tokenInterceptor", [
        "LocalDataStorageService",
        function (localDataStorageService) {

            var haderInjector = {
                request: function (config) {
                    if (localDataStorageService.getToken() !== null)
                        config.headers["Authorization"] = localDataStorageService.getToken();

                    return config;
                }
            };

            return haderInjector;
        }
    ]);