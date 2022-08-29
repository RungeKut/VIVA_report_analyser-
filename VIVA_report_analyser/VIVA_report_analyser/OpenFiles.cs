using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VIVA_report_analyser
{
    public class DataFilesClass
    {
        // Поля класса
        public string fileName { get; set; }
        public string filePath { get; set; }
        public bool errorOpenFile { get; set; }
        public XDocument dataDoc { get; set; }
        public ParsedXml dataParse { get; set; }
        public List<FilterByTests> dataFilteredByTests { get; set; }
        public bool visibleFile { get; set; }
    }
    internal class OpenFiles
    {
        public static List<DataFilesClass> dataFile { get; set; } = new List<DataFilesClass>();
        public static List<string> openFilesNames
        {
            get
            {
                return (from DataFilesClass file in dataFile
                        where file.errorOpenFile == false
                        select file.fileName).ToList();
            }
        }
        public static List<string> errorOpenFilesNames
        {
            get
            {
                return (from DataFilesClass file in dataFile
                        where file.errorOpenFile == true
                        select file.fileName).ToList();
            }
        }
        public static int openCount { get { return OpenFiles.openFilesNames.Count; } }
        public static int errOpenCount { get { return OpenFiles.errorOpenFilesNames.Count; } }
        public static List<DataFilesClass> LoadXmlDocument()
        {
            string errorList = null;
            List<DataFilesClass> returnData = new List<DataFilesClass>();
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
            if (openFileDialog.ShowDialog() != DialogResult.OK) errorList += "Что-то не так в диалоге выбора файлов.\n";
            else
            {
                int quantityFiles = openFileDialog.FileNames.Length;
                for (int file = 0; file < quantityFiles; file++)
                {
                    string Path = openFileDialog.FileNames[file];
                    string Name = openFileDialog.SafeFileNames[file];

                    XDocument doc = new XDocument(); // создаем пустой XML документ
                    using (var Reader = new StreamReader(Path, System.Text.Encoding.UTF8))
                    {
                        doc = XDocument.Load(Reader);
                        Reader.Close();
                    }
                    var tuple = ParseXml.Parse(doc);
                    if (tuple.Item1 != null)
                    {
                        returnData.Add(new DataFilesClass()
                        {
                            fileName = Name,
                            filePath = Path,
                            errorOpenFile = false,
                            dataDoc = doc,
                            dataParse = tuple.Item1,
                            dataFilteredByTests = FilterByTests.FilteringTests(tuple.Item1)
                        });
                        if (tuple.Item2 != null)
                        {
                            errorList += "В файле " + Name + " отсутствуют параметры:\n" + tuple.Item2;
                        }
                    }
                    else
                    {
                        returnData.Add(new DataFilesClass()
                        {
                            fileName = Name,
                            filePath = Path,
                            errorOpenFile = true,
                            dataDoc = doc
                        });
                        errorList += "Неверный формат файла " + Name + "\n";
                    }
                }
            }
            if (errorList != null)
            {
                if (MessageBox.Show(errorList + "\nПродолжить выполнение?", "Чтение файлов", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return null;
            }
            return returnData;
        }
    }
}
