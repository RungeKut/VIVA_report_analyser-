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
            WorkThreads.openFiles.RunWorkerAsync();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            StartDeviationCalculateThread();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            MessageBox.Show("т.к. это хорошо заметно в последнем столбце расчетов МАХ отклонения", "Нереализованная функциональность", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public void StartUpdateThread()
        {
            Thread updateThread = new Thread(UpdateView.updateForm);
            updateThread.Name = "UpdateThread";
            updateThread.IsBackground = true;
            updateThread.Start();
        }
        public void StartDeviationCalculateThread()
        {
            Thread deviationCalculateThread = new Thread(UpdateView.CreateTabDeviationCalc);
            deviationCalculateThread.Name = "DeviationCalculateThread";
            deviationCalculateThread.IsBackground = true;
            deviationCalculateThread.Start();
        }
    }

    public class DoubleBufferedDataGridView : DataGridView
    // Двойная буфферизация для таблиц, ускоряет работу
    {
        protected override bool DoubleBuffered { get => true; }
    }

    
}
