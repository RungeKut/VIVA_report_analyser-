using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVA_report_analyser.MainForm
{
    internal class MenuCheckedListBox
    {
        public class CheckClass
        {
            public string Name;
            public string Value;
        }
        public static List<CheckClass> Checks = new List<CheckClass>
        {
            new CheckClass { Name = "Сортировать по тестам", Value ="" },
            new CheckClass { Name = "Только ошибки", Value ="" },
            new CheckClass { Name = "Сначала ошибки", Value ="" },
            new CheckClass { Name = "Количество ошибок", Value ="" },
            new CheckClass { Name = "Выбранный тест на всех платах", Value ="" },
            new CheckClass { Name = "Выбранный компонент на всех платах", Value ="" },
            new CheckClass { Name = "БЛАбла", Value ="" },
        };
        public static void Init()
        {
            foreach (CheckClass Check in Checks)
            {
                MainForm.mainForm.optionsCheckedListBox.Items.Add(Check.Name, false);
            }
        }
    }
}
