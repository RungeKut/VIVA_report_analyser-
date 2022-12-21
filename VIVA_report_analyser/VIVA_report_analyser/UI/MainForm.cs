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
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VIVA_report_analyser.UI
{
    public partial class MainForm : Form
    {
        public static MainForm mainForm = null;
        private delegate void EnableDelegate(bool enable);
        private static Logger log = LogManager.GetCurrentClassLogger();
        public System.Windows.Forms.ToolTip toolTipButton = null;
        
        public MainForm()
        {
            log.Info("InitializeComponent main Form");
            WorkThreads.Init();
            mainForm = this;

            this.FormClosing += Form_FormClosing;
            this.DragEnter += MainForm_DragEnter;
            this.DragDrop += MainForm_DragDrop;
            Application.ApplicationExit += Application_ApplicationExit;
            //StartUpdateThread();
            
            InitializeComponent();
            this.progressBar1.Maximum = 1000;
            this.progressBar1.Visible = true;
            MenuCheckedListBox.Init();
            this.tabControl2.Alignment = TabAlignment.Left;
            //tabControl2.Multiline = false; //не работает совместно с TabAlignment
            this.tabControl2.MouseUp += RightMouseClickFileTab.FileTab_MouseClick;
            this.tabControl2.DrawItem += TabControl2_DrawItem;
            this.openFileButton.MouseHover += OpenFileButton_MouseHover;
            this.openFileButton.MouseLeave += OpenFileButton_MouseLeave;
            this.updateButton.MouseHover += UpdateButton_MouseHover;
            this.updateButton.MouseLeave += UpdateButton_MouseLeave;
            this.generateDocxReportButton.MouseHover += GenerateDocxReportButton_MouseHover;
            this.generateDocxReportButton.MouseLeave += GenerateDocxReportButton_MouseLeave;
        }

        private void OpenFileButton_MouseLeave(object sender, EventArgs e)
        {
            if (toolTipButton != null) { toolTipButton.RemoveAll(); }
        }

        private void UpdateButton_MouseLeave(object sender, EventArgs e)
        {
            if (toolTipButton != null) { toolTipButton.RemoveAll(); }
        }

        private void GenerateDocxReportButton_MouseLeave(object sender, EventArgs e)
        {
            if (toolTipButton != null) { toolTipButton.RemoveAll(); }
        }

        private void GenerateDocxReportButton_MouseHover(object sender, EventArgs e)
        {
            toolTipButton = new System.Windows.Forms.ToolTip();
            toolTipButton.Show("Сгенерировать отчет", updateButton);
        }

        private void UpdateButton_MouseHover(object sender, EventArgs e)
        {
            toolTipButton = new System.Windows.Forms.ToolTip();
            toolTipButton.Show("Обновить", updateButton);
        }

        private void OpenFileButton_MouseHover(object sender, EventArgs e)
        {
            toolTipButton = new System.Windows.Forms.ToolTip();
            toolTipButton.Show("Открыть файлы", openFileButton);
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string findFileMess = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false)) //Если дропаются файлы то
            {
                bool allowFilesDrop = false;
                //Извлекаем пути перетаскиваемых файлов
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, true);
                int fileQuantity = files.Length;
                string[] filePaths = new string[fileQuantity];
                
                for (int i = 0; i < fileQuantity; i++)
                {
                    //Проверяем того ли они расширения
                    string fileExtension = (new System.IO.FileInfo(files[i])).Extension;
                    allowFilesDrop = (fileExtension == ".xml") |
                                     (fileExtension == ".Xml") |
                                     (fileExtension == ".XML");
                    if (allowFilesDrop)
                    {
                        filePaths[i] = files[i];
                    }
                    else
                    {
                        filePaths[i] = null;
                        findFileMess += System.IO.Path.GetFileName(files[i]) + "\n";
                    }
                }
                if (filePaths.Length > 0)
                {
                    e.Effect = DragDropEffects.All;
                    LoadDropFile(filePaths);
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                    MessageBox.Show("Поддерживаются только xml файлы!", "Формат не поддерживается!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (findFileMess != null)
            {
                MessageBox.Show("Файлы:\n" + findFileMess + "имеют неподдерживаемый формат!", "Эти файлы не поддерживаются!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                findFileMess = null;
            }
        }
        private void LoadDropFile(string[] filePaths)
        {
            if (filePaths == null) return;
            int quantityFiles = filePaths.Length;
            string findFileMess = null;
            DataModel.dataFiles.busy = true;
            for (int file = 0; file < quantityFiles; file++)
            {
                if (filePaths[file] == null) break;
                string Path = filePaths[file];
                string Name = (System.IO.Path.GetFileName(filePaths[file]));
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
            }
            DataModel.dataFiles.busy = false;
            DataModel.dataFiles.needParser = true;
            if (findFileMess != null)
            {
                MessageBox.Show("Файлы:\n" + findFileMess + "уже открыты в программе!", "Эти файлы уже открыты!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                findFileMess = null;
            }
            WorkThreads.parser.RunWorkerAsync();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false)) //Если дропаются файлы то
            {
                //Тут можно прописать условия при которых будет меняться курсор мыши и выполняться что-то в зависимости от дропаемых вещей до отпускания кнопки мыши
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {
            //Делаем вкладку горизонтальной
            Graphics g;
            string sText;
            int iX;
            float iY;

            SizeF sizeText;
            TabControl ctlTab;

            ctlTab = (TabControl)sender;

            g = e.Graphics;

            sText = ctlTab.TabPages[e.Index].Text;
            sizeText = g.MeasureString(sText, ctlTab.Font);
            iX = e.Bounds.Left + 6;
            iY = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2;
            g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY);

            //Красим выделенную вкладку цветом
            e.Graphics.SetClip(e.Bounds);
            string text = tabControl2.TabPages[e.Index].Text;
            SizeF sz = e.Graphics.MeasureString(text, e.Font);

            bool bSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using (SolidBrush b = new SolidBrush(bSelected ? SystemColors.Highlight : SystemColors.Control))
                e.Graphics.FillRectangle(b, e.Bounds);

            using (SolidBrush b = new SolidBrush(bSelected ? SystemColors.HighlightText : SystemColors.ControlText))
                e.Graphics.DrawString(text, e.Font, b, e.Bounds.X + 2, e.Bounds.Y + (e.Bounds.Height - sz.Height) / 2);

            if (tabControl2.SelectedIndex == e.Index)
                e.DrawFocusRectangle();

            e.Graphics.ResetClip();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Обработка дропа файлов на ярлык приложения, "открыть с помощью" и поддержка ассоциации фалов с приложением
            string findFileMess = null;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length >= 2)
            {
                bool allowFilesDrop = false;
                //Удалим первый аргумент командной строки (путь к нашей программе)
                string[] filePaths = new string[args.Length - 1];
                for (int i = 0; i < args.Length - 1; i++)
                {
                    //За одно проверим расширения файлов
                    string fileExtension = (new System.IO.FileInfo(args[i + 1])).Extension;
                    allowFilesDrop = (fileExtension == ".xml") |
                                     (fileExtension == ".Xml") |
                                     (fileExtension == ".XML");
                    if (allowFilesDrop)
                    {
                        filePaths[i] = args[i + 1];
                    }
                    else
                    {
                        filePaths[i] = null;
                        findFileMess += System.IO.Path.GetFileName(args[i + 1]) + "\n";
                    }
                }
                LoadDropFile(filePaths);
            }
            if (findFileMess != null)
            {
                MessageBox.Show("Файлы:\n" + findFileMess + "имеют неподдерживаемый формат!", "Эти файлы не поддерживаются!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                findFileMess = null;
            }

            //Восстановление размеров формы перед открытием
            if (Properties.Settings.Default.WindowSize.Width <= 800 || Properties.Settings.Default.WindowSize.Height <= 600)
            {
                // Старт если параметры отсутствуют или по-умолчанию
                this.WindowState = FormWindowState.Normal;
                this.Width = 1440;
                this.Height = 1010;
            }
            else
            {
                this.WindowState = Properties.Settings.Default.WindowState;

                // Если окно было свернуто то разворачиваем (нам не нужно свернутое окно при запуске)
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
                // Восстанавливаем положение и размер
                this.Location = Properties.Settings.Default.WindowLocation;
                this.Size = Properties.Settings.Default.WindowSize;
            }
            //menuTabControl.MakeTransparent();
        }
        
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Возвращаем состояние формы
            Properties.Settings.Default.WindowState = this.WindowState;
            if (this.WindowState == FormWindowState.Normal)
            {
                //Если форма в нормальном состоянии
                Properties.Settings.Default.WindowLocation = this.Location;
                Properties.Settings.Default.WindowSize = this.Size;
            }
            else //Если форма свернута или развернута
            {
                Properties.Settings.Default.WindowLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.WindowSize = this.RestoreBounds.Size;
            }
            //Сохранение настроек
            Properties.Settings.Default.Save();
        }
        private void openFileButton_Click_1(object sender, EventArgs e)
        {
            openFileButton.Enabled = false;
            progressBar1.Visible = true;
            WorkThreads.openFiles.RunWorkerAsync();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            progressBar1.Visible = true;
            MaxDeviationCalculate.uniqueTest.RunWorkerAsync();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            OutputTextBox outputTextBox = new OutputTextBox();
            outputTextBox.Show();
        }

        private void UpdateProgressAction(ProgressInfo obj)
        {
            progressBar1.Value = (int)obj.CompletedPercentage;
            label1.Text = obj.ProgressStatusText + " " + String.Format("{0:0.0}", obj.CompletedPercentage) + "%";
        }

        internal class WorkClass
        {
            public async Task LongMethod(IReadOnlyList<string> something, IProgress<ProgressInfo> progress)
            {
                await Task.Factory.StartNew(() =>
                {
                    var count = something.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var element = something[i];
                        Thread.Sleep(5);
                        progress.Report(new ProgressInfo((double)(i + 1) / count, element));
                    }
                });
            }
        }

        private void generateDocxReportButton_Click_1(object sender, EventArgs e)
        {
            ReportGenerator.OpenXMLReportMaker oxrm = new ReportGenerator.OpenXMLReportMaker();
            oxrm.MakeReportFromTemplate("TestReport");
        }
    }
}
