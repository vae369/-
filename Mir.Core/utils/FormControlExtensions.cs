using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir.Core.utils
{
    public static class FormControlExtensions
    {
        /// <summary>
        /// Invokes actions in UI thread.
        /// </summary>
        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(action));
            }
            else
            {
                action();
            }
        }
    }
}
