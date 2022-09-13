using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIVA_report_analyser
{
    internal class RightMouseClickFileTab
    {
        public static ContextMenuStrip rightMouseClickFileTabContextMenuStrip;
        private static int tabNumber = 0;
        private static int closeTabCount = 0;
        private static TabControl selectTab;
        public static ContextMenuStrip InitializeRightMouseClickFileTab(TabControl tab)
        {
            tab.Focus();
            tab.MouseUp += FileTab_MouseClick;
            ContextMenuStrip fileTabMenu = new ContextMenuStrip();
            ToolStripMenuItem CloseTab_MenuItem = new ToolStripMenuItem("Закрыть вкладку");
            ToolStripMenuItem CloseTabs_MenuItem = new ToolStripMenuItem("Закрыть все вкладки");
            ToolStripMenuItem RecoverTab_MenuItem = new ToolStripMenuItem("Открыть закрытую вкладку");

            fileTabMenu.Items.AddRange(new[]
            {
                CloseTab_MenuItem,
                CloseTabs_MenuItem,
                RecoverTab_MenuItem
            });
            tab.ContextMenuStrip = fileTabMenu;
            fileTabMenu.Items[2].Enabled = false;
            CloseTab_MenuItem.Click += CloseTab_MenuItem_Click;
            CloseTabs_MenuItem.Click += CloseTabs_MenuItem_Click;
            RecoverTab_MenuItem.Click += RecoverTab_MenuItem_Click;
            fileTabMenu.Show(Cursor.Position);
            selectTab = tab;
            return fileTabMenu;
        }
        private static void FileTab_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < selectTab.TabPages.Count; i++)
                {
                    if (selectTab.GetTabRect(i).Contains(e.Location))
                    {
                        tabNumber = i;
                        return;
                    }
                }
            }
        }
        private static void CloseTab_MenuItem_Click(object sender, EventArgs e)
        {
            closeTabCount++;
            for (int file = 0; file < DataModel.dataFiles.Count; file++)
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++)
                {
                    if (selectTab.TabPages[tabNumber].Name ==
                        DataModel.dataFiles[file].Name + " | " + DataModel.dataFiles[file].biSec.BI[numBI].ID + " | " + DataModel.dataFiles[file].biSec.BI[numBI].BC)
                    {
                        DataModel.dataFiles[file].biSec.BI[numBI].visible = false;
                        DataModel.dataFiles[file].biSec.BI[numBI].closeNumber = closeTabCount;
                        selectTab.TabPages.Remove(selectTab.TabPages[tabNumber] as TabPage);
                        rightMouseClickFileTabContextMenuStrip.Items[2].Enabled = true;
                        return;
                    }
                }
            }
            if (selectTab.TabPages[tabNumber].Name == ParseXml.Сalculations[1].translation)
                selectTab.TabPages.Remove(selectTab.TabPages[tabNumber] as TabPage);
        }
        private static void CloseTabs_MenuItem_Click(object sender, EventArgs e)
        {
            selectTab.TabPages.Clear();
            DataModel.dataFiles.Clear();
        }
        private static void RecoverTab_MenuItem_Click(object sender, EventArgs e)
        {
            for (int file = 0; file < DataModel.dataFiles.Count; file++)
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++)
                {
                    if (DataModel.dataFiles[file].biSec.BI[numBI].closeNumber == closeTabCount)
                    {
                        closeTabCount--;
                        string tabName = DataModel.dataFiles[file].Name + " | " + DataModel.dataFiles[file].biSec.BI[numBI].ID + " | " + DataModel.dataFiles[file].biSec.BI[numBI].BC;
                        TabPage page = new TabPage(tabName);
                        page.Name = tabName;
                        selectTab.TabPages.Add(page);
                        //page.MouseClick += Page_MouseClick;
                        TabControl tabTests = new TabControl();
                        page.Controls.Add(tabTests);
                        tabTests.Dock = DockStyle.Fill;
                        tabTests.ItemSize = new System.Drawing.Size(0, 24);
                        tabTests.SelectedIndex = 0;
                        tabTests.TabIndex = 1;
                        tabTests.Name = DataModel.dataFiles[file].Name;

                        for (int test = 0; test < ParseXml.testCount; test++)
                        {
                            Form1.AddNewComponentTab
                            (
                                ParseXml.vivaXmlTests[test].translation,
                                tabTests,
                                DataModel.dataFiles[file].biSec.BI[numBI].dataFilteredByTests[test].Tests
                            );

                        }
                        Form1.AddNewComponentTab
                            (
                                ParseXml.Сalculations[0].translation,
                                tabTests,
                                DataModel.dataFiles[file].biSec.BI[numBI].testsSec.TEST
                            );
                        DataModel.dataFiles[file].biSec.BI[numBI].visible = true;
                        DataModel.dataFiles[file].biSec.BI[numBI].closeNumber = 0;
                        if (closeTabCount <= 0)
                        {
                            closeTabCount = 0;
                            rightMouseClickFileTabContextMenuStrip.Items[2].Enabled = false;
                        }
                        break;
                    }
                }
            }
        }
    }
}
