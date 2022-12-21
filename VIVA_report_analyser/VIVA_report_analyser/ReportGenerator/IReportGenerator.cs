using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVA_report_analyser.ReportGenerator
{
    internal interface IReportGenerator
    {
        void MakeReportFromTemplate(string fileName);
    }
}
