using FastMember;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
        private static Logger log = LogManager.GetCurrentClassLogger();
        private String _BCP;
        public String BCP //
        {
            get
            {
                return _BCP;
            }
            set
            {
                if (value == null) log.Warn("Попытка присвоить BCP значение null");
                else _BCP = value;
            }
        }
        private String _BC;
        public String BC
        {
            set
            {
                if (value == null) log.Warn("Попытка присвоить BC значение null");
                else _BC = value;
            }
            get
            {
                return _BC;
            }
        }
        private Double _ID;
        public Double ID //
        {
            set
            {
                if (value == 0) log.Warn("Попытка присвоить ID значение 0");
                else _ID = value;
            }
            get
            {
                return _ID;
            }
        }
        private Double _TR;
        public Double TR //
        {
            set
            {
                if (value == 0) log.Warn("Попытка присвоить TR значение 0");
                else _TR = value;
            }
            get
            {
                return _TR;
            }
        }
        private Double _AK;
        public Double AK //
        {
            set
            {
                if (value == 0) log.Warn("Попытка присвоить AK значение 0");
                else _AK = value;
            }
            get
            {
                return _AK;
            }
        }
        private String _SD;
        public String SD // Test start date
        {
            set
            {
                if (value == null) log.Warn("Попытка присвоить SD значение null");
                else _SD = value;
            }
            get
            {
                return _SD;
            }
        }
        private Double _TT;
        public Double TT // 
        {
            set
            {
                if (value == 0) log.Warn("Попытка присвоить TT значение 0");
                else _TT = value;
            }
            get
            {
                return _TT;
            }
        }
        private Double _NT;
        public Double NT // Number of tests
        {
            set
            {
                if (value == 0) log.Warn("Попытка присвоить NT значение 0");
                else _NT = value;
            }
            get
            {
                return _NT;
            }
        }
        private Double _NF;
        public Double NF // Number of errors
        {
            set
            {
                if (value == 0) log.Warn("Попытка присвоить NF значение 0");
                else _NF = value;
            }
            get
            {
                return _NF;
            }
        }
        public List<ColumnsClass> Test { get; set; }
    }
    public class ColumnsClass
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        // Поля класса
        private String _F  ;
        public String F   { set { if (value == null) log.Warn("Попытка присвоить F значение null"); else _F   = value; } get { return _F  ; } } //
        private String _FT ;
        public String FT  { set { if (value == null) log.Warn("Попытка присвоить FT значение null"); else _FT  = value; } get { return _FT ; } } //
        private String _C  ;
        public String C   { set { if (value == null) log.Warn("Попытка присвоить C значение null"); else _C   = value; } get { return _C  ; } } //
        private String _SG1;
        public String SG1 { set { if (value == null) log.Warn("Попытка присвоить SG1 значение null"); else _SG1 = value; } get { return _SG1; } } // Net name 1
        private String _SG2;
        public String SG2 { set { if (value == null) log.Warn("Попытка присвоить SG2 значение null"); else _SG2 = value; } get { return _SG2; } } // Net name 2
        private String _PD1;
        public String PD1 { set { if (value == null) log.Warn("Попытка присвоить PD1 значение null"); else _PD1 = value; } get { return _PD1; } } // Pad name 1
        private String _PD2;
        public String PD2 { set { if (value == null) log.Warn("Попытка присвоить PD2 значение null"); else _PD2 = value; } get { return _PD2; } } // Pad name 2
        private String _XY1;
        public String XY1 { set { if (value == null) log.Warn("Попытка присвоить XY1 значение null"); else _XY1 = value; } get { return _XY1; } } // Probe coordinates 1
        private String _XY2;
        public String XY2 { set { if (value == null) log.Warn("Попытка присвоить XY2 значение null"); else _XY2 = value; } get { return _XY2; } } // Probe coordinates 1
        private String _CP1;
        public String CP1 { set { if (value == null) log.Warn("Попытка присвоить CP1 значение null"); else _CP1 = value; } get { return _CP1; } } // 
        private String _CP2;
        public String CP2 { set { if (value == null) log.Warn("Попытка присвоить CP2 значение null"); else _CP2 = value; } get { return _CP2; } } // 
        private String _SC ;
        public String SC  { set { if (value == null) log.Warn("Попытка присвоить SC значение null"); else _SC  = value; } get { return _SC ; } } // 
        private String _NM ;
        public String NM  { set { if (value == null) log.Warn("Попытка присвоить NM значение null"); else _NM  = value; } get { return _NM ; } } // Component name
        private String _DN ;
        public String DN  { set { if (value == null) log.Warn("Попытка присвоить DN значение null"); else _DN  = value; } get { return _DN ; } } // Component description
        private Double _PT ;
        public Double PT  { set { if (value == 0) log.Warn("Попытка присвоить PT значение 0"); else _PT  = value; } get { return _PT ; } } // Positive tolerance ?
        private Double _NT ;
        public Double NT  { set { if (value == 0) log.Warn("Попытка присвоить NT значение 0"); else _NT  = value; } get { return _NT ; } } // Negative tolerance ?
        private Double _IDC;
        public Double IDC { set { if (value == 0) log.Warn("Попытка присвоить IDC значение 0"); else _IDC = value; } get { return _IDC; } } //
        private String _MK ;
        public String MK  { set { if (value == null) log.Warn("Попытка присвоить MK значение null"); else _MK  = value; } get { return _MK ; } } //
        private Double _IDM;
        public Double IDM { set { if (value == 0) log.Warn("Попытка присвоить IDM значение 0"); else _IDM = value; } get { return _IDM; } } //
        private Double _PW ;
        public Double PW  { set { if (value == 0) log.Warn("Попытка присвоить PW значение 0"); else _PW  = value; } get { return _PW ; } } //
        private String _LB ;
        public String LB  { set { if (value == null) log.Warn("Попытка присвоить LB значение null"); else _LB  = value; } get { return _LB ; } } //
        private String _IN ;
        public String IN  { set { if (value == null) log.Warn("Попытка присвоить IN значение null"); else _IN  = value; } get { return _IN ; } } //
        private Double _IDL;
        public Double IDL { set { if (value == 0) log.Warn("Попытка присвоить IDL значение 0"); else _IDL = value; } get { return _IDL; } } //
        private Double _TR ;
        public Double TR  { set { if (value == 0) log.Warn("Попытка присвоить TR значение 0"); else _TR  = value; } get { return _TR ; } } //
        private String _MU ;
        public String MU  { set { if (value == null) log.Warn("Попытка присвоить MU значение null"); else _MU  = value; } get { return _MU ; } } // Units of measurement
        private Double _ML ;
        public Double ML  { set { if (value == 0) log.Warn("Попытка присвоить ML значение 0"); else _ML  = value; } get { return _ML ; } } // Minimum value
        private Double _MM ;
        public Double MM  { set { if (value == 0) log.Warn("Попытка присвоить MM значение 0"); else _MM  = value; } get { return _MM ; } } // Set value
        private Double _MH ;
        public Double MH  { set { if (value == 0) log.Warn("Попытка присвоить MH значение 0"); else _MH  = value; } get { return _MH ; } } // Maximum value
        private Double _MR ;
        public Double MR  { set { if (value == 0) log.Warn("Попытка присвоить MR значение 0"); else _MR  = value; } get { return _MR ; } } // Measured value
        private Double _MP ;
        public Double MP  { set { if (value == 0) log.Warn("Попытка присвоить MP значение 0"); else _MP  = value; } get { return _MP ; } } // Measurement deviation %
        private Double _TT ;
        public Double TT  { set { if (value == 0) log.Warn("Попытка присвоить TT значение 0"); else _TT  = value; } get { return _TT ; } } //
        private Double _IS ;
        public Double IS  { set { if (value == 0) log.Warn("Попытка присвоить IS значение 0"); else _IS  = value; } get { return _IS ; } } //
        private Double _DG ;
        public Double DG  { set { if (value == 0) log.Warn("Попытка присвоить DG значение 0"); else _DG  = value; } get { return _DG ; } } //
        private String _FR ;
        public String FR  { set { if (value == null) log.Warn("Попытка присвоить FR значение null"); else _FR  = value; } get { return _FR ; } } // Error description
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
        public string name { get; set; }
        public string translation { get; set; }
    }
    internal class VivaXmlTestsClass : VivaXmlColumnsClass { }
    internal class СalculationsClass : VivaXmlColumnsClass { }
    internal class ParseXml
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public static List<VivaXmlColumnsClass> vivaXmlColumns = new List<VivaXmlColumnsClass>
        {
            new VivaXmlColumnsClass { name = "F"  ,            translation ="Тест"                      }, // 0
            new VivaXmlColumnsClass { name = "FT" ,            translation ="Функция"                   }, // 1
            new VivaXmlColumnsClass { name = "C"  ,            translation ="Каналы"                    }, // 2
            new VivaXmlColumnsClass { name = "SG1",            translation ="Имя цепи 1"                }, // 3
            new VivaXmlColumnsClass { name = "SG2",            translation ="Имя цепи 2"                }, // 4
            new VivaXmlColumnsClass { name = "PD1",            translation ="Точка подключения 1"       }, // 5
            new VivaXmlColumnsClass { name = "PD2",            translation ="Точка подключения 2"       }, // 6
            new VivaXmlColumnsClass { name = "XY1",            translation ="Координаты подключения 1"  }, // 7
            new VivaXmlColumnsClass { name = "XY2",            translation ="Координаты подключения 2"  }, // 8
            new VivaXmlColumnsClass { name = "CP1",            translation ="CP1"                       }, // 9
            new VivaXmlColumnsClass { name = "CP2",            translation ="CP2"                       }, // 10
            new VivaXmlColumnsClass { name = "SC" ,            translation ="SC"                        }, // 11
            new VivaXmlColumnsClass { name = "NM" ,            translation ="Имя компонента"            }, // 12
            new VivaXmlColumnsClass { name = "DN" ,            translation ="DN"                        }, // 13
            new VivaXmlColumnsClass { name = "PT" ,            translation ="PT"                        }, // 14
            new VivaXmlColumnsClass { name = "NT" ,            translation ="NT"                        }, // 15
            new VivaXmlColumnsClass { name = "IDC",            translation ="IDC"                       }, // 16
            new VivaXmlColumnsClass { name = "MK" ,            translation ="MK"                        }, // 17
            new VivaXmlColumnsClass { name = "IDM",            translation ="IDM"                       }, // 18
            new VivaXmlColumnsClass { name = "PW" ,            translation ="PW"                        }, // 19
            new VivaXmlColumnsClass { name = "LB" ,            translation ="LB"                        }, // 20
            new VivaXmlColumnsClass { name = "IN" ,            translation ="IN"                        }, // 21
            new VivaXmlColumnsClass { name = "IDL",            translation ="IDL"                       }, // 22
            new VivaXmlColumnsClass { name = "TR" ,            translation ="TR"                        }, // 23
            new VivaXmlColumnsClass { name = "MU" ,            translation ="Единицы измерения"         }, // 24
            new VivaXmlColumnsClass { name = "ML" ,            translation ="Минимальное"               }, // 25
            new VivaXmlColumnsClass { name = "MM" ,            translation ="Уставка"                   }, // 26
            new VivaXmlColumnsClass { name = "MH" ,            translation ="Максимальное"              }, // 27
            new VivaXmlColumnsClass { name = "MR" ,            translation ="Измеренное"                }, // 28
            new VivaXmlColumnsClass { name = "MP" ,            translation ="Отклонение, %"             }, // 29
            new VivaXmlColumnsClass { name = "TT" ,            translation ="TT"                        }, // 30
            new VivaXmlColumnsClass { name = "IS" ,            translation ="IS"                        }, // 31
            new VivaXmlColumnsClass { name = "DG" ,            translation ="DG"                        }, // 32
            new VivaXmlColumnsClass { name = "FR" ,            translation ="Описание ошибки"           }, // 33
            new VivaXmlColumnsClass { name = "uniqueTestName", translation ="Идентификатор"             }  // 34
        };
        public static List<VivaXmlTestsClass> vivaXmlTests = new List<VivaXmlTestsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного теста
        {
            new VivaXmlTestsClass { name = "CONTINUITY", translation ="Тест на обрыв" }, 
            new VivaXmlTestsClass { name = "ISOLATION",  translation ="Тест изоляции" },
            new VivaXmlTestsClass { name = "RESISTOR",   translation ="Резисторы"     },
            new VivaXmlTestsClass { name = "CAPACITOR",  translation ="Конденсаторы"  },
            new VivaXmlTestsClass { name = "INDUCTANCE", translation ="Индуктивности" },
            new VivaXmlTestsClass { name = "DIODE",      translation ="Диоды"         },
            new VivaXmlTestsClass { name = "TRANSISTOR", translation ="Транзисторы"   },
            new VivaXmlTestsClass { name = "AUTIC",      translation ="Чип"           }
        };
        public static List<СalculationsClass> Сalculations = new List<СalculationsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного вычисления
        {
            new СalculationsClass { name = "All",          translation ="Все тесты"      }, 
            new СalculationsClass { name = "MaxDeviation", translation ="MAX отклонение" }
        };
        public static int testCount { get { return ParseXml.vivaXmlTests.Count; } }
        internal static (ParsedXml, string) Parse(XDocument doc)
        {
            string errorList = null;
            ParsedXml returnData = new ParsedXml();
            /*
            Type type = typeof(ParsedXml);
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                field.SetValue(returnData, field.GetValue(null));
            }
            */

            returnData.Info = new InfoClass();
            returnData.Info.Version = AtrbToStr(doc, "Info", "Version");

            returnData.PrgC = new PrgCClass();
            returnData.PrgC.AD = AtrbToStr(doc, "PrgC", "AD");
            returnData.PrgC.SD = AtrbToStr(doc, "PrgC", "SD");
            returnData.PrgC.PN = AtrbToStr(doc, "PrgC", "PN");
            returnData.PrgC.TU = AtrbToDbl(doc, "PrgC", "TU");
            returnData.PrgC.TN = AtrbToDbl(doc, "PrgC", "TN");
            returnData.PrgC.TD = AtrbToDbl(doc, "PrgC", "TD");
            returnData.PrgC.TT = AtrbToDbl(doc, "PrgC", "TT");
            returnData.PrgC.BU = AtrbToDbl(doc, "PrgC", "BU");
            returnData.PrgC.BN = AtrbToDbl(doc, "PrgC", "BN");
            returnData.PrgC.BD = AtrbToDbl(doc, "PrgC", "BD");
            returnData.PrgC.BT = AtrbToDbl(doc, "PrgC", "BT");
            returnData.PrgC.TH = AtrbToDbl(doc, "PrgC", "TH");
            returnData.PrgC.SX = AtrbToDbl(doc, "PrgC", "SX");
            returnData.PrgC.SZ = AtrbToDbl(doc, "PrgC", "SZ");
            returnData.PrgC.TO = AtrbToStr(doc, "PrgC", "TO");
            returnData.PrgC.TY = AtrbToStr(doc, "PrgC", "TY");
            returnData.PrgC.MR = AtrbToStr(doc, "PrgC", "MR");
            returnData.PrgC.TM = AtrbToDbl(doc, "PrgC", "TM");
            returnData.PrgC.BM = AtrbToDbl(doc, "PrgC", "BM");
            returnData.PrgC.RT = AtrbToStr(doc, "PrgC", "RT");
            returnData.PrgC.NR = AtrbToDbl(doc, "PrgC", "NR");
            returnData.PrgC.MO = AtrbToDbl(doc, "PrgC", "MO");
            returnData.PrgC.RA = AtrbToStr(doc, "PrgC", "RA");
            returnData.PrgC.PV = AtrbToDbl(doc, "PrgC", "PV");

            returnData.ST = new STClass();
            returnData.ST.TN =  AtrbToStr(doc, "ST", "TN");
            returnData.ST.NMP = AtrbToStr(doc, "ST", "NM");
            returnData.ST.NM =  AtrbToStr(doc, "ST", "NM");
            returnData.ST.LT =  AtrbToStr(doc, "ST", "LT");
            returnData.ST.BC =  AtrbToStr(doc, "ST", "BC");
            returnData.ST.OP =  AtrbToStr(doc, "ST", "OP");
            returnData.ST.TS =  AtrbToStr(doc, "ST", "TS");
            returnData.ST.WS =  AtrbToStr(doc, "ST", "WS");
            returnData.ST.SD =  AtrbToStr(doc, "ST", "SD");
            returnData.ST.ME =  AtrbToDbl(doc, "ST", "ME");
            returnData.ST.PA =  AtrbToDbl(doc, "ST", "PA");
            returnData.ST.SI =  AtrbToDbl(doc, "ST", "SI");
            if (GetElement(doc, "BI") == null)
            {
                log.Warn("Отсутствует секция BI");
                return (null, errorList);
            }
            else returnData.BI = doc.Root.Elements("BI")?.Select(b => new BIClass
            {
                BCP = b.Attribute("BCP")?.Value,
                BC = b.Attribute("BC")?.Value,
                ID = Double.Parse(b.Attribute("ID")?.Value, new CultureInfo("en-US")),
                TR = Double.Parse(b.Attribute("TR")?.Value, new CultureInfo("en-US")),
                AK = Double.Parse(b.Attribute("AK")?.Value, new CultureInfo("en-US")),
                SD = b.Attribute("SD")?.Value,
                TT = Double.Parse(b.Attribute("TT")?.Value, new CultureInfo("en-US")),
                NT = Double.Parse(b.Attribute("NT")?.Value, new CultureInfo("en-US")),
                NF = Double.Parse(b.Attribute("NF")?.Value, new CultureInfo("en-US")),
                Test = b.Elements("TEST").Select(t => new ColumnsClass
                {
                    F = t.Attribute("F")?.Value,
                    FT = t.Attribute("FT")?.Value,
                    C = t.Attribute("C")?.Value,
                    SG1 = t.Attribute("SG1")?.Value,
                    SG2 = t.Attribute("SG2")?.Value,
                    PD1 = t.Attribute("PD1")?.Value,
                    PD2 = t.Attribute("PD2")?.Value,
                    XY1 = t.Attribute("XY1")?.Value,
                    XY2 = t.Attribute("XY2")?.Value,
                    CP1 = t.Attribute("CP1")?.Value,
                    CP2 = t.Attribute("CP2")?.Value,
                    SC = t.Attribute("SC")?.Value,
                    NM = t.Attribute("NM")?.Value,
                    DN = t.Attribute("DN")?.Value,
                    PT = Double.Parse(t.Attribute("PT")?.Value, new CultureInfo("en-US")),
                    NT = Double.Parse(t.Attribute("NT")?.Value, new CultureInfo("en-US")),
                    IDC = Double.Parse(t.Attribute("IDC")?.Value, new CultureInfo("en-US")),
                    MK = t.Attribute("MK")?.Value,
                    IDM = Double.Parse(t.Attribute("IDM")?.Value, new CultureInfo("en-US")),
                    PW = Double.Parse(t.Attribute("PW")?.Value, new CultureInfo("en-US")),
                    LB = t.Attribute("LB")?.Value,
                    IN = t.Attribute("IN")?.Value,
                    IDL = Double.Parse(t.Attribute("IDL")?.Value, new CultureInfo("en-US")),
                    TR = Double.Parse(t.Attribute("TR")?.Value, new CultureInfo("en-US")),
                    MU = t.Attribute("MU")?.Value,
                    ML = Double.Parse(t.Attribute("ML")?.Value, new CultureInfo("en-US")),
                    MM = Double.Parse(t.Attribute("MM")?.Value, new CultureInfo("en-US")),
                    MH = Double.Parse(t.Attribute("MH")?.Value, new CultureInfo("en-US")),
                    MR = Double.Parse(t.Attribute("MR")?.Value, new CultureInfo("en-US")),
                    MP = Double.Parse(t.Attribute("MP")?.Value.TrimEnd('%'), new CultureInfo("en-US")),
                    TT = Double.Parse(t.Attribute("TT")?.Value, new CultureInfo("en-US")),
                    IS = Double.Parse(t.Attribute("IS")?.Value, new CultureInfo("en-US")),
                    DG = Double.Parse(t.Attribute("DG")?.Value, new CultureInfo("en-US")),
                    FR = t.Attribute("FR")?.Value
                }).ToList()
            }).ToList();

            returnData.ET = new ETClass();

            returnData.ET.NMP = AtrbToStr(doc, "ET", "NMP");
            returnData.ET.NM =  AtrbToStr(doc, "ET", "NM" );
            returnData.ET.LT =  AtrbToStr(doc, "ET", "LT" );
            returnData.ET.BC =  AtrbToStr(doc, "ET", "BC" );
            returnData.ET.OP =  AtrbToStr(doc, "ET", "OP" );
            returnData.ET.TR =  AtrbToDbl(doc, "ET", "TR" );
            returnData.ET.AK =  AtrbToDbl(doc, "ET", "AK" );
            returnData.ET.TT =  AtrbToStr(doc, "ET", "TT" );
            returnData.ET.NT =  AtrbToDbl(doc, "ET", "NT" );
            returnData.ET.NF =  AtrbToDbl(doc, "ET", "NF" );
            returnData.ET.ED =  AtrbToStr(doc, "ET", "ED" );
            returnData.ET.DM =  AtrbToDbl(doc, "ET", "DM");
            
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
        private static Double AtrbToDbl(XDocument doc, String element, String attribute)
        {
            string temp = null;
            temp = GetAttribute(GetElement(doc, element), attribute)?.Value;
            if (temp == null)
            {
                log.Warn("Отсутствует атрибут " + element + "-" + attribute);
                return 0;
            }
            else
            {
                return Double.Parse(temp.TrimEnd('%'), new CultureInfo("en-US"));
            }
        }
        private static String AtrbToStr(XDocument doc, String element, String attribute)
        {
            string temp = null;
            temp = GetAttribute(GetElement(doc, element), attribute)?.Value;
            if (temp == null)
            {
                log.Warn("Отсутствует атрибут " + element + "-" + attribute);
                return null;
            }
            else
            {
                return temp;
            }
        }
    }
}
