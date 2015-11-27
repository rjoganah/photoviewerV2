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
    public partial class DialogPictureInfo : Form
    {
        public delegate void GetPictureInfo(PictureBoxCustom pictureBox);
        public event GetPictureInfo OnGetPictureInfo;

        private PictureBoxCustom pictureBox;

        public DialogPictureInfo()
        {
            InitializeComponent();
        }

        public DialogPictureInfo(PictureBoxCustom p)
        {
            InitializeComponent();

            PictureBox = p;
            this.dateTimePicker.MaxDate = DateTime.Today;
        }

        public PictureBoxCustom PictureBox
        {
            set { this.pictureBox = value; }
        }

        public void FillDialog()
        {
            this.textBoxTitle.Text = pictureBox.Picture.Title;

            this.textBoxComment.Text = pictureBox.Picture.Comment;

            if (pictureBox.Picture.Rating != null)
                this.numericUpDown.Value = int.Parse(pictureBox.Picture.Rating);
            else this.numericUpDown.Value = 0;

            this.textBoxCategory.Text = pictureBox.Picture.Category;

            if (pictureBox.Picture.Date != null)
                this.dateTimePicker.Text = pictureBox.Picture.Date;
            else
                this.dateTimePicker.Text = DateTime.Today.ToShortDateString();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.pictureBox.Picture.Rating = this.numericUpDown.Value.ToString();

            this.pictureBox.Picture.Category = this.textBoxCategory.Text.ToString();

            this.pictureBox.Picture.Comment = this.textBoxComment.Text.ToString();
            this.pictureBox.Picture.Date = this.dateTimePicker.Text.ToString();
            this.pictureBox.Picture.Title = this.textBoxTitle.Text.ToString();

            this.OnGetPictureInfo(this.pictureBox);
        }
    }
}
