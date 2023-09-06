angular.module("dentalApp")
    .controller("PatientServiceControlller", [
        "$scope", "$http", "UrlService",
        function ($scope, $http, urlService) {
            "use strict";

            $scope.init = function() {
                $scope.service = { Name: "", Charge: "", Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString() };
            };
            $scope.init();

            $scope.isUpdateMode = false;

            $scope.services = [];
            $scope.getMedicalServices = function () {
                $http.get(urlService.MedicalServiceUrl + "/GetAll").then(
                    function (response) {
                        console.log(response);
                        $scope.services = response;
                    }, function (error) {
                        console.log(error);
                    });
            };
            $scope.getMedicalServices();

            String.prototype.toCapitalize = function () {
                return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
            };

            $scope.save = function() {
                $scope.service.Name = $scope.service.Name.toCapitalize();
                $scope.service.Created = new Date().toLocaleString();
                $scope.service.LastUpdate = new Date().toLocaleString();

                $http.post(urlService.MedicalServiceUrl + "/Create", JSON.stringify($scope.service)).then(function(response) {
                    console.log(response);
                    $scope.init();
                    $scope.getMedicalServices();
                }, function(error) {
                    console.log(error);
                    alert("Service Add failed! Please try again.");
                });
            };


            $scope.getMedicalService = function(id) {
                $http.get(urlService.MedicalServiceUrl + "/GetById", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        $scope.service = response.data;
                        $scope.service.Charge = parseInt($scope.service.Charge);
                    }
                }, function(error) {
                    console.log(error);
                    $scope.isUpdateMode = false;
                });
            };


            $scope.edit = function(id) {
                $scope.isUpdateMode = true;
                $scope.getMedicalService(id);
            };

            $scope.update = function() {
                $scope.service.LastUpdate = new Date().toLocaleString();

                $http.put(urlService.MedicalServiceUrl + "/Update", JSON.stringify($scope.service)).then(function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        $scope.isUpdateMode = false;
                        $scope.init();
                        $scope.getMedicalServices();
                    }
                }, function(error) {
                    console.log(error);
                    $scope.isUpdateMode = true;
                    alert("Update failed! Please try again.");
                });
            };


            $scope.delete = function(id) {
                var isDelete = confirm("Are you sure, you want to delete " + $scope.service.Name + " service?");

                if (isDelete) {
                    $http.delete(urlService.MedicalServiceUrl + "/Delete", { params: { request: id } }).then(function(response) {
                        console.log(response);
                        $scope.getMedicalServices();
                    }, function(error) {
                        console.log(error);
                        alert("Delete failed! Please try again.");
                    });
                }
            };
        }
    ]);