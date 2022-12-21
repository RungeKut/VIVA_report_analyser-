using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading.Tasks;

namespace VIVA_report_analyser.ReportGenerator
{
    /// <summary>
    /// Тут собраны различные методы для верстки документа
    /// </summary>
    internal class OpenXML_Function
    {
        /// <summary>
        /// Добавить текст
        /// </summary>
        /// <param name="str">Добавляемый тест</param>
        /// <param name="fontStyle">Стиль текста</param>
        /// <param name="boldStyle">Жирный</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="alignment">Выравнивание</param>
        /// <param name="breakFlag"></param>
        /// <param name="after">Интервал</param>
        /// <param name="tabstop"></param>
        /// <param name="keepNext"></param>
        /// <param name="pageSectionBreak">Разрыв страницы после текста</param>
        /// <param name="colour">Цвет текста</param>
        /// <returns>Объект Paragraph</returns>
        public static Paragraph AddNewText(String str,
            String fontStyle = "Arial",
            Boolean boldStyle = false,
            int fontSize = 24,
            JustificationValues alignment = JustificationValues.Left,
            Boolean breakFlag = false,
            int? after = null,
            int[] tabstop = null,
            bool keepNext = false,
            bool pageSectionBreak = false,
            String colour = "000000"
            )
        {
            Paragraph paragraph = new Paragraph();

            ParagraphProperties paragraphProperties = new ParagraphProperties();

            if (keepNext)
            {
                paragraphProperties.Append(new KeepNext());
                paragraphProperties.Append(new KeepLines());
            }

            Justification justification = new Justification() { Val = alignment };

            ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
            RunFonts font = new RunFonts { Ascii = fontStyle };

            font = new RunFonts() { Ascii = fontStyle, HighAnsi = fontStyle };
            if (boldStyle == true)
            {
                Bold bold = new Bold();
                paragraphMarkRunProperties.Append(bold);
            }
            FontSize fontSize1 = new FontSize() { Val = fontSize.ToString() };

            paragraphMarkRunProperties.Append(font);
            paragraphMarkRunProperties.Append(fontSize1);

            paragraphProperties.Append(justification);
            paragraphProperties.Append(paragraphMarkRunProperties);

            if (tabstop != null)
            {
                Tabs tabs1 = new Tabs();
                foreach (int pos in tabstop)
                {

                    TabStop tabStop1 = new TabStop() { Val = TabStopValues.Left, Position = pos };

                    tabs1.Append(tabStop1);
                }

                paragraphProperties.Append(tabs1);
            }

            if (after != null)
            {
                SpacingBetweenLines spacingBetweenLines = new SpacingBetweenLines() { Line = "240", LineRule = LineSpacingRuleValues.Auto, Before = "0", After = after.ToString() };
                paragraphProperties.Append(spacingBetweenLines);
            }
            else
            {
                SpacingBetweenLines spacingBetweenLines = new SpacingBetweenLines() { Line = "240", LineRule = LineSpacingRuleValues.Auto, Before = "0", After = "0" };
                paragraphProperties.Append(spacingBetweenLines);
            }

            Run run = new Run();
            RunProperties rPr = new RunProperties();

            font = new RunFonts() { Ascii = fontStyle, HighAnsi = fontStyle };
            if (boldStyle == true)
            {
                Bold bold = new Bold();
                rPr.AppendChild(bold);
            }

            Color color = new Color() { Val = colour };

            rPr.AppendChild(color);

            fontSize1 = new FontSize() { Val = fontSize.ToString() };

            rPr.AppendChild(font);
            rPr.AppendChild(fontSize1);


            run.Append(rPr);

            if (pageSectionBreak)
            {
                LastRenderedPageBreak lastRenderedPageBreak1 = new LastRenderedPageBreak();
                run.Append(lastRenderedPageBreak1);
            }


            String[] textBlocks = str.Split('\t');

            for (int i = 0; i < textBlocks.Length; i++)
            {

                Text text1 = new Text();
                text1.Text = textBlocks[i];


                run.Append(text1);

                if (i != textBlocks.Length - 1)
                    run.Append(new TabChar());
            }
            if (breakFlag)
            {
                Break _break = new Break() { Type = BreakValues.Page };
                run.Append(_break);
            }

            paragraph.Append(paragraphProperties);
            paragraph.Append(run);
            return paragraph;
        }
    }
}
