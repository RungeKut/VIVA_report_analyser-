using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVA_report_analyser
{
    public class FilterByTests
    {
        public string testName { get; set; }
        public List<ColumnsClass> Tests { get; set; }
        public static List<FilterByTests> FilteringTests(BIClass dataParse)
        {
            // Выборка результатов конкретного теста
            List<FilterByTests> filteredTest = new List<FilterByTests>();
            foreach (VivaXmlTestsClass test in ParseXml.vivaXmlTests)
            {
                List<ColumnsClass> temp = (from ColumnsClass n in dataParse.Test
                                            where n.F == test.Name
                                             select n).ToList();
                filteredTest.Add(new FilterByTests()
                {
                    testName = test.Translation,
                    Tests = temp
                });
            }
            return filteredTest;
        }
    }
}
