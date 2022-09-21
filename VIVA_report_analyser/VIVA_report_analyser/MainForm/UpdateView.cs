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
        public static void updateForm()
        {
            uint i = 0;
            while (true)
            {
                if (DataModel.dataFiles.needUpdateView)
                {
                    //MainForm.mainForm.tabControl2.Invoke(new Action(() => { MainForm.mainForm.tabControl2.SuspendLayout(); }));
                    for (int file = 0; file < DataModel.dataFiles.Count; file++) // Перебираем открытые файлы
                    {
                        if (!DataModel.dataFiles[file].errorOpen) // Если файл смог открыться
                        {
                            for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++) // Перебираем все секции с платами в одном файле
                            {
                                if (!DataModel.dataFiles[file].biSec.BI[numBI].visible) // Если секция еще не отображается, то создаем новую вкладку с ней
                                if (DataModel.dataFiles[file].biSec.BI[numBI].closeNumber == 0)
                                {
                                    string tabName = DataModel.dataFiles[file].Name + " | " + DataModel.dataFiles[file].biSec.BI[numBI].ID + " | " + DataModel.dataFiles[file].biSec.BI[numBI].BC;
                                    TabPage page = new TabPage(tabName);
                                    page.Name = tabName;

                                    TabControl tabTests = new TabControl();
                                    page.Controls.Add(tabTests);
                                    tabTests.Dock = DockStyle.Fill;
                                    tabTests.ItemSize = new System.Drawing.Size(0, 24);
                                    tabTests.SelectedIndex = 0;
                                    tabTests.TabIndex = 1;
                                    tabTests.Name = DataModel.dataFiles[file].Name;

                                    for (int test = 0; test < ParseXml.testCount; test++)
                                    {
                                        AddNewComponentTab
                                        (
                                            ParseXml.vivaXmlTests[test].translation,
                                            tabTests,
                                            DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].Tests,
                                            DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].Tests.Count,
                                            DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].errorTests.Count
                                        );

                                    }
                                    AddNewComponentTab
                                    (
                                        ParseXml.Сalculations[0].translation,
                                        tabTests,
                                        DataModel.dataFiles[file].biSec.BI[numBI].testsSec.TEST,
                                        DataModel.dataFiles[file].biSec.BI[numBI].testsSec.TEST.Count,
                                        0
                                    );
                                    MainForm.mainForm.tabControl2.BeginInvoke(new Action(() =>
                                    {
                                        MainForm.mainForm.tabControl2.TabPages.Add(page);
                                    }));
                                    /*MainForm.mainForm.tabControl2.Invoke(new Action(() =>
                                    {
                                        MainForm.mainForm.tabControl2.TabPages.Add(page);
                                    }));*/
                                    DataModel.dataFiles[file].biSec.BI[numBI].visible = true;
                                }
                            }
                        }
                    }
                    //MainForm.mainForm.tabControl2.Invoke(new Action(() => { MainForm.mainForm.tabControl2.ResumeLayout(); }));
                }
                else
                {
                    Thread.Sleep(1000);
                    i++;
                    //log.Info("Поток обновления формы спит " + i + " сек.");
                }
            }
        }
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
        public static void CreateTabDeviationCalc()
        {
            int tabOpenCount = 0;
            for (int file = 0; file < DataModel.dataFiles.Count; file++) // Перебираем открытые файлы
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++) // Перебираем все секции с платами в одном файле
                {
                    if (DataModel.dataFiles[file].biSec.BI[numBI].visible)
                        tabOpenCount++;
                    if (tabOpenCount > 1) break;
                }
                if (tabOpenCount > 1) break;
            }
            if (tabOpenCount == 0)
            {
                MessageBox.Show("Нет открытых файлов", "Не буду считать!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MainForm.mainForm.button2.Invoke(new Action(() => { MainForm.mainForm.button2.Enabled = true; }));
                return;
            }
            if (tabOpenCount == 1)
            {
                MessageBox.Show("Необходимо хотя бы ДВА открытых файла для выборки значений", "Не буду считать!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MainForm.mainForm.button2.Invoke(new Action(() => { MainForm.mainForm.button2.Enabled = true; }));
                return;
            }

            List<MaxDeviationCalculateFilteredTests> data = MaxDeviationCalculate.DeviationCalculate();
            TabPage page = new TabPage(ParseXml.Сalculations[1].translation);
            page.Name = ParseXml.Сalculations[1].translation;
            page.Visible = true;
            page.Select();
            TabControl tabTests = new TabControl();
            page.Controls.Add(tabTests);
            tabTests.Dock = DockStyle.Fill;
            tabTests.ItemSize = new System.Drawing.Size(0, 24);
            tabTests.SelectedIndex = 0;
            tabTests.TabIndex = 1;
            tabTests.Name = ParseXml.Сalculations[1].translation;
            tabTests.Visible = true;
            //int tabCount = tabControl2.TabCount;


            //for (int test = 0; test < ParseXml.testCount; test++)
            foreach (var test in data)
            {
                MaxDeviationCalculate.DeviationAddNewComponentTab
                (
                    test.testName,
                    tabTests,
                    test.data
                );

            }
            MainForm.mainForm.Invoke(new Action(() =>
            {
                MainForm.mainForm.tabControl2.TabPages.Add(page);
                MainForm.mainForm.tabControl2.SelectTab(MainForm.mainForm.tabControl2.TabCount - 1);
                MainForm.mainForm.button2.Enabled = true;
            }));
        }
    }
}
