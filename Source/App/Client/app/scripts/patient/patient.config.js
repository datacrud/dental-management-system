angular.module("dentalApp")
    .config([
        "$urlRouterProvider", "$stateProvider",
        function ($urlRouterProvider, $stateProvider) {
            "use strict";

            $stateProvider.state("root.patient", {
                    //abustract: true,
                    url: "/patient",
                    views: {
                        "": {
                            templateUrl: "app/views/patient/patient.tpl.html",
                            controller: "PatientController"
                        }
                    }
                })
                .state("root.patient-create", {
                    url: "/patient/create",
                    views: {
                        "": {
                            templateUrl: "app/views/patient/patient-create.tpl.html",
                            controller: "PatientCreateController"
                        }
                    }
                })
                .state("root.patient-detail", {
                    url: "/patient/{patientId:[P]+[0-9]{1,30}}",
                    views: {
                        "": {
                            templateUrl: "app/views/patient/patient-detail.tpl.html",
                            controller: "PatientDetailControlller"
                        }
                    }
                })
                .state("root.patient-service", {
                    url: "/patient/service",
                    views: {
                        "" : {
                            templateUrl: "app/views/patient/patient-service.tpl.html",
                            controller: "PatientServiceControlller"
                        }
                    }
                })
                .state("root.patient-report", {
                    url: "/patient/report",
                    views: {
                        "" : {
                            templateUrl: "app/views/patient/patient-report.tpl.html",
                            controller: "PatientReportController"
                        }
                    }
                })
                .state("root.patient-appointment", {
                    url: "/patient/appointment",
                    views: {
                        "": {
                            templateUrl: "app/views/patient/patient-appointment.tpl.html",
                            controller: "PatientAppointmentController"
                        }
                    }
                });
        }
    ]);