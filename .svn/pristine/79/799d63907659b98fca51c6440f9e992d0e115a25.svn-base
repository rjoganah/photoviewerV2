using PhotoViewer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoViewer
{
    public partial class DialogMoveOrCopy : Form
    {
        public const int CHOICE_CANCEL = 0;
        public const int CHOICE_MOVE = 1;
        public const int CHOICE_COPY = 2;

        public delegate void MessageChoice(int choice, Album album, PictureBoxCustom pictureBox);
        public event MessageChoice OnMessageChoice;

        private Album album;
        private PictureBoxCustom pictureBox;

        public DialogMoveOrCopy()
        {
            InitializeComponent();
        }

        public DialogMoveOrCopy(Album album, PictureBoxCustom pictureBox)
        {
            InitializeComponent();
            this.album = album;
            this.pictureBox = pictureBox;
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            this.OnMessageChoice(CHOICE_MOVE, this.album, this.pictureBox);
            this.Close();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            this.OnMessageChoice(CHOICE_COPY, this.album, this.pictureBox);
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.OnMessageChoice(CHOICE_CANCEL, this.album, this.pictureBox);
            this.Close();
        }
    }
}
