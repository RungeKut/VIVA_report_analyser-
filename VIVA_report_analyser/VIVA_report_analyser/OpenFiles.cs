using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VIVA_report_analyser
{
    internal class OpenFiles
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public static string findFileMess = null;
        public static void LoadXmlDocument()
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
            if (openFileDialog.ShowDialog() != DialogResult.OK) { } //errorList += "Что-то не так в диалоге выбора файлов.\n";
            else
            {
                DataModel.dataFiles.busy = true;
                int quantityFiles = openFileDialog.FileNames.Length;
                MainForm.ProgressView.progressMax(quantityFiles - 1);
                for (int file = 0; file < quantityFiles; file++)
                {
                    string Path = openFileDialog.FileNames[file];
                    string Name = openFileDialog.SafeFileNames[file];
                    if (DataModel.CheckExistenceFile(Name))
                    {
                        findFileMess += Name + "\n";
                    }
                    else
                    {
                        XDocument loadDoc = new XDocument(); // создаем пустой XML документ
                        using (var Reader = new StreamReader(Path, System.Text.Encoding.UTF8))
                        {
                            loadDoc = XDocument.Load(Reader);
                            Reader.Close();
                        }
                        DataModel.dataFiles.Add(new DataModel.DataFile()
                        {
                            Name = Name,
                            Path = Path,
                            errorOpen = false,
                            doc = loadDoc
                        });
                    }
                    MainForm.ProgressView.progress(file, Path);
                }
                DataModel.dataFiles.busy = false;
                DataModel.dataFiles.needParser = true;
                if (findFileMess != null)
                {
                    MessageBox.Show("Файлы:\n" + findFileMess + "уже открыты в программе!", "Эти файлы уже открыты!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    findFileMess = null;
                }
            }
        }
    }
}
