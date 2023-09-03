angular.module("dentalApp")
    .controller("PatientCreateController", [
        "$scope", "$http", "PatientService", "$state", "UrlService", "toaster",
        function ($scope, $http, PatientService, $state, urlService, toaster) {
            "use strict";

            $scope.init = function() {
                $scope.patient = { Name: "", Age: "", Gender: "", Phone: "", Email: "", Address: "", Note: "", Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString() };
                $scope.patientMedicalService = { PatientId: "", PrescriptionId: "", MedicalServiceId: "", Quantity: 1, Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString() };
                $scope.service = { Charge: 0, DiscountPercent: 0, DiscountAmount: 0, FixedDiscount: 0, TotalDiscountAmount: 0, TotalPayable: 0, TotalPaid: 0, TotalDue: 0 };
                $scope.selection = [];

                $scope.gender = [
                    "Male",
                    "Female",
                    "Others"
                ];

                $scope.pageName = 'new-patient';
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
                    $scope.service.FixedDiscount = $scope.patientPrescription.FixedDiscount;

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

                        $scope.pageName = 'add-services';
                    }
                }, function(error) {
                    console.log(error);
                    toaster.pop("error", "Patient create faild! Plese try again");
                });
            };

            $scope.calculateDiscount = function() {
                if ($scope.service.DiscountPercent > 100 || $scope.service.DiscountPercent < 0) {
                    toaster.pop('warning', "Hey, Your are trying to give an impossible discount to this patient. Discount must be between 0% to 100 %");
                } else {
                    var dp = $scope.service.DiscountPercent == "" ? 0 : parseFloat($scope.service.DiscountPercent);
                    $scope.service.DiscountAmount = parseFloat(parseFloat($scope.service.Charge) * ( dp / 100)).toFixed(2);                    
                }

                var fd = $scope.service.FixedDiscount == "" ? 0 : parseFloat($scope.service.FixedDiscount);
                $scope.service.TotalDiscountAmount = parseFloat(parseFloat($scope.service.DiscountAmount) + fd).toFixed(2);

                $scope.service.TotalPayable = parseFloat(parseFloat($scope.service.Charge) - parseFloat($scope.service.TotalDiscountAmount)).toFixed(2);
                $scope.service.TotalDue = parseFloat(parseFloat($scope.service.TotalPayable) - parseFloat($scope.service.TotalPaid)).toFixed(2);
            };

            $scope.calculateCharge = function (index) {
                $scope.medicalServices[index].TotalCharge = parseInt($scope.medicalServices[index].Quantity) * parseInt($scope.medicalServices[index].Charge);

                $scope.calculateTotalCharge();
            };

           
            $scope.calculateTotalCharge = function () {
                let totalCharge = 0;
                $scope.patientMedicalServices = [];

                for (let i = 0; i < $scope.selection.length; i++) {

                    let medicalService = $scope.selection[i];

                    totalCharge += medicalService.TotalCharge;

                    var service = angular.copy($scope.patientMedicalService);
                    service.MedicalServiceId = medicalService.Id;
                    service.Quantity = medicalService.Quantity;
                    service.Created = new Date().toLocaleString();
                    service.LastUpdate = new Date().toLocaleString();
                    $scope.patientMedicalServices.push(service);
                }

                $scope.service.Charge = parseFloat(totalCharge).toFixed(2);
                $scope.service.TotalPayable = $scope.service.Charge;

                $scope.calculateDiscount();

                $scope.service.TotalDue = parseFloat($scope.service.TotalPayable) - parseFloat($scope.service.TotalPaid);                
            }
                       
            $scope.toggleSelection = function(medicalService) {
                var idx = $scope.selection.indexOf(medicalService);
                
                if (idx > -1) {
                    $scope.selection.splice(idx, 1);

                } else {
                    $scope.selection.push(medicalService);
                }

                $scope.calculateTotalCharge();                
            };

            $scope.loadPatientMedicalServices = function(id) {
                $http.get(urlService.PatientMedicalServiceUrl + "/GetByPrescriptionId", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    var patientMedicalServices = response.data;

                    for (var j = 0; j < patientMedicalServices.length; j++) {
                        var pms = patientMedicalServices[j];
                        for (var i = 0; i < $scope.medicalServices.length; i++) {

                            if (pms.MedicalServiceId == $scope.medicalServices[i].Id) {

                                $scope.medicalServices[i].Quantity = pms.Quantity;
                                $scope.medicalServices[i].TotalCharge = parseInt($scope.medicalServices[i].Quantity) * parseInt($scope.medicalServices[i].Charge);

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
                $scope.patientPrescription.FixedDiscount = $scope.service.FixedDiscount;
                $scope.patientPrescription.TotalPayable = $scope.service.TotalPayable;
                $scope.patientPrescription.TotalPaid = $scope.service.TotalPaid;
                $scope.patientPrescription.TotalDue = parseFloat($scope.service.TotalPayable) - parseFloat($scope.service.TotalPaid);
                $scope.patientPrescription.LastUpdate = new Date().toLocaleString();

                $http.put(urlService.PrescriptionUrl + "/Update", JSON.stringify($scope.patientPrescription)).then(function(response) {
                    console.log(response);

                    $scope.backToPatientDetail();
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.savePatientMedicalService = function() {
                $http.post(urlService.PatientMedicalServiceUrl + "/CreateList", JSON.stringify($scope.patientMedicalServices)).then(function(response) {
                    console.log(response);
                    $scope.updatePrescription();
                    //alert("Service added successfully");
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

            $scope.loadPatientId = function () {
                $scope.patientId = PatientService.getPatientId();
                $scope.pageName = PatientService.getPageName();

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

            $scope.backToPatientDetail = function () {
                $state.go("root.patient-detail", { patientId: $scope.patientSuccess.Code });
            };
        }
    ]);