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
        private static Logger log = LogManager.GetCurrentClassLogger();
        public DataFiles dataFiles
        {
            set
            {
                if (value == null) log.Warn("Попытка присвоить dataFiles значение null");
                else dataFiles = value;
            }
            get
            {
                return dataFiles;
            }
        }
        public class DataFiles: List<DataFile> { }
        public class DataFile
        {
            public String Name
            {
                set
                {
                    if (value == null) log.Warn("Попытка присвоить Name значение null");
                    else Name = value;
                }
                get
                {
                    return Name;
                }
            }
            public String Path
            {
                set
                {
                    if (value == null) log.Warn("Попытка присвоить Path значение null");
                    else Path = value;
                }
                get
                {
                    return Path;
                }
            }
            public Boolean errorOpen { get; set; } // True if the file has not opened
            public XDocument doc
            {
                set
                {
                    if (value == null) log.Warn("Попытка присвоить doc значение null");
                    else doc = value;
                }
                get
                {
                    return doc;
                }
            }
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
            public BIsections BIsec { get; set; }
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
        public class BIsections: List<BIClass> { }
        public class BIClass
        {
            public Boolean visible { get; set; } // Visible on the main Form & participates in calculations
            public Decimal closeNumber { get; set; } // Sequence number of hiding the tab
            public String BCP { get; set; } //
            public String BC { get; set; } //
            public Double ID { get; set; } //
            public Double TR { get; set; } //
            public Double AK { get; set; } //
            public String SD { get; set; } // Test start date
            public Double TT { get; set; } // 
            public Double NT { get; set; } // Number of tests
            public Double NF { get; set; } // Number of errors
            public Tests tests { get; set; }
        }
        public class Tests: List<Test> { }
        public class Test
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
            public Double TR { get; set; } //
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
            public String uniqueTestName { get { return NM + " | " + F + " | " + PD1 + " | " + PD2; } } // Составной уникальный идентификатор теста
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
    }
}
