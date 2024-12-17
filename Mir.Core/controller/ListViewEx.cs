
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Mir.Core.controller
{
    public class ListViewEx : ListView
    {
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true; // 使用默认的绘制方式

            // 自定义绘制标题时，可以在这里添加代码来改变字体、颜色等
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent); // 根据内容自动调整列宽
                                                                          // 你可以选择其他的自动调整样式，如ColumnHeaderAutoResizeStyle.HeaderSize
        }
    }
}
