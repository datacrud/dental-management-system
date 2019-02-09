angular.module("dentalApp")
    .controller("PatientDetailControlller", [
        "$scope", "$http", "UrlService", "$stateParams", "PatientService", "$state", "$uibModal", "$log",
        function ($scope, $http, urlService, $stateParams, patientService, $state, $uibModal, $log) {
            "use strict";

            $scope.init = function() {
                $scope.payment = { PrescriptionId: "", Amount: 0, Comment: "", Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString() };
            };
            $scope.init();

            $scope.isUpdateMode = false;

            $scope.patientInfo = [];
            $scope.loadPatientInfo = function() {
                var code = $stateParams.patientId;
                $scope.patientInfo = [];

                $http.get(urlService.PatientCreateUrl + "/GetPatientByCode", { params: { request: code } }).then(function(response) {
                    console.log(response);
                    $scope.patientInfo = response.data;
                    $scope.loadPatientServices($scope.patientInfo.Id);
                }, function(error) {
                    console.log(error);
                    $scope.patientInfo = [];
                });
            };
            $scope.loadPatientInfo();

            $scope.update = function(patientInfo) {
                $http.put(urlService.PatientCreateUrl + "/Update", JSON.stringify(patientInfo)).then(function(response) {
                    console.log(response);
                    alert("Patient Info Successfully Updated");
                    $scope.loadPatientInfo();
                }, function(error) {
                    console.log((error));
                    alert("Patient Info Update Failed");
                    $scope.loadPatientInfo();
                });
            };

            $scope.patientPrescription = [];
            $scope.loadPatientServices = function(patientId) {
                $scope.patientPrescription = [];
                $http.get(urlService.PrescriptionUrl + "/GetPatientCurrentPrescription", { params: { request: patientId } }).then(function(response) {
                    console.log(response);
                    $scope.patientPrescription = response.data;
                    $scope.loadPatientPrescriptionMedicalServices($scope.patientPrescription.Id);
                    $scope.loadPayments($scope.patientPrescription.Id);
                }, function(error) {
                    console.log(error);
                    $scope.patientPrescription = [];
                });
            };

            $scope.patientPrescriptionMedicalServices = [];
            $scope.loadPatientPrescriptionMedicalServices = function(prescriptionId) {
                $scope.patientPrescriptionMedicalServices = [];
                $http.get(urlService.PatientDetailUrl + "/GetPatientPrescriptionMedicalServices", { params: { request: prescriptionId } }).then(function(response) {
                    console.log(response);
                    $scope.patientPrescriptionMedicalServices = response.data;
                }, function(error) {
                    console.log(error);
                    $scope.patientPrescriptionMedicalServices = [];
                });
            };

            $scope.payments = [];
            $scope.loadPayments = function(id) {
                $http.get(urlService.PaymentUrl + "/GetByPrescriptionId", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    $scope.payments = response.data;
                }, function(error) {
                    console.log(error);
                    $scope.payments = [];
                });
            };

            $scope.savePayment = function() {
                $scope.payment.Created = new Date().toLocaleString();
                $scope.payment.LastUpdate = new Date().toLocaleString();
                $scope.payment.PrescriptionId = $scope.patientPrescription.Id;

                if ($scope.payment.Amount > $scope.patientPrescription.TotalDue) {
                    alert("You are trying to paid more then due amount");
                } else {
                    $http.post(urlService.PaymentUrl + "/Create", JSON.stringify($scope.payment)).then(function(response) {
                        console.log(response);
                        $scope.loadPayments($scope.payment.PrescriptionId);

                        $scope.patientPrescription.TotalPaid = parseFloat($scope.patientPrescription.TotalPaid) + parseFloat($scope.payment.Amount);
                        $scope.patientPrescription.TotalDue = parseFloat($scope.patientPrescription.TotalDue) - parseFloat($scope.payment.Amount);

                        $scope.updatePatientPrescription();

                    }, function(error) {
                        console.log(error);
                    });
                }
            };


            $scope.updatePatientPrescription = function() {
                $scope.patientPrescription.LastUpdate = new Date().toLocaleString();

                $http.put(urlService.PrescriptionUrl + "/Update", JSON.stringify($scope.patientPrescription)).then(function(response) {
                    console.log(response);
                    $scope.loadPatientServices($scope.patientInfo.Id);
                    $scope.init();
                    $scope.payment.Created = new Date().toLocaleString();
                }, function(error) {
                    console.log(error);
                });
            };

            //$scope.editedPayment = [];
            //$scope.editPayment = function (payment) {
            //    $scope.editedPayment = angular.copy(payment);

            //    $scope.isUpdateMode = true;
            //    $scope.payment = payment;

            //    $http.put(urlService.PaymentUrl + "/Update", JSON.stringify($scope.payment)).then(function(response) {
            //        console.log(response);
            //    }, function(error) {
            //        console.log(error);
            //    });
            //}

            $scope.deletedPayment = [];
            $scope.loadPaymentById = function(id) {
                $scope.deletedPayment = [];
                $http.get(urlService.PaymentUrl + "/GetById", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    $scope.deletedPayment = response.data;
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.deletePayment = function(id) {
                $scope.loadPaymentById(id);
                var isConfirm = confirm("Are you sure, you want to delete this payment information?");

                if (isConfirm) {
                    $http.delete(urlService.PaymentUrl + "/Delete", { params: { request: id } }).then(function(response) {
                        console.log(response);
                        $scope.patientPrescription.TotalPaid = parseFloat($scope.patientPrescription.TotalPaid) - parseFloat($scope.deletedPayment.Amount);
                        $scope.patientPrescription.TotalDue = parseFloat($scope.patientPrescription.TotalDue) + parseFloat($scope.deletedPayment.Amount);

                        $scope.updatePatientPrescription();

                        $scope.loadPayments($scope.patientPrescription.Id);
                    }, function(erroe) {
                        console.log(error);
                    });
                }
            };


            $scope.addMoreService = function() {
                patientService.setPatientId($scope.patientInfo.Id);
                $state.go("root.patient-create");
            };

            $scope.generatePatientBill = function() {
                $scope.patientPrescription.LastUpdate = new Date().toLocaleString();
                $scope.patientPrescription.StatusId = 6;

                $http.put(urlService.PrescriptionUrl + "/Update", JSON.stringify($scope.patientPrescription)).then(function(response) {
                    console.log(response);

                    var prescription = { Code: "", PatientId: $scope.patientPrescription.PatientId, Created: new Date().toLocaleString(), LastUpdate: new Date().toLocaleString(), TotalCharge: 0, DiscountPercent: 0, DiscountAmount: 0, TotalPayable: 0, TotalPaid: 0, TotalDue: 0, StatusId: 5 };

                    $http.post(urlService.PrescriptionUrl + "/Create", JSON.stringify(prescription)).then(function(response) {
                        console.log(response);
                        $scope.loadPatientInfo();
                        alert("This bill closed successfully. A new bill has been auto generated for this patient.");
                    }, function(error) {
                        console.log(error);
                        alert("Patient bill close failed! Please, try again.");
                    });

                }, function(error) {
                    console.log(error);
                });
            };

            $scope.newBill = function() {

                if ($scope.patientPrescription.TotalDue > 0) {
                    alert("You can not close this current bill to open a new bill due to due payment.\n Please clear the due payment first.");
                } else {

                    var isConfirm = confirm("Are you sure, do you realy want to close this active bill now?");

                    if (isConfirm) {
                        $scope.generatePatientBill();
                    }
                }

            };

            $scope.forceNewBill = function() {
                var isConfirm = confirm("Are you sure, do you realy want to close this active bill now?");

                if (isConfirm) {
                    $scope.generatePatientBill();
                }
            };

            //$scope.printDiv = function(divName) {
            //    var printContents = document.getElementById(divName).innerHTML;
            //    var originalContents = document.body.innerHTML;
            //    var popupWin;
            //    if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
            //        popupWin = window.open('', '_blank', 'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
            //        popupWin.window.focus();
            //        popupWin.document.write('<!DOCTYPE html><html><head>' + '<link href="Content/bootstrap/bootstrap.min.css" type="text/css" rel="stylesheet"/>' + '</head><body onload="window.print()"><div class="reward-body">' + printContents + '</div></html>');
            //        popupWin.onbeforeunload = function(event) {
            //            popupWin.close();
            //            return '.n';
            //        };
            //        popupWin.onabort = function(event) {
            //            popupWin.document.close();
            //            popupWin.close();
            //        }
            //    } else {
            //        popupWin = window.open('', '_blank', 'width=800,height=600');
            //        popupWin.document.open();
            //        popupWin.document.write('<html><head><link href="../Content/bootstrap/bootstrap.min.css" type="text/css" rel="stylesheet"/></head><body onload="window.print()">' + printContents + '</html>');
            //        popupWin.document.close();
            //    }
            //    popupWin.document.close();
            //    return true;
            //};

            //$scope.printDiv = function (divName) {
            //    var printContents = document.getElementById(divName).innerHTML;
            //    var popupWin = window.open('', '_blank', 'width=300,height=300');
            //    popupWin.document.open();
            //    popupWin.document.write('<html><head><link href="../Content/bootstrap/bootstrap.min.css" type="text/css" rel="stylesheet"/></head><body onload="window.print()">' + printContents + '</html>');
            //    popupWin.document.close();
            //}


            //print instance model
            $scope.animationsEnabled = true;
            $scope.open = function (size, payment) {
                var modalInstance = $uibModal.open({
                    animation: $scope.animationsEnabled,
                    templateUrl: "patientPaymentModal.html",
                    controller: "PatientPaymentModalInstanceCtrl",
                    size: size,
                    resolve: {
                        payment: function () {
                            return payment;
                        },
                        patient: function () {
                            return $scope.patientInfo;
                        },
                        patientBill: function () {
                            return $scope.patientPrescription;
                        },

                    }
                });

                modalInstance.result.then(function (selectedItem) {
                    $scope.selected = selectedItem;
                }, function () {
                    $log.info("Modal dismissed at: " + new Date());
                });
            };
        }
    ]);

angular.module("dentalApp")
    .controller("PatientPaymentModalInstanceCtrl", [
        "$scope", "$uibModalInstance", "payment", "patient", "patientBill",
        function ($scope, $uibModalInstance, payment, patient, patientBill) {

            $scope.patient = patient;
            $scope.payment = payment;
            $scope.patientBill = patientBill;
            $scope.today = new Date();

            $scope.ok = function () {
                $uibModalInstance.close();
            };            


            $scope.printDiv = function (divName) {
                var printContents = document.getElementById(divName).innerHTML;
                var originalContents = document.body.innerHTML;
                var popupWin;
                if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
                    popupWin = window.open('', '_blank', 'width=750,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
                    popupWin.window.focus();
                    popupWin.document.write('<!DOCTYPE html><html><head>' + '<link href="Content/bootstrap/bootstrap.min.css" type="text/css" rel="stylesheet"/>' + '</head><body onload="window.print()"><div style="width: 800px; height:auto;">' + printContents + '</div></html>');
                    popupWin.onbeforeunload = function (event) {
                        popupWin.close();
                        return '.n';
                    };
                    popupWin.onabort = function(event) {
                        popupWin.document.close();
                        popupWin.close();
                    };
                } else {
                    popupWin = window.open('', '_blank', 'width=750,height=600');
                    popupWin.document.open();
                    popupWin.document.write('<html><head><link href="../Content/bootstrap/bootstrap.min.css" type="text/css" rel="stylesheet"/></head><body onload="window.print()">' + printContents + '</html>');
                    popupWin.document.close();
                }
                popupWin.document.close();
                $scope.cancel();
                return true;
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss("cancel");
            };
        }
    ]);