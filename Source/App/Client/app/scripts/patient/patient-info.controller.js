angular.module("dentalApp")
    .controller("PatientInfoControlller", [
        "$scope", "$http", "UrlService",
        function ($scope, $http, urlService) {
            "use strict";

            $scope.init = function() {
                $scope.model = { Name: "", Charge: "", Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString() };
            };
            $scope.init();

            $scope.isUpdateMode = false;

            $scope.infos = [];
            $scope.getMedicalInfos = function() {
                $http.get(urlService.MedicalInfoUrl + "/GetAll").then(function (response) {
                    console.log(response);
                    $scope.infos = response;
                }, function (error) {
                    console.log(error);
                });
            };
            $scope.getMedicalInfos();

            String.prototype.toCapitalize = function () {
                return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
            };

            $scope.save = function() {
                $scope.model.Name = $scope.model.Name.toCapitalize();
                $scope.model.Created = new Date().toLocaleString();
                $scope.model.LastUpdate = new Date().toLocaleString();

                $http.post(urlService.MedicalInfoUrl + "/Create", JSON.stringify($scope.model)).then(function(response) {
                    console.log(response);
                    $scope.init();
                    $scope.getMedicalInfos();
                }, function(error) {
                    console.log(error);
                    alert("Service Add failed! Please try again.");
                });
            };


            $scope.getMedicalInfo = function(id) {
                $http.get(urlService.MedicalInfoUrl + "/GetById", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        $scope.model = response.data;
                        $scope.model.Charge = parseInt($scope.model.Charge);
                    }
                }, function(error) {
                    console.log(error);
                    $scope.isUpdateMode = false;
                });
            };


            $scope.edit = function(id) {
                $scope.isUpdateMode = true;
                $scope.getMedicalInfo(id);
            };

            $scope.update = function() {
                $scope.model.LastUpdate = new Date().toLocaleString();

                $http.put(urlService.MedicalInfoUrl + "/Update", JSON.stringify($scope.model)).then(function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        $scope.isUpdateMode = false;
                        $scope.init();
                        $scope.getMedicalInfos();
                    }
                }, function(error) {
                    console.log(error);
                    $scope.isUpdateMode = true;
                    alert("Update failed! Please try again.");
                });
            };


            $scope.delete = function(id) {
                var isDelete = confirm("Are you sure, you want to delete " + $scope.model.Name + " service?");

                if (isDelete) {
                    $http.delete(urlService.MedicalInfoUrl + "/Delete", { params: { request: id } }).then(function(response) {
                        console.log(response);
                        $scope.getMedicalInfos();
                    }, function(error) {
                        console.log(error);
                        alert("Delete failed! Please try again.");
                    });
                }
            };
        }
    ]);