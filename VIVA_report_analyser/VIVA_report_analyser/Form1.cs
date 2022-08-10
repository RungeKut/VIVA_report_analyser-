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

        public Dictionary<string, Dictionary<string, DataView>> filteredTestOnFile;

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "xml",
                DereferenceLinks = true,
                Filter = "VIVA full report xml file (*.xml)|*.xml|All files (*.*)|*.*",
                FilterIndex = 1,
                ValidateNames = true,
                Multiselect = true,
                Title = "Выберите файлы .xml"
                //InitialDirectory = @"C:\"
            };
            //openFileDialog.ShowDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                //if (openFileDialog.FileName == String.Empty)
                //return;

                for (int file = 0; file < openFileDialog.FileNames.Length; file++)
                {
                    try
                    {
                        XDocument doc = null; // создаем пустой XML документ
                        using (var Reader = new StreamReader(openFileDialog.FileNames[file], System.Text.Encoding.UTF8))
                        {
                            doc = XDocument.Load(Reader);
                            Reader.Close();
                        }
                        XElement root = doc.Root;
                        if ((root.Element("BI") == null) || (root.Element("BI").Element("TEST") == null))
                        {
                            TestDto.errorOpenFilesNames.Add(openFileDialog.SafeFileNames[file]);
                            TestDto.errorOpenFileFlag = true;
                        }
                        else
                        {
                            Dictionary<string, DataView> filteredTest = TestDto.SelectComponentTests(TestDto.vivaXmlTests, root);
                            filteredTestOnFile.Add(openFileDialog.SafeFileNames[file], filteredTest);

                            TabPage page = new TabPage(openFileDialog.SafeFileNames[file]);
                            tabControl2.TabPages.Add(page);

                            tabControl2.MouseClick += FileTab_MouseClick;
                            page.MouseClick += Page_MouseClick;

                            ContextMenuStrip FileTabMenu = new ContextMenuStrip();
                            ToolStripMenuItem CloseTab_MenuItem = new ToolStripMenuItem("Закрыть вкладку");
                            ToolStripMenuItem CloseTabs_MenuItem = new ToolStripMenuItem("Закрыть все вкладки");
                            ToolStripMenuItem RecoverTab_MenuItem = new ToolStripMenuItem("Открыть закрытую вкладку");

                            FileTabMenu.Items.AddRange(new[]
                            {
                            CloseTab_MenuItem,
                            CloseTabs_MenuItem,
                            RecoverTab_MenuItem
                        });
                            page.ContextMenuStrip = FileTabMenu;

                            TabControl tabTests = new TabControl();
                            page.Controls.Add(tabTests);
                            tabTests.Dock = DockStyle.Fill;
                            tabTests.ItemSize = new System.Drawing.Size(0, 24);
                            tabTests.SelectedIndex = 0;
                            tabTests.TabIndex = 1;
                            tabTests.Name = openFileDialog.SafeFileNames[file];

                            DataView gettedView;
                            foreach (var test in TestDto.vivaXmlTests)
                            {
                                if (filteredTest.TryGetValue(test.Name, out gettedView))
                                    AddNewComponentTab(test.Translation, tabTests, gettedView, test.Mask);
                                else
                                    MessageBox.Show("Ошибка чтения словаря по ключу " + test.Name, "Ошибка чтения данных словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (filteredTest.TryGetValue(TestDto.Сalculations[0].Name, out gettedView))
                                AddNewComponentTab(TestDto.Сalculations[0].Translation, tabTests, gettedView, TestDto.Сalculations[0].Mask);
                            else
                                MessageBox.Show("Ошибка чтения словаря по ключу " + TestDto.Сalculations[0].Name, "Ошибка чтения данных словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            TestDto.openFilesNames.Add(tabTests.Name);
                        }
                    }
                    catch (Exception ReadFileError)
                    {
                        MessageBox.Show("Ошибка при открытии файла из указанного места. Подробнее: " + ReadFileError.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            if (TestDto.errorOpenFileFlag)
            {
                string files = String.Join("\n", TestDto.errorOpenFilesNames);
                MessageBox.Show("Неверный формат файлов:\n\n" + files, "Ошибка чтения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TestDto.errorOpenFilesNames.Clear();
                TestDto.errorOpenFileFlag = false;
            }
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

        private void AddNewComponentTab(string nameTab, TabControl tabControl, DataView view, ulong ColumnMask)
        // Создание фкладки с именем компонента во вкладке с файлом
        {
            try
            {
                TabPage page = new TabPage(nameTab);
                tabControl.Click += ComponentTab_MouseClick;
                tabControl.TabPages.Add(page);
                //page.Tag = "";
                //page.MouseClick += page_MouseClick;
                //page.MouseClick += new MouseEventHandler(page_MouseClick);
                DoubleBufferedDataGridView dataGridView = new DoubleBufferedDataGridView();
                page.Controls.Add(dataGridView);
                dataGridView.Dock = DockStyle.Fill;
                dataGridView.DataSource = view;
                dataGridView.VirtualMode = true; //отрисовываются только те ячейки, которые видны в данный момент
                dataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick; ;
                TestDto.VisibleColumns(ColumnMask, dataGridView);
                dataGridView.Columns["MP"].DefaultCellStyle.Format = "#0.0\\%";

            }
            catch (Exception ReadFileError)
            {
                MessageBox.Show("Ошибка при создании вкладки " + nameTab + ". Подробнее: " + ReadFileError.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataView view = MaxDeviationCalculate.DeviationCalculate(filteredTestOnFile);
                AddNewComponentTab(TestDto.Сalculations[1].Translation, tabControl2, view, TestDto.Сalculations[1].Mask);
            }
            catch (Exception CalculateError)
            {
                MessageBox.Show("Ошибка при создании вкладки вычислений. Подробнее: " + CalculateError.Message, "Ошибка вычисления максимального отклонения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    
}
