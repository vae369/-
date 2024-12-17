using ICSharpCode.AvalonEdit;
using Mir.Core.file;
using Mir.ORM.SqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;

namespace Mir2.Script
{
    public partial class FrmFloat : UIForm2
    {
        List<string> fileStringList = null;
        public FrmFloat(string path, string text)
        {
            InitializeComponent();

            tbContent.TextEditor.Text = text;
            Text = path;
        }

        private void FrmFloat_Load(object sender, EventArgs e)
        {
            plVersion.Text = "当前版本：" + FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\Mir2.exe").FileVersion;
            tbContent.TextEditor.TextArea.Caret.PositionChanged += CaretOnPositionChanged;

            tbContent.KeyDown += userControl_KeyDown;
            fileStringList = FileHelper.ReadTxtReturnList(Text);

            string str = tbContent.TextEditor.Text;

            string pattern = @"^\[.*\]$";

            foreach (string line in fileStringList)
            {
                if (Regex.IsMatch(line, pattern))
                {
                    lbContent.Items.Add(line);
                }
            }
        }
        private void CaretOnPositionChanged(object sender, EventArgs eventArgs)
        {
            plColRow.Text = $"行 {tbContent.TextEditor.TextArea.Caret.Line} 列 {tbContent.TextEditor.TextArea.Caret.Column}";
        }
        private void userControl_KeyDown(object sender, EventArgs e)
        {
            System.Windows.Input.KeyEventArgs kea = e as System.Windows.Input.KeyEventArgs;
            if (kea.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control && kea.Key == Key.S)
            {
                SaveDocument();
            }
        }

        private void SaveDocument()
        {
            string filePath = Text;

            string ct = tbContent.TextEditor.Text.ToUpper();

            FileHelper.WriteAllText(filePath, ct);
            plMsg.Text = "保存成功！";
        }

        private void lbContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbContent.SelectedItem != null)
            {
                string str = lbContent.SelectedItem.ToString();
                //绑定右侧listbox内容

                int highlightStart = fileStringList.IndexOf(str);
                int highlightLength = str.Length;
                int textIndex = tbContent.TextEditor.Text.IndexOf(str);
                if(textIndex>0)
                {
                    // 设置选择区域
                    tbContent.TextEditor.Select(textIndex, highlightLength);
                    // 滚动到选择区域
                    tbContent.TextEditor.ScrollTo(highlightStart, highlightLength);
                }                
            }
        }
    }
}
