angular.module("dentalApp")
    .controller("PatientCreateController", [
        "$scope", "$http",  "PatientService", "$state", "UrlService",
        function ($scope, $http, PatientService, $state, urlService) {
            "use strict";

            $scope.init = function() {
                $scope.patient = { Name: "", Age: "", Phone: "", Email: "", Address: "", Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString() };
                $scope.patientMedicalService = { PatientId: "", PrescriptionId: "", MedicalServiceId: "", Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString() };
                $scope.service = { Charge: 0, DiscountPercent: 0, DiscountAmount: 0, TotalPayable: 0, TotalPaid: 0, TotalDue: 0 };
                $scope.selection = [];
            };
            $scope.init();

            $scope.isUpdateMode = false;


            String.prototype.toCapitalize = function () {
                return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
            };
            

            $scope.medicalServices = [];
            $scope.getMedicalServices = function() {
                $scope.medicalServices = [];
                $http.get(urlService.MedicalServiceUrl + "/GetAllOrderByName").success(function(response) {
                    console.log(response);
                    $scope.medicalServices = response;
                }).error(function(error) {
                    console.log(error);
                });
            };
            $scope.getMedicalServices();


            $scope.getPatientCurrentPrescription = function(id) {
                $http.get(urlService.PrescriptionUrl + "/GetPatientCurrentPrescription", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    $scope.patientPrescription = response.data;
                    $scope.patientMedicalService.PrescriptionId = $scope.patientPrescription.Id;
                    $scope.service.DiscountPercent = $scope.patientPrescription.DiscountPercent;

                    $scope.service.TotalPaid = $scope.patientPrescription.TotalPaid;
                    $scope.service.TotalDue = $scope.patientPrescription.TotalDue;

                    $scope.loadPatientMedicalServices($scope.patientPrescription.Id);
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.patientSuccess = [];
            $scope.getPatient = function(id) {
                $scope.patientSuccess = [];
                $http.get(urlService.PatientCreateUrl + "/GetById", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    $scope.patientSuccess = response.data;
                    $scope.patientMedicalService.PatientId = $scope.patientSuccess.Id;

                    $scope.getPatientCurrentPrescription($scope.patientSuccess.Id);

                }, function(error) {
                    console.log(error);
                });
            };
            //$scope.getPatient("450e40c8-02b7-e511-9bf1-402cf40f4b2f");//temp

            $scope.addPatient = function() {
                $scope.patient.Name = $scope.patient.Name.toCapitalize();
                $scope.patient.Created = new Date().toLocaleString();
                $scope.patient.LastUpdate = new Date().toLocaleString();

                $http.post(urlService.PatientCreateUrl + "/Create", JSON.stringify($scope.patient)).then(function(response) {
                    console.log(response);
                    if (response.data) {
                        $scope.getPatient(response.data);
                        $scope.init();
                    }
                }, function(error) {
                    console.log(error);
                    alert("Patient create faild! Plese try again");
                });
            };

            $scope.calculateDiscount = function() {
                if ($scope.service.DiscountPercent > 100 || $scope.service.DiscountPercent < 0) {
                    alert("Hey, Your are trying to give an impossible discount to this patient. Discount must be between 0% to 100 %");
                } else {
                    $scope.service.DiscountAmount = parseFloat(parseFloat($scope.service.Charge) * (parseFloat($scope.service.DiscountPercent) / 100)).toFixed(2);
                    $scope.service.TotalPayable = parseFloat(parseFloat($scope.service.Charge) - parseFloat($scope.service.DiscountAmount)).toFixed(2);
                    $scope.service.TotalDue = parseFloat(parseFloat($scope.service.TotalPayable) - parseFloat($scope.service.TotalPaid)).toFixed(2);
                }
            };
                       
            $scope.patientMedicalServices = [];
            $scope.toggleSelection = function(medicalService) {
                var idx = $scope.selection.indexOf(medicalService);

                if (idx > -1) {
                    $scope.selection.splice(idx, 1);

                    for (var i = 0; i < $scope.patientMedicalServices.length; i++) {
                        if ($scope.patientMedicalServices[i].MedicalServiceId == medicalService.Id) {
                            $scope.patientMedicalServices.splice(i, 1);
                        }                            
                    }                    
                    $scope.service.Charge = parseFloat(parseFloat($scope.service.Charge) - parseFloat(medicalService.Charge)).toFixed(2);
                    $scope.service.TotalPayable = $scope.service.Charge;                    
                    $scope.calculateDiscount();
                }
                else {
                    $scope.selection.push(medicalService);
                    
                    $scope.service.Charge = parseFloat(parseFloat($scope.service.Charge) + parseFloat(medicalService.Charge)).toFixed(2);
                    $scope.service.TotalPayable = $scope.service.Charge;
                    $scope.calculateDiscount();

                    var service = angular.copy($scope.patientMedicalService);
                    service.MedicalServiceId = medicalService.Id;
                    service.Created = new Date().toLocaleString();
                    service.LastUpdate = new Date().toLocaleString();                    
                    $scope.patientMedicalServices.push(service);
                }
                $scope.service.TotalDue = parseFloat($scope.service.TotalPayable) - parseFloat($scope.service.TotalPaid);
            };

            $scope.loadPatientMedicalServices = function(id) {
                $http.get(urlService.PatientMedicalServiceUrl + "/GetByPrescriptionId", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    var patientMedicalServices = response.data;

                    for (var j = 0; j < patientMedicalServices.length; j++) {
                        for (var i = 0; i < $scope.medicalServices.length; i++) {
                            if (patientMedicalServices[j].MedicalServiceId == $scope.medicalServices[i].Id) {
                                $scope.toggleSelection($scope.medicalServices[i]);
                                break;
                            }
                        }
                    }

                }, function(error) {
                    console.log(error);
                });
            };

            $scope.updatePrescription = function() {
                $scope.patientPrescription.TotalCharge = $scope.service.Charge;
                $scope.patientPrescription.DiscountPercent = $scope.service.DiscountPercent;
                $scope.patientPrescription.DiscountAmount = $scope.service.DiscountAmount;
                $scope.patientPrescription.TotalPayable = $scope.service.TotalPayable;
                $scope.patientPrescription.TotalPaid = $scope.service.TotalPaid;
                $scope.patientPrescription.TotalDue = parseFloat($scope.service.TotalPayable) - parseFloat($scope.service.TotalPaid);
                $scope.patientPrescription.LastUpdate = new Date().toLocaleString();

                $http.put(urlService.PrescriptionUrl + "/Update", JSON.stringify($scope.patientPrescription)).then(function(response) {
                    console.log(response);
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.savePatientMedicalService = function() {
                $http.post(urlService.PatientMedicalServiceUrl + "/CreateList", JSON.stringify($scope.patientMedicalServices)).then(function(response) {
                    console.log(response);
                    $scope.updatePrescription();
                    alert("Service added successfully");
                }, function(error) {
                    console.log(error);
                });
            };

            //$scope.getMedicalService = function (id) {
            //    $http.get(urlService.PatientCreateUrl + "/GetById", { params: { request: id } }).then(function (response) {
            //        console.log(response);
            //    }, function (error) {
            //        console.log(error);
            //    });
            //}


            //$scope.edit = function (id) {
            //    $scope.getMedicalService(id);
            //}

            //$scope.update = function () {
            //    $http.put(urlService.PatientCreateUrl + "/Update", JSON.stringify($scope.patient)).then(function (response) {
            //        console.log(response);
            //    }, function (error) {
            //        console.log(error);
            //    });
            //}


            //$scope.delete = function (id) {
            //    $http.delete(urlService.PatientCreateUrl + "/Delete", { params: { request: id } }).then(function (response) {
            //        console.log(response);
            //    }, function (error) {
            //        console.log(error);
            //    });
            //}

            $scope.loadPatientId = function() {
                $scope.patientId = PatientService.getPatientId();

                if ($scope.patientId !== null) {
                    $scope.getPatient($scope.patientId);
                }
            };
            $scope.loadPatientId();

            $scope.addPayment = function() {
                if ($scope.patientSuccess.Id === null) {
                    alert("You haven't added a patient yet. Please add a pateint first");
                } else {
                    $state.go("root.patient-detail", { patientId: $scope.patientSuccess.Code });
                }
            };
        }
    ]);