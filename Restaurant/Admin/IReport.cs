using Assignement2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public interface IReport
    {
        void GenerateReport(IList<EmployeeActivity> emplActivityList);
    }
}
