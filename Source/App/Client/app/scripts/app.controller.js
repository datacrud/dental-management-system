angular.module("dentalApp")
    .controller("AppControlller", [
        "$scope", "$http", "AppService", "$rootScope", "$state", "LocalDataStorageService",
        function ($scope, $http, appService, $rootScope, $state, localDataStorageService) {
            "use strict";


            var init = function () {
                $scope.isLoggedIn = localDataStorageService.getToken();
                $scope.user = localDataStorageService.getUserInfo();
                if ($scope.user !== null) {
                    $scope.isAdminUser = ($scope.user.RoleNames[0] === "Admin") ? true : false;
                    $scope.isSystemAdminUser = ($scope.user.RoleNames[0] === "SystemAdmin") ? true : false;
                }
            };

            $scope.logout = function() {
                localDataStorageService.logout();
                $rootScope.$broadcast('loggedOut');
                $state.go("root.login", {}, { reload: true });
            };


            $rootScope.$on('loggedIn', function (event, args) {
                console.log(event);
                init();
            });


            $rootScope.$on('loggedOut', function (event, args) {
                console.log(event);
                init();
            });


            $scope.homeState = function() {
                var responseToState = appService.nextRoute();

                $scope.isLoggedIn = responseToState.IsLoggedIn;
                $rootScope.$broadcast(responseToState.Broadcast);
                $state.go(responseToState.ToState);

            };


            $scope.copyright = new Date();
            $scope.version = "Version: 1.0.0";

       
            init();
        }
    ]);