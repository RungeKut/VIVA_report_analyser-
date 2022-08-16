using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace VIVA_report_analyser
{
    internal class VivaXmlColumnsClass
    {
        // Поля класса
        public string Name { get; set; }
        public string Translation { get; set; }
        public ulong Mask { get; set; }
    }
    internal class VivaXmlTestsClass : VivaXmlColumnsClass { }
    internal class СalculationsClass : VivaXmlColumnsClass { }
    internal class TestDto
    {
        // Поля класса
        
        // Константы класса
        
        // Методы класса
        public static void VisibleColumns(ulong ColumnsMask, DataGridView dataGridView)
        {
            // Настройка видимости столбцов
            try
            {
                if (dataGridView.Columns[0].HeaderText == ParseXml.vivaXmlColumns[0].Name)
                foreach (var column in ParseXml.vivaXmlColumns)
                {
                    if ((ColumnsMask & column.Mask) > 0) dataGridView.Columns[column.Name].Visible = true;
                                                    else dataGridView.Columns[column.Name].Visible = false;
                };
            }
            catch (Exception e)
            {
                throw new ArgumentException("Ошибка метода VisibleColumns.\nПодробнее:\n" + e.Message);
            }
        }
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            try
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
            catch (Exception e)
            {
                throw new ArgumentException("Ошибка метода ConvertToDataTable.\nПодробнее:\n" + e.Message);
            }
        }
    }
    class DoubleBufferedDataGridView : DataGridView
    // Двойная буфферизация для таблиц, ускоряет работу
    {
        protected override bool DoubleBuffered { get => true; }
    }
}
