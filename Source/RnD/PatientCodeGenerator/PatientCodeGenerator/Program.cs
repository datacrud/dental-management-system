using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace PatientCodeGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string right = (100 + 1 ).ToString();

            string code = GetNextPatientCode(right);

            Console.WriteLine(code);
        }

        public static string GetNextPatientCode(string right)
        {
            int len = right.Length;

            for (int i = 0; i < 6-len; i++)
            {
                right = Concat("0", right);
            }


            string v = Concat("P", right);

            return v;
        }
    }
}
