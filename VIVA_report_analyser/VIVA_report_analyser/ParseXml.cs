using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VIVA_report_analyser
{
    internal class ParsedXml
    {
        internal InfoClass Info { get; set; }
        internal PrgCClass PrgC { get; set; }
        internal STClass   ST   { get; set; }
        internal BIClass   BI   { get; set; }
        internal ETClass   ET   { get; set; }
    }
    internal class InfoClass
    {
        public String Version { get; set; }
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
        public String TN  { get; set; }
        public String NMP { get; set; }
        public String NM  { get; set; }
        public String LT  { get; set; }
        public String BC  { get; set; }
        public String OP  { get; set; }
        public String TS  { get; set; }
        public String WS  { get; set; }
        public String SD  { get; set; }
        public Double ME { get; set; }
        public Double PA  { get; set; }
        public Double SI  { get; set; }
    }
    internal class BIClass
    {
        public String BCP { get; set; }
        public String BC  { get; set; }
        public Double ID  { get; set; }
        public Double TR  { get; set; }
        public Double AK  { get; set; }
        public String SD  { get; set; }
        public Double TT  { get; set; }
        public Double NT  { get; set; }
        public Double NF  { get; set; }
        public List<ColumnsClass> Test { get; set; }
    }
    internal class ColumnsClass
    {
        // Поля класса
        public String F { get; set; }
        public String FT { get; set; }
        public String C { get; set; }
        public String SG1 { get; set; }
        public String SG2 { get; set; }
        public String PD1 { get; set; }
        public String PD2 { get; set; }
        public String XY1 { get; set; }
        public String XY2 { get; set; }
        public String CP1 { get; set; }
        public String CP2 { get; set; }
        public String SC { get; set; }
        public String NM { get; set; }
        public String DN { get; set; }
        public Double PT { get; set; }
        public Double NT { get; set; }
        public Double IDC { get; set; }
        public String MK { get; set; }
        public Double IDM { get; set; }
        public Double PW { get; set; }
        public String LB { get; set; }
        public String IN { get; set; }
        public Double IDL { get; set; }
        public Double TR { get; set; }
        public String MU { get; set; }
        public Double ML { get; set; }
        public Double MM { get; set; }
        public Double MH { get; set; }
        public Double MR { get; set; }
        public Double MP { get; set; }
        public Double TT { get; set; }
        public Double IS { get; set; }
        public Double DG { get; set; }
        public String uniqueTestName { get { return NM + "|" + F + "|" + PD1 + "|" + PD2; } }
    }
    internal class ETClass
    {
        public String NMP { get; set; }
        public String NM  { get; set; }
        public String LT  { get; set; }
        public String BC  { get; set; }
        public String OP  { get; set; }
        public Double TR  { get; set; }
        public Double AK  { get; set; }
        public String TT  { get; set; }
        public Double NT  { get; set; }
        public Double NF  { get; set; }
        public String ED  { get; set; }
        public Double DM  { get; set; }
    }
    internal class ParseXml
    {
        internal static ParsedXml Parse(XDocument doc)
        {
            ParsedXml returnData = new ParsedXml()
            {
                Info = new InfoClass()
                {
                    Version = doc.Root.Element("Info").Attribute("Version").Value
                },
                PrgC = new PrgCClass()
                {
                    AD = doc.Root.Element("PrgC").Attribute("AD").Value,
                    SD = doc.Root.Element("PrgC").Attribute("SD").Value,
                    PN = doc.Root.Element("PrgC").Attribute("PN").Value,
                    TU = Double.Parse(doc.Root.Element("PrgC").Attribute("TU").Value, new CultureInfo("en-US")),
                    TN = Double.Parse(doc.Root.Element("PrgC").Attribute("TN").Value, new CultureInfo("en-US")),
                    TD = Double.Parse(doc.Root.Element("PrgC").Attribute("TD").Value, new CultureInfo("en-US")),
                    TT = Double.Parse(doc.Root.Element("PrgC").Attribute("TT").Value, new CultureInfo("en-US")),
                    BU = Double.Parse(doc.Root.Element("PrgC").Attribute("BU").Value, new CultureInfo("en-US")),
                    BN = Double.Parse(doc.Root.Element("PrgC").Attribute("BN").Value, new CultureInfo("en-US")),
                    BD = Double.Parse(doc.Root.Element("PrgC").Attribute("BD").Value, new CultureInfo("en-US")),
                    BT = Double.Parse(doc.Root.Element("PrgC").Attribute("BT").Value, new CultureInfo("en-US")),
                    TH = Double.Parse(doc.Root.Element("PrgC").Attribute("TH").Value, new CultureInfo("en-US")),
                    SX = Double.Parse(doc.Root.Element("PrgC").Attribute("SX").Value, new CultureInfo("en-US")),
                    SZ = Double.Parse(doc.Root.Element("PrgC").Attribute("SZ").Value, new CultureInfo("en-US")),
                    TO = doc.Root.Element("PrgC").Attribute("TO").Value,
                    TY = doc.Root.Element("PrgC").Attribute("TY").Value,
                    MR = doc.Root.Element("PrgC").Attribute("MR").Value,
                    TM = Double.Parse(doc.Root.Element("PrgC").Attribute("TM").Value, new CultureInfo("en-US")),
                    BM = Double.Parse(doc.Root.Element("PrgC").Attribute("BM").Value, new CultureInfo("en-US")),
                    RT = doc.Root.Element("PrgC").Attribute("RT").Value,
                    NR = Double.Parse(doc.Root.Element("PrgC").Attribute("NR").Value, new CultureInfo("en-US")),
                    MO = Double.Parse(doc.Root.Element("PrgC").Attribute("MO").Value, new CultureInfo("en-US")),
                    RA = doc.Root.Element("PrgC").Attribute("RA").Value,
                    PV = Double.Parse(doc.Root.Element("PrgC").Attribute("PV").Value, new CultureInfo("en-US"))
                },
                ST = new STClass()
                {
                    TN = doc.Root.Element("ST").Attribute("TN").Value,
                    NMP = doc.Root.Element("ST").Attribute("NM").Value,
                    NM = doc.Root.Element("ST").Attribute("NM").Value,
                    LT = doc.Root.Element("ST").Attribute("LT").Value,
                    BC = doc.Root.Element("ST").Attribute("BC").Value,
                    OP = doc.Root.Element("ST").Attribute("OP").Value,
                    TS = doc.Root.Element("ST").Attribute("TS").Value,
                    WS = doc.Root.Element("ST").Attribute("WS").Value,
                    SD = doc.Root.Element("ST").Attribute("SD").Value,
                    ME = Double.Parse(doc.Root.Element("ST").Attribute("ME").Value, new CultureInfo("en-US")),
                    PA = Double.Parse(doc.Root.Element("ST").Attribute("PA").Value, new CultureInfo("en-US")),
                    SI = Double.Parse(doc.Root.Element("ST").Attribute("SI").Value, new CultureInfo("en-US"))
                },
                BI = new BIClass()
                {
                    BCP = doc.Root.Element("BI").Attribute("BCP").Value,
                    BC = doc.Root.Element("BI").Attribute("BC").Value,
                    ID = Double.Parse(doc.Root.Element("BI").Attribute("ID").Value, new CultureInfo("en-US")),
                    TR = Double.Parse(doc.Root.Element("BI").Attribute("TR").Value, new CultureInfo("en-US")),
                    AK = Double.Parse(doc.Root.Element("BI").Attribute("AK").Value, new CultureInfo("en-US")),
                    SD = doc.Root.Element("BI").Attribute("SD").Value,
                    TT = Double.Parse(doc.Root.Element("BI").Attribute("TT").Value, new CultureInfo("en-US")),
                    NT = Double.Parse(doc.Root.Element("BI").Attribute("NT").Value, new CultureInfo("en-US")),
                    NF = Double.Parse(doc.Root.Element("BI").Attribute("NF").Value, new CultureInfo("en-US")),
                    Test = doc.Root.Element("BI").Elements().ToList().Select
                    (t => new XmlColumsClass
                        {
                            F = t.Attribute("F").Value,
                            FT = t.Attribute("FT").Value,
                            C = t.Attribute("C").Value,
                            SG1 = t.Attribute("SG1").Value,
                            SG2 = t.Attribute("SG2").Value,
                            PD1 = t.Attribute("PD1").Value,
                            PD2 = t.Attribute("PD2").Value,
                            XY1 = t.Attribute("XY1").Value,
                            XY2 = t.Attribute("XY2").Value,
                            CP1 = t.Attribute("CP1").Value,
                            CP2 = t.Attribute("CP2").Value,
                            SC = t.Attribute("SC").Value,
                            NM = t.Attribute("NM").Value,
                            DN = t.Attribute("DN").Value,
                            PT = Double.Parse(t.Attribute("PT").Value, new CultureInfo("en-US")),
                            NT = Double.Parse(t.Attribute("NT").Value, new CultureInfo("en-US")),
                            IDC = Double.Parse(t.Attribute("IDC").Value, new CultureInfo("en-US")),
                            MK = t.Attribute("MK").Value,
                            IDM = Double.Parse(t.Attribute("IDM").Value, new CultureInfo("en-US")),
                            PW = Double.Parse(t.Attribute("PW").Value, new CultureInfo("en-US")),
                            LB = t.Attribute("LB").Value,
                            IN = t.Attribute("IN").Value,
                            IDL = Double.Parse(t.Attribute("IDL").Value, new CultureInfo("en-US")),
                            TR = Double.Parse(t.Attribute("TR").Value, new CultureInfo("en-US")),
                            MU = t.Attribute("MU").Value,
                            ML = Double.Parse(t.Attribute("ML").Value, new CultureInfo("en-US")),
                            MM = Double.Parse(t.Attribute("MM").Value, new CultureInfo("en-US")),
                            MH = Double.Parse(t.Attribute("MH").Value, new CultureInfo("en-US")),
                            MR = Double.Parse(t.Attribute("MR").Value, new CultureInfo("en-US")),
                            MP = Double.Parse(t.Attribute("MP").Value.TrimEnd('%'), new CultureInfo("en-US")),
                            TT = Double.Parse(t.Attribute("TT").Value, new CultureInfo("en-US")),
                            IS = Double.Parse(t.Attribute("IS").Value, new CultureInfo("en-US")),
                            DG = Double.Parse(t.Attribute("DG").Value, new CultureInfo("en-US"))
                        }
                    )
                },
                ET = new ETClass()
                {
                    NMP = doc.Root.Element("ET").Attribute("NMP").Value,
                    NM  = doc.Root.Element("ET").Attribute("NM").Value,
                    LT  = doc.Root.Element("ET").Attribute("LT").Value,
                    BC  = doc.Root.Element("ET").Attribute("BC").Value,
                    OP  = doc.Root.Element("ET").Attribute("OP").Value,
                    TR  = Double.Parse(doc.Root.Element("ET").Attribute("TR").Value, new CultureInfo("en-US")),
                    AK  = Double.Parse(doc.Root.Element("ET").Attribute("AK").Value, new CultureInfo("en-US")),
                    TT  = doc.Root.Element("ET").Attribute("TT").Value,
                    NT  = Double.Parse(doc.Root.Element("ET").Attribute("NT").Value, new CultureInfo("en-US")),
                    NF  = Double.Parse(doc.Root.Element("ET").Attribute("NF").Value, new CultureInfo("en-US")),
                    ED  = doc.Root.Element("ET").Attribute("ED").Value,
                    DM  = Double.Parse(doc.Root.Element("ET").Attribute("DM").Value, new CultureInfo("en-US")),
                }
                
            };

            return returnData;
        }
    }
}
