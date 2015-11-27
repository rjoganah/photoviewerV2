using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhotoViewer.Model;

namespace PhotoViewer
{
    public partial class PictureBoxMenuStrip : UserControl
    {
        public delegate void MessageDelete(PictureBoxCustom pictureBox);
        public event MessageDelete OnMessageDelete;

        public delegate void MessageProperty(PictureBoxCustom pictureBox);
        public event MessageProperty OnMessageProperty;

        private PictureBoxCustom mPictureBox;

        public PictureBoxMenuStrip()
        {
            InitializeComponent();
        }

        public void Show(Point point)
        {
            this.contextMenuStrip2.Show(point);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnMessageDelete(PictureBox);
        }

        public PictureBoxCustom PictureBox
        {
            get { return this.mPictureBox; }
            set { this.mPictureBox = value; }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnMessageProperty(PictureBox);
        }
    }
}
