using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VIVA_report_analyser
{
    internal class MaxDeviationColumnsClass
    {
        // Поля класса
        public string Name { get; set; }
        public string Translation { get; set; }
        public ulong Mask { get; set; }
    }
    internal class UniqueTestNameClass
    {
        // Поля класса
        public string uniqueTestName { get; set; }
        public Double MR { get; set; }
        public Double MP { get; set; }
        public bool attend { get; set; }
    }
    internal class MaxDeviationCalculate
    {
        public Double fileMin { get; set; }
        public Double minValue { get; set; }
        public Double fileMax { get; set; }
        public Double maxValue { get; set; }
        public Double delta { get; set; }
        public Double fileMinP { get; set; }
        public Double minValueP { get; set; }
        public Double fileMaxP { get; set; }
        public Double maxValueP { get; set; }
        public Double deltaP { get; set; }
        public static List<MaxDeviationColumnsClass> MaxDeviationColumns = new List<MaxDeviationColumnsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного теста
        {
            new MaxDeviationColumnsClass { Name = "fileMin",   Translation ="Файл с минимумом",  Mask = 0x000000001 }, // 0
            new MaxDeviationColumnsClass { Name = "minValue",  Translation ="Минимум",           Mask = 0x000000002 }, // 1
            new MaxDeviationColumnsClass { Name = "fileMax",   Translation ="Файл с максимумом", Mask = 0x000000004 }, // 2
            new MaxDeviationColumnsClass { Name = "maxValue",  Translation ="Максимум",          Mask = 0x000000008 }, // 3
            new MaxDeviationColumnsClass { Name = "delta",     Translation ="Размах",            Mask = 0x000000010 }, // 4
            new MaxDeviationColumnsClass { Name = "fileMinP",  Translation ="Файл с min %",      Mask = 0x000000020 }, // 5
            new MaxDeviationColumnsClass { Name = "minValueP", Translation ="min %",             Mask = 0x000000040 }, // 6
            new MaxDeviationColumnsClass { Name = "fileMaxP",  Translation ="Файл с max %",      Mask = 0x000000080 }, // 7
            new MaxDeviationColumnsClass { Name = "maxValueP", Translation ="max %",             Mask = 0x000000100 }, // 8
            new MaxDeviationColumnsClass { Name = "deltaP",    Translation ="Размах %",          Mask = 0x000000200 }, // 9
        };
        public static Dictionary<string, List<XElement>> DeviationCalculate()
        // Выборка результатов конкретного теста
        {
            try
            {
                var t = CollectValuesForCalc();


                return null;
            }
            catch (Exception Error)
            {
                MessageBox.Show("Ошибка выборки максимального отклонения.\nПодробнее:\n" + Error.Message, "Ошибка выборки максимального отклонения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static List<List<UniqueTestNameClass>> CollectValuesForCalc()
        {
            List<string> uniTest = UniqueTest();
            int numTests = uniTest.Count;
            int numFiles = TestDto.openFilesNames.Count;
            List<List<UniqueTestNameClass>> valuesForCalc = new List<List<UniqueTestNameClass>>();
            for (int f = 0; f < numFiles; f++)
            {
                List<UniqueTestNameClass> uniqueTests = new List<UniqueTestNameClass>();
                for (int t = 0; t < numTests; t++)
                {
                    if ((TestDto.dataFile[f].allTests[t].Attribute("NM").Value  + "|" +
                         TestDto.dataFile[f].allTests[t].Attribute("F").Value   + "|" +
                         TestDto.dataFile[f].allTests[t].Attribute("PD1").Value + "|" +
                         TestDto.dataFile[f].allTests[t].Attribute("PD2").Value)
                         == uniTest[t])
                    {
                        uniqueTests.Add(new UniqueTestNameClass()
                        {
                            uniqueTestName = uniTest[t],
                            MR = Double.Parse(TestDto.dataFile[f].allTests[t].Attribute("MR").Value, new CultureInfo("en-US")),
                            MP = Double.Parse(TestDto.dataFile[f].allTests[t].Attribute("MP").Value, new CultureInfo("en-US")),
                            attend = true
                        });
                    }
                    else
                    {
                        uniqueTests.Add(new UniqueTestNameClass()
                        {
                            uniqueTestName = uniTest[t],
                            MR = 0,
                            MP = 0,
                            attend = false
                        });
                    }
                    valuesForCalc.Add(uniqueTests);
                }
            }
            return valuesForCalc;
        }
        public static List<string> UniqueTest()
        {
            int numFiles = TestDto.openFilesNames.Count;
            List<string> returnFile = new List<string>();
            foreach (var test in TestDto.dataFile[0].allTests)
            {
                returnFile.Add
                (
                    test.Attribute("NM").Value  + "|" +
                    test.Attribute("F").Value   + "|" +
                    test.Attribute("PD1").Value + "|" +
                    test.Attribute("PD2").Value
                );
            }
            for ( int i = 1; i < numFiles; i++ )
            {
                List<string> tempFile = new List<string>();
                foreach (var test in TestDto.dataFile[i].allTests)
                {
                    tempFile.Add
                    (
                        test.Attribute("NM").Value  + "|" +
                        test.Attribute("F").Value   + "|" +
                        test.Attribute("PD1").Value + "|" +
                        test.Attribute("PD2").Value
                    );
                }
                returnFile = (from cell in returnFile.Union(tempFile)
                             select cell).ToList();
            }
            return returnFile;
        }
    }
}
