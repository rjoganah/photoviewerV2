using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer.Model
{
    public class PicturePositionComparer : IComparer<Picture>
    {
        public int Compare(Picture p1, Picture p2)
        {
            return p1.Position.CompareTo(p2.Position);
        }
    }
}
