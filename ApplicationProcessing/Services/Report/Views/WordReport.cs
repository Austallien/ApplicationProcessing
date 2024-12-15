using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wordroller;

namespace ApplicationProcessing.Services.Report.Views
{
    internal class WordReport : IReport
    {
        /// <summary>
        /// Application title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// When application has been created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// When application has been created
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Application content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Document save name
        /// </summary>
        public string FileName { get; set; }

        private static readonly Uri Template = new Uri("Resources/Documents/Templates/ApplicationReport.docx", UriKind.Relative);

        public bool Create()
        {
            try
            {
                var readStream = new FileStream(Template.OriginalString, FileMode.Open);

                using (var document = new WordDocument(readStream))
                {
                    Dictionary<string, string> items = typeof(WordReport).GetProperties()
                                                                         .Where(item => !item.Name.Equals(nameof(FileName)))
                                                                         .ToDictionary(item => $"<{item.Name}>", item => item.GetValue(this).ToString());

                    foreach (var item in items)
                    {
                        var results = document.Body.FindText(item.Key, StringComparison.Ordinal);
                        foreach (var result in results)
                            result.ReplaceWithTextRun(item.Value, true);
                    }

                    string savePath = $"{DateTime.UtcNow.Ticks}.docx";

                    using (var saveStream = new FileStream(FileName ?? savePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                        document.Save(saveStream);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}