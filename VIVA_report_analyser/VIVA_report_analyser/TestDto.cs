using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIVA_report_analyser
{
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
        public const ulong Column_F   = 0x000000001; // 0 
        public const ulong Column_FT  = 0x000000002; // 1
        public const ulong Column_C   = 0x000000004; // 2
        public const ulong Column_SG1 = 0x000000008; // 3
        public const ulong Column_SG2 = 0x000000010; // 4
        public const ulong Column_PD1 = 0x000000020; // 5
        public const ulong Column_PD2 = 0x000000040; // 6
        public const ulong Column_XY1 = 0x000000080; // 7
        public const ulong Column_XY2 = 0x000000100; // 8
        public const ulong Column_CP1 = 0x000000200; // 9
        public const ulong Column_CP2 = 0x000000400; // 10
        public const ulong Column_SC  = 0x000000800; // 11
        public const ulong Column_NM  = 0x000001000; // 12
        public const ulong Column_DN  = 0x000002000; // 13
        public const ulong Column_PT  = 0x000004000; // 14
        public const ulong Column_NT  = 0x000008000; // 15
        public const ulong Column_IDC = 0x000010000; // 16
        public const ulong Column_MK  = 0x000020000; // 17
        public const ulong Column_IDM = 0x000040000; // 18
        public const ulong Column_PW  = 0x000080000; // 19
        public const ulong Column_LB  = 0x000100000; // 20
        public const ulong Column_IN  = 0x000200000; // 21
        public const ulong Column_IDL = 0x000400000; // 22
        public const ulong Column_TR  = 0x000800000; // 23
        public const ulong Column_MU  = 0x001000000; // 24
        public const ulong Column_ML  = 0x002000000; // 25
        public const ulong Column_MM  = 0x004000000; // 26
        public const ulong Column_MH  = 0x008000000; // 27
        public const ulong Column_MR  = 0x010000000; // 28
        public const ulong Column_MP  = 0x020000000; // 29
        public const ulong Column_TT  = 0x040000000; // 30
        public const ulong Column_IS  = 0x080000000; // 31
        public const ulong Column_DG  = 0x100000000; // 32

        public enum ColumsMask : ulong
        {
            Column_F   = 0x000000001, // 0 
            Column_FT  = 0x000000002, // 1
            Column_C   = 0x000000004, // 2
            Column_SG1 = 0x000000008, // 3
            Column_SG2 = 0x000000010, // 4
            Column_PD1 = 0x000000020, // 5
            Column_PD2 = 0x000000040, // 6
            Column_XY1 = 0x000000080, // 7
            Column_XY2 = 0x000000100, // 8
            Column_CP1 = 0x000000200, // 9
            Column_CP2 = 0x000000400, // 10
            Column_SC  = 0x000000800, // 11
            Column_NM  = 0x000001000, // 12
            Column_DN  = 0x000002000, // 13
            Column_PT  = 0x000004000, // 14
            Column_NT  = 0x000008000, // 15
            Column_IDC = 0x000010000, // 16
            Column_MK  = 0x000020000, // 17
            Column_IDM = 0x000040000, // 18
            Column_PW  = 0x000080000, // 19
            Column_LB  = 0x000100000, // 20
            Column_IN  = 0x000200000, // 21
            Column_IDL = 0x000400000, // 22
            Column_TR  = 0x000800000, // 23
            Column_MU  = 0x001000000, // 24
            Column_ML  = 0x002000000, // 25
            Column_MM  = 0x004000000, // 26
            Column_MH  = 0x008000000, // 27
            Column_MR  = 0x010000000, // 28
            Column_MP  = 0x020000000, // 29
            Column_TT  = 0x040000000, // 30
            Column_IS  = 0x080000000, // 31
            Column_DG  = 0x100000000  // 32
        }

        public const ulong ColumnMaskContinuity = 0x07000007C; // Тест на обрыв
        public const ulong ColumnMaskIsolation = 0x07000007C; // Тест изоляции
        public const ulong ColumnMaskResistor = 0x07F00007C; // Резисторы
        public const ulong ColumnMaskCapacitor = 0x07F00007C; // Конденсаторы
        public const ulong ColumnMaskInductance = 0x07F00007C; // Индуктивности
        public const ulong ColumnMaskAutic = 0x07F00007C; // Чип
        public const ulong ColumnMaskOther = 0x07F00007D; // Остальное

        public enum ColumMasks : ulong
        {
            ColumnMaskContinuity = 0x07000007C, // Тест на обрыв
            ColumnMaskIsolation  = 0x07000007C, // Тест изоляции
            ColumnMaskResistor   = 0x07F00007C, // Резисторы
            ColumnMaskCapacitor  = 0x07F00007C, // Конденсаторы
            ColumnMaskInductance = 0x07F00007C, // Индуктивности
            ColumnMaskAutic      = 0x07F00007C, // Чип
            ColumnMaskOther      = 0x07F00007D  // Остальное
        }

        public Type PageColumnMasks = typeof(ColumMasks);

        //public string[] TestColumnName = { "F", "FT", "C", "SG1", "SG2", "PD1", "PD2", "XY1", "XY2", "CP1", "CP2", "SC", "NM", "DN", "PT", "NT", "IDC", "MK", "IDM", "PW", "LB", "IN", "IDL", "TR", "MU", "ML", "MM", "MH", "MR", "MP", "TT", "IS", "DG" };
        public enum TestColumnName { F, FT, C, SG1, SG2, PD1, PD2, XY1, XY2, CP1, CP2, SC, NM, DN, PT, NT, IDC, MK, IDM, PW, LB, IN, IDL, TR, MU, ML, MM, MH, MR, MP, TT, IS, DG };
        

        // Методы класса

        public static void VisibleColumns(ulong ColumnsMask, DataGridView dataGridView)
        // Настройка видимости столбцов
        {
        /* F   */  if ((ColumnsMask & TestDto.Column_F)   > 0) dataGridView.Columns["F"  ].Visible = true; else dataGridView.Columns["F"  ].Visible = false;
        /* FT  */  if ((ColumnsMask & TestDto.Column_FT)  > 0) dataGridView.Columns["FT" ].Visible = true; else dataGridView.Columns["FT" ].Visible = false;
        /* C   */  if ((ColumnsMask & TestDto.Column_C)   > 0) dataGridView.Columns["C"  ].Visible = true; else dataGridView.Columns["C"  ].Visible = false;
        /* SG1 */  if ((ColumnsMask & TestDto.Column_SG1) > 0) dataGridView.Columns["SG1"].Visible = true; else dataGridView.Columns["SG1"].Visible = false;
        /* SG2 */  if ((ColumnsMask & TestDto.Column_SG2) > 0) dataGridView.Columns["SG2"].Visible = true; else dataGridView.Columns["SG2"].Visible = false;
        /* PD1 */  if ((ColumnsMask & TestDto.Column_PD1) > 0) dataGridView.Columns["PD1"].Visible = true; else dataGridView.Columns["PD1"].Visible = false;
        /* PD2 */  if ((ColumnsMask & TestDto.Column_PD2) > 0) dataGridView.Columns["PD2"].Visible = true; else dataGridView.Columns["PD2"].Visible = false;
        /* XY1 */  if ((ColumnsMask & TestDto.Column_XY1) > 0) dataGridView.Columns["XY1"].Visible = true; else dataGridView.Columns["XY1"].Visible = false;
        /* XY2 */  if ((ColumnsMask & TestDto.Column_XY2) > 0) dataGridView.Columns["XY2"].Visible = true; else dataGridView.Columns["XY2"].Visible = false;
        /* CP1 */  if ((ColumnsMask & TestDto.Column_CP1) > 0) dataGridView.Columns["CP1"].Visible = true; else dataGridView.Columns["CP1"].Visible = false;
        /* CP2 */  if ((ColumnsMask & TestDto.Column_CP2) > 0) dataGridView.Columns["CP2"].Visible = true; else dataGridView.Columns["CP2"].Visible = false;
        /* SC  */  if ((ColumnsMask & TestDto.Column_SC)  > 0) dataGridView.Columns["SC" ].Visible = true; else dataGridView.Columns["SC" ].Visible = false;
        /* NM  */  if ((ColumnsMask & TestDto.Column_NM)  > 0) dataGridView.Columns["NM" ].Visible = true; else dataGridView.Columns["NM" ].Visible = false;
        /* DN  */  if ((ColumnsMask & TestDto.Column_DN)  > 0) dataGridView.Columns["DN" ].Visible = true; else dataGridView.Columns["DN" ].Visible = false;
        /* PT  */  if ((ColumnsMask & TestDto.Column_PT)  > 0) dataGridView.Columns["PT" ].Visible = true; else dataGridView.Columns["PT" ].Visible = false;
        /* NT  */  if ((ColumnsMask & TestDto.Column_NT)  > 0) dataGridView.Columns["NT" ].Visible = true; else dataGridView.Columns["NT" ].Visible = false;
        /* IDC */  if ((ColumnsMask & TestDto.Column_IDC) > 0) dataGridView.Columns["IDC"].Visible = true; else dataGridView.Columns["IDC"].Visible = false;
        /* MK  */  if ((ColumnsMask & TestDto.Column_MK)  > 0) dataGridView.Columns["MK" ].Visible = true; else dataGridView.Columns["MK" ].Visible = false;
        /* IDM */  if ((ColumnsMask & TestDto.Column_IDM) > 0) dataGridView.Columns["IDM"].Visible = true; else dataGridView.Columns["IDM"].Visible = false;
        /* PW  */  if ((ColumnsMask & TestDto.Column_PW)  > 0) dataGridView.Columns["PW" ].Visible = true; else dataGridView.Columns["PW" ].Visible = false;
        /* LB  */  if ((ColumnsMask & TestDto.Column_LB)  > 0) dataGridView.Columns["LB" ].Visible = true; else dataGridView.Columns["LB" ].Visible = false;
        /* IN  */  if ((ColumnsMask & TestDto.Column_IN)  > 0) dataGridView.Columns["IN" ].Visible = true; else dataGridView.Columns["IN" ].Visible = false;
        /* IDL */  if ((ColumnsMask & TestDto.Column_IDL) > 0) dataGridView.Columns["IDL"].Visible = true; else dataGridView.Columns["IDL"].Visible = false;
        /* TR  */  if ((ColumnsMask & TestDto.Column_TR)  > 0) dataGridView.Columns["TR" ].Visible = true; else dataGridView.Columns["TR" ].Visible = false;
        /* MU  */  if ((ColumnsMask & TestDto.Column_MU)  > 0) dataGridView.Columns["MU" ].Visible = true; else dataGridView.Columns["MU" ].Visible = false;
        /* ML  */  if ((ColumnsMask & TestDto.Column_ML)  > 0) dataGridView.Columns["ML" ].Visible = true; else dataGridView.Columns["ML" ].Visible = false;
        /* MM  */  if ((ColumnsMask & TestDto.Column_MM)  > 0) dataGridView.Columns["MM" ].Visible = true; else dataGridView.Columns["MM" ].Visible = false;
        /* MH  */  if ((ColumnsMask & TestDto.Column_MH)  > 0) dataGridView.Columns["MH" ].Visible = true; else dataGridView.Columns["MH" ].Visible = false;
        /* MR  */  if ((ColumnsMask & TestDto.Column_MR)  > 0) dataGridView.Columns["MR" ].Visible = true; else dataGridView.Columns["MR" ].Visible = false;
        /* MP  */  if ((ColumnsMask & TestDto.Column_MP)  > 0) dataGridView.Columns["MP" ].Visible = true; else dataGridView.Columns["MP" ].Visible = false;
        /* TT  */  if ((ColumnsMask & TestDto.Column_TT)  > 0) dataGridView.Columns["TT" ].Visible = true; else dataGridView.Columns["TT" ].Visible = false;
        /* IS  */  if ((ColumnsMask & TestDto.Column_IS)  > 0) dataGridView.Columns["IS" ].Visible = true; else dataGridView.Columns["IS" ].Visible = false;
        /* DG  */  if ((ColumnsMask & TestDto.Column_DG)  > 0) dataGridView.Columns["DG" ].Visible = true; else dataGridView.Columns["DG" ].Visible = false;
        }
    }
}
