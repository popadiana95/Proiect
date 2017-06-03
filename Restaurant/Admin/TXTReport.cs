using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignement2;

namespace Assignment2
{
    public class TXTReport: IReport
    {

        void GenerateReport(IList<EmployeeActivity> emplActivityList)
        {
            string lines = "Employee activity" + emplActivityList[0].idEmployee + "\r\n";
            for (int i = 0; i < emplActivityList.Count(); i++)
                lines += emplActivityList[i].toString() + "\r\n";
            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("Report.txt");
            file.WriteLine(lines);
            file.Close();
            
        }
    }
}
