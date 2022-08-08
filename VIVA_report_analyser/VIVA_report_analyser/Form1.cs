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
            openFileDialog.ShowDialog();
            //if (openFileDialog.FileName == String.Empty)
            //    return;
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            foreach (string file in openFileDialog.FileNames)
            {
                try
                {
                    XDocument doc = null; // создаем пустой XML документ
                    using (var Reader = new StreamReader(file, System.Text.Encoding.UTF8))
                    {
                        doc = XDocument.Load(Reader);
                        Reader.Close();
                    }
                    XElement root = doc.Root;
                    List<XElement> tests = root.Element("BI").Elements("TEST").ToList();

                    List<XElement> testsContinuity = SelectComponentTests("CONTINUITY", root);
                    List<XElement> testsIsolation = SelectComponentTests("ISOLATION", root);
                    List<XElement> testsResistor = SelectComponentTests("RESISTOR", root);
                    List<XElement> testsCapacitor = SelectComponentTests("CAPACITOR", root);
                    List<XElement> testsInductance = SelectComponentTests("INDUCTANCE", root);
                    List<XElement> testsAutic = SelectComponentTests("AUTIC", root);
                    //List<XElement> testsOther      = SelectComponentTests("", root);

                    var queryContinuity = testsContinuity.Select(t =>
                    new TestDto
                    {
                        C = t.Attribute("C").Value,
                        SG1 = t.Attribute("SG1").Value,
                        SG2 = t.Attribute("SG2").Value,
                        PD1 = t.Attribute("PD1").Value,
                        PD2 = t.Attribute("PD2").Value,
                        MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                        MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")) / 100,
                        TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US"))
                    });
                    DataTable tableContinuity = this.ConvertToDataTable(queryContinuity.ToList());
                    DataView viewContinuity = tableContinuity.DefaultView;

                    var queryIsolation = testsIsolation.Select(t =>
                    new TestDto
                    {
                        C = t.Attribute("C").Value,
                        SG1 = t.Attribute("SG1").Value,
                        SG2 = t.Attribute("SG2").Value,
                        PD1 = t.Attribute("PD1").Value,
                        PD2 = t.Attribute("PD2").Value,
                        MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                        MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")) / 100,
                        TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US"))
                    });
                    DataTable tableIsolation = this.ConvertToDataTable(queryIsolation.ToList());
                    DataView viewIsolation = tableIsolation.DefaultView;

                    var queryResistor = testsResistor.Select(t =>
                    new TestDto
                    {
                        C = t.Attribute("C").Value,
                        SG1 = t.Attribute("SG1").Value,
                        SG2 = t.Attribute("SG2").Value,
                        PD1 = t.Attribute("PD1").Value,
                        PD2 = t.Attribute("PD2").Value,
                        MU = t.Attribute("MU").Value,
                        ML = Double.Parse(t.Attribute("ML").Value, new CultureInfo("en-US")),
                        MM = Double.Parse(t.Attribute("MM").Value, new CultureInfo("en-US")),
                        MH = Double.Parse(t.Attribute("MH").Value, new CultureInfo("en-US")),
                        MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                        MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")) / 100,
                        TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US"))
                    });
                    DataTable tableResistor = this.ConvertToDataTable(queryResistor.ToList());
                    DataView viewResistor = tableResistor.DefaultView;

                    var queryCapacitor = testsCapacitor.Select(t =>
                    new TestDto
                    {
                        C = t.Attribute("C").Value,
                        SG1 = t.Attribute("SG1").Value,
                        SG2 = t.Attribute("SG2").Value,
                        PD1 = t.Attribute("PD1").Value,
                        PD2 = t.Attribute("PD2").Value,
                        MU = t.Attribute("MU").Value,
                        ML = Double.Parse(t.Attribute("ML").Value, new CultureInfo("en-US")),
                        MM = Double.Parse(t.Attribute("MM").Value, new CultureInfo("en-US")),
                        MH = Double.Parse(t.Attribute("MH").Value, new CultureInfo("en-US")),
                        MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                        MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")) / 100,
                        TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US"))
                    });
                    DataTable tableCapacitor = this.ConvertToDataTable(queryCapacitor.ToList());
                    DataView viewCapacitor = tableCapacitor.DefaultView;

                    var queryInductance = testsInductance.Select(t =>
                    new TestDto
                    {
                        C = t.Attribute("C").Value,
                        SG1 = t.Attribute("SG1").Value,
                        SG2 = t.Attribute("SG2").Value,
                        PD1 = t.Attribute("PD1").Value,
                        PD2 = t.Attribute("PD2").Value,
                        MU = t.Attribute("MU").Value,
                        ML = Double.Parse(t.Attribute("ML").Value, new CultureInfo("en-US")),
                        MM = Double.Parse(t.Attribute("MM").Value, new CultureInfo("en-US")),
                        MH = Double.Parse(t.Attribute("MH").Value, new CultureInfo("en-US")),
                        MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                        MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")) / 100,
                        TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US"))
                    });
                    DataTable tableInductance = this.ConvertToDataTable(queryInductance.ToList());
                    DataView viewInductance = tableInductance.DefaultView;

                    var queryAutic = testsAutic.Select(t =>
                    new TestDto
                    {
                        C = t.Attribute("C").Value,
                        SG1 = t.Attribute("SG1").Value,
                        SG2 = t.Attribute("SG2").Value,
                        PD1 = t.Attribute("PD1").Value,
                        PD2 = t.Attribute("PD2").Value,
                        MU = t.Attribute("MU").Value,
                        ML = Double.Parse(t.Attribute("ML").Value, new CultureInfo("en-US")),
                        MM = Double.Parse(t.Attribute("MM").Value, new CultureInfo("en-US")),
                        MH = Double.Parse(t.Attribute("MH").Value, new CultureInfo("en-US")),
                        MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                        MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")) / 100,
                        TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US"))
                    });
                    DataTable tableAutic = this.ConvertToDataTable(queryAutic.ToList());
                    DataView viewAutic = tableAutic.DefaultView;

                    var queryOther = tests.Select(t =>
                    new TestDto
                    {
                        F = t.Attribute("F").Value,
                        C = t.Attribute("C").Value,
                        SG1 = t.Attribute("SG1").Value,
                        SG2 = t.Attribute("SG2").Value,
                        PD1 = t.Attribute("PD1").Value,
                        PD2 = t.Attribute("PD2").Value,
                        MU = t.Attribute("MU").Value,
                        ML = Double.Parse(t.Attribute("ML").Value, new CultureInfo("en-US")),
                        MM = Double.Parse(t.Attribute("MM").Value, new CultureInfo("en-US")),
                        MH = Double.Parse(t.Attribute("MH").Value, new CultureInfo("en-US")),
                        MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                        MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")) / 100,
                        TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US"))
                    });
                    DataTable tableOther = this.ConvertToDataTable(queryOther.ToList());
                    DataView viewOther = tableOther.DefaultView;




                    TabPage page = new TabPage(openFileDialog.SafeFileName);
                    tabControl2.TabPages.Add(page);

                    TabControl tabTests = new TabControl();
                    page.Controls.Add(tabTests);
                    tabTests.Dock = DockStyle.Fill;
                    tabTests.ItemSize = new System.Drawing.Size(0, 24);
                    tabTests.SelectedIndex = 0;
                    tabTests.TabIndex = 1;

                    AddNewComponentTab("Тест на обрыв", tabTests, viewContinuity, TestDto.ColumnMaskContinuity);
                    AddNewComponentTab("Тест изоляции", tabTests, viewIsolation,  TestDto.ColumnMaskIsolation);
                    AddNewComponentTab("Резисторы",     tabTests, viewResistor,   TestDto.ColumnMaskResistor);
                    AddNewComponentTab("Конденсаторы",  tabTests, viewCapacitor,  TestDto.ColumnMaskCapacitor);
                    AddNewComponentTab("Индуктивности", tabTests, viewInductance, TestDto.ColumnMaskInductance);
                    AddNewComponentTab("Чип",           tabTests, viewAutic,      TestDto.ColumnMaskAutic);
                    AddNewComponentTab("Остальное",     tabTests, viewOther,      TestDto.ColumnMaskOther);
                }
                catch (Exception ReadFileError)
                {
                    MessageBox.Show("Ошибка при открытии файла из указанного места. Подробнее: " + ReadFileError.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
                
                m.Show(tabControl2,e.Location);
            }
        }

        private void ComponentTab_MouseClick(object sender, EventArgs e)
        {
            
        }

        private List<XElement> SelectComponentTests(string nameTest, XElement data)
        // Выборка результатов конкретного теста
        {
            try
            {
                var test = from n in data.Descendants("BI").Elements("TEST")
                           where n.Attribute("F").Value == nameTest
                           select n;
                return test.ToList();
            }
            catch (Exception ReadFileError)
            {
                MessageBox.Show("Ошибка выборки результатов " + nameTest + " теста. Подробнее: " + ReadFileError.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
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

            //if (e.Button == MouseButtons.Right)
            //{
            MessageBox.Show("Ну", "Зачем");
            
            /*ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Cut"));
                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));

            /*int currentMouseOverRow = dataGridView.HitTest(e.X, e.Y).RowIndex;

            if (currentMouseOverRow >= 0)
            {
                m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
            }

            m.Show(dataGridView, new Point(e.X, e.Y));


            //добавление вкладки
TabPage newTabPage = new TabPage();
newTabPage.Text = "Континенты";
tabControl1.TabPages.Add(newTabPage);
             // удаление вкладки
// по индексу
tabControl1.TabPages.RemoveAt(0);
// по объекту
tabControl1.TabPages.Remove(newTabPage);
            // изменение свойств
tabControl1.TabPages[0].Text = "Первая вкладка";



             */

            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ну", "Зачем");
        }
    }

    
}
