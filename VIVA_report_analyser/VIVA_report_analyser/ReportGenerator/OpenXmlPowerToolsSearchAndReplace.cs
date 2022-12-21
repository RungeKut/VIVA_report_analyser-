using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVA_report_analyser.ReportGenerator
{
    internal class OpenXmlPowerToolsSearchAndReplace
    {
        // Dictionary<string, string>() {
        //       {"text-to-replace-1", "replaced-text-1"},
        //       {"text-to-replace-2", "replaced-text-2"},
        // });
        public void begin(string filePath, Dictionary<string, string> data)
        {
            var templateDoc = File.ReadAllBytes(filePath);
            var generatedDoc = SearchAndReplace(templateDoc, data);
            File.WriteAllBytes(filePath, generatedDoc);
        }
        protected byte[] SearchAndReplace(byte[] file, IDictionary<string, string> translations)
        {
            WmlDocument doc = new WmlDocument(file.Length.ToString(), file);

            foreach (var translation in translations)
                doc = doc.SearchAndReplace(translation.Key, translation.Value, true);

            return doc.DocumentByteArray;
        }
    }
}
