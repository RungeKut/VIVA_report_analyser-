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
        Document document;
        public void MakeReportFromTemplate(string fileName)
        {
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
            var documentPath = saveFileDialog.FileName;
            WordprocessingDocument doc = null;
            try
            {
                doc = WordprocessingDocument.Create(documentPath, WordprocessingDocumentType.Document);
            }
            catch (Exception ex)
            {
                throw new Exception("Невозможно открыть файл с именем, выбранным для сохранения отчета", ex);
            }

            using (doc)
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                document = mainPart.Document;
                Body body = doc.MainDocumentPart.Document.AppendChild(new Body());
                PageSize ps;
                PageMargin pm;
                ps = new PageSize() { Width = (UInt32Value)16839U, Height = (UInt32Value)11907U, Orient = PageOrientationValues.Landscape };
                pm = new PageMargin() { Top = 900, Right = (UInt32Value)850U, Bottom = 600, Left = (UInt32Value)850U, Footer = 300, Header = 450 };
                SectionProperties sp = new SectionProperties() { RsidR = "00E0164B", RsidSect = "00E0164B" };
                sp.AppendChild(ps);
                sp.AppendChild(pm);

                Table table1 = new Table();
                TableBorders borders = new TableBorders();
                TableProperties tableProp = new TableProperties();
                //TableStyle tableStyle = new TableStyle() { Val = "TableGrid" };


                TableWidth tableWidth = new TableWidth() { Width = "9356", Type = TableWidthUnitValues.Dxa };

                tableProp.Append(tableWidth);

                //tableProp.Append(tableStyle);
                table1.AppendChild(tableProp);

                TableGrid tg = new TableGrid(new GridColumn());
                table1.AppendChild(tg);

                TableRow row1 = new TableRow();
                TableCell cell1_1 = new TableCell();
                cell1_1.Append(OpenXML_Function.AddNewText(str: "АКТ", boldStyle: true, fontSize: 32, alignment: JustificationValues.Center, colour:"000065"));
                row1.Append(cell1_1);
                table1.Append(row1);

                TableRow row2 = new TableRow();
                TableCell cell1_2 = new TableCell();
                cell1_2.Append(OpenXML_Function.AddNewText(str: "Автоматизированного электрического контроля ПУ № 1 от " + DateTime.Now.ToString("dd MMMM yyyy") + " г.", boldStyle: true, alignment: JustificationValues.Center, colour: "000065"));
                row2.Append(cell1_2);
                table1.Append(row2);

                TableRow row3 = new TableRow();
                TableCell cell1_3 = new TableCell();
                cell1_3.Append(OpenXML_Function.AddNewText(str: ""));
                row3.Append(cell1_3);
                table1.Append(row3);

                TableRow row4 = new TableRow();
                TableCell cell1_4 = new TableCell();
                cell1_4.Append(OpenXML_Function.AddNewText(str: "Децимальный номер ПУ:", boldStyle: true));
                cell1_4.Append(new TableCellProperties(new TableCellWidth() { Width = "6521", Type = TableWidthUnitValues.Dxa }));
                row4.Append(cell1_4);
                TableCell cell2_4 = new TableCell();
                cell2_4.Append(OpenXML_Function.AddNewText(str: "LNVK.000000.001", boldStyle: true));
                cell2_4.Append(new TableCellProperties(new TableCellWidth() { Width = "2835", Type = TableWidthUnitValues.Dxa }));
                row4.Append(cell2_4);
                table1.Append(row4);

                body.Append(table1);
            }
        }
    }
}
