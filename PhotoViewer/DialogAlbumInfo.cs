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
    public partial class DialogAlbumInfo : Form
    {
        public delegate void GetAlbumInfo(string title, string sub, string date, List<string> keywords);
        public event GetAlbumInfo OnGetAlbumInfo;

        private Album album;

        public DialogAlbumInfo()
        {
            InitializeComponent();
        }

        public DialogAlbumInfo(Album album)
        {
            InitializeComponent();

            this.dateTimePicker.MaxDate = DateTime.Today;
            Album = album;
        }

        public Album Album
        {
            set { this.album = value; }
        }

        public void FillDialog()
        {
            this.textBoxTitle.Text = album.Title;

            if (album.Subtitle != null)
                this.textBoxSubTitle.Text = album.Subtitle;

            if (album.Date != null)
                this.dateTimePicker.Text = album.Date;

            foreach (string word in album.Keywords)
            {
                this.dataGridView1.Rows.Add(word);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(this.textBoxTitle.Text.Length > 0) {

                List<string> keywords = new List<string>();

                if (this.dataGridView1.Rows.Count > 0)
                {
                    for(int i = 0; i <dataGridView1.RowCount - 1; i++)
                    {
                        keywords.Add(this.dataGridView1.Rows[i].Cells[0].Value.ToString());
                    }
                }

                this.OnGetAlbumInfo(this.textBoxTitle.Text, this.textBoxSubTitle.Text, this.dateTimePicker.Text.ToString(), keywords);
            }
        }
    }
}
