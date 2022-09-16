using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVA_report_analyser.MainForm
{
    internal class ProgressView
    {
        public static void progressMax(int p)
        {
            if (MainForm.mainForm != null)
            {
                MainForm.mainForm.Invoke(new Action(() =>
                {
                    if (MainForm.mainForm != null)
                        MainForm.mainForm.progressBar1.Maximum = p;
                }));
            }
        }
        public static void progress(int p, string t)
        {
            if (MainForm.mainForm != null)
            {
                MainForm.mainForm.Invoke(new Action(() =>
                {
                    if (p <= MainForm.mainForm.progressBar1.Maximum)
                    {
                        MainForm.mainForm.progressBar1.Value = p;
                        MainForm.mainForm.label1.Text = t;
                    }
                }));
            }
        }
        public static void progressReset()
        {
            if (MainForm.mainForm != null)
            {
                MainForm.mainForm.Invoke(new Action(() =>
                {
                    MainForm.mainForm.progressBar1.Value = 0;
                    //Form1.form.progressBar1.Maximum = 0;
                    MainForm.mainForm.label1.Text = "";
                }));
            }
        }
        public static void progressV(bool p)
        {
            if (MainForm.mainForm != null)
            {
                MainForm.mainForm.Invoke(new Action(() =>
                {
                    if (p)
                    {
                        MainForm.mainForm.progressBar1.Visible = true;
                    }
                    else
                    {
                        MainForm.mainForm.progressBar1.Visible = false;
                    }
                }));
            }
        }
    }
}
