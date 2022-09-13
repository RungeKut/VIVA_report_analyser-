using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVA_report_analyser
{
    internal class ProgressView
    {
        public static void progressMax(int p)
        {
            if (Form1.form != null)
            {
                Form1.form.Invoke(new Action(() =>
                {
                    if (Form1.form != null)
                        Form1.form.progressBar1.Maximum = p;
                }));
            }
        }
        public static void progress(int p, string t)
        {
            if (Form1.form != null)
            {
                Form1.form.Invoke(new Action(() =>
                {
                    if (p <= Form1.form.progressBar1.Maximum)
                    {
                        Form1.form.progressBar1.Value = p;
                        Form1.form.label1.Text = t;
                    }
                }));
            }
        }
        public static void progressReset()
        {
            if (Form1.form != null)
            {
                Form1.form.Invoke(new Action(() =>
                {
                    Form1.form.progressBar1.Value = 0;
                    //Form1.form.progressBar1.Maximum = 0;
                    Form1.form.label1.Text = "";
                }));
            }
        }
        public static void progressV(bool p)
        {
            if (Form1.form != null)
            {
                Form1.form.Invoke(new Action(() =>
                {
                    if (p)
                    {
                        Form1.form.progressBar1.Visible = true;
                    }
                    else
                    {
                        Form1.form.progressBar1.Visible = false;
                    }
                }));
            }
        }
    }
}
