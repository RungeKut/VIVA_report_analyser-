using System;
using System.Collections.Generic;
using System.Data;
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
        public static DataView DeviationCalculate(Dictionary<string, Dictionary<string, DataView>> filesTests)
        // Выборка результатов конкретного теста
        {
            try
            {
                foreach (var file in filesTests) // По файлам
                {
                    foreach (var tests in file.Value) // По вкладкам тестов
                    {
                        foreach (DataRow row in tests.Value) // По строкам
                        {
                            row[]
                        }
                    }
                }
                

                return null;
            }
            catch (Exception Error)
            {
                MessageBox.Show("Ошибка выборки максимального отклонения. Подробнее: " + Error.Message, "выборки максимального отклонения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new ArgumentException("Ошибка выборки максимального отклонения");
            }
        }
    }
}
