using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.RequestModels
{
    public static class HelperRequestModel
    {        

        public static string GenerateBillCode(string patientCode, string right)
        {
            int len = right.Length;

            for (int i = 0; i < 3 - len; i++)
            {
                right = string.Concat("0", right);
            }

            string prescriptionId = string.Concat("BILL", right);

            return string.Concat(prescriptionId + "-", patientCode);
        }




        public static string GetThisPatientCode(string right)
        {
            int len = right.Length;

            for (int i = 0; i < 6 - len; i++)
            {
                right = string.Concat("0", right);
            }
            string code = string.Concat("P", right);

            return code;
        }

        public static string GenerateAppointmentCode(string right)
        {
            int len = right.Length;

            for (int i = 0; i < 3 - len; i++)
            {
                right = string.Concat("0", right);
            }
            string code = string.Concat("AP", right);

            return code;
        }


        public static string GeneratePrescriptionCode(string patientCode, string right)
        {
            int len = right.Length;

            for (int i = 0; i < 3 - len; i++)
            {
                right = string.Concat("0", right);
            }

            string prescriptionId = string.Concat("PRES", right);

            return string.Concat(prescriptionId + "-", patientCode);
        }


    }
}
