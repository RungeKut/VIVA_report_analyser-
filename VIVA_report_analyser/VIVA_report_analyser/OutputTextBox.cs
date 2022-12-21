using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIVA_report_analyser
{
    public partial class OutputTextBox : Form
    {
        public static OutputTextBox outputTextBox = null;
        public OutputTextBox()
        {
            outputTextBox = this;
            InitializeComponent();
            outputTextBox.FormClosing += OutputTextBox_FormClosing;
            string outText;
            foreach (var board in DataModel.openFiles)
            {

            }

        }

        private void OutputTextBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            UI.MainForm.mainForm.button3.Enabled = true;
        }
    }
}
