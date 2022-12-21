using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace VIVA_report_analyser.ReportGenerator
{
    internal class OpenXMLTagReplace
    {
        class DocumentTag
        {
            public DocumentTag()
            {
                ReplacementText = "";
            }

            public string Tag { get; set; }
            public string Table { get; set; }
            public string Column { get; set; }
            public string ReplacementText { get; set; }

            public override string ToString()
            {
                return ReplacementText ?? (Tag ?? "");
            }
        }

        private const string TAG_PATTERN = @"\[(.*?)[\.|\:](.*?)\]";
        private const string TAG_START = @"[";
        private const string TAG_END = @"]";

        /// <summary>
        /// Clones a document template into the temp folder and returns the newly created clone temp filename and path.
        /// </summary>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        public string CloneTemplateForEditing(string templatePath)
        {
            var tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()) + Path.GetExtension(templatePath);
            File.Copy(templatePath, tempFile);
            return tempFile;
        }

        /// <summary>
        /// Opens a given filename, replaces tags, and saves. 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Number of tags found</returns>
        public int FindAndReplaceTags(string filename)
        {
            var allTags = new List<DocumentTag>();

            using (WordprocessingDocument doc = WordprocessingDocument.Open(path: filename, isEditable: true))
            {
                var document = doc.MainDocumentPart.Document;

                // text may be split across multiple text runs so keep a collection of text objects
                List<Text> tagParts = new List<Text>();

                foreach (var text in document.Descendants<Text>())
                {
                    // search for any fully formed tags in this text run
                    var fullTags = GetTags(text.Text);

                    // replace values for fully formed tags
                    fullTags.ForEach(t => {
                        t = GetTagReplacementValue(t);
                        text.Text = text.Text.Replace(t.Tag, t.ReplacementText);
                        allTags.Add(t);
                    });

                    // continue working on current partial tag
                    if (tagParts.Count > 0)
                    {
                        // working on a tag
                        var joinText = string.Join("", tagParts.Select(x => x.Text)) + text.Text;

                        // see if tag ends with this block
                        if (joinText.Contains(TAG_END))
                        {
                            var joinTag = GetTags(joinText).FirstOrDefault(); // should be just one tag (or none)
                            if (joinTag == null)
                            {
                                throw new Exception($"Misformed document tag in block '{string.Join("", tagParts.Select(x => x.Text)) + text.Text}' ");
                            }

                            joinTag = GetTagReplacementValue(joinTag);
                            allTags.Add(joinTag);

                            // replace first text run in the tagParts set with the replacement value. 
                            // (This means the formatting used on the first character of the tag will be used)
                            var firstRun = tagParts.First();
                            firstRun.Text = firstRun.Text.Substring(0, firstRun.Text.LastIndexOf(TAG_START));
                            firstRun.Text += joinTag.ReplacementText;

                            // replace trailing text runs with empty strings
                            tagParts.Skip(1).ToList().ForEach(x => x.Text = "");

                            // replace all text up to and including the first index of TAG_END
                            text.Text = text.Text.Substring(text.Text.IndexOf(TAG_END) + 1);

                            // empty the tagParts list so we can start on a new tag
                            tagParts.Clear();
                        }
                        else
                        {
                            // no tag end so keep getting text runs
                            tagParts.Add(text);
                        }
                    }

                    // search for new partial tags
                    if (text.Text.Contains("["))
                    {
                        if (tagParts.Any())
                        {
                            throw new Exception($"Misformed document tag in block '{string.Join("", tagParts.Select(x => x.Text)) + text.Text}' ");
                        }
                        tagParts.Add(text);
                        continue;
                    }

                }

                // save the temp doc before closing
                doc.Save();
            }

            return allTags.Count;
        }

        /// <summary>
        /// Gets a unique set of document tags found in the passed fileText using Regex
        /// </summary>
        /// <param name="fileText"></param>
        /// <returns></returns>
        private List<DocumentTag> GetTags(string fileText)
        {
            List<DocumentTag> tags = new List<DocumentTag>();

            if (string.IsNullOrWhiteSpace(fileText))
            {
                return tags;
            }

            // TODO: custom regex for tag matching 
            // this example looks for tags in the formation "[table.column]" or "[table:column]" and captures the full tag, "table", and "column" into match Groups
            MatchCollection matches = Regex.Matches(fileText, TAG_PATTERN);
            foreach (Match match in matches)
            {
                try
                {

                    if (match.Groups.Count < 3
                        || string.IsNullOrWhiteSpace(match.Groups[0].Value)
                        || string.IsNullOrWhiteSpace(match.Groups[1].Value)
                        || string.IsNullOrWhiteSpace(match.Groups[2].Value))
                    {
                        continue;
                    }

                    tags.Add(new DocumentTag
                    {
                        Tag = match.Groups[0].Value,
                        Table = match.Groups[1].Value,
                        Column = match.Groups[2].Value
                    });
                }
                catch
                {

                }
            }

            return tags;
        }

        /// <summary>
        /// Set the Tag replacement value of the pasted tag
        /// </summary>
        /// <returns></returns>
        private DocumentTag GetTagReplacementValue(DocumentTag tag)
        {
            // TODO: custom routine to update tag Replacement Value

            tag.ReplacementText = "foobar";

            return tag;
        }
    }
}
