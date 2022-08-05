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
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                try
                {
                    XDocument doc = null;
                    using (var Reader = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.UTF8))
                    {
                        doc = XDocument.Load(Reader);
                        Reader.Close();
                    }
                    XElement root = doc.Root;
                    List<XElement> tests = root.Element("BI").Elements("TEST").ToList();

                    var queryContinuity = tests.Select(t => new TestDto { C = t.Attribute("C").Value, SG1 = t.Attribute("SG1").Value, SG2 = t.Attribute("SG2").Value, PD1 = t.Attribute("PD1").Value, PD2 = t.Attribute("PD2").Value, MR = t.Attribute("MR").Value, MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")) / 100, TT = t.Attribute("TT").Value, MM = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US")) });
                    DataTable tableContinuity = this.ConvertToDataTable(queryContinuity.ToList());
                    DataView viewContinuity = tableContinuity.DefaultView;

                    var queryIsolation = tests.Select(t => new TestDto { F = t.Attribute("F").Value, FT = t.Attribute("FT").Value, C = t.Attribute("C").Value });
                    DataTable tableIsolation = this.ConvertToDataTable(queryIsolation.ToList());
                    DataView viewIsolation = tableIsolation.DefaultView;

                    var queryResistor = tests.Select(t => new TestDto { F = t.Attribute("F").Value, FT = t.Attribute("FT").Value, C = t.Attribute("C").Value });
                    DataTable tableResistor = this.ConvertToDataTable(queryResistor.ToList());
                    DataView viewResistor = tableResistor.DefaultView;

                    var queryCapacitor = tests.Select(t => new TestDto { F = t.Attribute("F").Value, FT = t.Attribute("FT").Value, C = t.Attribute("C").Value });
                    DataTable tableCapacitor = this.ConvertToDataTable(queryCapacitor.ToList());
                    DataView viewCapacitor = tableCapacitor.DefaultView;

                    var queryInductance = tests.Select(t => new TestDto { F = t.Attribute("F").Value, FT = t.Attribute("FT").Value, C = t.Attribute("C").Value });
                    DataTable tableInductance = this.ConvertToDataTable(queryInductance.ToList());
                    DataView viewInductance = tableInductance.DefaultView;

                    var queryAutic = tests.Select(t => new TestDto { F = t.Attribute("F").Value, FT = t.Attribute("FT").Value, C = t.Attribute("C").Value });
                    DataTable tableAutic = this.ConvertToDataTable(queryAutic.ToList());
                    DataView viewAutic = tableAutic.DefaultView;

                    var queryOther = tests.Select(t => new TestDto { F = t.Attribute("F").Value, FT = t.Attribute("FT").Value, C = t.Attribute("C").Value });
                    DataTable tableOther = this.ConvertToDataTable(queryOther.ToList());
                    DataView viewOther = tableOther.DefaultView;




                    TabPage page = new TabPage(openFileDialog1.SafeFileName);
                    tabControl2.TabPages.Add(page);

                    TabControl tabTests = new TabControl();
                    page.Controls.Add(tabTests);
                    tabTests.Dock = DockStyle.Fill;
                    tabTests.ItemSize = new System.Drawing.Size(0, 24);
                    tabTests.SelectedIndex = 0;
                    tabTests.TabIndex = 1;

                    TabPage pageTestContinuity = new TabPage("Тест на обрыв");
                    tabTests.TabPages.Add(pageTestContinuity);
                    DataGridView dataGridViewContinuity = new DataGridView();
                    pageTestContinuity.Controls.Add(dataGridViewContinuity);
                    dataGridViewContinuity.Dock = DockStyle.Fill;

                    //dataGridViewContinuity.Columns["MP"].DefaultCellStyle.Format;

                    TabPage pageTestIsolation = new TabPage("Тест изоляции");
                    tabTests.TabPages.Add(pageTestIsolation);
                    DataGridView dataGridViewIsolation = new DataGridView();
                    pageTestIsolation.Controls.Add(dataGridViewIsolation);
                    dataGridViewIsolation.Dock = DockStyle.Fill;

                    TabPage pageTestResistor = new TabPage("Резисторы");
                    tabTests.TabPages.Add(pageTestResistor);
                    DataGridView dataGridViewResistor = new DataGridView();
                    pageTestResistor.Controls.Add(dataGridViewResistor);
                    dataGridViewResistor.Dock = DockStyle.Fill;

                    TabPage pageTestCapacitor = new TabPage("Конденсаторы");
                    tabTests.TabPages.Add(pageTestCapacitor);
                    DataGridView dataGridViewCapacitor = new DataGridView();
                    pageTestCapacitor.Controls.Add(dataGridViewCapacitor);
                    dataGridViewCapacitor.Dock = DockStyle.Fill;

                    TabPage pageTestInductance = new TabPage("Индуктивности");
                    tabTests.TabPages.Add(pageTestInductance);
                    DataGridView dataGridViewInductance = new DataGridView();
                    pageTestInductance.Controls.Add(dataGridViewInductance);
                    dataGridViewInductance.Dock = DockStyle.Fill;

                    TabPage pageTestAutic = new TabPage("Чип");
                    tabTests.TabPages.Add(pageTestAutic);
                    DataGridView dataGridViewAutic = new DataGridView();
                    pageTestAutic.Controls.Add(dataGridViewAutic);
                    dataGridViewAutic.Dock = DockStyle.Fill;

                    TabPage pageTestOther = new TabPage("Остальное");
                    tabTests.TabPages.Add(pageTestOther);
                    DataGridView dataGridViewOther = new DataGridView();
                    pageTestOther.Controls.Add(dataGridViewOther);
                    dataGridViewOther.Dock = DockStyle.Fill;

                    dataGridViewContinuity.DataSource = viewContinuity;
                    dataGridViewIsolation.DataSource = viewIsolation;
                    dataGridViewResistor.DataSource = viewResistor;
                    dataGridViewCapacitor.DataSource = viewCapacitor;
                    dataGridViewInductance.DataSource = viewInductance;
                    dataGridViewAutic.DataSource = viewAutic;
                    dataGridViewOther.DataSource = viewOther;
                }
                catch (Exception ReadFileError)
                {
                    MessageBox.Show("Ошибка при открытии файла из указанного места. Подробнее: " + ReadFileError.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void AddNewTabFile(string nameTab, TabControl tabControl, DataView view)
        {
            TabPage page = new TabPage(nameTab);
            tabControl.TabPages.Add(page);
            DataGridView dataGridView = new DataGridView();
            page.Controls.Add(dataGridView);
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.DataSource = view;
        }

        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }


        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Cut"));
                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));

                /*int currentMouseOverRow = dataGridView.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(dataGridView, new Point(e.X, e.Y));*/

            }
        }
    }
}
