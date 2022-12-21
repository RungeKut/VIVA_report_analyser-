using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIVA_report_analyser.UI
{
    internal class RightMouseClickFileTab
    {
        private class ClosedTab
        {
            public int fileCount { get; set; }
            public int boardCount { get; set; }
        }
        public static ContextMenuStrip rightMouseClickFileTabContextMenuStrip;
        private static int tabNumber = 0;
        private static bool hitTarget = false;
        private static List<ClosedTab> closedTabs = new List<ClosedTab>(); // История закрытия вкладок
        private static TabControl selectTab;
        public static ContextMenuStrip InitializeRightMouseClickFileTab(TabControl tab)
        {
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
            if (hitTarget)
                fileTabMenu.Show(Cursor.Position);
                
            selectTab = tab;
            return fileTabMenu;
        }
        public static void FileTab_MouseClick(object sender, MouseEventArgs e)
        {
            TabControl tab = (TabControl)sender;
            if (tab != MainForm.mainForm.tabControl2)
            {
                hitTarget = false;
                return;
            }
            hitTarget = true;
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tab.TabPages.Count; i++)
                {
                    if (tab.GetTabRect(i).Contains(e.Location))
                    {
                        tabNumber = i;
                        rightMouseClickFileTabContextMenuStrip = InitializeRightMouseClickFileTab(tab);
                        return;
                    }
                }
            }
        }
        private static void CloseTab_MenuItem_Click(object sender, EventArgs e)
        {
            if (!hitTarget) return;
            for (int file = 0; file < DataModel.dataFiles.Count; file++)
            {
                for (int numBI = 0; numBI < DataModel.dataFiles[file].biSec.BI.Count; numBI++)
                {
                    if (selectTab.TabPages[tabNumber].Name ==
                        DataModel.dataFiles[file].Name + " | " + DataModel.dataFiles[file].biSec.BI[numBI].ID + " | " + DataModel.dataFiles[file].biSec.BI[numBI].BC)
                    {
                        DataModel.dataFiles[file].biSec.BI[numBI].visible = false;
                        closedTabs.Add(new ClosedTab
                        {
                            fileCount = file,
                            boardCount = numBI,
                        });
                        DataModel.dataFiles[file].biSec.BI[numBI].closeNumber = closedTabs.Count;
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
            closedTabs.Clear();
        }
        private static void RecoverTab_MenuItem_Click(object sender, EventArgs e)
        {
            int closeNumber = closedTabs.Count - 1;
            DataModel.dataFiles[closedTabs[closeNumber].fileCount].biSec.BI[closedTabs[closeNumber].boardCount].closeNumber = 0;
            DataModel.dataFiles.needUpdateView = true;
            if (closeNumber <= 0)
                rightMouseClickFileTabContextMenuStrip.Items[2].Enabled = false;
            closedTabs.RemoveAt(closeNumber);
        }
                        
    }
}
