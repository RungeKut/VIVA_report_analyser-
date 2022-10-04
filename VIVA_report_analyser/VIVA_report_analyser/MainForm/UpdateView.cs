using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIVA_report_analyser.MainForm
{
    public class UpdateView
    {
        public static int activeComponentPage = 0;
        public static void AddNewComponentTab<T>(string nameTab, TabControl tabControl, IList<T> data, int testCount, int errCount)
        // Создание фкладки с именем компонента во вкладке с файлом
        {
            try
            {
                if (testCount <= 0) return;
                DataView view = ParseXml.ConvertToDataTable(data).DefaultView;
                TabPage page;
                if (errCount <= 0)
                    page = new TabPage(nameTab + " (" + testCount + ")");
                else
                    page = new TabPage(nameTab + " (" + testCount + ") (" + errCount + ")");
                tabControl.TabPages.Add(page);
                DoubleBufferedDataGridView dataGridView = new DoubleBufferedDataGridView();
                page.Controls.Add(dataGridView);
                page.MouseClick += ComponentPage_MouseClick1;
                dataGridView.Dock = DockStyle.Fill;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToDeleteRows = false;
                dataGridView.ReadOnly = true;
                dataGridView.AutoGenerateColumns = true;
                dataGridView.RowHeadersVisible = false;
                //dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True; //Переносить название колонок на новую строку
                dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //Запрещаем изменение высоты строк
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //Запрещаем изменение ширины столбцов
                dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize; //Запрещаем изменение высоты шапки таблицы
                dataGridView.DataSource = view;
                //dataGridView.VirtualMode = true; //отрисовываются только те ячейки, которые видны в данный момент
                dataGridView.RowsDefaultCellStyle.BackColor = Color.Ivory; //Строки всей таблицы
                dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.MintCream; //Цвет четных строк
                dataGridView.RowPrePaint += DataGridView_RowPrePaint;
                dataGridView.RowPostPaint += DataGridView_RowPostPaint;
                dataGridView.SortCompare += DataGridView_SortCompare;
                //dataGridView.AutoResizeColumns();
            }
            catch (Exception ReadFileError)
            {
                MessageBox.Show("Ошибка при создании вкладки " + nameTab + ". Подробнее: " + ReadFileError.Message, "Ошибка создания вкладки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid != null)
            {
                foreach (var tr in ParseXml.vivaXmlColumns)
                {
                    if (grid.Columns[tr.name] != null)
                    {
                        grid.Columns[tr.name].HeaderText = tr.translation;
                    }
                }
                if (grid.Columns["TR"] != null)
                {
                    grid.Columns["TR"].Visible = false;
                }
                if (grid.Columns["MP"] != null)
                    grid.Columns["MP"].DefaultCellStyle.Format = "#0.0\\%";
            }
        }

        private static void DataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        { //Это событие возникает только в том случае, если DataSource свойство не задано, а VirtualMode значение свойства равно false.
            DataGridView grid = sender as DataGridView;
            if (grid != null)
            {
                e.SortResult = System.String.Compare(
                    grid.Rows[e.RowIndex1].Cells["TR"].Value.ToString(),
                    grid.Rows[e.RowIndex2].Cells["MP"].Value.ToString());
            }
        }

        private static void ComponentPage_MouseClick1(object sender, MouseEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabPage page = sender as TabPage;
            if (page != null)
            {
                
            }
        }

        private static void DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid != null)
            {
                if (grid.Columns["TR"] != null)
                {
                    if (Double.Parse(grid["TR", e.RowIndex].Value.ToString(), new CultureInfo("en-US")) > 0)
                        grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
                }
            }
        }
    }
}
