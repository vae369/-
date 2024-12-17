using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using Mir.Core.LegendEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mir.Core.LegendEngine
{
    public class CompletionData : ICompletionData
    {        
        public CompletionData(string text, string description)
        {
            Text = text;
            Description = description;
        }

        public ImageSource Image => null;

        public string Text { get; }
        public static string MirType { get; set; }

        public object Content => Text;

        public object Description { get; }

        /// <inheritdoc />
        public double Priority { get; }
        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            string text = "";
            switch (MirType)
            {
                case "GOM": break;
                case "GEE\\GXX\\LF": text = GGLCompletionData.GGLCheckedData[Text].ToUpper(); break;
                case "Blue": text = BlueCompletionData.BlueCheckedData[Text].ToUpper(); break;
                case "Hero": break;
            }
            textArea.Document.Replace(completionSegment, text);
        }
    }
}
