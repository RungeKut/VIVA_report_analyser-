using FastMember;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VIVA_report_analyser
{
    internal class MaxDeviationColumnsClass
    {
        // Поля класса
        public string name { get; set; }
        public string translation { get; set; }
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
        public string pcbName { get; set; }
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
        public double MM { get; set; }
        public string MU { get; set; }
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
        private static Logger log = LogManager.GetCurrentClassLogger();
        public string NM             { get; set; }
        public double MM             { get; set; }
        public string MU             { get; set; }
        public string fileMin        { get; set; }
        public double minValue       { get; set; }
        public string fileMax        { get; set; }
        public double maxValue       { get; set; }
        public double delta          { get; set; }
        public string fileMinP       { get; set; }
        public double minValueP      { get; set; }
        public string fileMaxP       { get; set; }
        public double maxValueP      { get; set; }
        public double deltaP         { get; set; }
        public string missingInFiles { get; set; }
        public static List<MaxDeviationColumnsClass> MaxDeviationColumns = new List<MaxDeviationColumnsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного теста
        {
            new MaxDeviationColumnsClass { name = "NM" ,            translation ="Имя компонента"       },
            new MaxDeviationColumnsClass { name = "MM" ,            translation ="Номинал"              },
            new MaxDeviationColumnsClass { name = "MU",             translation ="Единицы"              },
            new MaxDeviationColumnsClass { name = "fileMin",        translation ="Файл min"             },
            new MaxDeviationColumnsClass { name = "minValue",       translation ="Минимум"              },
            new MaxDeviationColumnsClass { name = "fileMax",        translation ="Файл max"             },
            new MaxDeviationColumnsClass { name = "maxValue",       translation ="Максимум"             },
            new MaxDeviationColumnsClass { name = "delta",          translation ="Размах"               },
            new MaxDeviationColumnsClass { name = "fileMinP",       translation ="Файл min %"           },
            new MaxDeviationColumnsClass { name = "minValueP",      translation ="min %"                },
            new MaxDeviationColumnsClass { name = "fileMaxP",       translation ="Файл max %"           },
            new MaxDeviationColumnsClass { name = "maxValueP",      translation ="max %"                },
            new MaxDeviationColumnsClass { name = "deltaP",         translation ="Размах %"             },
            new MaxDeviationColumnsClass { name = "missingInFiles", translation ="Тест отсутствует в файлах" }
        };
        public static List<MaxDeviationCalculateFilteredTests> DeviationCalculate()
        // Выборка результатов конкретного теста
        {
            List<UniqueFileClass> data = CollectValuesForCalc();
            //MainForm.ProgressView.progressMax(data.Count - 1);
            List<MaxDeviationCalculateFilteredTests> maxDeviationCalculate = new List<MaxDeviationCalculateFilteredTests>();
            for (int f = 0; f < data.Count; f++)
            {
                for (int fltr = 0; fltr < data[f].tests.Count; fltr++)
                {
                    try { if (maxDeviationCalculate[fltr].testName != null){} }
                    catch (Exception) // Если елемента с таким индексом нет - то создаем
                    {
                        maxDeviationCalculate.Add(new MaxDeviationCalculateFilteredTests() // Создали раздел с фильтра по тесту
                        {
                            testName = data[f].tests[fltr].testName,
                            data = new List<MaxDeviationCalculate>()
                        });
                    }
                    
                    for (int t = 0; t < data[f].tests[fltr].uniqueTests.Count; t++)
                    {
                        try { if (maxDeviationCalculate[fltr].data[t].fileMin != null){} }
                        catch (Exception) // Если елемента с таким индексом нет - то создаем
                        {
                            if (data[f].tests[fltr].uniqueTests[t].attend == true)
                            {
                                maxDeviationCalculate[fltr].data.Add(new MaxDeviationCalculate() // Создали раздел теста
                                {
                                    NM = data[f].tests[fltr].uniqueTests[t].uniqueTestName,
                                    MM = data[f].tests[fltr].uniqueTests[t].MM,
                                    MU = data[f].tests[fltr].uniqueTests[t].MU,
                                    fileMin = data[f].pcbName,
                                    minValue = Math.Round(data[f].tests[fltr].uniqueTests[t].MR, 3),
                                    fileMax = data[f].pcbName,
                                    maxValue = Math.Round(data[f].tests[fltr].uniqueTests[t].MR, 3),
                                    fileMinP = data[f].pcbName,
                                    minValueP = Math.Round(data[f].tests[fltr].uniqueTests[t].MP, 1),
                                    fileMaxP = data[f].pcbName,
                                    maxValueP = Math.Round(data[f].tests[fltr].uniqueTests[t].MP, 1)
                                });
                            }
                            else
                            {
                                int fl = f + 1;
                                while (data[fl].tests[fltr].uniqueTests[t].attend != true)
                                {
                                    fl++;
                                }
                                maxDeviationCalculate[fltr].data.Add(new MaxDeviationCalculate() // Создали раздел теста
                                {
                                    NM = data[fl].tests[fltr].uniqueTests[t].uniqueTestName,
                                    MM = data[f].tests[fltr].uniqueTests[t].MM,
                                    MU = data[f].tests[fltr].uniqueTests[t].MU,
                                    fileMin = data[fl].pcbName,
                                    minValue = Math.Round(data[fl].tests[fltr].uniqueTests[t].MR, 3),
                                    fileMax = data[fl].pcbName,
                                    maxValue = Math.Round(data[fl].tests[fltr].uniqueTests[t].MR, 3),
                                    fileMinP = data[fl].pcbName,
                                    minValueP = Math.Round(data[fl].tests[fltr].uniqueTests[t].MP, 1),
                                    fileMaxP = data[fl].pcbName,
                                    maxValueP = Math.Round(data[fl].tests[fltr].uniqueTests[t].MP, 1)
                                });
                            }
                        }
                        if (data[f].tests[fltr].uniqueTests[t].attend == true)
                        {
                            if (maxDeviationCalculate[fltr].data[t].minValue > data[f].tests[fltr].uniqueTests[t].MR)
                            {
                                maxDeviationCalculate[fltr].data[t].minValue = Math.Round(data[f].tests[fltr].uniqueTests[t].MR, 3);
                                maxDeviationCalculate[fltr].data[t].fileMin = data[f].pcbName;
                            }
                            if (maxDeviationCalculate[fltr].data[t].maxValue < data[f].tests[fltr].uniqueTests[t].MR)
                            {
                                maxDeviationCalculate[fltr].data[t].maxValue = Math.Round(data[f].tests[fltr].uniqueTests[t].MR, 3);
                                maxDeviationCalculate[fltr].data[t].fileMax = data[f].pcbName;
                            }
                            maxDeviationCalculate[fltr].data[t].delta = Math.Round(maxDeviationCalculate[fltr].data[t].maxValue - maxDeviationCalculate[fltr].data[t].minValue, 3);
                            if (maxDeviationCalculate[fltr].data[t].minValueP > data[f].tests[fltr].uniqueTests[t].MP)
                            {
                                maxDeviationCalculate[fltr].data[t].minValueP = Math.Round(data[f].tests[fltr].uniqueTests[t].MP, 1);
                                maxDeviationCalculate[fltr].data[t].fileMinP = data[f].pcbName;
                            }
                            if (maxDeviationCalculate[fltr].data[t].maxValueP < data[f].tests[fltr].uniqueTests[t].MP)
                            {
                                maxDeviationCalculate[fltr].data[t].maxValueP = Math.Round(data[f].tests[fltr].uniqueTests[t].MP, 1);
                                maxDeviationCalculate[fltr].data[t].fileMaxP = data[f].pcbName;
                            }
                            maxDeviationCalculate[fltr].data[t].deltaP = Math.Round(maxDeviationCalculate[fltr].data[t].maxValueP - maxDeviationCalculate[fltr].data[t].minValueP, 1);
                        }
                        else
                        {
                            maxDeviationCalculate[fltr].data[t].missingInFiles += data[f].pcbName + ";";
                        }
                    }
                }
                //MainForm.ProgressView.progress(f, "Этап 3 из 3 - Расчет отклонения для платы: " + data[f].pcbName);
            }
            return maxDeviationCalculate;
        }
        public static List<UniqueFileClass> CollectValuesForCalc()
        {
            //MainForm.ProgressView.progressMax((DataModel.dataFiles.Count - 1) * 2);
            List<FilterTestClass> uniTest = UniqueTest();
            List<UniqueFileClass> uniqFile = new List<UniqueFileClass>();
            int pcbNumb = -1;
            for (int file = 0; file < DataModel.dataFiles.Count; file++) // Перебираем открытые файлы
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++) // Перебираем все секции с платами в одном файле
                {
                    uniqFile.Add(new UniqueFileClass() // Создали раздел с Платой
                    {
                        pcbName = DataModel.dataFiles[file].Name + " | " + DataModel.dataFiles[file].biSec.BI[numBI].ID + " | " + DataModel.dataFiles[file].biSec.BI[numBI].BC,
                        tests = new List<UniqueTestsClass>()
                    });
                    if (!DataModel.dataFiles[file].errorOpen)
                        if (DataModel.dataFiles[file].biSec.BI[numBI].visible)
                        {
                            pcbNumb++;
                            for (int test = 0; test < DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests.Count; test++)
                            {
                                uniqFile[pcbNumb].tests.Add(new UniqueTestsClass() //Создали раздел с фильтра тестов
                                {
                                    testName = DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].testName,
                                    uniqueTests = new List<UniqueTestClass>()
                                });
                                for (int t = 0; t < uniTest[test].uniqueTests.Count; t++)
                                {
                                    List<DataModel.TestClass> tempData = ( from   DataModel.TestClass n
                                                                           in     DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].Tests
                                                                           where  n.uniqueTestName == uniTest[test].uniqueTests[t]
                                                                           select n
                                                                         ).ToList();
                                    if (tempData.Count != 0)
                                    {
                                        int repeatData = 0;
                                        foreach (DataModel.TestClass columns in tempData)
                                        {
                                            repeatData++;
                                            uniqFile[pcbNumb].tests[test].uniqueTests.Add(new UniqueTestClass() //Если такой тест найден в разделе фильтра то создаем тест и копируем из него данные
                                            {
                                                uniqueTestName = uniTest[test].uniqueTests[t],
                                                MR = columns.MR,
                                                MP = columns.MP,
                                                MM = columns.MM,
                                                MU = columns.MU,
                                                attend = true
                                            });
                                            if (repeatData > 1)
                                            {
                                                //MessageBox.Show("Уникальное имя: " + uniTest[test].uniqueTests[t], "Внимание, повторяющийся тест!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                log.Warn("Повторяющийся тест! Плата: " + uniqFile[pcbNumb].pcbName + " Тест: " + uniTest[test].uniqueTests[t]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        uniqFile[pcbNumb].tests[test].uniqueTests.Add(new UniqueTestClass() //Если такой тест НЕнайден в разделе фильтра то всеравно создаем тест но без данных с флагом отсутсвует
                                        {
                                            uniqueTestName = uniTest[test].uniqueTests[t],
                                            MR = 0,
                                            MP = 0,
                                            attend = false
                                        });
                                    }
                                }
                            }
                        }
                }
                //MainForm.ProgressView.progress(file, "Этап 2 из 3 - Составляем выборку для групп компонентов файла: " + DataModel.dataFiles[file].Name);
            }
            //И повторяем для всех тестов
            pcbNumb = -1;
            int allTestNum = uniTest.Count - 1;
            for (int file = 0; file < DataModel.dataFiles.Count; file++) // Перебираем открытые файлы
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++) // Перебираем все секции с платами в одном файле
                {
                    if (!DataModel.dataFiles[file].errorOpen)
                        if (DataModel.dataFiles[file].biSec.BI[numBI].visible)
                        {
                            pcbNumb++;
                            uniqFile[pcbNumb].tests.Add(new UniqueTestsClass() //Создали раздел с фильтра тестов
                            {
                                testName = ParseXml.Сalculations[0].translation,
                                uniqueTests = new List<UniqueTestClass>()
                            });
                            for (int t = 0; t < uniTest[allTestNum].uniqueTests.Count; t++)
                            {
                                List<DataModel.TestClass> tempData = (from DataModel.TestClass n in DataModel.dataFiles[file].biSec.BI[numBI].testsSec.TEST
                                                                      where n.uniqueTestName == uniTest[allTestNum].uniqueTests[t]
                                                                      select n).ToList();
                                if (tempData.Count != 0)
                                {
                                    int repeatData = 0;
                                    foreach (DataModel.TestClass columns in tempData)
                                    {
                                        repeatData++;
                                        uniqFile[pcbNumb].tests[allTestNum].uniqueTests.Add(new UniqueTestClass() //Если такой тест найден в разделе фильтра то создаем тест и копируем из него данные
                                        {
                                            uniqueTestName = uniTest[allTestNum].uniqueTests[t],
                                            MR = columns.MR,
                                            MP = columns.MP,
                                            MM = columns.MM,
                                            MU = columns.MU,
                                            attend = true
                                        });
                                        if (repeatData > 1)
                                        {
                                            //MessageBox.Show("Уникальное имя: " + uniTest[allTestNum].uniqueTests[t], "Внимание, повторяющийся тест!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            log.Warn("Повторяющийся тест! Плата: " + uniqFile[pcbNumb].pcbName + " Тест: " + uniTest[allTestNum].uniqueTests[t]);
                                        }
                                    }
                                }
                                else
                                {
                                    uniqFile[pcbNumb].tests[allTestNum].uniqueTests.Add(new UniqueTestClass() //Если такой тест НЕнайден в разделе фильтра то всеравно создаем тест но без данных с флагом отсутсвует
                                    {
                                        uniqueTestName = uniTest[allTestNum].uniqueTests[t],
                                        MR = 0,
                                        MP = 0,
                                        attend = false
                                    });
                                }
                            }
                        }
                }
                //MainForm.ProgressView.progress((DataModel.dataFiles.Count - 1) + file, "Этап 2 из 3 - Составляем выборку для всех компонентов файла: " + DataModel.dataFiles[file].Name);
            }
            return uniqFile;
        }
        public static List<FilterTestClass> UniqueTest()
        {
            //MainForm.ProgressView.progressMax((DataModel.dataFiles.Count - 1) * 2);
            int progress = 0;
            //Составляем выборку уникальных тестов внутри фильтра по названию теста
            List<FilterTestClass> returnFile = new List<FilterTestClass>();
            int firstFile = 0;
            // Забиваем лист return File первым видимым массивом и выходим.
            for (int file = 0; file < DataModel.dataFiles.Count; file++) // Перебираем открытые файлы
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++) // Перебираем все секции с платами в одном файле
                {
                    if (!DataModel.dataFiles[file].errorOpen)
                        if (DataModel.dataFiles[file].biSec.BI[numBI].visible)
                        {
                            firstFile = file;
                            for (int filter = 0; filter < DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests.Count; filter++)
                            {
                                returnFile.Add(new FilterTestClass()
                                {
                                    testName = DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[filter].testName,
                                    uniqueTests = new List<string>()
                                });
                                foreach (var test in DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[filter].Tests)
                                {
                                    returnFile[filter].uniqueTests.Add(test.uniqueTestName);
                                }
                            }
                            file = DataModel.dataFiles.Count;
                            break;
                        }
                }
            }
            // Делаем объединение с оставшимися массивами
            for (int file = 0; file < DataModel.dataFiles.Count; file++) // Перебираем открытые файлы
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++) // Перебираем все секции с платами в одном файле
                {
                    if (!DataModel.dataFiles[file].errorOpen)
                        if (DataModel.dataFiles[file].biSec.BI[numBI].visible)
                        {
                            for (int filter = 0; filter < DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests.Count; filter++)
                            {
                                List<string> tempFile = new List<string>();
                                foreach (var test in DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[filter].Tests)
                                {
                                    tempFile.Add(test.uniqueTestName);
                                }
                                returnFile[filter].uniqueTests = (from cell in returnFile[filter].uniqueTests.Union(tempFile)
                                                                  select cell).ToList();
                            }
                        }
                }
                progress++;
                //MainForm.ProgressView.progress(progress, "Этап 1 из 3 - Составляем список уникальных тестов для групп компонентов файла: " + DataModel.dataFiles[file].Name);
            }
            //Составляем выборку уникальных тестов из всего списка тестов
            returnFile.Add(new FilterTestClass()
            {
                testName = ParseXml.Сalculations[0].translation,
                uniqueTests = new List<string>()
            });
            firstFile = 0;
            int lastTest = returnFile.Count - 1;
            // Так же в начале Забиваем лист return File первым видимым массивом и выходим.
            for (int file = 0; file < DataModel.dataFiles.Count; file++) // Перебираем открытые файлы
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++) // Перебираем все секции с платами в одном файле
                {
                    if (!DataModel.dataFiles[file].errorOpen)
                        if (DataModel.dataFiles[file].biSec.BI[numBI].visible)
                        {
                            firstFile = file;
                            foreach (var test in DataModel.dataFiles[file].biSec.BI[numBI].testsSec.TEST)
                            {
                                returnFile[lastTest].uniqueTests.Add(test.uniqueTestName);
                            }
                            file = DataModel.dataFiles.Count;
                            break;
                        }
                }
            }
            // Делаем объединение с оставшимися массивами
            for (int file = 0; file < DataModel.dataFiles.Count; file++) // Перебираем открытые файлы
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++) // Перебираем все секции с платами в одном файле
                {
                    if (!DataModel.dataFiles[file].errorOpen)
                        if (DataModel.dataFiles[file].biSec.BI[numBI].visible)
                        {
                            List<string> tempFile = new List<string>();
                            foreach (var test in DataModel.dataFiles[file].biSec.BI[numBI].testsSec.TEST)
                            {
                                tempFile.Add(test.uniqueTestName);
                            }
                            returnFile[lastTest].uniqueTests = (from cell in returnFile[lastTest].uniqueTests.Union(tempFile)
                                                                select cell).ToList();
                        }
                }
                progress++;
                //MainForm.ProgressView.progress(progress, "Этап 1 из 3 - Составляем список уникальных тестов для всех компонентов файла: " + DataModel.dataFiles[file].Name);
            }
            return returnFile;
        }
        public static void DeviationAddNewComponentTab<T>(string nameTab, TabControl tabControl, IList<T> data)
        // Создание фкладки с именем компонента во вкладке с файлом
        {
            try
            {
                DataView view = MaxDeviationCalculate.ConvertToDataTable(data).DefaultView;
                int rowCount = view.Count;
                TabPage page = new TabPage(nameTab + " (" + rowCount + ")");
                tabControl.TabPages.Add(page);
                //page.Tag = "";
                //page.MouseClick += page_MouseClick;
                //page.MouseClick += new MouseEventHandler(page_MouseClick);
                MainForm.DoubleBufferedDataGridView dataGridView = new MainForm.DoubleBufferedDataGridView();
                page.Controls.Add(dataGridView);
                dataGridView.Dock = DockStyle.Fill;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToDeleteRows = false;
                dataGridView.ReadOnly = true;
                dataGridView.AutoGenerateColumns = true;
                dataGridView.RowHeadersVisible = false;
                //dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView.DataSource = view;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView.VirtualMode = true; //отрисовываются только те ячейки, которые видны в данный момент

                //dataGridView.Columns["min %"].DefaultCellStyle.Format = "#0.0\\%";
                //dataGridView.Columns["max %"].DefaultCellStyle.Format = "#0.0\\%";
                //dataGridView.Columns["Размах %"].DefaultCellStyle.Format = "#0.0\\%";

                dataGridView.TopLeftHeaderCell.Value = "Тест"; // Заголовок столбца названия строк
                /*if (view.Count > 0)
                {
                    int i = 0;
                    foreach (var columnHeader in MaxDeviationColumns)
                    {
                        dataGridView.Columns[i].HeaderText = columnHeader.Translation;
                        i++;
                    }
                }*/
                dataGridView.RowsDefaultCellStyle.BackColor = Color.Ivory; //Строки всей таблицы
                //dataGridView.Rows[1].DefaultCellStyle.BackColor = Color.IndianRed; //Одной строки
                dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.MintCream; //Цвет четных строк
                dataGridView.RowPrePaint += DataGridView_RowPrePaint;
                /*for (int r = 0; r < rowCount; r++)
                {
                    var h = dataGridView.Rows[r].Cells[2].Value;
                        
                }*/
                dataGridView.AutoResizeColumns();
            }
            catch (Exception ReadFileError)
            {
                MessageBox.Show("Ошибка при создании вкладки " + nameTab + ". Подробнее: " + ReadFileError.Message, "Ошибка создания вкладки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid != null)
            {
                if (grid.Columns["min %"] != null)
                    grid.Columns["min %"].DefaultCellStyle.Format = "#0.0\\%";
                if (grid.Columns["max %"] != null)
                    grid.Columns["max %"].DefaultCellStyle.Format = "#0.0\\%";
                if (grid.Columns["Размах %"] != null)
                    grid.Columns["Размах %"].DefaultCellStyle.Format = "#0.0\\%";
            }
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(data, "NM", "MM", "MU", "fileMin", "minValue", "fileMax", "maxValue", "delta", "fileMinP", "minValueP", "fileMaxP", "maxValueP", "deltaP", "missingInFiles"))
            {
                table.Load(reader);
            }
            foreach (var columns in MaxDeviationColumns)
            {
                table.Columns[columns.name].ColumnName = columns.translation;
            }
            return table;
        }
    }
}
