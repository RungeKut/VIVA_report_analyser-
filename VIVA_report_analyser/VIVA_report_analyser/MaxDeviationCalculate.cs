using FastMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public string NM { get; set; }
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
            new MaxDeviationColumnsClass { Name = "NM" ,       Translation ="Имя компонента",    Mask = 0x000001000 },
            new MaxDeviationColumnsClass { Name = "fileMin",   Translation ="Файл min",  Mask = 0x000000001 },
            new MaxDeviationColumnsClass { Name = "minValue",  Translation ="Минимум",           Mask = 0x000000002 },
            new MaxDeviationColumnsClass { Name = "fileMax",   Translation ="Файл max", Mask = 0x000000004 },
            new MaxDeviationColumnsClass { Name = "maxValue",  Translation ="Максимум",          Mask = 0x000000008 },
            new MaxDeviationColumnsClass { Name = "delta",     Translation ="Размах",            Mask = 0x000000010 },
            new MaxDeviationColumnsClass { Name = "fileMinP",  Translation ="Файл min %",      Mask = 0x000000020 },
            new MaxDeviationColumnsClass { Name = "minValueP", Translation ="min %",             Mask = 0x000000040 },
            new MaxDeviationColumnsClass { Name = "fileMaxP",  Translation ="Файл max %",      Mask = 0x000000080 },
            new MaxDeviationColumnsClass { Name = "maxValueP", Translation ="max %",             Mask = 0x000000100 },
            new MaxDeviationColumnsClass { Name = "deltaP",    Translation ="Размах %",          Mask = 0x000000200 }
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
                                    fileMin = data[f].fileName,
                                    minValue = Math.Round(data[f].tests[fltr].uniqueTests[t].MR, 3),
                                    fileMax = data[f].fileName,
                                    maxValue = Math.Round(data[f].tests[fltr].uniqueTests[t].MR, 3),
                                    fileMinP = data[f].fileName,
                                    minValueP = Math.Round(data[f].tests[fltr].uniqueTests[t].MP, 1),
                                    fileMaxP = data[f].fileName,
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
                                    fileMin = data[fl].fileName,
                                    minValue = Math.Round(data[fl].tests[fltr].uniqueTests[t].MR, 3),
                                    fileMax = data[fl].fileName,
                                    maxValue = Math.Round(data[fl].tests[fltr].uniqueTests[t].MR, 3),
                                    fileMinP = data[fl].fileName,
                                    minValueP = Math.Round(data[fl].tests[fltr].uniqueTests[t].MP, 1),
                                    fileMaxP = data[fl].fileName,
                                    maxValueP = Math.Round(data[fl].tests[fltr].uniqueTests[t].MP, 1)
                                });
                            }
                        }
                        if (data[f].tests[fltr].uniqueTests[t].attend == true)
                        {
                            if (maxDeviationCalculate[fltr].data[t].minValue > data[f].tests[fltr].uniqueTests[t].MR)
                            {
                                maxDeviationCalculate[fltr].data[t].minValue = Math.Round(data[f].tests[fltr].uniqueTests[t].MR, 3);
                                maxDeviationCalculate[fltr].data[t].fileMin = data[f].fileName;
                            }
                            if (maxDeviationCalculate[fltr].data[t].maxValue < data[f].tests[fltr].uniqueTests[t].MR)
                            {
                                maxDeviationCalculate[fltr].data[t].maxValue = Math.Round(data[f].tests[fltr].uniqueTests[t].MR, 3);
                                maxDeviationCalculate[fltr].data[t].fileMax = data[f].fileName;
                            }
                            maxDeviationCalculate[fltr].data[t].delta = Math.Round(maxDeviationCalculate[fltr].data[t].maxValue - maxDeviationCalculate[fltr].data[t].minValue, 3);
                            if (maxDeviationCalculate[fltr].data[t].minValueP > data[f].tests[fltr].uniqueTests[t].MP)
                            {
                                maxDeviationCalculate[fltr].data[t].minValueP = Math.Round(data[f].tests[fltr].uniqueTests[t].MP, 1);
                                maxDeviationCalculate[fltr].data[t].fileMinP = data[f].fileName;
                            }
                            if (maxDeviationCalculate[fltr].data[t].maxValueP < data[f].tests[fltr].uniqueTests[t].MP)
                            {
                                maxDeviationCalculate[fltr].data[t].maxValueP = Math.Round(data[f].tests[fltr].uniqueTests[t].MP, 1);
                                maxDeviationCalculate[fltr].data[t].fileMaxP = data[f].fileName;
                            }
                            maxDeviationCalculate[fltr].data[t].deltaP = Math.Round(maxDeviationCalculate[fltr].data[t].maxValueP - maxDeviationCalculate[fltr].data[t].minValueP, 1);
                        }
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
                    fileName = OpenFiles.dataFile[f].fileName + " | " + OpenFiles.dataFile[f].boardID + " | " + OpenFiles.dataFile[f].boardName,
                    tests = new List<UniqueTestsClass>()
                });
                if (OpenFiles.dataFile[f].errorOpenFile != true)
                if (OpenFiles.dataFile[f].visible == true)
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
                                List<ColumnsClass> tempData = (from ColumnsClass n in OpenFiles.dataFile[f].dataFilteredByTests[filter].Tests
                                                           where n.uniqueTestName == uniTest[filter].uniqueTests[t]
                                                           select n).ToList();
                                if (tempData.Count != 0)
                                {
                                    int repeatData = 0;
                                    foreach (ColumnsClass columns in tempData)
                                    {
                                        repeatData++;
                                        uniqFile[f].tests[filter].uniqueTests.Add(new UniqueTestClass() //Если такой тест найден в разделе фильтра то создаем тест и копируем из него данные
                                        {
                                            uniqueTestName = uniTest[filter].uniqueTests[t],
                                            MR = columns.MR,
                                            MP = columns.MP,
                                            attend = true
                                        });
                                        if (repeatData > 1)
                                        {
                                            MessageBox.Show("Уникальное имя: " + uniTest[filter].uniqueTests[t], "Внимание, повторяющийся тест!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                                else
                                {
                                    uniqFile[f].tests[filter].uniqueTests.Add(new UniqueTestClass() //Если такой тест НЕнайден в разделе фильтра то всеравно создаем тест но без данных с флагом отсутсвует
                                    {
                                        uniqueTestName = uniTest[filter].uniqueTests[t],
                                        MR = 0,
                                        MP = 0,
                                        attend = false
                                    });
                                }
                            }
                        }  
                    }
            }
            //И повторяем для всех тестов
            int allTestNum = uniTest.Count - 1;
            for (int f = 0; f < OpenFiles.dataFile.Count; f++)
            {
                if (OpenFiles.dataFile[f].errorOpenFile != true)
                    if (OpenFiles.dataFile[f].visible == true)
                    {
                        uniqFile[f].tests.Add(new UniqueTestsClass() //Создали раздел с фильтра тестов
                        {
                            testName = ParseXml.Сalculations[0].Translation,
                            uniqueTests = new List<UniqueTestClass>()
                        });
                        for (int t = 0; t < uniTest[allTestNum].uniqueTests.Count; t++)
                        {
                            List<ColumnsClass> tempData = (from ColumnsClass n in OpenFiles.dataFile[f].dataParse.Test
                                                           where n.uniqueTestName == uniTest[allTestNum].uniqueTests[t]
                                                           select n).ToList();
                            if (tempData.Count != 0)
                            {
                                int repeatData = 0;
                                foreach (ColumnsClass columns in tempData)
                                {
                                    repeatData++;
                                    uniqFile[f].tests[allTestNum].uniqueTests.Add(new UniqueTestClass() //Если такой тест найден в разделе фильтра то создаем тест и копируем из него данные
                                    {
                                        uniqueTestName = uniTest[allTestNum].uniqueTests[t],
                                        MR = columns.MR,
                                        MP = columns.MP,
                                        attend = true
                                    });
                                    if (repeatData > 1)
                                    {
                                        MessageBox.Show("Уникальное имя: " + uniTest[allTestNum].uniqueTests[t], "Внимание, повторяющийся тест!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            else
                            {
                                uniqFile[f].tests[allTestNum].uniqueTests.Add(new UniqueTestClass() //Если такой тест НЕнайден в разделе фильтра то всеравно создаем тест но без данных с флагом отсутсвует
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
                    if (OpenFiles.dataFile[f].visible == true)
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
                    if (OpenFiles.dataFile[f].visible == true)
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
                    if (OpenFiles.dataFile[i].visible == true)
                    {
                        firstFile = i;
                        foreach (var test in OpenFiles.dataFile[i].dataParse.Test)
                        {
                            returnFile[lastTest].uniqueTests.Add(test.uniqueTestName);
                        }
                        i = OpenFiles.dataFile.Count;
                    }
            }
            for (int i = firstFile + 1; i < OpenFiles.dataFile.Count; i++)
            {
                if (OpenFiles.dataFile[i].errorOpenFile != true)
                    if (OpenFiles.dataFile[i].visible == true)
                    {
                        List<string> tempFile = new List<string>();
                        foreach (var test in OpenFiles.dataFile[i].dataParse.Test)
                        {
                            tempFile.Add(test.uniqueTestName);
                        }
                        returnFile[lastTest].uniqueTests = (from cell in returnFile[lastTest].uniqueTests.Union(tempFile)
                                      select cell).ToList();
                    }
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
                DoubleBufferedDataGridView dataGridView = new DoubleBufferedDataGridView();
                page.Controls.Add(dataGridView);
                dataGridView.Dock = DockStyle.Fill;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToDeleteRows = false;
                dataGridView.ReadOnly = true;
                //dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView.DataSource = view;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView.VirtualMode = true; //отрисовываются только те ячейки, которые видны в данный момент

                dataGridView.Columns["minValueP"].DefaultCellStyle.Format = "#0.0\\%";
                dataGridView.Columns["maxValueP"].DefaultCellStyle.Format = "#0.0\\%";
                dataGridView.Columns["deltaP"].DefaultCellStyle.Format = "#0.0\\%";

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
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(data, "NM", "fileMin", "minValue", "fileMax", "maxValue", "delta", "fileMinP", "minValueP", "fileMaxP", "maxValueP", "deltaP")) // (data, "Id", "Name", "Description")
            {
                table.Load(reader);
            }
            return table;
        }
    }
}
