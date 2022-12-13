using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TSPP
{
    class Report
    {
        String report_text;
        String FileName;
        private double[,] optimal_plan;

        string[] provider_addresses;
        string[] clien_addresses;

        public void get_data_for_report(double[,] plan, string[] provider_addresses_, string[] clien_addresses_)
        {
            optimal_plan = new double[plan.GetLength(0), plan.GetLength(1)];
            provider_addresses = new string[provider_addresses_.Length];
            clien_addresses = new string[clien_addresses_.Length];

            Array.Copy(plan, optimal_plan, plan.Length);
            Array.Copy(provider_addresses_, provider_addresses, provider_addresses.Length);
            Array.Copy(clien_addresses_, clien_addresses, clien_addresses.Length);
            Fill_out_a_report();
        }

        private void Fill_out_a_report()
        {
            for (int i = 0; i < optimal_plan.GetLength(0); i++)
            {
                for (int j = 0; j < optimal_plan.GetLength(1); j++)
                {
                    if (optimal_plan[i, j] != 0)
                    {
                        report_text = report_text + "З адреси " + provider_addresses[i] + " треба направити " + optimal_plan[i, j] + " одиниць вантажу до споживача за адресою " + clien_addresses[j] + ". \n";
                    }
                }
            }

            Console.WriteLine(report_text);
        }

        public string return_report_text()
        {
            return report_text;
        }

        private void Filename_confirmation(String name)
        {
            FileName = name + ".txt";
        }

        public bool Save_report(string name)
        {
            int error = 0;
            Filename_confirmation(name);
            StreamWriter SW = new StreamWriter(new FileStream(FileName, FileMode.Create, FileAccess.Write));
            try
            {
                SW.Write(report_text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                error = 1;
            }
            finally
            {
                SW.Close();
            }

            if (error == 1)
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        ~Report()
        { }

    }
}
