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
            public event EventHandler<EventArgs> OnBusyChanged;
            private void InvokeOnBusyChanged() { OnBusyChanged?.Invoke(this, EventArgs.Empty); }    
            

            public Boolean busy; // Занята процессом
            public Boolean Busy
            {
                get { return busy; }
                set { if (value != busy) { busy = value; InvokeOnBusyChanged(); } }
            }
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
            public Boolean parsed { get; set; } // True if the file has been parsed
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
            private InfoClass _Info;
            public InfoClass Info
            {
                set
                {
                    if (value == null) log.Warn("Попытка присвоить Info значение null");
                    else _Info = value;
                }
                get
                {
                    return _Info;
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
                    _ID = value;
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
                    _TR = value;
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
                    _AK = value;
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
                    _TT = value;
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
                    _NT = value;
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
                    _NF = value;
                }
                get
                {
                    return _NF;
                }
            }
            public TestsSectionsClass testsSec { get; set; } // All tests
        }
        public class TestsSectionsClass
        {
            public List<TestClass> TEST { get; set; }
        }
        public class TestClass
        {
            private static Logger log = LogManager.GetCurrentClassLogger();
            // Поля класса
            private String _F;
            public String F { set { if (value == null) log.Warn("Попытка присвоить F значение null"); else _F = value; } get { return _F; } } //
            private String _FT;
            public String FT { set { if (value == null) log.Warn("Попытка присвоить FT значение null"); else _FT = value; } get { return _FT; } } //
            private String _C;
            public String C { set { if (value == null) log.Warn("Попытка присвоить C значение null"); else _C = value; } get { return _C; } } //
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
            private String _SC;
            public String SC { set { if (value == null) log.Warn("Попытка присвоить SC значение null"); else _SC = value; } get { return _SC; } } // 
            private String _NM;
            public String NM { set { if (value == null) log.Warn("Попытка присвоить NM значение null"); else _NM = value; } get { return _NM; } } // Component name
            private String _DN;
            public String DN { set { if (value == null) log.Warn("Попытка присвоить DN значение null"); else _DN = value; } get { return _DN; } } // Component description
            private Double _PT;
            public Double PT { set { _PT = value; } get { return _PT; } } // Positive tolerance ?
            private Double _NT;
            public Double NT { set { _NT = value; } get { return _NT; } } // Negative tolerance ?
            private Double _IDC;
            public Double IDC { set { _IDC = value; } get { return _IDC; } } //
            private String _MK;
            public String MK { set { if (value == null) log.Warn("Попытка присвоить MK значение null"); else _MK = value; } get { return _MK; } } //
            private Double _IDM;
            public Double IDM { set { _IDM = value; } get { return _IDM; } } //
            private Double _PW;
            public Double PW { set { _PW = value; } get { return _PW; } } //
            private String _LB;
            public String LB { set { if (value == null) log.Warn("Попытка присвоить LB значение null"); else _LB = value; } get { return _LB; } } //
            private String _IN;
            public String IN { set { if (value == null) log.Warn("Попытка присвоить IN значение null"); else _IN = value; } get { return _IN; } } //
            private Double _IDL;
            public Double IDL { set { _IDL = value; } get { return _IDL; } } //
            private Double _TR;
            public Double TR { set { _TR = value; } get { return _TR; } } // Error test
            private String _MU;
            public String MU { set { if (value == null) log.Warn("Попытка присвоить MU значение null"); else _MU = value; } get { return _MU; } } // Units of measurement
            private Double _ML;
            public Double ML { set { _ML = value; } get { return _ML; } } // Minimum value
            private Double _MM;
            public Double MM { set { _MM = value; } get { return _MM; } } // Set value
            private Double _MH;
            public Double MH { set { _MH = value; } get { return _MH; } } // Maximum value
            private Double _MR;
            public Double MR { set { _MR = value; } get { return _MR; } } // Measured value
            private Double _MP;
            public Double MP { set { _MP = value; } get { return _MP; } } // Measurement deviation %
            private Double _TT;
            public Double TT { set { _TT = value; } get { return _TT; } } //
            private Double _IS;
            public Double IS { set { _IS = value; } get { return _IS; } } //
            private Double _DG;
            public Double DG { set { _DG = value; } get { return _DG; } } //
            private String _FR;
            public String FR { set { if (value == null) log.Warn("Попытка присвоить FR значение null"); else _FR = value; } get { return _FR; } } // Error description
            private String _AL;
            public String AL { set { if (value == null) log.Warn("Попытка присвоить AL значение null"); else _AL = value; } get { return _AL; } } //
            public String uniqueTestName { get { return NM/* + ";" + F + ";" + PD1 + ";" + PD2 + ";" + MR*/; } private set { } } // Составной уникальный идентификатор теста
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
            public List<DataModel.TestClass> errorTests { get; set; }
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
                    List<DataModel.TestClass> ertemp = (from DataModel.TestClass n in temp
                                                        where (n.F == test.name) & (n.TR != 0)
                                                      select n).ToList();
                    filteredTest.Add(new FilterByTestType()
                    {
                        testName = test.translation,
                        Tests = temp,
                        errorTests = ertemp
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
