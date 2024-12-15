using ApplicationProcessing.Services.Report.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationProcessing.Services.Report
{
    internal class ReportService
    {
        public bool Report(IReport report)
        {
            bool result = report.Create();
            return result;
        }
    }
}