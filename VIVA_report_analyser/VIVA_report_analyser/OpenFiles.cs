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
            if (openFileDialog.ShowDialog() != DialogResult.OK) return null;
            int quantityFiles = openFileDialog.FileNames.Length;

            List<DataFilesClass> returnData = new List<DataFilesClass>();

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
                if ((doc.Root.Element("BI") == null) || (doc.Root.Element("BI").Element("TEST") == null))
                {
                    returnData.Add(new DataFilesClass()
                    {
                        fileName = Name,
                        filePath = Path,
                        errorOpenFile = true,
                        dataDoc = doc
                    });
                }
                else
                {
                    ParsedXml d = ParseXml.Parse(doc);
                    returnData.Add(new DataFilesClass()
                    {
                        fileName = Name,
                        filePath = Path,
                        errorOpenFile = false,
                        dataDoc = doc,
                        dataParse = d,
                        dataFilteredByTests = FilterByTests.FilteringTests(d)
                    });
                }
            }
            List<string> errOFN = (from DataFilesClass file in returnData
                                  where file.errorOpenFile == true
                                  select file.fileName).ToList();
            if (errOFN.Count > 0)
            {
                string files = String.Join("\n", errOFN);
                MessageBox.Show("Неверный формат файлов:\n\n" + files, "Ошибка чтения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //errorOpenFilesNames.Clear();
            }
            return returnData;
        }
    }
}
