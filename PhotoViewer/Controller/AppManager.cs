using PhotoViewer.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PhotoViewer.Controller
{
    /**
     * Controleur qui gere les albums, ainsi que les photos, etc...
     **/
    public class AppManager
    {
        public static string XML_FOLDER = "XML";
        public static string XML_FILENAME = "database.xml";
        public static string WEB_FOLDER = "WEB";
        public static string WEB_PICTURES_FOLDER = "PICTURES";
        public static string WEB_FILENAME = "webpage.html";

        public static string ROOT_FOLDER = "SavedFiles";
        public static string SLASH_SEPARATOR = "\\";

        private AlbumManager mAlbumManager;

        public AppManager()
        {
            AlbumManager = ReadInXMLFile();
        }

        public AlbumManager AlbumManager
        {
            get { return this.mAlbumManager; }
            set { this.mAlbumManager = value; }
        }

        public void CreateSavingFolderDatas()
        {
            System.IO.Directory.CreateDirectory(ROOT_FOLDER);
        }

        public void SaveInXMLFile()
        {
            System.IO.Directory.CreateDirectory(XML_FOLDER);

            XmlSerializer xs = new XmlSerializer(typeof(AlbumManager));

            using (StreamWriter writer = new StreamWriter(XML_FOLDER + SLASH_SEPARATOR + XML_FILENAME))
            {
                xs.Serialize(writer, AlbumManager);
            }
        }

        public AlbumManager ReadInXMLFile()
        {
            XmlSerializer xs = new XmlSerializer(typeof(AlbumManager));

            if (System.IO.Directory.Exists(XML_FOLDER))
            {
                if (System.IO.File.Exists(XML_FOLDER + SLASH_SEPARATOR + XML_FILENAME))
                {
                    using (StreamReader reader = new StreamReader(XML_FOLDER + SLASH_SEPARATOR + XML_FILENAME))
                    {
                        AlbumManager albumManager = (AlbumManager)xs.Deserialize(reader);

                        return albumManager;
                    }
                }
                else
                    return new AlbumManager();
            }
            else
                return new AlbumManager();
        }

        public Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public Size ScaleSize(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            return new Size(newWidth, newHeight);
        }
    }
}
