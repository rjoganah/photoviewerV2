using PhotoViewer.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PhotoViewer.Model
{
    [Serializable]
    public class Album
    {
        private string mId;

        private string mTitle;
        private string mSubtitle;
        private string mDate;
        private List<string> mKeywords;

        private int mPosition;
        private string mPath;

        private List<Picture> mListPicture;

        public Album()
        {
            Id = Guid.NewGuid().ToString();

            Title = null;
            Subtitle = null;
            Date = DateTime.Today.ToShortDateString();
            Keywords = new List<string>();

            Position = -1;
            Path = AppManager.ROOT_FOLDER + AppManager.SLASH_SEPARATOR + Id;

            ListPicture = new List<Picture>();
        }

        public string Id
        {
            get { return this.mId; }
            set { this.mId = value; }
        }

        public string Title
        {
            get { return this.mTitle; }
            set { this.mTitle = value; }
        }

        public string Subtitle
        {
            get { return this.mSubtitle; }
            set { this.mSubtitle = value; }
        }

        public string Date
        {
            get { return this.mDate; }
            set { this.mDate = value; }
        }

        public List<string> Keywords
        {
            get { return this.mKeywords; }
            set { this.mKeywords = value; }
        }

        public int Position
        {
            get { return this.mPosition; }
            set { this.mPosition = value; }
        }

        public string Path
        {
            get { return this.mPath; }
            set { this.mPath = value; }
        }

        [XmlArrayAttribute("Pictures")]
        public List<Picture> ListPicture
        {
            get { return this.mListPicture; }
            set { this.mListPicture = value; }
        }
    }
}
