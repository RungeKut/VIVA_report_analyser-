using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIVA_report_analyser // 30 46 56 61 112
{
    public class StyleColor
    {
        public static Color g1 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30))))); } }
        public static Color g2 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46))))); } }
        public static Color g3 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56))))); } }
        public static Color g4 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61))))); } }
        public static Color g5 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112))))); } }
        public static void Init()
        {   /*
            // form 1
            Form1.form.BackColor = g1;
            Form1.form.ForeColor = Color.White;
            Form1.form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            Form1.form.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            // tabControl1 1

            // tabPage 1
            Form1.form.tabPage1.BackColor = g1;
            Form1.form.tabPage1.ForeColor = g1;
            Form1.form.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            Form1.form.ResumeLayout(false);
            // bottom 1
            Form1.form.button1.BackColor = g1;
            Form1.form.button1.FlatAppearance.MouseOverBackColor = g4;
            Form1.form.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            */
        }
    }
}
