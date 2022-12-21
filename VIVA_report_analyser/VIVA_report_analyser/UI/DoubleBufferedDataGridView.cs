using System.Windows.Forms;

namespace VIVA_report_analyser.UI
{
    /// <summary>
    /// Двойная буфферизация для класса таблиц, ускоряет их вывод на экран
    /// </summary>
    internal class DoubleBufferedDataGridView : DataGridView
    {
        protected override bool DoubleBuffered { get => true; }
    }
}
