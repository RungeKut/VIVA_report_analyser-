using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VIVA_report_analyser.MainForm
{
    public class WorkThreads
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public static BackgroundWorker openFiles = new BackgroundWorker();
        private static BackgroundWorker parser = new BackgroundWorker();
        private static BackgroundWorker update = new BackgroundWorker();
        public static void Init()
        {
            openFiles.WorkerReportsProgress = true;
            openFiles.WorkerSupportsCancellation = true;
            openFiles.ProgressChanged += OpenFiles_ProgressChanged;
            openFiles.DoWork += OpenFiles_DoWork;
            openFiles.RunWorkerCompleted += OpenFiles_RunWorkerCompleted;
            //openFiles.RunWorkerAsync();

            parser.WorkerReportsProgress = true;
            parser.WorkerSupportsCancellation = true;
            parser.ProgressChanged += Parser_ProgressChanged;
            parser.DoWork += Parser_DoWork;
            parser.RunWorkerAsync();

            update.WorkerReportsProgress = true;
            update.WorkerSupportsCancellation = true;
            update.ProgressChanged += Update_ProgressChanged;
            update.DoWork += Update_DoWork;
            update.RunWorkerAsync();
        }
        private static void OpenFiles_DoWork(object sender, DoWorkEventArgs e)
        {
            int progress = 0;
            parser.ReportProgress(progress);
            string findFileMess = null;
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
            MainForm.mainForm.Invoke(new Action(() => { if (openFileDialog.ShowDialog() != DialogResult.OK) { return; } })); //errorList += "Что-то не так в диалоге выбора файлов.\n";
            
            DataModel.dataFiles.busy = true;
            int quantityFiles = openFileDialog.FileNames.Length;
            int stepProgress = 1000 / quantityFiles;
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
                progress += stepProgress;
                WorkThreads.openFiles.ReportProgress(progress);
            }
            parser.ReportProgress(1000);
            DataModel.dataFiles.busy = false;
            DataModel.dataFiles.needParser = true;
            if (findFileMess != null)
            {
                MessageBox.Show("Файлы:\n" + findFileMess + "уже открыты в программе!", "Эти файлы уже открыты!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                findFileMess = null;
            }
            
        }
        private static void OpenFiles_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MainForm.mainForm.button1.Enabled = true;
        }
        private static void OpenFiles_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MainForm.mainForm.progressBar1.Value = e.ProgressPercentage;
            MainForm.mainForm.label1.Text = "   Идет чтение файлов   " + String.Format("{0:0.0}", (Double)e.ProgressPercentage / 10) + "%";
        }
        private static void Parser_DoWork(object sender, DoWorkEventArgs e)
        {
            uint i = 0;
            while (true)
            {
                if (DataModel.dataFiles.needParser)
                {
                    int progress = 0;
                    parser.ReportProgress(progress);
                    if (!DataModel.dataFiles.busy)
                    {
                        int stepProgress = 1000 / DataModel.dataFiles.Count;
                        foreach (var file in DataModel.dataFiles)
                        {
                            if (!file.errorOpen)
                            {
                                DataModel.XmlData temp = ParseXml.Parse(file.doc);
                                DataModel.dataFiles.busy = true;
                                file.Info = temp.Info;
                                file.FidMrk = temp.FidMrk;
                                file.PrgC = temp.PrgC;
                                file.ST = temp.ST;
                                file.biSec = temp.biSec;
                                file.ET = temp.ET;
                                if (file.biSec == null)
                                {
                                    file.errorOpen = true;
                                    log.Warn("Файл/Секция помечены ошибкой открытия т.к. отсутсвует секция BI " + file.Name);
                                    break;
                                }
                                foreach (var numBI in file.biSec.BI)
                                {
                                    numBI.dataFilteredByTests = DataModel.FilterByTestType.FilteringTests(numBI.testsSec);
                                }
                            }
                            progress += stepProgress;
                            parser.ReportProgress(progress);
                        }
                        parser.ReportProgress(1000);
                        DataModel.dataFiles.busy = false;
                        DataModel.dataFiles.needParser = false;
                        DataModel.dataFiles.needUpdateView = true;
                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    i++;
                    //log.Info("Поток парсера спит " + i + " сек.");
                }
            }
        }
        private static void Parser_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MainForm.mainForm.progressBar1.Value = e.ProgressPercentage;
            MainForm.mainForm.label1.Text = "   Идет расшифровка xml данных   " + String.Format("{0:0.0}", (Double)e.ProgressPercentage / 10) + "%";
        }
        private static void Update_DoWork(object sender, DoWorkEventArgs e)
        {
            uint i = 0;
            while (true)
            {
                if (DataModel.dataFiles.needUpdateView)
                {
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
                                            UpdateView.AddNewComponentTab
                                            (
                                                ParseXml.vivaXmlTests[test].translation,
                                                tabTests,
                                                DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].Tests,
                                                DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].Tests.Count,
                                                DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].errorTests.Count
                                            );

                                        }
                                        UpdateView.AddNewComponentTab
                                            (
                                                ParseXml.Сalculations[0].translation,
                                                tabTests,
                                                DataModel.dataFiles[file].biSec.BI[numBI].testsSec.TEST,
                                                DataModel.dataFiles[file].biSec.BI[numBI].testsSec.TEST.Count,
                                                0
                                            );
                                        MainForm.mainForm.tabControl2.Invoke(new Action(() => { MainForm.mainForm.tabControl2.TabPages.Add(page); }));
                                        DataModel.dataFiles[file].biSec.BI[numBI].visible = true;
                                    }
                            }
                        }
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    i++;
                    //log.Info("Поток обновления формы спит " + i + " сек.");
                }
            }
        }
        private static void Update_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MainForm.mainForm.progressBar1.Value = e.ProgressPercentage;
            MainForm.mainForm.label1.Text = "   Идет обновление информации на экране   " + String.Format("{0:0.0}", (Double)e.ProgressPercentage / 10) + "%";
        }
    }
}
