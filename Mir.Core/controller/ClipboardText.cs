using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir.Core.controller
{
    public class ClipboardText
    {
        /// <summary>
        /// 复制操作
        /// </summary>
        /// <param name="text"></param>
        public static void CopyToClipboard(string text)
        {
            Clipboard.SetText(text);
        }

        /// <summary>
        /// 粘贴操作
        /// </summary>
        /// <returns></returns>
        public static string PasteFromClipboard()
        {
            return Clipboard.GetText();
        }

        /// <summary>
        /// 剪切操作（这里假设有一个文本框textBox来存储数据）
        /// windows文本框
        /// </summary>
        /// <param name="textBox"></param>
        public static void CutToClipboardByWindows(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.SelectedText))
            {
                CopyToClipboard(textBox.SelectedText);
                textBox.SelectedText = "";
            }
        }

        /// <summary>
        /// 剪切操作（这里假设有一个文本框textBox来存储数据）
        /// WPF文本框
        /// </summary>
        /// <param name="textBox"></param>
        public static void CutToClipboardByWPF(TextEditor textBox)
        {
            if (!string.IsNullOrEmpty(textBox.SelectedText))
            {
                CopyToClipboard(textBox.SelectedText);
                textBox.SelectedText = "";
            }
        }
    }
}
