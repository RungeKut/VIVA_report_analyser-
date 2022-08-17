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
    internal class FilterTestClass
    {
        // Поля класса
        public string testName { get; set; }
        public List<string> uniqueTests { get; set; }
    }
    internal class UniqueFileClass
    {
        // Поля класса
        public string fileName { get; set; }
        public List<UniqueTestsClass> tests { get; set; }
    }
    internal class UniqueTestsClass
    {
        // Поля класса
        public string testName { get; set; }
        public List<UniqueTestClass> uniqueTests { get; set; }
    }
    internal class UniqueTestClass
    {
        // Поля класса
        public string uniqueTestName { get; set; }
        public double MR { get; set; }
        public double MP { get; set; }
        public bool attend { get; set; }
    }
    internal class MaxDeviationCalculateFilteredTests
    {
        // Поля класса
        public string testName { get; set; }
        public List<MaxDeviationCalculate> data { get; set; }
    }
    internal class MaxDeviationCalculate
    {
        public string fileMin { get; set; }
        public double minValue { get; set; }
        public string fileMax { get; set; }
        public double maxValue { get; set; }
        public double delta { get; set; }
        public string fileMinP { get; set; }
        public double minValueP { get; set; }
        public string fileMaxP { get; set; }
        public double maxValueP { get; set; }
        public double deltaP { get; set; }
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
        public static List<MaxDeviationCalculateFilteredTests> DeviationCalculate()
        // Выборка результатов конкретного теста
        {
            List<UniqueFileClass> data = CollectValuesForCalc();
            List<MaxDeviationCalculateFilteredTests> maxDeviationCalculate = new List<MaxDeviationCalculateFilteredTests>();
            for (int f = 0; f < data.Count; f++)
            {
                for (int fltr = 0; fltr < data[f].tests.Count; fltr++)
                {
                    try
                    {
                        if (maxDeviationCalculate[fltr].testName != null)
                        {

                        }
                    }
                    catch (Exception e) // Если елемента с таким индексом нет - то создаем
                    {
                        maxDeviationCalculate.Add(new MaxDeviationCalculateFilteredTests() // Создали раздел с фильтра по тесту
                        {
                            testName = data[f].tests[fltr].testName,
                            data = new List<MaxDeviationCalculate>()
                        });
                    }
                    
                    for (int t = 0; t < data[f].tests[fltr].uniqueTests.Count; t++)
                    {
                        try
                        {
                            if (maxDeviationCalculate[fltr].data[t].fileMin != null)
                            {

                            }
                        }
                        catch (Exception e) // Если елемента с таким индексом нет - то создаем
                        {
                            maxDeviationCalculate[fltr].data.Add(new MaxDeviationCalculate() // Создали раздел теста
                            {
                                fileMin = data[f].fileName,
                                minValue = data[f].tests[fltr].uniqueTests[t].MR,
                                fileMax = data[f].fileName,
                                maxValue = data[f].tests[fltr].uniqueTests[t].MR,
                                fileMinP = data[f].fileName,
                                minValueP = data[f].tests[fltr].uniqueTests[t].MP,
                                fileMaxP = data[f].fileName,
                                maxValueP = data[f].tests[fltr].uniqueTests[t].MP
                            });
                        }
                        if (maxDeviationCalculate[fltr].data[t].minValue > data[f].tests[fltr].uniqueTests[t].MR)
                        {
                            maxDeviationCalculate[fltr].data[t].minValue = data[f].tests[fltr].uniqueTests[t].MR;
                            maxDeviationCalculate[fltr].data[t].fileMin = data[f].fileName;
                        }
                        if (maxDeviationCalculate[fltr].data[t].maxValue < data[f].tests[fltr].uniqueTests[t].MR)
                        {
                            maxDeviationCalculate[fltr].data[t].maxValue = data[f].tests[fltr].uniqueTests[t].MR;
                            maxDeviationCalculate[fltr].data[t].fileMax = data[f].fileName;
                        }
                        maxDeviationCalculate[fltr].data[t].delta = maxDeviationCalculate[fltr].data[t].maxValue - maxDeviationCalculate[fltr].data[t].minValue;
                        if (maxDeviationCalculate[fltr].data[t].minValueP > data[f].tests[fltr].uniqueTests[t].MP)
                        {
                            maxDeviationCalculate[fltr].data[t].minValueP = data[f].tests[fltr].uniqueTests[t].MP;
                            maxDeviationCalculate[fltr].data[t].fileMinP = data[f].fileName;
                        }
                        if (maxDeviationCalculate[fltr].data[t].maxValueP < data[f].tests[fltr].uniqueTests[t].MP)
                        {
                            maxDeviationCalculate[fltr].data[t].maxValueP = data[f].tests[fltr].uniqueTests[t].MP;
                            maxDeviationCalculate[fltr].data[t].fileMaxP = data[f].fileName;
                        }
                        maxDeviationCalculate[fltr].data[t].deltaP = maxDeviationCalculate[fltr].data[t].maxValueP - maxDeviationCalculate[fltr].data[t].minValueP;
                    }
                }
            }
            return maxDeviationCalculate;
        }
        public static List<UniqueFileClass> CollectValuesForCalc()
        {
            List<FilterTestClass> uniTest = UniqueTest();
            List<UniqueFileClass> uniqFile = new List<UniqueFileClass>();
            for (int f = 0; f < OpenFiles.dataFile.Count; f++)
            {
                uniqFile.Add(new UniqueFileClass() // Создали раздел с Файлом
                {
                    fileName = OpenFiles.dataFile[f].fileName,
                    tests = new List<UniqueTestsClass>()
                });
                if (OpenFiles.dataFile[f].errorOpenFile != true)
                if (OpenFiles.dataFile[f].visibleFile == true)
                    {
                        for (int filter = 0; filter < OpenFiles.dataFile[f].dataFilteredByTests.Count; filter++)
                        {
                            uniqFile[f].tests.Add(new UniqueTestsClass() //Создали раздел с фильтра тестов
                            {
                                testName = OpenFiles.dataFile[f].dataFilteredByTests[filter].testName,
                                uniqueTests = new List<UniqueTestClass>()
                            });
                            for (int t = 0; t < uniTest[filter].uniqueTests.Count; t++)
                            {
                                //Следующее условие - ошибочно. Нужно выполнять поиск в массиве а не сравнивать. Пока как заглушка
                                //т.к. если длинна массива dataFile будет меньше уникального - то выборка будет неверной
                                if (OpenFiles.dataFile[f].dataFilteredByTests[filter].Tests[t].uniqueTestName
                                     == uniTest[filter].uniqueTests[t])
                                {
                                    uniqFile[f].tests[filter].uniqueTests.Add(new UniqueTestClass() //Если такой тест найден в разделе фильтра то создаем тест и копируем из него данные
                                    {
                                        uniqueTestName = uniTest[filter].testName,
                                        MR = OpenFiles.dataFile[f].dataFilteredByTests[filter].Tests[t].MR,
                                        MP = OpenFiles.dataFile[f].dataFilteredByTests[filter].Tests[t].MP,
                                        attend = true
                                    });
                                }
                                else
                                {
                                    uniqFile[f].tests[filter].uniqueTests.Add(new UniqueTestClass() //Если такой тест НЕнайден в разделе фильтра то всеравно создаем тест но без данных с флагом отсутсвует
                                    {
                                        uniqueTestName = uniTest[filter].testName,
                                        MR = 0,
                                        MP = 0,
                                        attend = false
                                    });
                                }
                            }
                        }  
                    }
            }
            return uniqFile;
        }
        public static List<FilterTestClass> UniqueTest()
        {
            //Составляем выборку уникальных тестов внутри фильтра по названию теста
            List<FilterTestClass> returnFile = new List<FilterTestClass>();
            int firstFile = 0;
            for (int f = 0; f < OpenFiles.dataFile.Count; f++)
            {
                if (OpenFiles.dataFile[f].errorOpenFile != true)
                    if (OpenFiles.dataFile[f].visibleFile == true)
                    {
                        firstFile = f;
                        for (int filter = 0; filter < OpenFiles.dataFile[f].dataFilteredByTests.Count; filter++)
                        {
                            returnFile.Add(new FilterTestClass()
                            {
                                testName = OpenFiles.dataFile[f].dataFilteredByTests[filter].testName,
                                uniqueTests = new List<string>()
                            });
                            foreach (var test in OpenFiles.dataFile[f].dataFilteredByTests[filter].Tests)
                            {
                                returnFile[filter].uniqueTests.Add(test.uniqueTestName);
                            }
                        }
                        f = OpenFiles.dataFile.Count;
                    }
            }
            for ( int f = firstFile + 1; f < OpenFiles.dataFile.Count; f++ )
            {
                if (OpenFiles.dataFile[f].errorOpenFile != true)
                    if (OpenFiles.dataFile[f].visibleFile == true)
                    {
                        for (int filter = 0; filter < OpenFiles.dataFile[f].dataFilteredByTests.Count; filter++)
                        {
                            List<string> tempFile = new List<string>();
                            foreach (var test in OpenFiles.dataFile[f].dataFilteredByTests[filter].Tests)
                            {
                                tempFile.Add(test.uniqueTestName);
                            }
                            returnFile[filter].uniqueTests = (from cell in returnFile[filter].uniqueTests.Union(tempFile)
                                          select cell).ToList();
                        }
                    }
            }
            //Составляем выборку уникальных тестов из всего списка тестов
            returnFile.Add(new FilterTestClass()
            {
                testName = ParseXml.Сalculations[0].Translation,
                uniqueTests = new List<string>()
            });
            firstFile = 0;
            int lastTest = returnFile.Count - 1;
            for (int i = 0; i < OpenFiles.dataFile.Count; i++)
            {
                if (OpenFiles.dataFile[i].errorOpenFile != true)
                    if (OpenFiles.dataFile[i].visibleFile == true)
                    {
                        firstFile = i;
                        foreach (var test in OpenFiles.dataFile[i].dataParse.BI.Test)
                        {
                            returnFile[lastTest].uniqueTests.Add(test.uniqueTestName);
                        }
                        i = OpenFiles.dataFile.Count;
                    }
            }
            for (int i = firstFile + 1; i < OpenFiles.dataFile.Count; i++)
            {
                if (OpenFiles.dataFile[i].errorOpenFile != true)
                    if (OpenFiles.dataFile[i].visibleFile == true)
                    {
                        List<string> tempFile = new List<string>();
                        foreach (var test in OpenFiles.dataFile[i].dataParse.BI.Test)
                        {
                            tempFile.Add(test.uniqueTestName);
                        }
                        returnFile[lastTest].uniqueTests = (from cell in returnFile[lastTest].uniqueTests.Union(tempFile)
                                      select cell).ToList();
                    }
            }

            return returnFile;
        }
    }
}
