using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIVA_report_analyser.MainForm // 30 46 56 61 112
{
    public class StyleColor
    {
        public static Color g1 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30))))); } }
        public static Color g2 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46))))); } }
        public static Color g3 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56))))); } }
        public static Color g4 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61))))); } }
        public static Color g5 { get { return System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112))))); } }
        public static void Init()
        {
            /*
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
    public class TransparentTabControl : TabControl
    {
        private List<Panel> pages = new List<Panel>();

        public void MakeTransparent()
        {
            if (TabCount == 0) throw new InvalidOperationException();
            var height = GetTabRect(0).Bottom;
            // Move controls to panels
            for (int tab = 0; tab < TabCount; ++tab)
            {
                var page = new Panel
                {
                    Left = this.Left,
                    Top = this.Top + height,
                    Width = this.Width,
                    Height = this.Height - height,
                    BackColor = Color.Transparent,
                    Visible = tab == this.SelectedIndex
                };
                for (int ix = TabPages[tab].Controls.Count - 1; ix >= 0; --ix)
                {
                    TabPages[tab].Controls[ix].Parent = page;
                }
                pages.Add(page);
                this.Parent.Controls.Add(page);
            }
            this.Height = height /* + 1 */;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            for (int tab = 0; tab < pages.Count; ++tab)
            {
                pages[tab].Visible = tab == SelectedIndex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) foreach (var page in pages) page.Dispose();
            base.Dispose(disposing);
        }
    }
}
