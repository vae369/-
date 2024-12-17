using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Search;
using Mir.Core.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ICSharpCode.AvalonEdit.CodeCompletion;
using Mir.Models.DTO;
using Mir.Core.LegendEngine;

namespace Mir2
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private FoldingManager FoldingManager { get; set; }
        public string uc_type { get; set; }
        public UserControl1()
        {
            InitializeComponent();
        }
        public UserControl1(string uc_type)
        {
            InitializeComponent();
            this.uc_type = uc_type;
        }

        public void TextSetting()
        {
            //注册自定义高亮
            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(UserControl1).Assembly.GetManifestResourceStream("Mir2.other.TextXML.xshd"))
            {
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            //要设置一个后缀名字，在这里我设置了blue
            HighlightingManager.Instance.RegisterHighlighting("TextEngine", new string[] { ".txt" }, customHighlighting);

            //使用自定义高亮语法
            TextEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".txt");

             //使用查询
            //ICSharpCode.AvalonEdit.Search.SearchPanel.Install(TextEditor);

            //TextEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;

            SearchPanel.Install(TextEditor);

            CompletionDataList = BlueCompletionData.CompletionDataList;

            if (FoldingManager != null)
                FoldingManager.Uninstall(FoldingManager);
            // 初始化折叠管理器
            FoldingManager = FoldingManager.Install(TextEditor.TextArea);

            // 创建并添加大括号折叠策略
            var braceFoldingStrategy = new TXTBraceFoldingStrategy();
            braceFoldingStrategy.UpdateFoldings(FoldingManager, TextEditor.Document);
        }

        /// <summary>
        /// Blue引擎设置
        /// </summary>
        public void BlueSetting()
        {
            //注册自定义高亮
            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(UserControl1).Assembly.GetManifestResourceStream("Mir2.other.BlueXML.xshd"))
            {
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            //要设置一个后缀名字，在这里我设置了blue
            HighlightingManager.Instance.RegisterHighlighting("BlueEngine", new string[] { ".blue" }, customHighlighting);
            //使用自定义高亮语法
            TextEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".blue");
            //使用查询
            ICSharpCode.AvalonEdit.Search.SearchPanel.Install(TextEditor);
            TextEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            SearchPanel.Install(TextEditor);
            CompletionDataList = BlueCompletionData.CompletionDataList;
            if (FoldingManager != null)
                FoldingManager.Uninstall(FoldingManager);
            // 初始化折叠管理器
            FoldingManager = FoldingManager.Install(TextEditor.TextArea);
            // 创建并添加大括号折叠策略
            var braceFoldingStrategy = new BlueBraceFoldingStrategy();
            braceFoldingStrategy.UpdateFoldings(FoldingManager, TextEditor.Document);
            this.uc_type = "Blue";
        }

        /// <summary>
        /// Blue引擎设置
        /// </summary>
        public void GEE_GXX_LFSetting()
        {
            //注册自定义高亮
            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(UserControl1).Assembly.GetManifestResourceStream("Mir2.other.GEE_GXX_LFML.xshd"))
            {
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            //要设置一个后缀名字，在这里我设置了blue
            HighlightingManager.Instance.RegisterHighlighting("GEE_GXX_LFMLEngine", new string[] { ".ggl" }, customHighlighting);

            //使用自定义高亮语法
            TextEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".ggl");
            //使用查询
            ICSharpCode.AvalonEdit.Search.SearchPanel.Install(TextEditor);

            TextEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;

            SearchPanel.Install(TextEditor);

            CompletionDataList = GGLCompletionData.CompletionDataList;

            if (FoldingManager != null)
                FoldingManager.Uninstall(FoldingManager);
            // 初始化折叠管理器
            FoldingManager = FoldingManager.Install(TextEditor.TextArea);

            // 创建并添加大括号折叠策略
            var braceFoldingStrategy = new GGLBraceFoldingStrategy();
            braceFoldingStrategy.UpdateFoldings(FoldingManager, TextEditor.Document);
            this.uc_type = "GEE\\GXX\\LF";
        }

        private void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            bool flag = CompletionDataList.Where(s => s.Text.Contains(e.Text)).Any();
            if (flag)
            {
                BindCompletionWindow();
            }
        }
        public List<CompletionData> CompletionDataList = new List<CompletionData>();
        private CompletionWindow _completionWindow;
        private void BindCompletionWindow()
        {
            try
            {
                _completionWindow = new CompletionWindow(TextEditor.TextArea);
                _completionWindow.Width = 260;
                _completionWindow.ResizeMode = ResizeMode.NoResize;
                var completionData = _completionWindow.CompletionList.CompletionData;
                switch (uc_type)
                {
                    case "GOM": break;
                    case "GEE\\GXX\\LF":
                        GGLCompletionData.CompletionDataList.ForEach(data => {completionData.Add(data);});
                        break;
                    case "Blue":
                        BlueCompletionData.CompletionDataList.ForEach(data =>{completionData.Add(data);});
                        break;
                    case "Hero": break;
                }
                _completionWindow.Show();
                _completionWindow.Closed += (o, args) => _completionWindow = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
