using FastMember;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VIVA_report_analyser
{
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
            new VivaXmlColumnsClass { name = "MM" ,            translation ="Номинал"                   }, // 26
            new VivaXmlColumnsClass { name = "MH" ,            translation ="Максимальное"              }, // 27
            new VivaXmlColumnsClass { name = "MR" ,            translation ="Измеренное"                }, // 28
            new VivaXmlColumnsClass { name = "MP" ,            translation ="Отклонение, %"             }, // 29
            new VivaXmlColumnsClass { name = "TT" ,            translation ="TT"                        }, // 30
            new VivaXmlColumnsClass { name = "IS" ,            translation ="IS"                        }, // 31
            new VivaXmlColumnsClass { name = "DG" ,            translation ="DG"                        }, // 32
            new VivaXmlColumnsClass { name = "FR" ,            translation ="Описание ошибки"           }, // 33
            new VivaXmlColumnsClass { name = "AL" ,            translation ="AL"                        }, // 34
            new VivaXmlColumnsClass { name = "uniqueTestName", translation ="Идентификатор"             }  // 35
        };
        public static List<VivaXmlTestsClass> vivaXmlTests = new List<VivaXmlTestsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного теста
        {
            new VivaXmlTestsClass { name = "DISCHARGE",  translation ="Разряд"        },
            new VivaXmlTestsClass { name = "FNODE",      translation ="Отклик цепи"   },
            new VivaXmlTestsClass { name = "CONTINUITY", translation ="Тест на обрыв" }, 
            new VivaXmlTestsClass { name = "ISOLATION",  translation ="Тест изоляции" },
            new VivaXmlTestsClass { name = "JUMPER",     translation ="Перемычки"     },
            new VivaXmlTestsClass { name = "RESISTOR",   translation ="Резисторы"     },
            new VivaXmlTestsClass { name = "CAPACITOR",  translation ="Конденсаторы"  },
            new VivaXmlTestsClass { name = "INDUCTANCE", translation ="Индуктивности" },
            new VivaXmlTestsClass { name = "DIODE",      translation ="Диоды"         },
            new VivaXmlTestsClass { name = "TRANSISTOR", translation ="Транзисторы"   },
            new VivaXmlTestsClass { name = "ZENER",      translation ="Стабилитроны"  },
            new VivaXmlTestsClass { name = "AUTIC",      translation ="Чип (диоды)"   },
            new VivaXmlTestsClass { name = "OPENFIX",    translation ="Чип (емкость)" },
            new VivaXmlTestsClass { name = "ALI",        translation ="Высота"        }
        };
        public static List<СalculationsClass> Сalculations = new List<СalculationsClass>
        // Битовая маска указывает какие столбцы интересны для конкретного вычисления
        {
            new СalculationsClass { name = "All",          translation ="Все тесты"      }, 
            new СalculationsClass { name = "MaxDeviation", translation ="MAX отклонение" }
        };
        public static int testCount { get { return ParseXml.vivaXmlTests.Count; } }
        public static DataModel.XmlData Parse(XDocument doc)
        {
            if (doc == null) return null;

            DataModel.XmlData tempData = new DataModel.XmlData();
            
            tempData.Info = new DataModel.InfoClass();
            tempData.Info.Version = AtrbToStr(doc, "Info", "Version");

            tempData.PrgC = new DataModel.PrgCClass();
            tempData.PrgC.AD = AtrbToStr(doc, "PrgC", "AD");
            tempData.PrgC.SD = AtrbToStr(doc, "PrgC", "SD");
            tempData.PrgC.PN = AtrbToStr(doc, "PrgC", "PN");
            tempData.PrgC.TU = AtrbToDbl(doc, "PrgC", "TU");
            tempData.PrgC.TN = AtrbToDbl(doc, "PrgC", "TN");
            tempData.PrgC.TD = AtrbToDbl(doc, "PrgC", "TD");
            tempData.PrgC.TT = AtrbToDbl(doc, "PrgC", "TT");
            tempData.PrgC.BU = AtrbToDbl(doc, "PrgC", "BU");
            tempData.PrgC.BN = AtrbToDbl(doc, "PrgC", "BN");
            tempData.PrgC.BD = AtrbToDbl(doc, "PrgC", "BD");
            tempData.PrgC.BT = AtrbToDbl(doc, "PrgC", "BT");
            tempData.PrgC.TH = AtrbToDbl(doc, "PrgC", "TH");
            tempData.PrgC.SX = AtrbToDbl(doc, "PrgC", "SX");
            tempData.PrgC.SZ = AtrbToDbl(doc, "PrgC", "SZ");
            tempData.PrgC.TO = AtrbToStr(doc, "PrgC", "TO");
            tempData.PrgC.TY = AtrbToStr(doc, "PrgC", "TY");
            tempData.PrgC.MR = AtrbToStr(doc, "PrgC", "MR");
            tempData.PrgC.TM = AtrbToDbl(doc, "PrgC", "TM");
            tempData.PrgC.BM = AtrbToDbl(doc, "PrgC", "BM");
            tempData.PrgC.RT = AtrbToStr(doc, "PrgC", "RT");
            tempData.PrgC.NR = AtrbToDbl(doc, "PrgC", "NR");
            tempData.PrgC.MO = AtrbToDbl(doc, "PrgC", "MO");
            tempData.PrgC.RA = AtrbToStr(doc, "PrgC", "RA");
            tempData.PrgC.PV = AtrbToDbl(doc, "PrgC", "PV");

            tempData.ST = new DataModel.STClass();
            tempData.ST.TN =  AtrbToStr(doc, "ST", "TN");
            tempData.ST.NMP = AtrbToStr(doc, "ST", "NM");
            tempData.ST.NM =  AtrbToStr(doc, "ST", "NM");
            tempData.ST.LT =  AtrbToStr(doc, "ST", "LT");
            tempData.ST.BC =  AtrbToStr(doc, "ST", "BC");
            tempData.ST.OP =  AtrbToStr(doc, "ST", "OP");
            tempData.ST.TS =  AtrbToStr(doc, "ST", "TS");
            tempData.ST.WS =  AtrbToStr(doc, "ST", "WS");
            tempData.ST.SD =  AtrbToStr(doc, "ST", "SD");
            tempData.ST.ME =  AtrbToDbl(doc, "ST", "ME");
            tempData.ST.PA =  AtrbToDbl(doc, "ST", "PA");
            tempData.ST.SI =  AtrbToDbl(doc, "ST", "SI");
            if (GetElement(doc, "BI") == null)
            {
                log.Warn("Отсутствует секция BI");
            }
            else
            {
                tempData.biSec = new DataModel.BIsectionsClass
                {
                    BI = doc.Root.Elements("BI")?.Select(b => new DataModel.BIParamClass
                    {
                        closeNumber = 0,
                        visible = false,
                        BCP = b.Attribute("BCP")?.Value,
                        BC = b.Attribute("BC")?.Value,
                        ID = Double.Parse(b.Attribute("ID")?.Value, new CultureInfo("en-US")),
                        TR = Double.Parse(b.Attribute("TR")?.Value, new CultureInfo("en-US")),
                        AK = Double.Parse(b.Attribute("AK")?.Value, new CultureInfo("en-US")),
                        SD = b.Attribute("SD")?.Value,
                        TT = Double.Parse(b.Attribute("TT")?.Value, new CultureInfo("en-US")),
                        NT = Double.Parse(b.Attribute("NT")?.Value, new CultureInfo("en-US")),
                        NF = Double.Parse(b.Attribute("NF")?.Value, new CultureInfo("en-US")),
                        testsSec = new DataModel.TestsSectionsClass
                        {
                            TEST = b.Elements("TEST").Select(t => new DataModel.TestClass
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
                                MU = ConvertUnit(t.Attribute("MU")?.Value),
                                ML = Double.Parse(t.Attribute("ML")?.Value, new CultureInfo("en-US")),
                                MM = Double.Parse(t.Attribute("MM")?.Value, new CultureInfo("en-US")),
                                MH = Double.Parse(t.Attribute("MH")?.Value, new CultureInfo("en-US")),
                                MR = Double.Parse(t.Attribute("MR")?.Value, new CultureInfo("en-US")),
                                MP = Math.Abs(Double.Parse(t.Attribute("MP")?.Value.TrimEnd('%'), new CultureInfo("en-US"))),
                                TT = Double.Parse(t.Attribute("TT")?.Value, new CultureInfo("en-US")),
                                IS = Double.Parse(t.Attribute("IS")?.Value, new CultureInfo("en-US")),
                                DG = Double.Parse(t.Attribute("DG")?.Value, new CultureInfo("en-US")),
                                FR = t.Attribute("FR")?.Value,
                                AL = t.Attribute("AL")?.Value
                            }).ToList()
                        }
                    }).ToList()
                };
            }
            tempData.ET = new DataModel.ETClass();
            tempData.ET.NMP = AtrbToStr(doc, "ET", "NMP");
            tempData.ET.NM =  AtrbToStr(doc, "ET", "NM" );
            tempData.ET.LT =  AtrbToStr(doc, "ET", "LT" );
            tempData.ET.BC =  AtrbToStr(doc, "ET", "BC" );
            tempData.ET.OP =  AtrbToStr(doc, "ET", "OP" );
            tempData.ET.TR =  AtrbToDbl(doc, "ET", "TR" );
            tempData.ET.AK =  AtrbToDbl(doc, "ET", "AK" );
            tempData.ET.TT =  AtrbToStr(doc, "ET", "TT" );
            tempData.ET.NT =  AtrbToDbl(doc, "ET", "NT" );
            tempData.ET.NF =  AtrbToDbl(doc, "ET", "NF" );
            tempData.ET.ED =  AtrbToStr(doc, "ET", "ED" );
            tempData.ET.DM =  AtrbToDbl(doc, "ET", "DM");
            return tempData;
        }
        private static String ConvertUnit(string data)
        {
            string temp = data;
            if (data == "O") temp = "Ом";
            if (data == "KOhm") temp = "КОм";
            if (data == "uF") temp = "мкФ";
            if (data == "nF") temp = "нФ";
            if (data == "pF") temp = "пФ";
            if (data == "uH") temp = "мкГн";
            if (data == "nH") temp = "нГн";
            if (data == "mH") temp = "мГн";
            if (data == "V") temp = "В";
            if (data == "mV") temp = "мВ";
            if (data == "uV") temp = "мкВ";
            if (data == "A") temp = "А";
            if (data == "mA") temp = "мА";
            if (data == "uA") temp = "мкА";
            return temp;
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
            using (var reader = ObjectReader.Create(data, "TR", "NM", "C", "SG1", "SG2", "PD1", "PD2", "ML", "MM", "MH", "MR", "MU", "MP", "FR", "AL"))
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
