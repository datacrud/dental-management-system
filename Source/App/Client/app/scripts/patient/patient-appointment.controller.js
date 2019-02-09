angular.module("dentalApp")
    .controller("PatientAppointmentController", [
        "$scope", "$http", "$log", "$filter", "$uibModal", "UrlService",
        function ($scope, $http, $log, $filter, $uibModal, urlService) {
            "use strict";


            $scope.filter = { DateKey: new Date(), SearchKey: "" };
            var init = function() {
                $scope.appointment = { PatientNameOrId: "", Age: "", Phone: "", Date: new Date(), Time: new Date(), StatusId: 7, DoctorId: "9b6ba3ad-c9be-e511-9bf4-402cf40f4b2f", Created: new Date(), LastUpdate: new Date() };
                $scope.appointment.Date = $scope.filter.DateKey;
                $scope.isUpdateMode = false;
                $scope.appointments = [];
                $scope.loadedAppointment = [];
            };
            init();
            

            String.prototype.toCapitalize = function () {
                return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
            };

            /**
             * Datepicker
             */
            $scope.clear = function () {
                $scope.appointment.Date = null;
            };

            // Disable weekend selection
            //$scope.disabled = function (date, mode) {
            //    return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
            //};

            $scope.toggleMin = function () {
                $scope.minDate = $scope.minDate ? null : new Date();
            };
            $scope.toggleMin();

            $scope.maxDate = new Date(2050, 12, 31);

            $scope.open1 = function () {
                $scope.popup1.opened = true;
            };          

            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
            $scope.format = $scope.formats[0];
            $scope.altInputFormats = ['M!/d!/yyyy'];

            $scope.popup1 = {
                opened: false
            };


            /**
             * Timepicker
             */
            $scope.hstep = 1;
            $scope.mstep = 15;

            $scope.options = {
                hstep: [1, 2, 3],
                mstep: [1, 5, 10, 15, 25, 30]
            };

            $scope.ismeridian = true;
            $scope.toggleMode = function () {
                $scope.ismeridian = !$scope.ismeridian;
            };

            /**
             * end
             */


            $scope.loadAppointmentsByDate = function() {
                $scope.appointments = [];
                $http.get(urlService.AppointmentUrl + "/GetByDate", { params: { request: $scope.filter.DateKey.toLocaleString() } }).then(function(response) {
                    console.log(response);
                    $scope.appointments = response.data;
                }, function(error) {
                    console.log(error);
                });
            };
            $scope.loadAppointmentsByDate();

            $scope.loadAppointmentById = function(id) {
                $scope.loadedAppointment = [];
                $http.get(urlService.AppointmentUrl + "/GetById", { params: { request: id } }).then(function(response) {
                    console.log(response);
                    $scope.loadedAppointment = response.data;
                    if ($scope.isUpdateMode) {
                        $scope.appointment = $scope.loadedAppointment;
                        $scope.appointment.Date = $scope.appointment.Date;
                        $scope.appointment.Time = new Date($scope.appointment.Time);
                    }
                }, function(error) {
                    console.log(error);
                    $scope.isUpdateMode = true;
                });
            };
            

            $scope.loadThisDateAppointments = function () {
                $scope.filter.DateKey = $scope.appointment.Date;
                $scope.loadAppointmentsByDate();
            };

            $scope.save = function() {
                $scope.appointment.PatientNameOrId = $scope.appointment.PatientNameOrId.toCapitalize();
                $scope.appointment.Date = $scope.appointment.Date.toLocaleString();
                $scope.appointment.Time = $scope.appointment.Time.toLocaleString();
                $scope.appointment.Created = new Date().toLocaleString();
                $scope.appointment.LastUpdate = new Date().toLocaleString();

                $http.post(urlService.AppointmentUrl + "/Create", JSON.stringify($scope.appointment)).then(function(response) {
                    console.log(response);
                    $scope.filter.DateKey = $scope.appointment.Date;
                    $scope.loadAppointmentsByDate();
                    $scope.init();
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.edit = function(id) {
                $scope.isUpdateMode = true;
                $scope.loadAppointmentById(id);
            };

            $scope.update = function() {
                $scope.appointment.PatientNameOrId = $scope.appointment.PatientNameOrId.toCapitalize();
                $scope.appointment.LastUpdate = new Date().toLocaleString();
                $scope.appointment.Date = $scope.appointment.Date.toLocaleString();
                $scope.appointment.Time = $scope.appointment.Time.toLocaleString();

                $http.put(urlService.AppointmentUrl + "/Update", JSON.stringify($scope.appointment)).then(function(response) {
                    console.log(response);
                    $scope.filter.DateKey = $scope.appointment.Date;
                    $scope.loadAppointmentsByDate();
                    $scope.init();
                }, function(error) {
                    console.log(error);
                });
            };


            $scope.visited = function(id) {

                var approved = confirm("Are you sure, you want to move it into visited state");
                if (approved) {
                    $http.get(urlService.AppointmentUrl + "/GetById", { params: { request: id } }).then(function(response) {
                        console.log(response);
                        if (response.data !== "") {
                            response.data.StatusId = 8;
                            var appoint = response.data;
                            $http.put(urlService.AppointmentUrl + "/Update", JSON.stringify(response.data)).then(function(updateResponse) {
                                console.log(updateResponse);
                                $scope.filter.DateKey = appoint.Date;
                                $scope.loadAppointmentsByDate();
                                $scope.init();
                            }, function(error) {
                                console.log(error);
                            });
                        }
                    }, function(error) {
                        console.log(error);
                        $scope.isUpdateMode = true;
                    });
                }
            };


            $scope.loadPreviousDateAppointment = function () {
                $scope.appointment.Date = $scope.filter.DateKey;
                $scope.loadAppointmentsByDate();
            };

            $scope.search = function(key) {

                if (key === undefined) {
                    $scope.loadAppointmentsByDate();
                } else {
                    $http.get(urlService.AppointmentUrl + "/Search", { params: { request: key } }).then(function(response) {
                        console.log(response);
                        $scope.appointments = response.data;
                    }, function(error) {
                        console.log(error);
                    });
                }
            };


            //print instance model
            $scope.animationsEnabled = true;
            $scope.open = function (size, appointment) {
                var modalInstance = $uibModal.open({
                    animation: $scope.animationsEnabled,
                    templateUrl: "patientAppointmentModal.html",
                    controller: "PatientAppointmentModalInstanceCtrl",
                    size: size,
                    resolve: {
                        appointment: function () {
                            return appointment;
                        }
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
    .controller("PatientAppointmentModalInstanceCtrl", [
        "$scope", "$uibModalInstance", "appointment", 
        function ($scope, $uibModalInstance, appointment) {

            $scope.appointment = appointment;
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