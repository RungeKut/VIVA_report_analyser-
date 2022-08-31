using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VIVA_report_analyser
{
    public class ParsedXml
    {
        internal InfoClass     Info   { get; set; }
        internal FidMrkClass   FidMrk { get; set; }
        internal PrgCClass     PrgC   { get; set; }
        internal STClass       ST     { get; set; }
        internal List<BIClass> BI     { get; set; }
        internal ETClass       ET     { get; set; }
    }
    internal class InfoClass
    {
        public String Version { get; set; }
    }
    internal class FidMrkClass
    {
        internal List<MIClass> MI { get; set; }
    }
    internal class MIClass
    {
        public String NM { get; set; }
        public String MZ { get; set; }
        public String K  { get; set; }
        public String SD { get; set; }
        public String M  { get; set; }
        public Double C  { get; set; }
        public Double X  { get; set; }
        public Double Y  { get; set; }
        public Double XO { get; set; }
        public Double YO { get; set; }
        public String F  { get; set; }
    }
    internal class PrgCClass
    {
        public String AD { get; set; }
        public String SD { get; set; }
        public String PN { get; set; }
        public Double TU { get; set; }
        public Double TN { get; set; }
        public Double TD { get; set; }
        public Double TT { get; set; }
        public Double BU { get; set; }
        public Double BN { get; set; }
        public Double BD { get; set; }
        public Double BT { get; set; }
        public Double TH { get; set; }
        public Double SX { get; set; }
        public Double SZ { get; set; }
        public String TO { get; set; }
        public String TY { get; set; }
        public String MR { get; set; }
        public Double TM { get; set; }
        public Double BM { get; set; }
        public String RT { get; set; }
        public Double NR { get; set; }
        public Double MO { get; set; }
        public String RA { get; set; }
        public Double PV { get; set; }
    }
    internal class STClass
    {
        public String TN  { get; set; } //
        public String NMP { get; set; } //
        public String NM  { get; set; } // Board Name
        public String LT  { get; set; } // Batch code Код партии
        public String BC  { get; set; } // Board code
        public String OP  { get; set; } // Operator name
        public String TS  { get; set; } // Test Metod (Normal test)
        public String WS  { get; set; } //
        public String SD  { get; set; } // Test start date
        public Double ME  { get; set; } //
        public Double PA  { get; set; } //
        public Double SI  { get; set; } //
    }
    public class BIClass
    {
        public String BCP { get; set; } //
        public String BC  { get; set; } //
        public Double ID  { get; set; } //
        public Double TR  { get; set; } //
        public Double AK  { get; set; } //
        public String SD  { get; set; } // Test start date
        public Double TT  { get; set; } // 
        public Double NT  { get; set; } // Number of tests
        public Double NF  { get; set; } // Number of errors
        public List<ColumnsClass> Test { get; set; }
    }
    public class ColumnsClass
    {
        // Поля класса
        public String F   { get; set; } //
        public String FT  { get; set; } //
        public String C   { get; set; } //
        public String SG1 { get; set; } // Net name 1
        public String SG2 { get; set; } // Net name 2
        public String PD1 { get; set; } // Pad name 1
        public String PD2 { get; set; } // Pad name 2
        public String XY1 { get; set; } // Probe coordinates 1
        public String XY2 { get; set; } // Probe coordinates 1
        public String CP1 { get; set; } // 
        public String CP2 { get; set; } // 
        public String SC  { get; set; } // 
        public String NM  { get; set; } // Component name
        public String DN  { get; set; } // Component description
        public Double PT  { get; set; } // Positive tolerance ?
        public Double NT  { get; set; } // Negative tolerance ?
        public Double IDC { get; set; } //
        public String MK  { get; set; } //
        public Double IDM { get; set; } //
        public Double PW  { get; set; } //
        public String LB  { get; set; } //
        public String IN  { get; set; } //
        public Double IDL { get; set; } //
        public Double TR  { get; set; } //
        public String MU  { get; set; } // Units of measurement
        public Double ML  { get; set; } // Minimum value
        public Double MM  { get; set; } // Set value
        public Double MH  { get; set; } // Maximum value
        public Double MR  { get; set; } // Measured value
        public Double MP  { get; set; } // Measurement deviation %
        public Double TT  { get; set; } //
        public Double IS  { get; set; } //
        public Double DG  { get; set; } //
        public String FR  { get; set; } // Error description
        public String uniqueTestName { get { return NM + " | " + F + " | " + PD1 + " | " + PD2; } } // Составной уникальный идентификатор теста
    }
    internal class ETClass
    {
        public String NMP { get; set; } //
        public String NM  { get; set; } //
        public String LT  { get; set; } //
        public String BC  { get; set; } //
        public String OP  { get; set; } //
        public Double TR  { get; set; } //
        public Double AK  { get; set; } //
        public String TT  { get; set; } //
        public Double NT  { get; set; } //
        public Double NF  { get; set; } //
        public String ED  { get; set; } // Test end date
        public Double DM  { get; set; } //
    }
    internal class VivaXmlColumnsClass
    {
        // Поля класса
        public string Name { get; set; }
        public string Translation { get; set; }
    }
    internal class VivaXmlTestsClass : VivaXmlColumnsClass { }
    internal class СalculationsClass : VivaXmlColumnsClass { }
    internal class ParseXml
    {
        public static List<VivaXmlColumnsClass> vivaXmlColumns = new List<VivaXmlColumnsClass>
        {
            new VivaXmlColumnsClass { Name = "F"  ,            Translation ="Тест"                      }, // 0
            new VivaXmlColumnsClass { Name = "FT" ,            Translation ="Функция"                   }, // 1
            new VivaXmlColumnsClass { Name = "C"  ,            Translation ="Каналы"                    }, // 2
            new VivaXmlColumnsClass { Name = "SG1",            Translation ="Имя цепи 1"                }, // 3
            new VivaXmlColumnsClass { Name = "SG2",            Translation ="Имя цепи 2"                }, // 4
            new VivaXmlColumnsClass { Name = "PD1",            Translation ="Точка подключения 1"       }, // 5
            new VivaXmlColumnsClass { Name = "PD2",            Translation ="Точка подключения 2"       }, // 6
            new VivaXmlColumnsClass { Name = "XY1",            Translation ="Координаты подключения 1"  }, // 7
            new VivaXmlColumnsClass { Name = "XY2",            Translation ="Координаты подключения 2"  }, // 8
            new VivaXmlColumnsClass { Name = "CP1",            Translation ="CP1"                       }, // 9
            new VivaXmlColumnsClass { Name = "CP2",            Translation ="CP2"                       }, // 10
            new VivaXmlColumnsClass { Name = "SC" ,            Translation ="SC"                        }, // 11
            new VivaXmlColumnsClass { Name = "NM" ,            Translation ="Имя компонента"            }, // 12
            new VivaXmlColumnsClass { Name = "DN" ,            Translation ="DN"                        }, // 13
            new VivaXmlColumnsClass { Name = "PT" ,            Translation ="PT"                        }, // 14
            new VivaXmlColumnsClass { Name = "NT" ,            Translation ="NT"                        }, // 15
            new VivaXmlColumnsClass { Name = "IDC",            Translation ="IDC"                       }, // 16
            new VivaXmlColumnsClass { Name = "MK" ,            Translation ="MK"                        }, // 17
            new VivaXmlColumnsClass { Name = "IDM",            Translation ="IDM"                       }, // 18
            new VivaXmlColumnsClass { Name = "PW" ,            Translation ="PW"                        }, // 19
            new VivaXmlColumnsClass { Name = "LB" ,            Translation ="LB"                        }, // 20
            new VivaXmlColumnsClass { Name = "IN" ,            Translation ="IN"                        }, // 21
            new VivaXmlColumnsClass { Name = "IDL",            Translation ="IDL"                       }, // 22
            new VivaXmlColumnsClass { Name = "TR" ,            Translation ="TR"                        }, // 23
            new VivaXmlColumnsClass { Name = "MU" ,            Translation ="Единицы измерения"         }, // 24
            new VivaXmlColumnsClass { Name = "ML" ,            Translation ="Минимальное"               }, // 25
            new VivaXmlColumnsClass { Name = "MM" ,            Translation ="Уставка"                   }, // 26
            new VivaXmlColumnsClass { Name = "MH" ,            Translation ="Максимальное"              }, // 27
            new VivaXmlColumnsClass { Name = "MR" ,            Translation ="Измеренное"                }, // 28
            new VivaXmlColumnsClass { Name = "MP" ,            Translation ="Отклонение, %"             }, // 29
            new VivaXmlColumnsClass { Name = "TT" ,            Translation ="TT"                        }, // 30
            new VivaXmlColumnsClass { Name = "IS" ,            Translation ="IS"                        }, // 31
            new VivaXmlColumnsClass { Name = "DG" ,            Translation ="DG"                        }, // 32
            new VivaXmlColumnsClass { Name = "FR" ,            Translation ="Описание ошибки"           }, // 33
            new VivaXmlColumnsClass { Name = "uniqueTestName", Translation ="Идентификатор"             }  // 34
        };
        public static List<VivaXmlTestsClass> vivaXmlTests = new List<VivaXmlTestsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного теста
        {
            new VivaXmlTestsClass { Name = "CONTINUITY", Translation ="Тест на обрыв" }, 
            new VivaXmlTestsClass { Name = "ISOLATION",  Translation ="Тест изоляции" },
            new VivaXmlTestsClass { Name = "RESISTOR",   Translation ="Резисторы"     },
            new VivaXmlTestsClass { Name = "CAPACITOR",  Translation ="Конденсаторы"  },
            new VivaXmlTestsClass { Name = "INDUCTANCE", Translation ="Индуктивности" },
            new VivaXmlTestsClass { Name = "DIODE",      Translation ="Диоды"         },
            new VivaXmlTestsClass { Name = "TRANSISTOR", Translation ="Транзисторы"   },
            new VivaXmlTestsClass { Name = "AUTIC",      Translation ="Чип"           }
        };
        public static List<СalculationsClass> Сalculations = new List<СalculationsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного вычисления
        {
            new СalculationsClass { Name = "All",          Translation ="Все тесты"      }, 
            new СalculationsClass { Name = "MaxDeviation", Translation ="MAX отклонение" }
        };
        public static int testCount { get { return ParseXml.vivaXmlTests.Count; } }
        internal static (ParsedXml, string) Parse(XDocument doc)
        {
            string errorList = null;
            ParsedXml returnData = new ParsedXml();

            returnData.Info = new InfoClass();
            if (GetAttribute(GetElement(doc, "Info"), "Version") != null)
            returnData.Info.Version = GetAttribute(GetElement(doc, "Info"), "Version")?.Value;

            returnData.PrgC = new PrgCClass();

            if (GetAttribute(GetElement(doc, "PrgC"), "AD") != null)
            returnData.PrgC.AD =              GetAttribute(GetElement(doc, "PrgC"), "AD")?.Value;
            if (GetAttribute(GetElement(doc, "PrgC"), "SD") != null)
            returnData.PrgC.SD =              GetAttribute(GetElement(doc, "PrgC"), "SD")?.Value;
            if (GetAttribute(GetElement(doc, "PrgC"), "PN") != null)
            returnData.PrgC.PN =              GetAttribute(GetElement(doc, "PrgC"), "PN")?.Value;
            if (GetAttribute(GetElement(doc, "PrgC"), "TU") != null)
            returnData.PrgC.TU = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "TU")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "TN") != null)
            returnData.PrgC.TN = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "TN")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "TD") != null)
            returnData.PrgC.TD = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "TD")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "TT") != null)
            returnData.PrgC.TT = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "TT")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "BU") != null)
            returnData.PrgC.BU = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "BU")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "BN") != null)
            returnData.PrgC.BN = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "BN")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "BD") != null)
            returnData.PrgC.BD = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "BD")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "BT") != null)
            returnData.PrgC.BT = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "BT")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "TH") != null)
            returnData.PrgC.TH = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "TH")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "SX") != null)
            returnData.PrgC.SX = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "SX")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "SZ") != null)
            returnData.PrgC.SZ = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "SZ")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "TO") != null)
            returnData.PrgC.TO =              GetAttribute(GetElement(doc, "PrgC"), "TO")?.Value;
            if (GetAttribute(GetElement(doc, "PrgC"), "TY") != null)
            returnData.PrgC.TY =              GetAttribute(GetElement(doc, "PrgC"), "TY")?.Value;
            if (GetAttribute(GetElement(doc, "PrgC"), "MR") != null)
            returnData.PrgC.MR =              GetAttribute(GetElement(doc, "PrgC"), "MR")?.Value;
            if (GetAttribute(GetElement(doc, "PrgC"), "TM") != null)
            returnData.PrgC.TM = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "TM")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "BM") != null)
            returnData.PrgC.BM = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "BM")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "RT") != null)
            returnData.PrgC.RT =              GetAttribute(GetElement(doc, "PrgC"), "RT")?.Value;
            if (GetAttribute(GetElement(doc, "PrgC"), "NR") != null)
            returnData.PrgC.NR = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "NR")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "MO") != null)
            returnData.PrgC.MO = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "MO")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "PrgC"), "RA") != null)
            returnData.PrgC.RA =              GetAttribute(GetElement(doc, "PrgC"), "RA")?.Value;
            if (GetAttribute(GetElement(doc, "PrgC"), "PV") != null)
            returnData.PrgC.PV = Double.Parse(GetAttribute(GetElement(doc, "PrgC"), "PV")?.Value, new CultureInfo("en-US"));

            returnData.ST = new STClass();

            if (GetAttribute(GetElement(doc, "ST"), "TN") != null)
            returnData.ST.TN =              GetAttribute(GetElement(doc, "ST"), "TN")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "NM") != null)
            returnData.ST.NMP =             GetAttribute(GetElement(doc, "ST"), "NM")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "NM") != null)
            returnData.ST.NM =              GetAttribute(GetElement(doc, "ST"), "NM")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "LT") != null)
            returnData.ST.LT =              GetAttribute(GetElement(doc, "ST"), "LT")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "BC") != null)
            returnData.ST.BC =              GetAttribute(GetElement(doc, "ST"), "BC")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "OP") != null)
            returnData.ST.OP =              GetAttribute(GetElement(doc, "ST"), "OP")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "TS") != null)
            returnData.ST.TS =              GetAttribute(GetElement(doc, "ST"), "TS")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "WS") != null)
            returnData.ST.WS =              GetAttribute(GetElement(doc, "ST"), "WS")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "SD") != null)
            returnData.ST.SD =              GetAttribute(GetElement(doc, "ST"), "SD")?.Value;
            if (GetAttribute(GetElement(doc, "ST"), "ME") != null)
            returnData.ST.ME = Double.Parse(GetAttribute(GetElement(doc, "ST"), "ME")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "ST"), "PA") != null)
            returnData.ST.PA = Double.Parse(GetAttribute(GetElement(doc, "ST"), "PA")?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "ST"), "SI") != null)
            returnData.ST.SI = Double.Parse(GetAttribute(GetElement(doc, "ST"), "SI")?.Value, new CultureInfo("en-US"));
            if (GetElement(doc, "BI") != null)
                returnData.BI = doc.Root.Elements("BI")?.Select(b => new BIClass
                {
                    BCP =             b.Attribute("BCP")?.Value,
                    BC =              b.Attribute("BC" )?.Value,
                    ID = Double.Parse(b.Attribute("ID" )?.Value, new CultureInfo("en-US")),
                    TR = Double.Parse(b.Attribute("TR" )?.Value, new CultureInfo("en-US")),
                    AK = Double.Parse(b.Attribute("AK" )?.Value, new CultureInfo("en-US")),
                    SD =              b.Attribute("SD" )?.Value,
                    TT = Double.Parse(b.Attribute("TT" )?.Value, new CultureInfo("en-US")),
                    NT = Double.Parse(b.Attribute("NT" )?.Value, new CultureInfo("en-US")),
                    NF = Double.Parse(b.Attribute("NF" )?.Value, new CultureInfo("en-US")),
                    Test = b.Elements("TEST").Select(t => new ColumnsClass
                    {
                        F =                t.Attribute("F"  )?.Value,
                        FT =               t.Attribute("FT" )?.Value,
                        C =                t.Attribute("C"  )?.Value,
                        SG1 =              t.Attribute("SG1")?.Value,
                        SG2 =              t.Attribute("SG2")?.Value,
                        PD1 =              t.Attribute("PD1")?.Value,
                        PD2 =              t.Attribute("PD2")?.Value,
                        XY1 =              t.Attribute("XY1")?.Value,
                        XY2 =              t.Attribute("XY2")?.Value,
                        CP1 =              t.Attribute("CP1")?.Value,
                        CP2 =              t.Attribute("CP2")?.Value,
                        SC =               t.Attribute("SC" )?.Value,
                        NM =               t.Attribute("NM" )?.Value,
                        DN =               t.Attribute("DN" )?.Value,
                        PT =  Double.Parse(t.Attribute("PT" )?.Value, new CultureInfo("en-US")),
                        NT =  Double.Parse(t.Attribute("NT" )?.Value, new CultureInfo("en-US")),
                        IDC = Double.Parse(t.Attribute("IDC")?.Value, new CultureInfo("en-US")),
                        MK =               t.Attribute("MK" )?.Value,
                        IDM = Double.Parse(t.Attribute("IDM")?.Value, new CultureInfo("en-US")),
                        PW =  Double.Parse(t.Attribute("PW" )?.Value, new CultureInfo("en-US")),
                        LB =               t.Attribute("LB" )?.Value,
                        IN =               t.Attribute("IN" )?.Value,
                        IDL = Double.Parse(t.Attribute("IDL")?.Value, new CultureInfo("en-US")),
                        TR =  Double.Parse(t.Attribute("TR" )?.Value, new CultureInfo("en-US")),
                        MU =               t.Attribute("MU" )?.Value,
                        ML =  Double.Parse(t.Attribute("ML" )?.Value, new CultureInfo("en-US")),
                        MM =  Double.Parse(t.Attribute("MM" )?.Value, new CultureInfo("en-US")),
                        MH =  Double.Parse(t.Attribute("MH" )?.Value, new CultureInfo("en-US")),
                        MR =  Double.Parse(t.Attribute("MR" )?.Value, new CultureInfo("en-US")),
                        MP =  Double.Parse(t.Attribute("MP" )?.Value.TrimEnd('%'), new CultureInfo("en-US")),
                        TT =  Double.Parse(t.Attribute("TT" )?.Value, new CultureInfo("en-US")),
                        IS =  Double.Parse(t.Attribute("IS" )?.Value, new CultureInfo("en-US")),
                        DG =  Double.Parse(t.Attribute("DG" )?.Value, new CultureInfo("en-US")),
                        FR =               t.Attribute("FR" )?.Value
                    }).ToList()
                }).ToList();
            else
            {
                return (null, errorList);
            }

            returnData.ET = new ETClass();

            if (GetAttribute(GetElement(doc, "ET"), "NMP") != null)
            returnData.ET.NMP =             GetAttribute(GetElement(doc, "ET"), "NMP")?.Value;
            if (GetAttribute(GetElement(doc, "ET"), "NM" ) != null)
            returnData.ET.NM =              GetAttribute(GetElement(doc, "ET"), "NM" )?.Value;
            if (GetAttribute(GetElement(doc, "ET"), "LT" ) != null)
            returnData.ET.LT =              GetAttribute(GetElement(doc, "ET"), "LT" )?.Value;
            if (GetAttribute(GetElement(doc, "ET"), "BC" ) != null)
            returnData.ET.BC =              GetAttribute(GetElement(doc, "ET"), "BC" )?.Value;
            if (GetAttribute(GetElement(doc, "ET"), "OP" ) != null)
            returnData.ET.OP =              GetAttribute(GetElement(doc, "ET"), "OP" )?.Value;
            if (GetAttribute(GetElement(doc, "ET"), "TR" ) != null)
            returnData.ET.TR = Double.Parse(GetAttribute(GetElement(doc, "ET"), "TR" )?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "ET"), "AK" ) != null)
            returnData.ET.AK = Double.Parse(GetAttribute(GetElement(doc, "ET"), "AK" )?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "ET"), "TT" ) != null)
            returnData.ET.TT =              GetAttribute(GetElement(doc, "ET"), "TT" )?.Value;
            if (GetAttribute(GetElement(doc, "ET"), "NT" ) != null)
            returnData.ET.NT = Double.Parse(GetAttribute(GetElement(doc, "ET"), "NT" )?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "ET"), "NF" ) != null)
            returnData.ET.NF = Double.Parse(GetAttribute(GetElement(doc, "ET"), "NF" )?.Value, new CultureInfo("en-US"));
            if (GetAttribute(GetElement(doc, "ET"), "ED" ) != null)
            returnData.ET.ED =              GetAttribute(GetElement(doc, "ET"), "ED" )?.Value;
            if (GetAttribute(GetElement(doc, "ET"), "DM") != null)
            returnData.ET.DM = Double.Parse(GetAttribute(GetElement(doc, "ET"), "DM" )?.Value, new CultureInfo("en-US"));
            
            return (returnData, errorList); //кортеж
        }
        private static XElement GetElement(XDocument doc, string elementName)
        {
            if (doc != null)
            foreach (var node in doc.DescendantNodes())
            {
                if (node is XElement)
                {
                    XElement element = (XElement)node;
                    if (element.Name.LocalName.Equals(elementName))
                        return element;
                }
            }
            return null;
        }
        private static XAttribute GetAttribute(XElement docElement, string attributeName)
        {
            if (docElement != null)
            foreach (var node in docElement.Attributes())
            {
                if (node is XAttribute)
                {
                    XAttribute pr = (XAttribute)node;
                    if (pr.Name.LocalName.Equals(attributeName))
                        return pr;
                }
            }
            return null;
        }
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(data, "NM", "C", "SG1", "SG2", "PD1", "PD2", "MU", "ML", "MM", "MH", "MR", "MP", "FR"))
            {
                table.Load(reader);
            }
            return table;
        }
    }
}
