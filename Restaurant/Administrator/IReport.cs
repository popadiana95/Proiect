
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrator
{
    public interface IReport
    {
        void GenerateReport(IList<Order> orderList);
    }
}
