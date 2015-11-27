﻿namespace PhotoViewer
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelLeftMain = new System.Windows.Forms.Panel();
            this.splitContainerLeftPanel = new System.Windows.Forms.SplitContainer();
            this.listViewAlbum = new PhotoViewer.ListViewCustom();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddAlbum = new PhotoViewer.ButtonCustom();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelSubTitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.splitContainerRightMain = new System.Windows.Forms.SplitContainer();
            this.buttonSlideshow = new PhotoViewer.ButtonCustom();
            this.buttonWebPage = new PhotoViewer.ButtonCustom();
            this.buttonAddPicture = new PhotoViewer.ButtonCustom();
            this.buttonGoBackToAlbum = new PhotoViewer.ButtonCustom();
            this.labelTitleSelectedAlbum = new System.Windows.Forms.Label();
            this.splitContainerFullPicture = new System.Windows.Forms.SplitContainer();
            this.pictureBoxFullSize = new System.Windows.Forms.PictureBox();
            this.buttonNext = new PhotoViewer.ButtonCustom();
            this.buttonPrevious = new PhotoViewer.ButtonCustom();
            this.labelDuration = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.flowPanelPictures = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelLeftMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftPanel)).BeginInit();
            this.splitContainerLeftPanel.Panel1.SuspendLayout();
            this.splitContainerLeftPanel.Panel2.SuspendLayout();
            this.splitContainerLeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightMain)).BeginInit();
            this.splitContainerRightMain.Panel1.SuspendLayout();
            this.splitContainerRightMain.Panel2.SuspendLayout();
            this.splitContainerRightMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFullPicture)).BeginInit();
            this.splitContainerFullPicture.Panel1.SuspendLayout();
            this.splitContainerFullPicture.Panel2.SuspendLayout();
            this.splitContainerFullPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFullSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.panelLeftMain, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.splitContainerRightMain, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(784, 562);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelLeftMain
            // 
            this.panelLeftMain.Controls.Add(this.splitContainerLeftPanel);
            this.panelLeftMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftMain.Location = new System.Drawing.Point(3, 3);
            this.panelLeftMain.MaximumSize = new System.Drawing.Size(300, 0);
            this.panelLeftMain.Name = "panelLeftMain";
            this.panelLeftMain.Size = new System.Drawing.Size(218, 556);
            this.panelLeftMain.TabIndex = 0;
            // 
            // splitContainerLeftPanel
            // 
            this.splitContainerLeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeftPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerLeftPanel.IsSplitterFixed = true;
            this.splitContainerLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeftPanel.Name = "splitContainerLeftPanel";
            this.splitContainerLeftPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeftPanel.Panel1
            // 
            this.splitContainerLeftPanel.Panel1.Controls.Add(this.listViewAlbum);
            this.splitContainerLeftPanel.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainerLeftPanel.Panel2
            // 
            this.splitContainerLeftPanel.Panel2.Controls.Add(this.buttonAddAlbum);
            this.splitContainerLeftPanel.Panel2.Controls.Add(this.labelDate);
            this.splitContainerLeftPanel.Panel2.Controls.Add(this.labelSubTitle);
            this.splitContainerLeftPanel.Panel2.Controls.Add(this.labelTitle);
            this.splitContainerLeftPanel.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainerLeftPanel.Size = new System.Drawing.Size(218, 556);
            this.splitContainerLeftPanel.SplitterDistance = 400;
            this.splitContainerLeftPanel.SplitterWidth = 6;
            this.splitContainerLeftPanel.TabIndex = 1;
            // 
            // listViewAlbum
            // 
            this.listViewAlbum.AllowDrop = true;
            this.listViewAlbum.BackColor = System.Drawing.SystemColors.Control;
            this.listViewAlbum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewAlbum.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewAlbum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewAlbum.FullRowSelect = true;
            this.listViewAlbum.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAlbum.HideSelection = false;
            this.listViewAlbum.Location = new System.Drawing.Point(0, 0);
            this.listViewAlbum.MultiSelect = false;
            this.listViewAlbum.Name = "listViewAlbum";
            this.listViewAlbum.Size = new System.Drawing.Size(216, 398);
            this.listViewAlbum.TabIndex = 0;
            this.listViewAlbum.TileSize = new System.Drawing.Size(208, 30);
            this.listViewAlbum.UseCompatibleStateImageBehavior = false;
            this.listViewAlbum.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 212;
            // 
            // buttonAddAlbum
            // 
            this.buttonAddAlbum.ImageName = global::PhotoViewer.Properties.Resources.button_add_album;
            this.buttonAddAlbum.Location = new System.Drawing.Point(8, 12);
            this.buttonAddAlbum.Name = "buttonAddAlbum";
            this.buttonAddAlbum.Size = new System.Drawing.Size(40, 34);
            this.buttonAddAlbum.TabIndex = 5;
            this.buttonAddAlbum.UseVisualStyleBackColor = true;
            this.buttonAddAlbum.Click += new System.EventHandler(this.buttonAddAlbum_Click);
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(8, 86);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(30, 13);
            this.labelDate.TabIndex = 4;
            this.labelDate.Text = "Date";
            this.labelDate.Visible = false;
            // 
            // labelSubTitle
            // 
            this.labelSubTitle.AutoSize = true;
            this.labelSubTitle.Location = new System.Drawing.Point(8, 110);
            this.labelSubTitle.Name = "labelSubTitle";
            this.labelSubTitle.Size = new System.Drawing.Size(46, 13);
            this.labelSubTitle.TabIndex = 3;
            this.labelSubTitle.Text = "SubTitle";
            this.labelSubTitle.Visible = false;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(8, 59);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(27, 13);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Title";
            this.labelTitle.Visible = false;
            // 
            // splitContainerRightMain
            // 
            this.splitContainerRightMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerRightMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRightMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerRightMain.IsSplitterFixed = true;
            this.splitContainerRightMain.Location = new System.Drawing.Point(227, 3);
            this.splitContainerRightMain.Name = "splitContainerRightMain";
            this.splitContainerRightMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRightMain.Panel1
            // 
            this.splitContainerRightMain.Panel1.Controls.Add(this.buttonSlideshow);
            this.splitContainerRightMain.Panel1.Controls.Add(this.buttonWebPage);
            this.splitContainerRightMain.Panel1.Controls.Add(this.buttonAddPicture);
            this.splitContainerRightMain.Panel1.Controls.Add(this.buttonGoBackToAlbum);
            this.splitContainerRightMain.Panel1.Controls.Add(this.labelTitleSelectedAlbum);
            // 
            // splitContainerRightMain.Panel2
            // 
            this.splitContainerRightMain.Panel2.Controls.Add(this.splitContainerFullPicture);
            this.splitContainerRightMain.Panel2.Controls.Add(this.flowPanelPictures);
            this.splitContainerRightMain.Size = new System.Drawing.Size(554, 556);
            this.splitContainerRightMain.SplitterDistance = 60;
            this.splitContainerRightMain.TabIndex = 1;
            // 
            // buttonSlideshow
            // 
            this.buttonSlideshow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSlideshow.ImageName = global::PhotoViewer.Properties.Resources.play;
            this.buttonSlideshow.Location = new System.Drawing.Point(56, 14);
            this.buttonSlideshow.Name = "buttonSlideshow";
            this.buttonSlideshow.Size = new System.Drawing.Size(37, 34);
            this.buttonSlideshow.TabIndex = 9;
            this.buttonSlideshow.UseVisualStyleBackColor = true;
            this.buttonSlideshow.Visible = false;
            this.buttonSlideshow.Click += new System.EventHandler(this.buttonSlideshow_Click);
            // 
            // buttonWebPage
            // 
            this.buttonWebPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWebPage.ImageName = global::PhotoViewer.Properties.Resources.browser;
            this.buttonWebPage.Location = new System.Drawing.Point(463, 14);
            this.buttonWebPage.Name = "buttonWebPage";
            this.buttonWebPage.Size = new System.Drawing.Size(35, 34);
            this.buttonWebPage.TabIndex = 8;
            this.buttonWebPage.UseVisualStyleBackColor = true;
            this.buttonWebPage.Visible = false;
            this.buttonWebPage.Click += new System.EventHandler(this.buttonWebPage_Click);
            // 
            // buttonAddPicture
            // 
            this.buttonAddPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddPicture.ImageName = global::PhotoViewer.Properties.Resources.button_add_picture;
            this.buttonAddPicture.Location = new System.Drawing.Point(509, 17);
            this.buttonAddPicture.Name = "buttonAddPicture";
            this.buttonAddPicture.Size = new System.Drawing.Size(35, 28);
            this.buttonAddPicture.TabIndex = 7;
            this.buttonAddPicture.UseVisualStyleBackColor = true;
            this.buttonAddPicture.Visible = false;
            this.buttonAddPicture.Click += new System.EventHandler(this.buttonAddPicture_Click);
            // 
            // buttonGoBackToAlbum
            // 
            this.buttonGoBackToAlbum.ImageName = global::PhotoViewer.Properties.Resources.back_button_3;
            this.buttonGoBackToAlbum.Location = new System.Drawing.Point(5, 20);
            this.buttonGoBackToAlbum.Name = "buttonGoBackToAlbum";
            this.buttonGoBackToAlbum.Size = new System.Drawing.Size(45, 23);
            this.buttonGoBackToAlbum.TabIndex = 6;
            this.buttonGoBackToAlbum.UseVisualStyleBackColor = true;
            this.buttonGoBackToAlbum.Visible = false;
            this.buttonGoBackToAlbum.Click += new System.EventHandler(this.buttonGoBackToAlbum_Click);
            // 
            // labelTitleSelectedAlbum
            // 
            this.labelTitleSelectedAlbum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitleSelectedAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleSelectedAlbum.Location = new System.Drawing.Point(0, 0);
            this.labelTitleSelectedAlbum.Name = "labelTitleSelectedAlbum";
            this.labelTitleSelectedAlbum.Size = new System.Drawing.Size(552, 58);
            this.labelTitleSelectedAlbum.TabIndex = 0;
            this.labelTitleSelectedAlbum.Text = "-";
            this.labelTitleSelectedAlbum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainerFullPicture
            // 
            this.splitContainerFullPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerFullPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFullPicture.IsSplitterFixed = true;
            this.splitContainerFullPicture.Location = new System.Drawing.Point(0, 0);
            this.splitContainerFullPicture.Name = "splitContainerFullPicture";
            this.splitContainerFullPicture.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerFullPicture.Panel1
            // 
            this.splitContainerFullPicture.Panel1.Controls.Add(this.pictureBoxFullSize);
            this.splitContainerFullPicture.Panel1MinSize = 420;
            // 
            // splitContainerFullPicture.Panel2
            // 
            this.splitContainerFullPicture.Panel2.Controls.Add(this.buttonNext);
            this.splitContainerFullPicture.Panel2.Controls.Add(this.buttonPrevious);
            this.splitContainerFullPicture.Panel2.Controls.Add(this.labelDuration);
            this.splitContainerFullPicture.Panel2.Controls.Add(this.numericUpDown1);
            this.splitContainerFullPicture.Panel2MinSize = 50;
            this.splitContainerFullPicture.Size = new System.Drawing.Size(554, 492);
            this.splitContainerFullPicture.SplitterDistance = 438;
            this.splitContainerFullPicture.TabIndex = 2;
            this.splitContainerFullPicture.Visible = false;
            // 
            // pictureBoxFullSize
            // 
            this.pictureBoxFullSize.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBoxFullSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxFullSize.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFullSize.Name = "pictureBoxFullSize";
            this.pictureBoxFullSize.Size = new System.Drawing.Size(552, 436);
            this.pictureBoxFullSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxFullSize.TabIndex = 1;
            this.pictureBoxFullSize.TabStop = false;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.ImageName = global::PhotoViewer.Properties.Resources.next_button;
            this.buttonNext.Location = new System.Drawing.Point(505, 3);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(44, 43);
            this.buttonNext.TabIndex = 8;
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPrevious.ImageName = global::PhotoViewer.Properties.Resources.previous_button;
            this.buttonPrevious.Location = new System.Drawing.Point(2, 3);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(44, 43);
            this.buttonPrevious.TabIndex = 7;
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // labelDuration
            // 
            this.labelDuration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDuration.AutoSize = true;
            this.labelDuration.Location = new System.Drawing.Point(207, 9);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(53, 13);
            this.labelDuration.TabIndex = 6;
            this.labelDuration.Text = "Duration :";
            this.labelDuration.Visible = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDown1.Location = new System.Drawing.Point(266, 6);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Visible = false;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // flowPanelPictures
            // 
            this.flowPanelPictures.AllowDrop = true;
            this.flowPanelPictures.AutoScroll = true;
            this.flowPanelPictures.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.flowPanelPictures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelPictures.Location = new System.Drawing.Point(0, 0);
            this.flowPanelPictures.Name = "flowPanelPictures";
            this.flowPanelPictures.Size = new System.Drawing.Size(554, 492);
            this.flowPanelPictures.TabIndex = 0;
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelLeftMain.ResumeLayout(false);
            this.splitContainerLeftPanel.Panel1.ResumeLayout(false);
            this.splitContainerLeftPanel.Panel2.ResumeLayout(false);
            this.splitContainerLeftPanel.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftPanel)).EndInit();
            this.splitContainerLeftPanel.ResumeLayout(false);
            this.splitContainerRightMain.Panel1.ResumeLayout(false);
            this.splitContainerRightMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightMain)).EndInit();
            this.splitContainerRightMain.ResumeLayout(false);
            this.splitContainerFullPicture.Panel1.ResumeLayout(false);
            this.splitContainerFullPicture.Panel2.ResumeLayout(false);
            this.splitContainerFullPicture.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFullPicture)).EndInit();
            this.splitContainerFullPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFullSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelLeftMain;
        private System.Windows.Forms.SplitContainer splitContainerLeftPanel;
        private ListViewCustom listViewAlbum;
        private System.Windows.Forms.SplitContainer splitContainerRightMain;
        private System.Windows.Forms.Label labelTitleSelectedAlbum;
        private System.Windows.Forms.FlowLayoutPanel flowPanelPictures;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.PictureBox pictureBoxFullSize;
        private System.Windows.Forms.SplitContainer splitContainerFullPicture;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelSubTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label labelDuration;
        private ButtonCustom buttonGoBackToAlbum;
        private ButtonCustom buttonAddAlbum;
        private ButtonCustom buttonAddPicture;
        private ButtonCustom buttonNext;
        private ButtonCustom buttonPrevious;
        private ButtonCustom buttonWebPage;
        private ButtonCustom buttonSlideshow;
    }
}

