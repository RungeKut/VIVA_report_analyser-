using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VIVA_report_analyser
{
    public class DataModel
    {
        public static DataFiles dataFiles;
        private static Logger log = LogManager.GetCurrentClassLogger();
        public class DataFiles : List<DataFile>
        {
            public Boolean busy; // Занята процессом
            public Boolean needParser; // Требуется запуск парсера
            public Boolean needUpdateView; // Требуется обновление данных на форме
        }
        public class DataFile : XmlData
        {
            private String _Name;
            public String Name
            {
                set
                {
                    if (value == null) log.Warn("Попытка присвоить Name значение null");
                    else _Name = value;
                }
                get
                {
                    return _Name;
                }
            }
            private String _Path;
            public String Path
            {
                set
                {
                    if (value == null) log.Warn("Попытка присвоить Path значение null");
                    else _Path = value;
                }
                get
                {
                    return _Path;
                }
            }
            public Boolean errorOpen { get; set; } // True if the file has not opened
            private XDocument _doc;
            public XDocument doc
            {
                set
                {
                    if (value == null) log.Warn("Попытка присвоить doc значение null");
                    else _doc = value;
                }
                get
                {
                    return _doc;
                }
            }
        }
        public class XmlData
        {
            public InfoClass Info
            {
                set
                {
                    if (value == null) log.Warn("Попытка присвоить Info значение null");
                    else Info = value;
                }
                get
                {
                    return Info;
                }
            }
            public FidMrkClass FidMrk { get; set; }
            public PrgCClass PrgC { get; set; }
            public STClass ST { get; set; }
            public BIsectionsClass biSec { get; set; }
            public ETClass ET { get; set; }
        }
        public class InfoClass
        {
            public String Version { get; set; }
        }
        public class FidMrkClass: List<MIClass> { }
        public class MIClass
        {
            public String NM { get; set; }
            public String MZ { get; set; }
            public String K { get; set; }
            public String SD { get; set; }
            public String M { get; set; }
            public Double C { get; set; }
            public Double X { get; set; }
            public Double Y { get; set; }
            public Double XO { get; set; }
            public Double YO { get; set; }
            public String F { get; set; }
        }
        public class PrgCClass
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
        public class STClass
        {
            public String TN { get; set; } //
            public String NMP { get; set; } //
            public String NM { get; set; } // Board Name
            public String LT { get; set; } // Batch code Код партии
            public String BC { get; set; } // Board code
            public String OP { get; set; } // Operator name
            public String TS { get; set; } // Test Metod (Normal test)
            public String WS { get; set; } //
            public String SD { get; set; } // Test start date
            public Double ME { get; set; } //
            public Double PA { get; set; } //
            public Double SI { get; set; } //
        }
        public class BIsectionsClass
        {
            public List<BIParamClass> BI { get; set; }
        }
        public class BIParamClass: BIClass
        {
            public Boolean visible { get; set; } // Visible on the main Form & participates in calculations
            public Decimal closeNumber { get; set; } // Sequence number of hiding the tab
            public List<FilterByTestType> dataFilteredByTests { get; set; } // Filtered test
        }
        public class BIClass
        {
            public String BCP { get; set; } //
            public String BC { get; set; } //
            public Double ID { get; set; } //
            public Double TR { get; set; } //
            public Double AK { get; set; } //
            public String SD { get; set; } // Test start date
            public Double TT { get; set; } // 
            public Double NT { get; set; } // Number of tests
            public Double NF { get; set; } // Number of errors
            public TestsSectionsClass testsSec { get; set; } // All tests
        }
        public class TestsSectionsClass
        {
            public List<TestClass> TEST { get; set; }
        }
        public class TestClass
        {
            public String F { get; set; }   //
            public String FT { get; set; }  //
            public String C { get; set; }   //
            public String SG1 { get; set; } // Net name 1
            public String SG2 { get; set; } // Net name 2
            public String PD1 { get; set; } // Pad name 1
            public String PD2 { get; set; } // Pad name 2
            public String XY1 { get; set; } // Probe coordinates 1
            public String XY2 { get; set; } // Probe coordinates 1
            public String CP1 { get; set; } // 
            public String CP2 { get; set; } // 
            public String SC { get; set; } // 
            public String NM { get; set; } // Component name
            public String DN { get; set; } // Component description
            public Double PT { get; set; } // Positive tolerance ?
            public Double NT { get; set; } // Negative tolerance ?
            public Double IDC { get; set; } //
            public String MK { get; set; } //
            public Double IDM { get; set; } //
            public Double PW { get; set; } //
            public String LB { get; set; } //
            public String IN { get; set; } //
            public Double IDL { get; set; } //
            public Boolean TR { get; set; } // Test with error
            public String MU { get; set; } // Units of measurement
            public Double ML { get; set; } // Minimum value
            public Double MM { get; set; } // Set value
            public Double MH { get; set; } // Maximum value
            public Double MR { get; set; } // Measured value
            public Double MP { get; set; } // Measurement deviation %
            public Double TT { get; set; } //
            public Double IS { get; set; } //
            public Double DG { get; set; } //
            public String FR { get; set; } // Error description
            public String AL { get; set; } //
            public String uniqueTestName { get { return NM + ";" + F + ";" + PD1 + ";" + PD2 + ";" + MR; } private set { } } // Составной уникальный идентификатор теста
        }
        public class ETClass
        {
            public String NMP { get; set; } //
            public String NM { get; set; } //
            public String LT { get; set; } //
            public String BC { get; set; } //
            public String OP { get; set; } //
            public Double TR { get; set; } //
            public Double AK { get; set; } //
            public String TT { get; set; } //
            public Double NT { get; set; } //
            public Double NF { get; set; } //
            public String ED { get; set; } // Test end date
            public Double DM { get; set; } //
        }
        public class FilterByTestType
        {
            private static Logger log = LogManager.GetCurrentClassLogger();
            public string testName { get; set; }
            public List<DataModel.TestClass> Tests { get; set; }
            public static List<FilterByTestType> FilteringTests(TestsSectionsClass data)
            {
                // Выборка результатов конкретного теста
                List<FilterByTestType> filteredTest = new List<FilterByTestType>();
                foreach (VivaXmlTestsClass test in ParseXml.vivaXmlTests)
                {
                    List<DataModel.TestClass> temp = (from DataModel.TestClass n in data.TEST
                                                 where n.F == test.name
                                                 select n).ToList();
                    filteredTest.Add(new FilterByTestType()
                    {
                        testName = test.translation,
                        Tests = temp
                    });
                }
                return filteredTest;
            }
        }
        public static void Init()
        {
            dataFiles = new DataFiles()
            {
                busy = false,
                needParser = false,
                needUpdateView = false
            };
        }
        public static List<string> openFiles
        {
            get
            {
                return (from DataFile file in DataModel.dataFiles
                        where file.errorOpen == false
                        select file.Name).ToList();
            }
            private set { }
        }
        public static List<string> errorOpenFiles
        {
            get
            {
                return (from DataFile file in DataModel.dataFiles
                        where file.errorOpen == true
                        select file.Name).ToList();
            }
            private set { }
        }
        public static Boolean CheckExistenceFile(string name)
        {
            List<DataFile> findFile = new List<DataFile>();
            findFile = (from DataFile n in DataModel.dataFiles
                        where n.Name == name
                        select n).ToList();
            if (findFile.Count == 0)
                return false;
            else
                return true;
        }
    }
}
