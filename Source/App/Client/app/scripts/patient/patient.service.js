angular.module("dentalApp")
    .service("PatientService", [
        function () {
            "use strict";

            var self = this;
            self.PrescriptionId = null;
            self.PatientId = null;

            var setPrescriotionId = function(id) {
                self.PrescriptionId = id;
            };

            var getPrescriotid = function() {
                return self.PrescriptionId;
            };

            var setPatientId = function(id) {
                self.PatientId = id;
            };

            var getPatientId = function() {
                return self.PatientId;
            };


            return {
                setPrescriptionId: setPrescriotionId,
                getPrescriptionId: getPrescriotid,
                setPatientId: setPatientId,
                getPatientId: getPatientId
            };
        }
    ]);