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

namespace VIVA_report_analyser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static Dictionary<string, Dictionary<string, DataTable>> filteredTestOnFile = new Dictionary<string, Dictionary<string, DataTable>>();
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            TestDto.ExtractData(TestDto.LoadXmlDocument());
            
            foreach (var files in TestDto.dataFile)
            {
                TabPage page = new TabPage(files.fileName);
                tabControl2.TabPages.Add(page);

                tabControl2.MouseClick += FileTab_MouseClick;
                page.MouseClick += Page_MouseClick;

                TabControl tabTests = new TabControl();
                page.Controls.Add(tabTests);
                tabTests.Dock = DockStyle.Fill;
                tabTests.ItemSize = new System.Drawing.Size(0, 24);
                tabTests.SelectedIndex = 0;
                tabTests.TabIndex = 1;
                tabTests.Name = files.fileName;
                int numTest = TestDto.vivaXmlTests.Count;
                List<XElement> gettedView = new List<XElement>();
                
                
                foreach (var test in TestDto.vivaXmlTests)
                {
                    if (files.filterByTests.TryGetValue(test.Translation, out gettedView))
                        AddNewComponentTab(test.Translation, tabTests, gettedView, test.Mask);
                    else
                        MessageBox.Show("Ошибка чтения словаря по ключу " + test.Name, "Ошибка чтения данных словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (files.filterByTests.TryGetValue(TestDto.Сalculations[0].Translation, out gettedView))
                    AddNewComponentTab(TestDto.Сalculations[0].Translation, tabTests, gettedView, TestDto.Сalculations[0].Mask);
                else
                    MessageBox.Show("Ошибка чтения словаря по ключу " + TestDto.Сalculations[0].Name, "Ошибка чтения данных словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
            //ContextMenuStrip FileTabMenu = new ContextMenuStrip();
            //ToolStripMenuItem CloseTab_MenuItem = new ToolStripMenuItem("Закрыть вкладку");
            //ToolStripMenuItem CloseTabs_MenuItem = new ToolStripMenuItem("Закрыть все вкладки");
            //ToolStripMenuItem RecoverTab_MenuItem = new ToolStripMenuItem("Открыть закрытую вкладку");
            //
            //FileTabMenu.Items.AddRange(new[]
            //{
            //    CloseTab_MenuItem,
            //    CloseTabs_MenuItem,
            //    RecoverTab_MenuItem
            //});
            //page.ContextMenuStrip = FileTabMenu;
            //
            //
            //
            //DataTable gettedView;
            //foreach (var test in TestDto.vivaXmlTests)
            //{
            //    if (filteredTest.TryGetValue(test.Translation, out gettedView))
            //        AddNewComponentTab(test.Translation, tabTests, gettedView.DefaultView, test.Mask);
            //    else
            //        MessageBox.Show("Ошибка чтения словаря по ключу " + test.Name, "Ошибка чтения данных словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //
            //if (filteredTest.TryGetValue(TestDto.Сalculations[0].Translation, out gettedView))
            //    AddNewComponentTab(TestDto.Сalculations[0].Translation, tabTests, gettedView.DefaultView, TestDto.Сalculations[0].Mask);
            //else
            //    MessageBox.Show("Ошибка чтения словаря по ключу " + TestDto.Сalculations[0].Name, "Ошибка чтения данных словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //
            //TestDto.openFilesNames.Add(tabTests.Name);
            //
            //filteredTestOnFile.Add(tabTests.Name, filteredTest);


        }

        public int nowMouseClickFileTab = 0;
        private void FileTab_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip fileTabMenu = new ContextMenuStrip();
                ToolStripMenuItem CloseTab_MenuItem = new ToolStripMenuItem("Закрыть вкладку");
                ToolStripMenuItem CloseTabs_MenuItem = new ToolStripMenuItem("Закрыть все вкладки");
                ToolStripMenuItem RecoverTab_MenuItem = new ToolStripMenuItem("Открыть закрытую вкладку");

                fileTabMenu.Items.AddRange(new[]
                {
                    CloseTab_MenuItem,
                    CloseTabs_MenuItem,
                    RecoverTab_MenuItem
                });
                tabControl2.ContextMenuStrip = fileTabMenu;

                //FileTabMenu.Tag = FileTabMenu.AccessibilityObject;
                CloseTab_MenuItem.Click += CloseTab_MenuItem_Click;
                fileTabMenu.Show(tabControl2, e.Location);

                for (int i = 0; i < tabControl2.TabPages.Count; i++)
                {
                    if (tabControl2.GetTabRect(i).Contains(e.Location))
                    {
                        nowMouseClickFileTab = i;
                        return;
                    }
                }

            }
        }

        private void CloseTab_MenuItem_Click(object sender, EventArgs e)
        {
            //tabControl1.TabPages.Remove(((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as );
            //var sourceControl = ((ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent()).SourceControl;
            tabControl2.TabPages.Remove(tabControl2.TabPages[nowMouseClickFileTab] as TabPage);
        }

        private void Page_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void ComponentTab_MouseClick(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void AddNewComponentTab(string nameTab, TabControl tabControl, List<XElement> data, ulong ColumnMask)
        // Создание фкладки с именем компонента во вкладке с файлом
        {
            try
            {
                DataView view = TestDto.ParseToDataView(data).DefaultView;
                int rowCount = view.Count;
                TabPage page = new TabPage(nameTab + " (" + rowCount + ")");
                tabControl.Click += ComponentTab_MouseClick;
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
                dataGridView.VirtualMode = true; //отрисовываются только те ячейки, которые видны в данный момент
                dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick; ;
                TestDto.VisibleColumns(ColumnMask, dataGridView);
                if (dataGridView.Columns["MP"] != null)
                dataGridView.Columns["MP"].DefaultCellStyle.Format = "#0.0\\%";
                dataGridView.TopLeftHeaderCell.Value = "Тест"; // Заголовок столбца названия строк
                //DataGridView.Columns[0].HeaderText = "название столбца";
                //dataGridView.Rows[0].HeaderCell.Value = "Название строки";
                if (view.Count > 0)
                {
                    int i = 0;
                    foreach (var columnHeader in TestDto.vivaXmlColumns)
                    {
                        dataGridView.Columns[i].HeaderText = columnHeader.Translation;
                        i++;
                    }
                    //for (i = 1; i <= rowCount; i++)
                    //{
                    //    dataGridView.Rows[i - 1].HeaderCell.Value = i.ToString();
                    //}
                }
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

        private DataTable ConvertToDataTable<T>(IList<T> data)
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


        private void page_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Ну", "Зачем");
        }
        public static Dictionary<string, List<XElement>> deviationCalculate = new Dictionary<string, List<XElement>>();
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (TestDto.openFilesNames.Count == 0) throw new ArgumentException("Нет открытых файлов");
                if (TestDto.openFilesNames.Count == 1) throw new ArgumentException("Необходимо хотя бы ДВА открытых файла для выборки значений");
                deviationCalculate = MaxDeviationCalculate.DeviationCalculate();
                TabPage page = new TabPage(TestDto.Сalculations[1].Translation);
                tabControl2.TabPages.Add(page);
                page.Visible = true;
                page.Select();
                TabControl tabTests = new TabControl();
                page.Controls.Add(tabTests);
                tabTests.Dock = DockStyle.Fill;
                tabTests.ItemSize = new System.Drawing.Size(0, 24);
                tabTests.SelectedIndex = 0;
                tabTests.TabIndex = 1;
                tabTests.Name = TestDto.Сalculations[1].Translation;
                tabTests.Visible = true;
                tabTests.Select();
                
                List<XElement> gettedView = new List<XElement>();
                

                foreach (var test in TestDto.vivaXmlTests)
                {
                    if (deviationCalculate.TryGetValue(test.Translation, out gettedView))
                        AddNewComponentTab(test.Translation, tabTests, gettedView, test.Mask);
                    else
                        MessageBox.Show("Ошибка чтения словаря по ключу " + test.Name, "Ошибка чтения данных словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (deviationCalculate.TryGetValue(TestDto.Сalculations[0].Translation, out gettedView))
                    AddNewComponentTab(TestDto.Сalculations[0].Translation, tabTests, gettedView, TestDto.Сalculations[0].Mask);
                else
                    MessageBox.Show("Ошибка чтения словаря по ключу " + TestDto.Сalculations[0].Name, "Ошибка чтения данных словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception CalculateError)
            {
                MessageBox.Show("Ошибка при создании вкладки вычислений.\nПодробнее:\n" + CalculateError.Message, "Ошибка вычисления максимального отклонения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    
}
