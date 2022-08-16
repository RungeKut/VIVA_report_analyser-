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
                var t = CollectValuesForCalc();
                return null;
        }
        public static List<UniqueTestNameClass> CollectValuesForCalc()
        {
            List<string> uniTest = UniqueTest();
            int numTests = uniTest.Count;
            List<List<UniqueTestNameClass>> valuesForCalc = new List<List<UniqueTestNameClass>>();
            for (int f = 0; f < OpenFiles.openCount; f++)
            {
                List<UniqueTestNameClass> uniqueTests = new List<UniqueTestNameClass>();
                for (int t = 0; t < numTests; t++)
                {
                    if (OpenFiles.dataFile[f].dataParse.BI.Test[t].uniqueTestName
                         == uniTest[t])
                    {
                        uniqueTests.Add(new UniqueTestNameClass()
                        {
                            uniqueTestName = uniTest[t],
                            MR = OpenFiles.dataFile[f].dataParse.BI.Test[t].MR,
                            MP = OpenFiles.dataFile[f].dataParse.BI.Test[t].MP,
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
            List<string> returnFile = new List<string>();
            int firstFile = 0;
            for (int i = 0; i < OpenFiles.dataFile.Count; i++)
            {
                if (OpenFiles.dataFile[i].errorOpenFile != true)
                    if (OpenFiles.dataFile[i].visibleFile == true)
                    {
                        firstFile = i;
                        foreach (var test in OpenFiles.dataFile[i].dataParse.BI.Test)
                        {
                            returnFile.Add(test.uniqueTestName);
                        }
                        i = OpenFiles.dataFile.Count;
                    }
            }
            for ( int i = firstFile + 1; i < OpenFiles.dataFile.Count; i++ )
            {
                if (OpenFiles.dataFile[i].errorOpenFile != true)
                if (OpenFiles.dataFile[i].visibleFile == true)
                    {
                        List<string> tempFile = new List<string>();
                        foreach (var test in OpenFiles.dataFile[i].dataParse.BI.Test)
                        {
                            tempFile.Add(test.uniqueTestName);
                        }
                        returnFile = (from cell in returnFile.Union(tempFile)
                                      select cell).ToList();
                    }
            }
            return returnFile;
        }
    }
}
