using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace VIVA_report_analyser.ReportGenerator
{
    internal class OpenXMLReportMaker
    {
        Document document;
        public void MakeReport(string fileName)
        {
            WordprocessingDocument doc = null;

            try
            {
                doc = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document);
            }
            catch (Exception ex)
            {
                throw new Exception("Невозможно открыть файл с именем, выбранным для сохранения отчета", ex);
            }
            try
            {
                using (doc)
                {
                    MainDocumentPart mainPart = doc.AddMainDocumentPart();

                    // Create the document structure and add some text.
                    mainPart.Document = new Document();
                    document = mainPart.Document;

                    Body body = doc.MainDocumentPart.Document.AppendChild(new Body());

                    //PageSize ps = new PageSize() { Width = (UInt32Value)16838U, Height = (UInt32Value)11906U, Orient = PageOrientationValues.Landscape };
                    //PageMargin pm = new PageMargin() { Top = 1701, Right = (UInt32Value)1134U, Bottom = 850, Left = (UInt32Value)1134U };

                    //Портретный вид

                    PageSize ps;
                    PageMargin pm;
                    if (/*settings.IsLandscapeFormat*/true)
                    {
                        ps = new PageSize() { Width = (UInt32Value)16839U, Height = (UInt32Value)11907U, Orient = PageOrientationValues.Landscape };

                        pm = new PageMargin() { Top = 900, Right = (UInt32Value)850U, Bottom = 600, Left = (UInt32Value)850U, Footer = 300, Header = 450 };
                    }
                    else
                    {
                        //ps = new PageSize() { Width = (UInt32Value)11906U, Height = (UInt32Value)16838U, Orient = PageOrientationValues.Portrait };
                        //pm = new PageMargin() { Top = 1701, Right = (UInt32Value)1134U, Bottom = 850, Left = (UInt32Value)1134U };

                        ps = new PageSize() { Width = (UInt32Value)11907U, Height = (UInt32Value)16839U, Orient = PageOrientationValues.Portrait };

                        pm = new PageMargin() { Top = (Int32Value)850, Right = 600, Bottom = (Int32Value)850, Left = 950, Footer = 300, Header = 450 };

                    }

                    SectionProperties sp = new SectionProperties() { RsidR = "00E0164B", RsidSect = "00E0164B" };

                    sp.AppendChild(ps);
                    sp.AppendChild(pm);

                    Paragraph paragraph;
                    // Выводим основной текст протокола
                    
                        paragraph = AddNewText("Times New Roman", true, 32, "Приложение № 2" , JustificationValues.Center, false);
                        body.Append(paragraph);
                        paragraph = AddNewText("Times New Roman", true, 24, "к протоколу предъявительских и приемо-сдаточных испытаний № _____", JustificationValues.Center, false);
                        body.Append(paragraph);



                    // Создание нижнего коллонтитула, содержащего номер страницы
                    FooterPart FootPart = doc.MainDocumentPart.AddNewPart<FooterPart>();

                    //GetSignatures(type);
                    String footertext = String.Format("Зав. № {0}");
                    FootPart.Footer = CreateFooterPart(footertext);

                    string strFootrID = doc.MainDocumentPart.GetIdOfPart(FootPart);
                    FooterReference objFooterReference = new FooterReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = strFootrID
                    };
                    sp.Append(objFooterReference);


                    //Колонтитул второго раздела
                    FooterPart FootPart2 = doc.MainDocumentPart.AddNewPart<FooterPart>();

                    //GetSignatures(type);
                    String footertext2 = String.Format("Зав. № {0}");

                    string strFootrID2 = doc.MainDocumentPart.GetIdOfPart(FootPart2);
                    FooterReference objFooterReference2 = new FooterReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = strFootrID2
                    };


                    //body.AppendChild(sp);

                    // Создание верхнего коллонтитула, содержащего уникальный номер проверки
                    HeaderPart HeaderPart = doc.MainDocumentPart.AddNewPart<HeaderPart>();

                    string headerString;

                    if (/*tphs.Hs.TphId != "-1"*/true)
                        headerString = "ID запуска:      ID проверки:  " ;
                    else
                        headerString = "ID запуска:      ID проверки:  Н/Д";


                    string strHeaderID = doc.MainDocumentPart.GetIdOfPart(HeaderPart);
                    HeaderReference objHeaderReference = new HeaderReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = strHeaderID
                    };
                    sp.Append(objHeaderReference);
                    //body.AppendChild(sp);

                    HeaderPart = doc.MainDocumentPart.AddNewPart<HeaderPart>();

                    string strHeaderID2 = doc.MainDocumentPart.GetIdOfPart(HeaderPart);
                    HeaderReference objHeaderReference2 = new HeaderReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = strHeaderID2

                    };


                    SectionProperties sectionProperties2 = new SectionProperties() { RsidRPr = "00674F31", RsidR = "00674F31", RsidSect = "00535B09" };
                    PageSize ps2;
                    PageMargin pm2;

                    if (/*settings.IsLandscapeFormat*/true)
                    {
                        ps2 = new PageSize() { Width = (UInt32Value)16838U, Height = (UInt32Value)11906U, Orient = PageOrientationValues.Landscape };
                        pm2 = new PageMargin() { Top = 1701, Right = (UInt32Value)1134U, Bottom = 850, Left = (UInt32Value)1134U };
                    }
                    else
                    {
                        ps2 = new PageSize() { Width = (UInt32Value)11906U, Height = (UInt32Value)16838U, Orient = PageOrientationValues.Portrait };
                        pm2 = new PageMargin() { Top = 1701, Right = (UInt32Value)1134U, Bottom = 850, Left = (UInt32Value)1134U };
                    }


                    sectionProperties2.Append(ps2);
                    sectionProperties2.Append(pm2);

                    sectionProperties2.Append(objHeaderReference2);
                    sectionProperties2.Append(objFooterReference2);

                    paragraph = AddNewText("Times New Roman", false, 24, "", JustificationValues.Left, false, null, null, false, true);

                    body.Append(paragraph);
                    paragraph.ParagraphProperties.Append(sp);

                    body.Append(paragraph);

                    paragraph = AddNewText("Times New Roman", false, 24, "", JustificationValues.Left, false);

                    body.Append(paragraph); //Документ должен заканчиваться параграфом

                    body.Append(sectionProperties2);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Не существует настроек для потока, в рамках которого создана данная запись в таблице истории программ тестирования. Создать отчет невозможно", ex);

            }
        }
        Table CreateFoterSignatures()
        {
            //**************************************************
            Table _table = new Table();

            TableProperties tblProp = new TableProperties();

            // Create a TableProperties object and specify its border information.
            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder() { Val = BorderValues.None, Color = "auto", Size = (UInt32Value)0U, Space = (UInt32Value)0U };

            tableBorders1.Append(topBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(bottomBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);

            tblProp.Append(tableBorders1);

            TableWidth _tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

            tblProp.Append(_tableWidth);

            // Append the TableProperties object to the empty table.
            _table.AppendChild<TableProperties>(tblProp);
            //**************************************************

            Table table1 = new Table();
            TableBorders borders = new TableBorders();
            TableProperties tableProp = new TableProperties();
            TableStyle tableStyle = new TableStyle() { Val = "TableGrid" };


            TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

            tableProp.Append(tableWidth);

            tableProp.Append(tableStyle);
            //tableProp.Append(new CantSplit());
            table1.AppendChild(tableProp);

            TableGrid tg = new TableGrid(new GridColumn(), new GridColumn(), new GridColumn());
            table1.AppendChild(tg);

            TableRow row1 = new TableRow();
            row1.TableRowProperties = new TableRowProperties(new CantSplit());
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();




            if (/*signatureList.Contains(SignatureTypes.performer)*/true)
                cell1.Append(AddNewText("Times New Roman", false, 20, "Исполнитель _____________________", JustificationValues.Left, false));
            else
                cell1.Append(AddNewText("Times New Roman", false, 20, "                                ", JustificationValues.Left, false));

            if (/*signatureList.Contains(SignatureTypes.otk)*/true)
                cell2.Append(AddNewText("Times New Roman", false, 20, "ОТК ______________________", JustificationValues.Left, false));
            else
                cell2.Append(AddNewText("Times New Roman", false, 20, "                         ", JustificationValues.Left, false));

            if (/*signatureList.Contains(SignatureTypes.vp)*/true)
                cell3.Append(AddNewText("Times New Roman", false, 20, "ВП ______________________", JustificationValues.Left, false));
            else
                cell3.Append(AddNewText("Times New Roman", false, 20, "                        ", JustificationValues.Left, false));


            row1.Append(cell1);
            row1.Append(cell2);
            row1.Append(cell3);

            table1.Append(row1);


            /*
            TableRow row2 = new TableRow();
            row2.TableRowProperties = new TableRowProperties(new CantSplit());
            TableCell cell2_1 = new TableCell();
            TableCell cell2_2 = new TableCell();
            TableCell cell2_3 = new TableCell();
            if (signatureList.Contains(SignatureTypes.performer))
                cell2_1.Append(AddNewText("Times New Roman", false, 20, "«____»__________________________", JustificationValues.Left, false));
            else
                cell2_1.Append(AddNewText("Times New Roman", false, 20, " ", JustificationValues.Left, false));
            if (signatureList.Contains(SignatureTypes.otk))
                cell2_2.Append(AddNewText("Times New Roman", false, 20, "«____»____________________", JustificationValues.Left, false));
            else
                cell2_2.Append(AddNewText("Times New Roman", false, 20, " ", JustificationValues.Left, false));
            if (signatureList.Contains(SignatureTypes.vp))
                cell2_3.Append(AddNewText("Times New Roman", false, 20, "«____»___________________", JustificationValues.Left, false));
            else
                cell2_3.Append(AddNewText("Times New Roman", false, 20, " ", JustificationValues.Left, false));


            row2.Append(cell2_1, cell2_2, cell2_3);
            table1.Append(row2);
            */
            //************************************************************************

            TableRow tr0 = new TableRow();
            tr0.TableRowProperties = new TableRowProperties(new CantSplit());

            //this.AddTableToTableCell(tr0, table1);

            _table.Append(tr0);
            //************************************************************************
            //return table1;
            return _table;
        }
        private Footer CreateFooterPart(string footerText)
        {
            Footer footer = new Footer(
               //new Paragraph(new Run(new Text(""))),
               // new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Right }/*, new SpacingBetweenLines() { Line = "240", LineRule = LineSpacingRuleValues.Auto, Before = "0", After = "5" }*/), new Run(new RunProperties(new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman" }, new Text(footerText)))),
               CreateFoterSignatures(),
               new Paragraph(
                   new ParagraphProperties(
                       new ParagraphStyleId() { /*Val = StyleHelper.Styles[UniqueIdsEnum.FooterStyle]*/  Val = "Footer" },
                       new FrameProperties()
                       {
                           Wrap = TextWrappingValues.Around,
                           HorizontalPosition = HorizontalAnchorValues.Margin,
                           VerticalPosition = VerticalAnchorValues.Text,
                           XAlign = HorizontalAlignmentValues.Center,
                           Y = "1"
                       },
                       new ParagraphMarkRunProperties(
                           new RunStyle() { Val = "PageNumber" })),
                       new Run(
                           new RunProperties(new RunFonts() { Ascii = "Times New Roman" },
                               new RunStyle() { Val = "PageNumber" }),
                           new Text("") { Space = SpaceProcessingModeValues.Preserve }),
                       new Run(
                           new RunProperties(new RunFonts() { Ascii = "Times New Roman" },
                               new RunStyle() { Val = "PageNumber" }),
                           new FieldChar() { FieldCharType = FieldCharValues.Begin }),
                       new Run(
                           new RunProperties(new RunFonts() { Ascii = "Times New Roman" },
                               new RunStyle() { Val = "PageNumber" }),
                           new FieldCode("PAGE  ") { Space = SpaceProcessingModeValues.Preserve }),
                       new Run(
                           new RunProperties(new RunFonts() { Ascii = "Times New Roman" },
                               new RunStyle() { Val = "PageNumber" }),
                           new FieldChar() { FieldCharType = FieldCharValues.Separate }),
                       new Run(
                           new RunProperties(new RunFonts() { Ascii = "Times New Roman" },
                               new RunStyle() { Val = "PageNumber" },
                               new NoProof()),
                           new Text("1")),
                       new Run(
                       new RunProperties(new RunFonts() { Ascii = "Times New Roman" },
                           new RunStyle() { Val = "PageNumber" }),
                       new FieldChar() { FieldCharType = FieldCharValues.End })


               )
               {
                   RsidParagraphAddition = new HexBinaryValue() { Value = "00B85852" },
                   RsidParagraphProperties = new HexBinaryValue() { Value = "00C859D1" },
                   RsidRunAdditionDefault = new HexBinaryValue() { Value = "00B85852" }
               },
               new Paragraph(
                   new ParagraphProperties(
                       new Justification() { Val = JustificationValues.Right },
                       new ParagraphStyleId() { /*Val = StyleHelper.Styles[UniqueIdsEnum.FooterStyle]*/  Val = "Footer" },
                       new Indentation() { Right = "360" },
                       new Run(new RunProperties(new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman" }, new Text(footerText))))
               )
               {
                   RsidParagraphAddition = new HexBinaryValue() { Value = "00B85852" },
                   RsidParagraphProperties = new HexBinaryValue() { Value = "00DD5BDD" },
                   RsidRunAdditionDefault = new HexBinaryValue() { Value = "00B85852" }
               });
            return footer;
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
