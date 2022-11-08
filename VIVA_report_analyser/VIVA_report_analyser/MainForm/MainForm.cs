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

namespace VIVA_report_analyser.MainForm
{
    public partial class MainForm : Form
    {
        public static MainForm mainForm = null;
        private delegate void EnableDelegate(bool enable);
        private static Logger log = LogManager.GetCurrentClassLogger();
        public MainForm()
        {
            log.Info("InitializeComponent main Form");
            WorkThreads.Init();
            mainForm = this;

            mainForm.FormClosing += Form_FormClosing;
            Application.ApplicationExit += Application_ApplicationExit;
            //StartUpdateThread();
            
            InitializeComponent();
            progressBar1.Maximum = 1000;
            progressBar1.Visible = true;
            StyleColor.Init();
            tabControl2.MouseUp += RightMouseClickFileTab.FileTab_MouseClick;
            tabControl2.DrawItem += TabControl2_DrawItem;
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
        }
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
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
            MessageBox.Show("т.к. это хорошо заметно в последнем столбце расчетов МАХ отклонения", "Нереализованная функциональность", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
    }

    public class DoubleBufferedDataGridView : DataGridView
    // Двойная буфферизация для таблиц, ускоряет работу
    {
        protected override bool DoubleBuffered { get => true; }
    }

    
}
