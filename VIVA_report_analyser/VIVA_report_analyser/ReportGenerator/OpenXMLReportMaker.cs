using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace VIVA_report_analyser.ReportGenerator
{
    internal class OpenXMLReportMaker : IReportGenerator
    {
        public void MakeReportFromTemplate(string fileName)
        {
            var templatePath = @"Y:\#10 Git Project\VIVA_report_analyser\supplementary_files\pattern.docx";
            var documentPath = @"Y:\#10 Git Project\VIVA_report_analyser\supplementary_files\newFilename.docx";
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                CheckFileExists = false,
                CheckPathExists = true,
                DefaultExt = "docx",
                DereferenceLinks = true,
                Filter = "VIVA report docx file (*.docx)|*.docx|All files (*.*)|*.*",
                FilterIndex = 1,
                ValidateNames = true,
                Title = "Сохранение файла .docx",
                FileName = "report.docx"
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileNames.Length <= 0)
            {
                //e.Cancel = true;
                return;
            }
            documentPath = saveFileDialog.FileName;

            using (var template = File.OpenRead(templatePath))
            {
                using (var documentStream = File.Open(documentPath, FileMode.OpenOrCreate))
                { // Скопируем шаблон
                    template.CopyTo(documentStream);

                    using (var document = WordprocessingDocument.Open(documentStream, true))
                    {
                        // Создадим временную таблицу
                        DataTable dt = new DataTable();
                        int rowCount = 0;
                        Body body = document.MainDocumentPart.Document.Body;
                        // Найдем первую таблицу в документе
                        Table table = body.Elements<Table>().First();
                        // Получим список список всех строк таблицы
                        IEnumerable<TableRow> rows = table.Elements<TableRow>();
                        // Пробежимся по ячейкам
                        TableCell cell = rows.ElementAt<TableRow>(3).Elements<TableCell>().Last<TableCell>();
                        //string designator = rows.ElementAt<TableRow>(3).Elements<TableCell>().Last<TableCell>().InnerText;
                        //document.MainDocumentPart.Document.Body.Elements<Table>().First().Elements<TableRow>().ElementAt<TableRow>(3).Elements<TableCell>().Last<TableCell>().InnerText;
                        //designator = "Сообщение";
                        //body.Append(cell);
                        //MessageBox.Show(cell.InnerText, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        document.MainDocumentPart.Document.Save();
                    }
                }
            }
        }
        
        public void MakeReport(string fileName)
        {
            
        }
        public Paragraph AddNewText(String fontName, Boolean boldFlag, int fontSizeNumber, String str, JustificationValues val, Boolean breakFlag)
        {
            return AddNewText(fontName, boldFlag, fontSizeNumber, str, val, breakFlag, null, null);
        }

        public Paragraph AddNewText(String fontName, Boolean boldFlag, int fontSizeNumber, String str, JustificationValues val, Boolean breakFlag, int? after)
        {
            return AddNewText(fontName, boldFlag, fontSizeNumber, str, val, breakFlag, after, null);
        }


        public Paragraph AddNewText(String fontName, Boolean boldFlag, int fontSizeNumber, String str, JustificationValues val, Boolean breakFlag, int? after, int[] tabstop)
        {
            return AddNewText(fontName, boldFlag, fontSizeNumber, str, val, breakFlag, after, tabstop, false);
        }

        public Paragraph AddNewText(String fontName, Boolean boldFlag, int fontSizeNumber, String str, JustificationValues val, Boolean breakFlag, int? after, int[] tabstop, bool keepNext)
        {
            return AddNewText(fontName, boldFlag, fontSizeNumber, str, val, breakFlag, after, tabstop, keepNext, false);
        }

        public Paragraph AddNewText(String fontName, Boolean boldFlag, int fontSizeNumber, String str, JustificationValues val, Boolean breakFlag, int? after, int[] tabstop, bool keepNext, bool pageSectionBreak)
        {
            Paragraph paragraph = new Paragraph();

            ParagraphProperties paragraphProperties = new ParagraphProperties();

            if (keepNext)
            {
                paragraphProperties.Append(new KeepNext());
                paragraphProperties.Append(new KeepLines());
            }

            Justification justification = new Justification() { Val = val };

            ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
            RunFonts font = new RunFonts { Ascii = fontName };

            font = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman" };
            if (boldFlag == true)
            {
                Bold bold = new Bold();
                paragraphMarkRunProperties.Append(bold);
            }
            FontSize fontSize = new FontSize() { Val = fontSizeNumber.ToString() };

            paragraphMarkRunProperties.Append(font);
            paragraphMarkRunProperties.Append(fontSize);

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

            Run run = new Run();
            RunProperties rPr = new RunProperties();

            font = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman" };
            if (boldFlag == true)
            {
                Bold bold = new Bold();
                rPr.AppendChild(bold);
            }
            fontSize = new FontSize() { Val = fontSizeNumber.ToString() };

            rPr.AppendChild(font);
            rPr.AppendChild(fontSize);


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
