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
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        public static string GetName<T>(int value)
            // Работает плохо. Только первые три члена - потом Null. Проблема глубоко
        {
            return Enum.GetName(typeof(T), value);
        }
    }
    internal class VivaXmlColumnsClass
    {
        // Поля класса
        public string Name { get; set; }
        public string Translation { get; set; }
        public ulong Mask { get; set; }
    }
    internal class VivaXmlTestsClass : VivaXmlColumnsClass { }
    internal class СalculationsClass : VivaXmlColumnsClass { }
    internal class TestDto
    {
        // Поля класса
        public String F { get; set; }
        public String FT { get; set; }
        public String C { get; set; }
        public String SG1 { get; set; }
        public String SG2 { get; set; }
        public String PD1 { get; set; }
        public String PD2 { get; set; }
        public String XY1 { get; set; }
        public String XY2 { get; set; }
        public String CP1 { get; set; }
        public String CP2 { get; set; }
        public String SC { get; set; }
        public String NM { get; set; }
        public String DN { get; set; }
        public Double PT { get; set; }
        public Double NT { get; set; }
        public Double IDC { get; set; }
        public String MK { get; set; }
        public Double IDM { get; set; }
        public Double PW { get; set; }
        public String LB { get; set; }
        public String IN { get; set; }
        public Double IDL { get; set; }
        public Double TR { get; set; }
        public String MU { get; set; }
        public Double ML { get; set; }
        public Double MM { get; set; }
        public Double MH { get; set; }
        public Double MR { get; set; }
        public Double MP { get; set; }
        public Double TT { get; set; }
        public Double IS { get; set; }
        public Double DG { get; set; }
        public static bool errorOpenFileFlag = false;
        public static List<string> openFilesNames = new List<string>() { null };
        public static List<string> errorOpenFilesNames = new List<string>() { null };
        // Константы класса
        public static List<VivaXmlColumnsClass> vivaXmlColumns = new List<VivaXmlColumnsClass>
        {
            new VivaXmlColumnsClass { Name = "F"  , Translation ="Тест",                     Mask = 0x000000001 }, // 0
            new VivaXmlColumnsClass { Name = "FT" , Translation ="Функция",                  Mask = 0x000000002 }, // 1
            new VivaXmlColumnsClass { Name = "C"  , Translation ="Каналы",                   Mask = 0x000000004 }, // 2
            new VivaXmlColumnsClass { Name = "SG1", Translation ="Имя цепи 1",               Mask = 0x000000008 }, // 3
            new VivaXmlColumnsClass { Name = "SG2", Translation ="Имя цепи 2",               Mask = 0x000000010 }, // 4
            new VivaXmlColumnsClass { Name = "PD1", Translation ="Точка подключения 1",      Mask = 0x000000020 }, // 5
            new VivaXmlColumnsClass { Name = "PD2", Translation ="Точка подключения 2",      Mask = 0x000000040 }, // 6
            new VivaXmlColumnsClass { Name = "XY1", Translation ="Координаты подключения 1", Mask = 0x000000080 }, // 7
            new VivaXmlColumnsClass { Name = "XY2", Translation ="Координаты подключения 2", Mask = 0x000000100 }, // 8
            new VivaXmlColumnsClass { Name = "CP1", Translation ="CP1",                      Mask = 0x000000200 }, // 9
            new VivaXmlColumnsClass { Name = "CP2", Translation ="CP2",                      Mask = 0x000000400 }, // 10
            new VivaXmlColumnsClass { Name = "SC" , Translation ="SC",                       Mask = 0x000000800 }, // 11
            new VivaXmlColumnsClass { Name = "NM" , Translation ="NM",                       Mask = 0x000001000 }, // 12
            new VivaXmlColumnsClass { Name = "DN" , Translation ="DN",                       Mask = 0x000002000 }, // 13
            new VivaXmlColumnsClass { Name = "PT" , Translation ="PT",                       Mask = 0x000004000 }, // 14
            new VivaXmlColumnsClass { Name = "NT" , Translation ="NT",                       Mask = 0x000008000 }, // 15
            new VivaXmlColumnsClass { Name = "IDC", Translation ="IDC",                      Mask = 0x000010000 }, // 16
            new VivaXmlColumnsClass { Name = "MK" , Translation ="MK",                       Mask = 0x000020000 }, // 17
            new VivaXmlColumnsClass { Name = "IDM", Translation ="IDM",                      Mask = 0x000040000 }, // 18
            new VivaXmlColumnsClass { Name = "PW" , Translation ="PW",                       Mask = 0x000080000 }, // 19
            new VivaXmlColumnsClass { Name = "LB" , Translation ="LB",                       Mask = 0x000100000 }, // 20
            new VivaXmlColumnsClass { Name = "IN" , Translation ="IN",                       Mask = 0x000200000 }, // 21
            new VivaXmlColumnsClass { Name = "IDL", Translation ="IDL",                      Mask = 0x000400000 }, // 22
            new VivaXmlColumnsClass { Name = "TR" , Translation ="TR",                       Mask = 0x000800000 }, // 23
            new VivaXmlColumnsClass { Name = "MU" , Translation ="Единицы измерения",        Mask = 0x001000000 }, // 24
            new VivaXmlColumnsClass { Name = "ML" , Translation ="Минимальное",              Mask = 0x002000000 }, // 25
            new VivaXmlColumnsClass { Name = "MM" , Translation ="Уставка",                  Mask = 0x004000000 }, // 26
            new VivaXmlColumnsClass { Name = "MH" , Translation ="Максимальное",             Mask = 0x008000000 }, // 27
            new VivaXmlColumnsClass { Name = "MR" , Translation ="Измеренное",               Mask = 0x010000000 }, // 28
            new VivaXmlColumnsClass { Name = "MP" , Translation ="Отклонение, %",            Mask = 0x020000000 }, // 29
            new VivaXmlColumnsClass { Name = "TT" , Translation ="TT",                       Mask = 0x040000000 }, // 30
            new VivaXmlColumnsClass { Name = "IS" , Translation ="IS",                       Mask = 0x080000000 }, // 31
            new VivaXmlColumnsClass { Name = "DG" , Translation ="DG",                       Mask = 0x100000000 }  // 32
        };
        public static List<VivaXmlTestsClass> vivaXmlTests = new List<VivaXmlTestsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного теста
        {
            new VivaXmlTestsClass { Name = "CONTINUITY", Translation ="Тест на обрыв", Mask = 0x07000007C }, // 0 
            new VivaXmlTestsClass { Name = "ISOLATION",  Translation ="Тест изоляции", Mask = 0x07000007C }, // 1
            new VivaXmlTestsClass { Name = "RESISTOR",   Translation ="Резисторы",     Mask = 0x07F00007C }, // 2
            new VivaXmlTestsClass { Name = "CAPACITOR",  Translation ="Конденсаторы",  Mask = 0x07F00007C }, // 3
            new VivaXmlTestsClass { Name = "INDUCTANCE", Translation ="Индуктивности", Mask = 0x07F00007C }, // 4
            new VivaXmlTestsClass { Name = "AUTIC",      Translation ="Чип",           Mask = 0x07F00007C }, // 5
        };
        public static List<СalculationsClass> Сalculations = new List<СalculationsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного вычисления
        {
            new СalculationsClass { Name = "Other", Translation ="Остальное", Mask = 0x07F00007D }, // 0 
            new СalculationsClass { Name = "MaxDeviation",  Translation ="MAX отклонение", Mask = 0xFFFFFFFFF }, // 1
        };

        // Методы класса

        public static void VisibleColumns(ulong ColumnsMask, DataGridView dataGridView)
        // Настройка видимости столбцов
        {
            foreach (var column in TestDto.vivaXmlColumns)
            {
                if ((ColumnsMask & column.Mask) > 0) dataGridView.Columns[column.Name].Visible = true;
                                                else dataGridView.Columns[column.Name].Visible = false;
            };
        }
        public static Dictionary<string, DataView> SelectComponentTests(List<VivaXmlTestsClass> Tests, XElement data)
        // Выборка результатов конкретного теста
        {
            try
            {
                var filteredTest = new Dictionary<string, DataView>();
                foreach (var test in Tests)
                {
                    var temp1 = from n in data.Descendants("BI").Elements("TEST")
                                where n.Attribute("F").Value == test.Name
                                select n;

                    List<XElement> temp2 = temp1.ToList();
                    DataView temp3 = ParseToDataView(temp2);
                    filteredTest.Add(test.Name, temp3);
                }
                filteredTest.Add(TestDto.Сalculations[0].Name, ParseToDataView(data.Element("BI").Elements("TEST").ToList()));
                return filteredTest;
            }
            catch (Exception ReadFileError)
            {
                MessageBox.Show("Ошибка выборки результатов теста. Подробнее: " + ReadFileError.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new ArgumentException("Ошибка выборки результатов теста");
            }
        }

        private static DataView ParseToDataView(List<XElement> data)
        {
            var temp3 = data.Select(t =>
                new TestDto
                {
                    F = t.Attribute("F").Value,
                    FT = t.Attribute("FT").Value,
                    C = t.Attribute("C").Value,
                    SG1 = t.Attribute("SG1").Value,
                    SG2 = t.Attribute("SG2").Value,
                    PD1 = t.Attribute("PD1").Value,
                    PD2 = t.Attribute("PD2").Value,
                    XY1 = t.Attribute("XY1").Value,
                    XY2 = t.Attribute("XY2").Value,
                    CP1 = t.Attribute("CP1").Value,
                    CP2 = t.Attribute("CP2").Value,
                    SC = t.Attribute("SC").Value,
                    NM = t.Attribute("NM").Value,
                    DN = t.Attribute("DN").Value,
                    PT = Double.Parse(t.Attribute("PT").Value, new CultureInfo("en-US")),
                    NT = Double.Parse(t.Attribute("NT").Value, new CultureInfo("en-US")),
                    IDC = Double.Parse(t.Attribute("IDC").Value, new CultureInfo("en-US")),
                    MK = t.Attribute("MK").Value,
                    IDM = Double.Parse(t.Attribute("IDM").Value, new CultureInfo("en-US")),
                    PW = Double.Parse(t.Attribute("PW").Value, new CultureInfo("en-US")),
                    LB = t.Attribute("LB").Value,
                    IN = t.Attribute("IN").Value,
                    IDL = Double.Parse(t.Attribute("IDL").Value, new CultureInfo("en-US")),
                    TR = Double.Parse(t.Attribute("TR").Value, new CultureInfo("en-US")),
                    MU = t.Attribute("MU").Value,
                    ML = Double.Parse(t.Attribute("ML").Value, new CultureInfo("en-US")),
                    MM = Double.Parse(t.Attribute("MM").Value, new CultureInfo("en-US")),
                    MH = Double.Parse(t.Attribute("MH").Value, new CultureInfo("en-US")),
                    MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                    MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")),
                    TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US")),
                    IS = Double.Parse(t.Attribute("IS").Value, new CultureInfo("en-US")),
                    DG = Double.Parse(t.Attribute("DG").Value, new CultureInfo("en-US"))
                });

            DataTable temp4 = ConvertToDataTable(temp3.ToList());
            return temp4.DefaultView;
        }

        private static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            //uint i = 0;
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); //prop.Name
                //table.Columns.
                //i++;
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
    class DoubleBufferedDataGridView : DataGridView
    // Двойная буфферизация для таблиц, ускоряет работу
    {
        protected override bool DoubleBuffered { get => true; }
    }
}
