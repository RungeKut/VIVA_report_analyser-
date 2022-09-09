using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using NLog;

namespace VIVA_report_analyser
{
    public class DoubleBufferedDataGridView : DataGridView
    // Двойная буфферизация для таблиц, ускоряет работу
    {
        protected override bool DoubleBuffered { get => true; }
    }
    public partial class Form1 : Form
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public Form1()
        {
            log.Info("InitializeComponent main Form");
            InitializeComponent();
            RightMouseClickFileTab.rightMouseClickFileTabContextMenuStrip = RightMouseClickFileTab.InitializeRightMouseClickFileTab(tabControl2);
        }
        public static Dictionary<string, Dictionary<string, DataTable>> filteredTestOnFile = new Dictionary<string, Dictionary<string, DataTable>>();
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFiles.LoadXmlDocument();
            /*
            if (OpenFiles.dataFile == null) return;
            for (int file = 0; file < OpenFiles.dataFile.Count; file++)
            {
                if (OpenFiles.dataFile[file].errorOpenFile != true)
                if (OpenFiles.dataFile[file].visible != true)
                    {
                        string tabName = OpenFiles.dataFile[file].fileName + " | " + OpenFiles.dataFile[file].boardID + " | " + OpenFiles.dataFile[file].boardName;
                        TabPage page = new TabPage(tabName);
                        page.Name = tabName;
                        tabControl2.TabPages.Add(page);
                        page.MouseClick += Page_MouseClick;
                        TabControl tabTests = new TabControl();
                        page.Controls.Add(tabTests);
                        tabTests.Dock = DockStyle.Fill;
                        tabTests.ItemSize = new System.Drawing.Size(0, 24);
                        tabTests.SelectedIndex = 0;
                        tabTests.TabIndex = 1;
                        tabTests.Name = OpenFiles.dataFile[file].fileName;

                        for (int test = 0; test < ParseXml.testCount; test++)
                        {
                            AddNewComponentTab
                            (
                                ParseXml.vivaXmlTests[test].translation,
                                tabTests,
                                OpenFiles.dataFile[file].dataFilteredByTests[test].Tests
                            );
                            
                        }
                        AddNewComponentTab
                            (
                                ParseXml.Сalculations[0].translation,
                                tabTests,
                                OpenFiles.dataFile[file].dataParse.Test
                            );
                        OpenFiles.dataFile[file].visible = true;
                    }
            }*/
        }
        private void Page_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                MessageBox.Show("Ошибка", "Ошибка созкладки", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ComponentTab_MouseClick(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public static void AddNewComponentTab<T>(string nameTab, TabControl tabControl, IList<T> data)
        // Создание фкладки с именем компонента во вкладке с файлом
        {
            try
            {
                DataView view = ParseXml.ConvertToDataTable(data).DefaultView;
                int rowCount = view.Count;
                TabPage page = new TabPage(nameTab + " (" + rowCount + ")");
                //tabControl.Click += ComponentTab_MouseClick;
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
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                
                dataGridView.DataSource = view;
                dataGridView.VirtualMode = true; //отрисовываются только те ячейки, которые видны в данный момент
                //dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
                if (dataGridView.Columns["MP"] != null)
                dataGridView.Columns["MP"].DefaultCellStyle.Format = "#0.0\\%";
                dataGridView.TopLeftHeaderCell.Value = "Тест"; // Заголовок столбца названия строк
                //DataGridView.Columns[0].HeaderText = "название столбца";
                //dataGridView.Rows[0].HeaderCell.Value = "Название строки";
                /*if (view.Count > 0)
                {
                    int i = 0;
                    foreach (var columnHeader in ParseXml.vivaXmlColumns)
                    {
                        dataGridView.Columns[i].HeaderText = columnHeader.Translation;
                        i++;
                    }
                    for (i = 1; i <= rowCount; i++)
                    {
                        dataGridView.Rows[i - 1].HeaderCell.Value = i.ToString();
                    }
                }*/
                dataGridView.AutoResizeColumns();
                //dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            }
            catch (Exception ReadFileError)
            {
                MessageBox.Show("Ошибка при создании вкладки " + nameTab + ". Подробнее: " + ReadFileError.Message, "Ошибка создания вкладки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Выбрать столбцы"));
                m.MenuItems.Add(new MenuItem("Скрыть"));
                m.MenuItems.Add(new MenuItem("Помощь"));

                m.Show(tabControl2, e.Location);
                tabControl2.ContextMenu = m;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int tabOpenCount = 0;
                /*foreach (var d in OpenFiles.dataFile)
                {
                    if (d.visible)
                        tabOpenCount++;
                }*/
                if (tabOpenCount == 0) throw new ArgumentException("Нет открытых файлов");
                if (tabOpenCount == 1) throw new ArgumentException("Необходимо хотя бы ДВА открытых файла для выборки значений");
                List<MaxDeviationCalculateFilteredTests> data = MaxDeviationCalculate.DeviationCalculate();
                TabPage page = new TabPage(ParseXml.Сalculations[1].translation);
                page.Name = ParseXml.Сalculations[1].translation;
                tabControl2.TabPages.Add(page);
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
                tabControl2.SelectTab(tabControl2.TabCount - 1);

                //for (int test = 0; test < ParseXml.testCount; test++)
                foreach ( var test in data)
                {
                    MaxDeviationCalculate.DeviationAddNewComponentTab
                    (
                        test.testName,
                        tabTests,
                        test.data
                    );

                }
            }
            catch (Exception CalculateError)
            {
                MessageBox.Show("Ошибка при создании вкладки вычислений.\nПодробнее:\n" + CalculateError.Message, "Ошибка вычисления максимального отклонения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("т.к. это хорошо заметно в последнем столбце расчетов МАХ отклонения", "Нереализованная функциональность", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    
}
