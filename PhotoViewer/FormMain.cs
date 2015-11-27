﻿using PhotoViewer.Controller;
using PhotoViewer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoViewer
{
    public partial class FormMain : Form
    {
        private const string mApplicationName = "My Photo Viewer";

        private AppManager mAppManager;

        private Timer timer;
        private ListViewAlbumItemMenuStrip mListViewAlbumItemMenuStrip;
        private PictureBoxMenuStrip mPictureBoxMenuStrip;
        static int idCurrentPicture;
        // Position souris quand click appuye
        private Point mouseDownPosition;

        public FormMain()
        {
            InitializeComponent();
            InitializeMainForm();

            // Chargement des donnees dans le constructeur AppManager
            mAppManager = new AppManager();
            mAppManager.CreateSavingFolderDatas();

            mListViewAlbumItemMenuStrip = new ListViewAlbumItemMenuStrip();
            mPictureBoxMenuStrip = new PictureBoxMenuStrip();
            timer = new Timer();
            InitializeListViewAlbum();
            InitializeFlowPanelPictures();
        }

        /*************************************************************/
        /*********************** INITIALIZATION **********************/
        /*************************************************************/

        private void InitializeMainForm()
        {
            this.Text = mApplicationName;
            this.FormClosing += FormMain_Closing;
            this.Resize += FormMain_Resize;
        }

        private void InitializeListViewAlbum()
        {
            this.listViewAlbum.SelectedIndexChanged += ListViewAlbum_SelectedIndexChanged;
            this.listViewAlbum.MouseClick += ListViewAlbum_MouseClick;
            this.listViewAlbum.AfterLabelEdit += ListViewAlbum_AfterLabelEdit;
            this.listViewAlbum.OnMessageSortAlbum += new ListViewCustom.MessageSortAlbum(this.SortListAlbumByPosition);

            this.listViewAlbum.OnMessageMovePicture += new ListViewCustom.MessageMovePicture(this.MovePictureToAlbum);

            mListViewAlbumItemMenuStrip.OnMessageDelete += new ListViewAlbumItemMenuStrip.MessageDelete(this.DeleteAlbum);
            mListViewAlbumItemMenuStrip.OnMessageRename += new ListViewAlbumItemMenuStrip.MessageRename(this.RenameAlbum);
            mListViewAlbumItemMenuStrip.OnMessageAddPicture += new ListViewAlbumItemMenuStrip.MessageAddPicture(this.AddPictureAlbum);
            mListViewAlbumItemMenuStrip.OnMessageProperty += new ListViewAlbumItemMenuStrip.MessageProperty(this.AlbumProperty);

            this.listViewAlbum.Focus();

            // Ajout des Albums dans la ListView
            if (mAppManager.AlbumManager.ListAlbum.Count > 0)
            {
                foreach (Album album in mAppManager.AlbumManager.ListAlbum)
                {
                    // Creation d'un item "customise" pour la ListView
                    ListViewItem item = mAppManager.AlbumManager.CreateListViewItemAlbum(album);

                    // Ajout de l'item a la ListView
                    this.listViewAlbum.Items.Add(item);
                }

                // Premier album selectionne
                this.listViewAlbum.Items[0].Selected = true;

                this.buttonAddPicture.Visible = true;
                this.buttonWebPage.Visible = true;

                this.labelTitle.Visible = true;
                this.labelSubTitle.Visible = true;
                this.labelDate.Visible = true;

                this.labelTitle.Text = ((Album)this.listViewAlbum.Items[0].Tag).Title;
                this.labelSubTitle.Text = ((Album)this.listViewAlbum.Items[0].Tag).Subtitle;
                this.labelDate.Text = ((Album)this.listViewAlbum.Items[0].Tag).Date;
            }
        }

        private void InitializeFlowPanelPictures()
        {
            this.flowPanelPictures.DragEnter += FlowPanelPictures_DragEnter;
            this.flowPanelPictures.DragDrop += FlowPanelPictures_DragDrop;
            this.flowPanelPictures.MouseClick += FlowPanelPictures_MouseClick;

            mPictureBoxMenuStrip.OnMessageDelete += this.DeletePicture;
            mPictureBoxMenuStrip.OnMessageProperty += this.OpenPictureInfos;
        }

        /*************************************************************/
        /********************* FLOW PANEL PICTURES *******************/
        /*************************************************************/

        private void FlowPanelPictures_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FlowPanelPictures_DragDrop(object sender, DragEventArgs e)
        {
            Album album = GetAlbumSelectedFromListView();

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] filePaths = (String[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string path in filePaths)
                {
                    CreatePicture(album, path);
                }
            }
        }

        private void FlowPanelPictures_MouseClick(object sender, MouseEventArgs e)
        {
            this.flowPanelPictures.Focus();

            if (e.Button == MouseButtons.Right)
            {
                // pour les actions du contexte, on a besoin de l'album en question
                mListViewAlbumItemMenuStrip.Album = this.GetAlbumSelectedFromListView();

                // on affiche le menu strip au niveau de l'item dans la liste
                mListViewAlbumItemMenuStrip.Show(Cursor.Position);
            }
        }

        private void FlowPanelPictures_Update(Album album)
        {
            // Suppression des vues dans le FlowLayoutPanel
            this.flowPanelPictures.Controls.Clear();

            if (album != null)
            {
                if (album.ListPicture.Count > 0)
                {
                    this.buttonWebPage.Visible = true;
                    this.buttonSlideshow.Visible = true;

                    // ajout des nouvelles vues
                    foreach (Picture picture in album.ListPicture)
                    {
                        PictureBoxCustom pictureBox = new PictureBoxCustom(picture.Id.ToString());
                        pictureBox.Picture = picture;

                        pictureBox.DragEnter += new DragEventHandler(PictureBox_DragEnter);
                        pictureBox.DragDrop += new DragEventHandler(PictureBox_DragDrop);
                        pictureBox.MouseClick += new MouseEventHandler(PictureBoxCustom_MouseClick);
                        pictureBox.MouseDown += new MouseEventHandler(PictureBoxCustom_MouseDown);
                        pictureBox.MouseMove += new MouseEventHandler(PictureBoxCustom_MouseMove);
                        pictureBox.MouseDoubleClick += new MouseEventHandler(PictureBoxCustom_MouseDoubleClick);

                        pictureBox.MouseEnter += new EventHandler(PictureBoxCustom_MouseEnter);
                        pictureBox.MouseLeave += new EventHandler(PictureBoxCustom_MouseLeave);
                        pictureBox.MouseHover += new EventHandler(PictureBoxCustom_MouseHover);

                        try
                        {
                            // Ouverture de l'image en memoire
                            Image img = Image.FromFile(pictureBox.Picture.Path + pictureBox.Picture.Type);

                            if (img.Width > img.Height)
                            {
                                pictureBox.Margin = new Padding(20, 45, 20, 20);
                               
                            }
                            else
                            {
                                pictureBox.Margin = new Padding(45, 20, 45, 20);
                               
                            }

                            pictureBox.Size = this.mAppManager.ScaleSize(img, 200, 200);
                            pictureBox.PictureBox.Image = mAppManager.ScaleImage(img, 200, 200);
                          

                            img.Dispose();

                            this.flowPanelPictures.Controls.Add(pictureBox);
                        }
                        catch (FileNotFoundException)
                        {

                        }
                    }
                }
                else
                {
                    this.buttonWebPage.Visible = false;
                    this.buttonSlideshow.Visible = false;
                }
            }

            this.flowPanelPictures.Refresh();
        }

        private void PictureBoxCustom_MouseHover(object sender, EventArgs e)
        {
            base.OnMouseHover(e);

            PictureBoxCustom p = (PictureBoxCustom)sender;

            p.ToolTip.Show("Title : " + p.Picture.Title + "\nCategory : " + p.Picture.Category + "\nDate : " + p.Picture.Date + "\nRating : " + p.Picture.Rating + "/5\n"
                , p, p.Size.Width - 100, p.Size.Height - 50, 2000);
        }

        /*************************************************************/
        /************************* FORM MAIN *************************/
        /*************************************************************/

        /*
         * Appelé lorsque l'utilisateur clique sur la croix de fermeture 
         */
        private void FormMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // SAVE
            mAppManager.SaveInXMLFile();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            string path = null;

            // On va recuperer le chemin de la photo qui a ete selectionne
            // pour un redimensionnement "optimise"
            if (!this.flowPanelPictures.Visible)
            {
                foreach (Control c in this.flowPanelPictures.Controls)
                {
                    PictureBoxCustom p = (PictureBoxCustom)c;

                    if (p.Selected == true)
                    {
                        path = p.Picture.Path + p.Picture.Type;
                    }
                }
            }

            // Si la grille de photo est visible, on les redessine tous
            if (this.flowPanelPictures.Visible)
            {
                this.flowPanelPictures.Refresh();

                foreach (Control c in this.flowPanelPictures.Controls)
                {
                    PictureBoxCustom p = (PictureBoxCustom)c;

                    if (p.Selected == true)
                    {
                        var borderColor = Color.FromKnownColor(KnownColor.Yellow);
                        var borderStyle = ButtonBorderStyle.Solid;
                        var borderWidth = 4;

                        Rectangle rect = p.ClientRectangle;
                        rect.Inflate(borderWidth, borderWidth);

                        rect.Location = new Point(p.Location.X - borderWidth, p.Location.Y - borderWidth);

                        ControlPaint.DrawBorder(p.Parent.CreateGraphics(), rect,
                            borderColor, borderWidth, borderStyle,
                            borderColor, borderWidth, borderStyle,
                            borderColor, borderWidth, borderStyle,
                            borderColor, borderWidth, borderStyle);
                    }
                }
            }
            else if (this.splitContainerFullPicture.Visible)
            {
                if (path != null)
                {
                    Image fullImage = Image.FromFile(path);

                    this.pictureBoxFullSize.Image = mAppManager.ScaleImage(fullImage, this.splitContainerRightMain.Panel2.Size.Width, this.splitContainerRightMain.Panel2.Size.Height);

                    fullImage.Dispose();
                }
            }
        }

        /*************************************************************/
        /********************** LISTVIEW ALBUM ***********************/
        /*************************************************************/

        private void buttonAddAlbum_Click(object sender, EventArgs e)
        {
            Album album = new Album();

            if (mAppManager.AlbumManager.AddAlbum(album))
            {
                if (this.listViewAlbum.Items.Count == 0)
                    this.buttonAddPicture.Visible = true;

                album.Position = mAppManager.AlbumManager.ListAlbum.Count - 1;

                // creation d'un dossier propre a l'album
                mAppManager.AlbumManager.CreateAlbumFolder(mAppManager.AlbumManager.ListAlbum[album.Position].Path);

                // Creation d'un item "customise" pour la ListView
                ListViewItem item = mAppManager.AlbumManager.CreateListViewItemAlbum(album);

                // Ajout de l'item a la ListView
                this.listViewAlbum.Items.Add(item);

                // Dernier album ajoute est selectionne
                this.listViewAlbum.Items[album.Position].Selected = true;

                this.RenameAlbum();
            }
            else
            {
                MessageBox.Show("An error occurs : add album");
            }
        }

        private void ListViewAlbum_SelectedIndexChanged(
        object sender, System.EventArgs e)
        {
            if (this.splitContainerFullPicture.Visible)
            {
                this.buttonGoBackToAlbum.PerformClick();
            }

            // Si un item est selectionne
            if (listViewAlbum.SelectedIndices.Count > 0)
            {
                // On recupere son index dans la ListView
                int index = listViewAlbum.SelectedItems[0].Index;

                // On recupere l'album correspondant
                Album album = GetAlbumSelectedFromListView();

                // On update la vue
                this.labelTitle.Text = GetAlbumSelectedFromListView().Title;
                this.labelSubTitle.Text = GetAlbumSelectedFromListView().Subtitle;
                this.labelDate.Text = GetAlbumSelectedFromListView().Date;
                this.labelTitleSelectedAlbum.Text = album.Title;
                FlowPanelPictures_Update(album);
            }
        }

        private void ListViewAlbum_MouseClick(object sender, MouseEventArgs e)
        {
            // Si clique droit sur la ListView
            if (e.Button == MouseButtons.Right)
            {
                // Si la souris est sur un element selectionne
                if (this.listViewAlbum.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    // On recupere son index dans la ListView
                    int index = listViewAlbum.FocusedItem.Index;

                    // pour les actions du contexte, on a besoin de l'album en question
                    mListViewAlbumItemMenuStrip.Album = this.GetAlbumSelectedFromListView();

                    // on affiche le menu strip au niveau de l'item dans la liste
                    mListViewAlbumItemMenuStrip.Show(Cursor.Position);
                }
            }
        }

        // Lorsqu'un element de la listview est renomme, cette methode est appelee
        private void ListViewAlbum_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
        {
            // Si le renommage est incorrecte on annule, valeur precedente reaffectee
            if (e.Label == null || e.Label.Equals(""))
            {
                e.CancelEdit = true;
            }
            else
            {
                if (e.Label.Length <= 20)
                    // Sinon on update le titre dans les datas
                    this.mAppManager.AlbumManager.RenameAlbumById(GetAlbumSelectedFromListView().Id, e.Label);
                else
                {
                    this.listViewAlbum.Items[e.Item].Text = e.Label.Substring(0, 20);
                    this.mAppManager.AlbumManager.RenameAlbumById(GetAlbumSelectedFromListView().Id, this.listViewAlbum.Items[e.Item].Text);
                }
            }

            // On indique qu'on ne peut pas renommer par simple clique sur l'item
            this.listViewAlbum.LabelEdit = false;

            // Active l'update de la vue
            int index = this.listViewAlbum.SelectedItems[0].Index;
            this.listViewAlbum.Items[index].Selected = false;
            this.listViewAlbum.Items[index].Selected = true;
        }

        private void SortListAlbumByPosition()
        {
            mAppManager.AlbumManager.ListAlbum.Sort(new AlbumPositionComparer());
        }

        /*************************************************************/
        /******************* FONCTION DELEGATE ***********************/
        /*************************************************************/

        private void AlbumProperty(Album album)
        {
            DialogAlbumInfo dialogAlbumInfo = new DialogAlbumInfo(album);

            dialogAlbumInfo.FillDialog();

            dialogAlbumInfo.OnGetAlbumInfo += AlbumInfo_Update;
            dialogAlbumInfo.ShowDialog();
        }

        private void DeleteAlbum(Album album)
        {
            // Supprime dans les List
            this.mAppManager.AlbumManager.DeleteAlbumById(album.Id);

            int indexDelete = this.listViewAlbum.SelectedItems[0].Index;

            // Suppression de la vue
            this.listViewAlbum.SelectedItems[0].Remove();

            if (this.listViewAlbum.Items.Count > 0)
            {
                // Selection de l'album precedent ou suivant
                if (indexDelete > 0)
                    this.listViewAlbum.Items[indexDelete - 1].Selected = true;
                else
                    this.listViewAlbum.Items[indexDelete].Selected = true;
            }
            else
            {
                //this.buttonEditAlbum.Visible = false;
                this.buttonSlideshow.Visible = false;
                this.buttonAddPicture.Visible = false;
                this.buttonWebPage.Visible = false;
                this.labelTitleSelectedAlbum.Text = "-";
                this.labelTitle.Visible = false;
                this.labelSubTitle.Visible = false;
                this.labelDate.Visible = false;
                FlowPanelPictures_Update(null);
            }
        }

        private void RenameAlbum()
        {
            // On active le renommage de l'item
            this.listViewAlbum.LabelEdit = true;

            // On focus sur l'item pour l'edition du titre
            this.listViewAlbum.SelectedItems[0].BeginEdit();
        }

        private void CreatePicture(Album album, string path)
        {
            // creation de la picture box qui contiendra la photo
            PictureBoxCustom pictureBox = new PictureBoxCustom(this.flowPanelPictures.Controls.Count.ToString());
            pictureBox.DragEnter += new DragEventHandler(PictureBox_DragEnter);
            pictureBox.DragDrop += new DragEventHandler(PictureBox_DragDrop);
            pictureBox.MouseClick += new MouseEventHandler(PictureBoxCustom_MouseClick);
            pictureBox.MouseDown += new MouseEventHandler(PictureBoxCustom_MouseDown);
            pictureBox.MouseMove += new MouseEventHandler(PictureBoxCustom_MouseMove);
            pictureBox.MouseDoubleClick += new MouseEventHandler(PictureBoxCustom_MouseDoubleClick);

            pictureBox.MouseEnter += new EventHandler(PictureBoxCustom_MouseEnter);
            pictureBox.MouseLeave += new EventHandler(PictureBoxCustom_MouseLeave);
            pictureBox.MouseHover += new EventHandler(PictureBoxCustom_MouseHover);

            // Allocation d'une nouvelle picture propre a la PictureBoxCustom
            pictureBox.Picture = new Picture(album.Id);

            // Init Picture
            pictureBox.Picture.Position = this.flowPanelPictures.Controls.Count;

            string fileType = path.Substring(path.LastIndexOf('.'));

            if (fileType.Equals(".jpg") || fileType.Equals(".jpeg") || fileType.Equals(".JPG") || fileType.Equals(".JPEG") || fileType.Equals(".png") || fileType.Equals(".PNG"))
            {
                pictureBox.Picture.Type = fileType;

                // Ajout a l'album en memoire
                album.ListPicture.Add(pictureBox.Picture);

                // Ouverture de l'image en memoire
                Image img = new Bitmap(path);

                // Copie dans le dossier de l'appli
                System.IO.File.Copy(path, pictureBox.Picture.Path + fileType);

                if (img.Width > img.Height)
                {
                    pictureBox.Margin = new Padding(20, 45, 20, 20);
                }
                else
                {
                    pictureBox.Margin = new Padding(45, 20, 45, 20);
                }

                pictureBox.Size = this.mAppManager.ScaleSize(img, 200, 200);
                pictureBox.PictureBox.Image = mAppManager.ScaleImage(img, 200, 200);

                img.Dispose();

                this.flowPanelPictures.Controls.Add(pictureBox);
            }
            else
            {
                MessageBox.Show("The file is not supported.\nOnly .jpg, .jpeg or .png");
            }
        }

        private void AddPictureAlbum(Album album)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "PNG files (*.png)|*.png|JPEG file (*jpeg;*.jpg)|*.jpeg;*.jpg|All images|*.png;*.jpeg;*.jpg";
            openFileDialog.FilterIndex = 3;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            CreatePicture(album, openFileDialog.FileName);

                            if (album.ListPicture.Count > 0)
                            {
                                this.buttonWebPage.Visible = true;
                                this.buttonSlideshow.Visible = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : Could not read file from disk.\n" + ex.Message);
                }
            }
        }

        private void MovePictureToAlbum(Album album, PictureBoxCustom pictureBox)
        {
            DialogMoveOrCopy dialogMoveOrCopy = new DialogMoveOrCopy(album, pictureBox);

            dialogMoveOrCopy.OnMessageChoice += DialogMoveOrCopy_OnMessageChoice;

            dialogMoveOrCopy.ShowDialog();
        }

        private void DialogMoveOrCopy_OnMessageChoice(int choice, Album album, PictureBoxCustom pictureBox)
        {
            PictureBoxCustom newPictureBox = null;
            string oldPath = pictureBox.Picture.Path + pictureBox.Picture.Type;

            switch (choice)
            {
                case DialogMoveOrCopy.CHOICE_CANCEL:
                    // Do nothing
                    break;
                case DialogMoveOrCopy.CHOICE_COPY:

                    foreach (Picture p in album.ListPicture)
                    {
                        if (p.Id == pictureBox.Picture.Id)
                        {
                            MessageBox.Show("Picture already exists in the album.");
                            return;
                        }
                    }

                    // creation de la picture box qui contiendra la photo
                    newPictureBox = new PictureBoxCustom(album.ListPicture.Count.ToString());
                    newPictureBox.DragEnter += new DragEventHandler(PictureBox_DragEnter);
                    newPictureBox.DragDrop += new DragEventHandler(PictureBox_DragDrop);
                    newPictureBox.MouseClick += new MouseEventHandler(PictureBoxCustom_MouseClick);
                    newPictureBox.MouseDown += new MouseEventHandler(PictureBoxCustom_MouseDown);
                    newPictureBox.MouseMove += new MouseEventHandler(PictureBoxCustom_MouseMove);
                    newPictureBox.MouseDoubleClick += new MouseEventHandler(PictureBoxCustom_MouseDoubleClick);

                    newPictureBox.MouseEnter += new EventHandler(PictureBoxCustom_MouseEnter);
                    newPictureBox.MouseLeave += new EventHandler(PictureBoxCustom_MouseLeave);
                    pictureBox.MouseHover += new EventHandler(PictureBoxCustom_MouseHover);

                    // Allocation d'une nouvelle picture propre a la PictureBoxCustom
                    newPictureBox.Picture = new Picture(album.Id);

                    newPictureBox.Picture.Id = pictureBox.Picture.Id;

                    newPictureBox.Picture.Path = AppManager.ROOT_FOLDER + AppManager.SLASH_SEPARATOR + newPictureBox.Picture.IdAlbum + AppManager.SLASH_SEPARATOR + newPictureBox.Picture.Id;
                    newPictureBox.Picture.Position = album.ListPicture.Count;

                    newPictureBox.Picture.Type = pictureBox.Picture.Type;
                    newPictureBox.Picture.Rating = pictureBox.Picture.Rating;
                    newPictureBox.Picture.Title = pictureBox.Picture.Title;
                    newPictureBox.Picture.Category = pictureBox.Picture.Category;
                    newPictureBox.Picture.Comment = pictureBox.Picture.Comment;
                    newPictureBox.Picture.Date = pictureBox.Picture.Date;

                    // Ajout a l'album en memoire
                    album.ListPicture.Add(newPictureBox.Picture);

                    // Ouverture de l'image en memoire
                    Image img = new Bitmap(oldPath);

                    // Copie dans le dossier de l'appli
                    System.IO.File.Copy(oldPath, newPictureBox.Picture.Path + newPictureBox.Picture.Type);

                    // liberation de la ressource utilisee par l'image
                    img.Dispose();
                    break;
                case DialogMoveOrCopy.CHOICE_MOVE:

                    foreach (Picture p in album.ListPicture)
                    {
                        if (p.Id == pictureBox.Picture.Id)
                        {
                            MessageBox.Show("Picture already exists in the album.");
                            return;
                        }
                    }

                    // creation de la picture box qui contiendra la photo
                    newPictureBox = new PictureBoxCustom(album.ListPicture.Count.ToString());
                    newPictureBox.DragEnter += new DragEventHandler(PictureBox_DragEnter);
                    newPictureBox.DragDrop += new DragEventHandler(PictureBox_DragDrop);
                    newPictureBox.MouseClick += new MouseEventHandler(PictureBoxCustom_MouseClick);
                    newPictureBox.MouseDown += new MouseEventHandler(PictureBoxCustom_MouseDown);
                    newPictureBox.MouseMove += new MouseEventHandler(PictureBoxCustom_MouseMove);
                    newPictureBox.MouseDoubleClick += new MouseEventHandler(PictureBoxCustom_MouseDoubleClick);

                    newPictureBox.MouseEnter += new EventHandler(PictureBoxCustom_MouseEnter);
                    newPictureBox.MouseLeave += new EventHandler(PictureBoxCustom_MouseLeave);
                    pictureBox.MouseHover += new EventHandler(PictureBoxCustom_MouseHover);

                    // Allocation d'une nouvelle picture propre a la PictureBoxCustom
                    newPictureBox.Picture = new Picture(album.Id);

                    newPictureBox.Picture.Id = pictureBox.Picture.Id;
                    newPictureBox.Picture.Position = album.ListPicture.Count;
                    newPictureBox.Picture.IdAlbum = album.Id;
                    newPictureBox.Picture.Path = AppManager.ROOT_FOLDER + AppManager.SLASH_SEPARATOR + newPictureBox.Picture.IdAlbum + AppManager.SLASH_SEPARATOR + newPictureBox.Picture.Id;

                    newPictureBox.Picture.Type = pictureBox.Picture.Type;
                    newPictureBox.Picture.Rating = pictureBox.Picture.Rating;
                    newPictureBox.Picture.Title = pictureBox.Picture.Title;
                    newPictureBox.Picture.Category = pictureBox.Picture.Category;
                    newPictureBox.Picture.Comment = pictureBox.Picture.Comment;
                    newPictureBox.Picture.Date = pictureBox.Picture.Date;

                    // Ajout a l'album en memoire
                    album.ListPicture.Add(newPictureBox.Picture);

                    // Copie dans le dossier de l'appli
                    System.IO.File.Move(oldPath, newPictureBox.Picture.Path + newPictureBox.Picture.Type);

                    mAppManager.AlbumManager.DeletePictureById(GetAlbumSelectedFromListView(), pictureBox);

                    this.flowPanelPictures.Controls.Remove(pictureBox);
                    break;
            }
        }

        /*************************************************************/
        /*************************** OTHERS **************************/
        /*************************************************************/

        private Album GetAlbumSelectedFromListView()
        {
            ListViewItem item = this.listViewAlbum.SelectedItems[0];

            return ((Album)item.Tag);
        }

        /*************************************************************/
        /*************************** PICTURES ************************/
        /*************************************************************/

        private void buttonAddPicture_Click(object sender, EventArgs e)
        {
            // Si un album est selectionne
            if (this.listViewAlbum.SelectedItems.Count > 0)
                AddPictureAlbum(GetAlbumSelectedFromListView());
        }

        private void PictureBoxCustom_MouseClick(object sender, MouseEventArgs e)
        {
            

            PictureBoxCustom pictureBox = (PictureBoxCustom)sender;

            pictureBox.Parent.Refresh();

            var borderColor = Color.FromKnownColor(KnownColor.Yellow);
            var borderStyle = ButtonBorderStyle.Solid;
            var borderWidth = 4;

            Rectangle rect = pictureBox.ClientRectangle;
            rect.Inflate(borderWidth, borderWidth);

            rect.Location = new Point(pictureBox.Location.X - borderWidth, pictureBox.Location.Y - borderWidth);

            ControlPaint.DrawBorder(pictureBox.Parent.CreateGraphics(), rect,
                borderColor, borderWidth, borderStyle,
                borderColor, borderWidth, borderStyle,
                borderColor, borderWidth, borderStyle,
                borderColor, borderWidth, borderStyle);

            // Si clique droit sur la ListView
            if (e.Button == MouseButtons.Right)
            {
                // on affiche le menu strip au niveau de l'item dans la liste
                mPictureBoxMenuStrip.PictureBox = pictureBox;

                mPictureBoxMenuStrip.Show(Cursor.Position);

                foreach (Control co in this.flowPanelPictures.Controls)
                {
                    PictureBoxCustom p = (PictureBoxCustom)co;
                    p.Selected = false;
                }

                pictureBox.Selected = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                foreach (Control co in this.flowPanelPictures.Controls)
                {
                    PictureBoxCustom p = (PictureBoxCustom)co;
                    p.Selected = false;
                }

                pictureBox.Selected = true;
            }
        }

        // Lorsque la souris est enfonce sur une Picture on recupere la position
        // Par la suite dans MouseMove on verifiera si c'est pour un DragDrop
        // ou bien pour un simple click souris dans MouseClick
        // Empeche le conflit
        private void PictureBoxCustom_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDownPosition = e.Location;
        }

        // Lorsque que l'item selectionne est droppe (souris non enfonce)
        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            Control.ControlCollection controls = this.flowPanelPictures.Controls;

            foreach (Control control in controls)
            {
                PictureBoxCustom pictureBox = (PictureBoxCustom)control;
                pictureBox.Selected = false;

                int pos = this.flowPanelPictures.Controls.IndexOf(pictureBox);

                pictureBox.Picture.Position = pos;
            }

            ((PictureBoxCustom)sender).Selected = true;

            Album album = GetAlbumSelectedFromListView();

            // Tri
            album.ListPicture.Sort(new PicturePositionComparer());
        }

        // Lorsque l'item selectionne entre dans une zone dragable
        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBoxCustom)))
            {
                PictureBoxCustom pictureBox = (PictureBoxCustom)e.Data.GetData(typeof(PictureBoxCustom));
                Control source = pictureBox.Control;

                if (source.IsChildOf(this.flowPanelPictures))
                {
                    e.Effect = DragDropEffects.Move;

                    Point point = new Point(e.X, e.Y);

                    Point mousePosition = this.flowPanelPictures.PointToClient(point);

                    Control destination = this.flowPanelPictures.GetChildAtPoint(mousePosition);

                    int indexDestination = this.flowPanelPictures.Controls.IndexOf(destination);

                    if (source.IsChildOf(this.flowPanelPictures))
                        this.flowPanelPictures.Controls.SetChildIndex(source, indexDestination);

                    this.flowPanelPictures.Refresh();
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OpenPictureInfos(PictureBoxCustom pictureBox)
        {
            DialogPictureInfo dialogPictureInfo = new DialogPictureInfo(pictureBox);

            dialogPictureInfo.FillDialog();

            dialogPictureInfo.OnGetPictureInfo += PictureInfo_Update;
            dialogPictureInfo.ShowDialog();
        }

        private void PictureInfo_Update(PictureBoxCustom pictureBox)
        {
            Album album = GetAlbumSelectedFromListView();

            for(int i = 0; i < album.ListPicture.Count; i++)
            {
                Picture pict = album.ListPicture[i];

                if (pict.Id == pictureBox.Picture.Id)
                {
                    album.ListPicture.RemoveAt(i);
                    album.ListPicture.Insert(i, pict);
                    break;
                }
            }
        }

        private void DeletePicture(PictureBoxCustom pictureBox)
        {
            int positionPictureDelete = pictureBox.Picture.Position;

            Album album = GetAlbumSelectedFromListView();

            // On supprime la photo selectionne
            mAppManager.AlbumManager.DeletePictureById(album, pictureBox);

            // On met a jour les positions des autres photos de l'album
            foreach (Picture p in album.ListPicture)
            {
                if (p.Position > positionPictureDelete)
                {
                    p.Position--;
                }
            }

            // On update la vue
            FlowPanelPictures_Update(album);
        }

        // La souris entre dans une picturebox
        // On affiche un cadre pour mieux visualiser
        private void PictureBoxCustom_MouseEnter(object sender, EventArgs e)
        {
            PictureBoxCustom p = (PictureBoxCustom)sender;

            if (p.Selected == false)
            {
                Control c = (Control)sender;

                var borderColor = Color.FromKnownColor(KnownColor.LightGray);
                var borderStyle = ButtonBorderStyle.Solid;
                var borderWidth = 4;

                Rectangle rect = c.ClientRectangle;
                rect.Inflate(borderWidth, borderWidth);

                rect.Location = new Point(c.Location.X - borderWidth, c.Location.Y - borderWidth);

                ControlPaint.DrawBorder(c.Parent.CreateGraphics(), rect,
                    borderColor, borderWidth, borderStyle,
                    borderColor, borderWidth, borderStyle,
                    borderColor, borderWidth, borderStyle,
                    borderColor, borderWidth, borderStyle);
            }
        }

        // Lorsque la souris sort d'une PictureBox, on redessine le Panel :
        // On efface le cadre de survol mais on laisse celui lorsque la photo est selectionnee
        private void PictureBoxCustom_MouseLeave(object sender, EventArgs e)
        {
            PictureBoxCustom pictureBox = (PictureBoxCustom)sender;

            pictureBox.ToolTip.Hide(pictureBox);

            pictureBox.Parent.Refresh();

            foreach (Control c in this.flowPanelPictures.Controls)
            {
                PictureBoxCustom p = (PictureBoxCustom)c;

                if (p.Selected == true)
                {
                    var borderColor = Color.FromKnownColor(KnownColor.Yellow);
                    var borderStyle = ButtonBorderStyle.Solid;
                    var borderWidth = 4;

                    Rectangle rect = p.ClientRectangle;
                    rect.Inflate(borderWidth, borderWidth);

                    rect.Location = new Point(p.Location.X - borderWidth, p.Location.Y - borderWidth);

                    ControlPaint.DrawBorder(p.Parent.CreateGraphics(), rect,
                        borderColor, borderWidth, borderStyle,
                        borderColor, borderWidth, borderStyle,
                        borderColor, borderWidth, borderStyle,
                        borderColor, borderWidth, borderStyle);
                }
            }
        }

        // Si la souris bouge assez apres un MouseDown, on active le DragDrop
        private void PictureBoxCustom_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (Math.Abs(e.X - mouseDownPosition.X) >= SystemInformation.DoubleClickSize.Width ||
                    Math.Abs(e.Y - mouseDownPosition.Y) >= SystemInformation.DoubleClickSize.Height)
                {
                    Control source = (Control)sender;
                    PictureBoxCustom pictureBox = (PictureBoxCustom)sender;

                    pictureBox.Control = source;

                    source.DoDragDrop(pictureBox, DragDropEffects.All);
                }
            }
        }

        private void PictureBoxCustom_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBoxCustom pictureBox = (PictureBoxCustom)sender;

            Image fullImage = Image.FromFile(pictureBox.Picture.Path + pictureBox.Picture.Type);
            idCurrentPicture = pictureBox.Picture.Position;
            this.pictureBoxFullSize.Image = mAppManager.ScaleImage(fullImage, this.splitContainerRightMain.Panel2.Size.Width, this.splitContainerRightMain.Panel2.Size.Height);

            fullImage.Dispose();

            this.splitContainerFullPicture.Visible = true;
            this.buttonGoBackToAlbum.Visible = true;

            this.flowPanelPictures.Visible = false;
        }

        private void buttonWebPage_Click(object sender, EventArgs e)
        {
            ShowProgressBarWhileWorking();
        }

        private void GenerateWebPage(Album album)
        {
            // Chemin vers les fichiers html de l'appli
            string pathToHtmlFile = AppManager.WEB_FOLDER + AppManager.SLASH_SEPARATOR + AppManager.WEB_FILENAME;

            System.IO.Directory.CreateDirectory(AppManager.WEB_FOLDER);

            // Ecriture dans le fichier pour la grille de photo
            StreamWriter file = new System.IO.StreamWriter(pathToHtmlFile);
            file.WriteLine(HTML_START);

            file.WriteLine(CreateHtmlCodeFromAlbum(album));

            file.WriteLine(HTML_END);
            file.Close();

            // Lance le navigateur par defaut avec le fichier html
            System.Diagnostics.Process.Start(pathToHtmlFile);
        }

        private void ShowProgressBarWhileWorking()
        {
            Action<Album> exec = GenerateWebPage;

            Album album = GetAlbumSelectedFromListView();

            ProgressForm p = new ProgressForm();

            BackgroundWorker b = new BackgroundWorker();

            // La methode generant le code html est appele lorsque RunWorkerAsync l'est
            b.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                exec.Invoke(album);
            };

            // Timer pour afficher un message a l'ecran
            Timer timer = new Timer();
            timer.Interval = 3000;

            timer.Tick += (object sender, EventArgs e) =>
            {
                timer.Dispose();

                if (p != null && p.Visible)
                    p.Close();
            };

            // Affichage progress bar
            p.Show();

            // Lancement tache asynchrone
            b.RunWorkerAsync();

            //Activation du timer
            timer.Enabled = true;
        }

        // Retourne le code html pour la grille de photo
        // Mais creer aussi les codes pour chaque photo quand on clique sur l'une
        private string CreateHtmlCodeFromAlbum(Album album)
        {
            string code = "<table border=\"0\" cellpadding=\"3\" cellspacing=\"10\" width=\"800\">\n";
            int i = 0;

            if (System.IO.Directory.Exists(AppManager.WEB_FOLDER + AppManager.SLASH_SEPARATOR + AppManager.WEB_PICTURES_FOLDER))
            {
                System.IO.Directory.Delete(AppManager.WEB_FOLDER + AppManager.SLASH_SEPARATOR + AppManager.WEB_PICTURES_FOLDER, true);
            }

            System.IO.Directory.CreateDirectory(AppManager.WEB_FOLDER + AppManager.SLASH_SEPARATOR + AppManager.WEB_PICTURES_FOLDER);

            string binPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            foreach (Picture p in album.ListPicture)
            {
                string pathHtmlPicture = AppManager.WEB_FOLDER + AppManager.SLASH_SEPARATOR + AppManager.WEB_PICTURES_FOLDER + AppManager.SLASH_SEPARATOR + p.Id + ".html";

                StreamWriter file = new System.IO.StreamWriter(pathHtmlPicture);
                file.WriteLine(HTML_START);
                file.WriteLine("<img border=\"3\" src=\"" + binPath + "\\" + p.Path + p.Type + "\">\n");

                file.WriteLine(HTML_END);
                file.Close();

                if (i == 0)
                {
                    code += "\n<tr>\n";
                }

                code += "<td width=\"150\" align=\"center\">\n" +
                            "<a href=\"" + binPath + "\\" + AppManager.WEB_FOLDER + AppManager.SLASH_SEPARATOR + AppManager.WEB_PICTURES_FOLDER + AppManager.SLASH_SEPARATOR + p.Id + ".html" + "\">\n" +
                            "<img border=\"0\" src=\"" + binPath + "\\" + p.Path + p.Type + "\" align=\"center\" width=\"150\" height=\"150\">\n" +
                            "</a>\n" +
                        "</td>";

                i++;
                if (i == HTML_NB_PICT_PER_LINE)
                {
                    code += "\n</tr>\n";
                    i = 0;
                }
            }

            code += "</table>\n";

            return code;
        }

        private const int HTML_NB_PICT_PER_LINE = 4;

        // The structure to create the HTML page
        private const string HTML_START = "<!DOCTYPE html>\n" +
                                            "<html>\n" +
                                            "<head>\n" +
                                                "<title>My Photo Viewer</title>\n" +
                                            "</head>\n" +
                                            "<body Bgcolor=\"#333333\">\n" +
                                                "<p align=\"center\">\n" +
                                                    "<div align=\"center\">\n" +
                                                        "<center>\n";
        //The structure to end the HTML page
        private const string HTML_END = "</center>\n" +
                                                    "</div>\n" +
                                            "</body>\n" +
                                            "</html>";

        private void buttonSlideshow_Click(object sender, EventArgs e)
        {
            if (this.timer.Enabled == false)
            {
                idCurrentPicture = 0;
                this.labelDuration.Visible = true;
                this.numericUpDown1.Visible = true;
                this.timer.Tick += this.timer_Tick;
                this.timer.Interval = 1000 * decimal.ToInt32(this.numericUpDown1.Value);
                this.timer.Enabled = true;
                this.changeImageSlideShow();
            }
            else
            {
                this.StopSlideShow();
            }
        }

        private void changeImage()
        {
            Album a = GetAlbumSelectedFromListView();
            List<Picture> l = a.ListPicture;

            if (l.Count > 0)
            {
                idCurrentPicture++;
                if (idCurrentPicture >= l.Count())
                {
                    idCurrentPicture = 0;
                }
                Image fullImage = Image.FromFile(l[idCurrentPicture].Path + l[idCurrentPicture].Type);

                this.pictureBoxFullSize.Image = mAppManager.ScaleImage(fullImage, this.splitContainerRightMain.Panel2.Size.Width, this.splitContainerRightMain.Panel2.Size.Height);

                fullImage.Dispose();

                this.splitContainerFullPicture.Visible = true;
                this.buttonGoBackToAlbum.Visible = true;

                this.flowPanelPictures.Visible = false;
            }
        }

        private void changeImageLeft()
        {
            Album a = GetAlbumSelectedFromListView();
            List<Picture> l = a.ListPicture;

            if (l.Count > 0)
            {
                idCurrentPicture--;
                if (idCurrentPicture < 0)
                {
                    idCurrentPicture = l.Count() - 1;
                }
                Image fullImage = Image.FromFile(l[idCurrentPicture].Path + l[idCurrentPicture].Type);

                this.pictureBoxFullSize.Image = mAppManager.ScaleImage(fullImage, this.splitContainerRightMain.Panel2.Size.Width, this.splitContainerRightMain.Panel2.Size.Height);

                fullImage.Dispose();

                this.splitContainerFullPicture.Visible = true;
                this.buttonGoBackToAlbum.Visible = true;

                this.flowPanelPictures.Visible = false;
            }
        }

        private void StopSlideShow()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.ControlBox = true;
            this.WindowState = FormWindowState.Normal;
            this.timer.Enabled = false;
            this.timer.Tick -= this.timer_Tick;
            this.numericUpDown1.Visible = false;
            this.labelDuration.Visible = false;
            this.panelLeftMain.Visible = true;
            this.flowPanelPictures.Visible = true;
            this.splitContainerRightMain.Panel1Collapsed = false;
        }
        
        static Point location;
        static int width;
        static int height;
        static FormWindowState state;
        static FormBorderStyle style;

        private void changeImageSlideShow()
        {
            Album a = GetAlbumSelectedFromListView();
            List<Picture> l = a.ListPicture;
            if (l.Count > 0)
            {
                idCurrentPicture++;
                if (idCurrentPicture >= l.Count())
                {
                    idCurrentPicture = 0;
                }
                Image fullImage = Image.FromFile(l[idCurrentPicture].Path + l[idCurrentPicture].Type);

                this.pictureBoxFullSize.Image = mAppManager.ScaleImage(fullImage, this.splitContainerRightMain.Panel2.Size.Width, this.splitContainerRightMain.Panel2.Size.Height);

                fullImage.Dispose();
                location = Form.ActiveForm.Location;
                width = Form.ActiveForm.Width;
                height = Form.ActiveForm.Height;
                style = Form.ActiveForm.FormBorderStyle;
                state = Form.ActiveForm.WindowState;
                
                this.splitContainerFullPicture.Visible = true;
                this.buttonGoBackToAlbum.Visible = true;
                this.panelLeftMain.Visible = false;
                this.flowPanelPictures.Visible = false;
                this.splitContainerRightMain.Panel1Collapsed = true;
              
                Form.ActiveForm.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right: 
                    this.changeImage();
                    break;
                case Keys.Left:
                    this.changeImageLeft();
                    break;
                case Keys.Up:
                    break;
                case Keys.Down:
                    break;
                case Keys.Escape:
                    if (this.timer.Enabled == true)
                        this.StopSlideShow();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            changeImageSlideShow();
        }

        private void buttonEditAlbum_Click(object sender, EventArgs e)
        {
            Album album = GetAlbumSelectedFromListView();

            DialogAlbumInfo dialogAlbumInfo = new DialogAlbumInfo(album);

            dialogAlbumInfo.FillDialog();

            dialogAlbumInfo.OnGetAlbumInfo += AlbumInfo_Update;
            dialogAlbumInfo.ShowDialog();
        }

        private void AlbumInfo_Update(string title, string sub, string date, List<string> keywords)
        {
            Album album = GetAlbumSelectedFromListView();

            album.Title = title;
            album.Subtitle = sub;
            album.Date = date;
            album.Keywords = keywords;

            // Update vue
            this.listViewAlbum.SelectedItems[0].Text = album.Title;
            this.labelTitleSelectedAlbum.Text = album.Title;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.timer.Interval = 1000 * decimal.ToInt32(this.numericUpDown1.Value);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            this.changeImage();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            this.changeImageLeft();
        }

        private void buttonGoBackToAlbum_Click(object sender, EventArgs e)
        {
            this.flowPanelPictures.Visible = true;

            this.pictureBoxFullSize.Image.Dispose();
            this.splitContainerFullPicture.Visible = false;

            this.buttonGoBackToAlbum.Visible = false;
        }
    }
}
