using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoViewer.Controller
{
    static class Utility
    {
        public static bool IsChildOf(this Control c, Control parent)
        {
            return ((c.Parent != null && c.Parent == parent) || (c.Parent != null ? c.Parent.IsChildOf(parent) : false));
        }
    }
}
