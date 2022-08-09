using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIVA_report_analyser
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        public static string GetName<T>(int value)
        {
            return Enum.GetName(typeof(T), value);
        }
    }

    //public static IEnumerable<T> GetEnumValues<T>()
    //{
    //    // Can't use type constraints on value types, so have to do check like this
    //    if (typeof(T).BaseType != typeof(Enum))
    //    {
    //        throw new ArgumentException("T must be of type System.Enum");
    //    }
    //
    //    return Enum.GetValues(typeof(T)).Cast<T>();
    //}

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

        // Константы класса
        public enum ColumnMask : ulong
        {
            F   = 0x000000001, // 0 
            FT  = 0x000000002, // 1
            C   = 0x000000004, // 2
            SG1 = 0x000000008, // 3
            SG2 = 0x000000010, // 4
            PD1 = 0x000000020, // 5
            PD2 = 0x000000040, // 6
            XY1 = 0x000000080, // 7
            XY2 = 0x000000100, // 8
            CP1 = 0x000000200, // 9
            CP2 = 0x000000400, // 10
            SC  = 0x000000800, // 11
            NM  = 0x000001000, // 12
            DN  = 0x000002000, // 13
            PT  = 0x000004000, // 14
            NT  = 0x000008000, // 15
            IDC = 0x000010000, // 16
            MK  = 0x000020000, // 17
            IDM = 0x000040000, // 18
            PW  = 0x000080000, // 19
            LB  = 0x000100000, // 20
            IN  = 0x000200000, // 21
            IDL = 0x000400000, // 22
            TR  = 0x000800000, // 23
            MU  = 0x001000000, // 24
            ML  = 0x002000000, // 25
            MM  = 0x004000000, // 26
            MH  = 0x008000000, // 27
            MR  = 0x010000000, // 28
            MP  = 0x020000000, // 29
            TT  = 0x040000000, // 30
            IS  = 0x080000000, // 31
            DG  = 0x100000000  // 32
        }

        public const ulong ColumnMaskContinuity = 0x07000007C; // Тест на обрыв
        public const ulong ColumnMaskIsolation = 0x07000007C; // Тест изоляции
        public const ulong ColumnMaskResistor = 0x07F00007C; // Резисторы
        public const ulong ColumnMaskCapacitor = 0x07F00007C; // Конденсаторы
        public const ulong ColumnMaskInductance = 0x07F00007C; // Индуктивности
        public const ulong ColumnMaskAutic = 0x07F00007C; // Чип
        public const ulong ColumnMaskOther = 0x07F00007D; // Остальное

        public enum TestMask : ulong
        {
            Continuity = 0x07000007C, // Тест на обрыв
            Isolation  = 0x07000007C, // Тест изоляции
            Resistor   = 0x07F00007C, // Резисторы
            Capacitor  = 0x07F00007C, // Конденсаторы
            Inductance = 0x07F00007C, // Индуктивности
            Autic      = 0x07F00007C, // Чип
            Other      = 0x07F00007D  // Остальное
        }

        public Type TypeColumnMask = typeof(ColumnMask);
        public Type TypeTestMask = typeof(TestMask);

        public static List<string> columnName = new List<string>()
        {
            "F",   // 0 
            "F",   // 1
            "C",   // 2
            "SG1", // 3
            "SG2", // 4
            "PD1", // 5
            "PD2", // 6
            "XY1", // 7
            "XY2", // 8
            "CP1", // 9
            "CP2", // 10
            "SC",  // 11
            "NM",  // 12
            "DN",  // 13
            "PT",  // 14
            "NT",  // 15
            "IDC", // 16
            "MK",  // 17
            "IDM", // 18
            "PW",  // 19
            "LB",  // 20
            "IN",  // 21
            "IDL", // 22
            "TR",  // 23
            "MU",  // 24
            "ML",  // 25
            "MM",  // 26
            "MH",  // 27
            "MR",  // 28
            "MP",  // 29
            "TT",  // 30
            "IS",  // 31
            "DG"   // 32
        };

        public static string[] TestColumnName = new string[33]
        {
            "Тест",// F
            "Функция",// FT
            "Каналы",// C
            "Имя цепи 1",// SG1
            "Имя цепи 2",// SG2
            "Точка подключения 1",// PD1
            "Точка подключения 2",// PD2
            "Координаты подключения 1",// XY1
            "Координаты подключения 2",// XY2
            "CP1",// CP1
            "CP2",// CP2
            "SC",// SC
            "NM",// NM
            "DN",// DN
            "PT",// PT
            "NT",// NT
            "IDC",// IDC
            "MK",// MK
            "IDM",// IDM
            "PW",// PW
            "LB",// LB
            "IN",// IN
            "IDL",// IDL
            "TR",// TR
            "MU",// MU
            "ML",// ML
            "MM",// MM
            "MH",// MH
            "MR",// MR
            "MP",// MP
            "TT",// TT
            "IS",// IS
            "DG" // DG
        };

        // Методы класса

        public static void VisibleColumns(ulong ColumnsMask, DataGridView dataGridView)
        // Настройка видимости столбцов
        {
            int i = 0;
            foreach (ulong column in EnumUtil.GetValues<ColumnMask>())
            {
                if ((ColumnsMask & column) > 0) dataGridView.Columns[columnName[i]].Visible = true; else dataGridView.Columns[columnName[i]].Visible = false;
                i++;
            };
        }
    }

    class DoubleBufferedDataGridView : DataGridView
    {
        protected override bool DoubleBuffered { get => true; }
    }
}
