using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using PhotoViewer.Model;

namespace PhotoViewer
{
    public partial class ListViewCustom : ListView
    {
        // Permet le tri des albums dans la List au niveau Data
        public delegate void MessageSortAlbum();
        public event MessageSortAlbum OnMessageSortAlbum;

        public delegate void MessageMovePicture(Album album, PictureBoxCustom pictureBox);
        public event MessageMovePicture OnMessageMovePicture;

        private int indexAlbumFirstSelected;

        public ListViewCustom()
            : base()
        {
            this.DoubleBuffered = true;
        }

        protected override void WndProc(ref Message m)
        {
            // Suppress mouse messages that are OUTSIDE of the items area
            if (m.Msg >= 0x201 && m.Msg <= 0x209)
            {
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                var hit = this.HitTest(pos);
                switch (hit.Location)
                {
                    case ListViewHitTestLocations.AboveClientArea:
                    case ListViewHitTestLocations.BelowClientArea:
                    case ListViewHitTestLocations.LeftOfClientArea:
                    case ListViewHitTestLocations.RightOfClientArea:
                    case ListViewHitTestLocations.None:
                        return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            base.DoDragDrop((ListViewItem)e.Item, DragDropEffects.Move);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            if (e.Data.GetDataPresent(typeof(ListViewItem)) || e.Data.GetDataPresent(typeof(PictureBoxCustom)))
            {
                indexAlbumFirstSelected = this.SelectedItems[0].Index;
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            Point cp = base.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);

            if (hoverItem == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                foreach (ListViewItem moveItem in base.SelectedItems)
                {
                    if (moveItem.Index == hoverItem.Index)
                    {
                        e.Effect = DragDropEffects.None;
                        hoverItem.EnsureVisible();
                        return;
                    }
                }
            }
            else if (e.Data.GetDataPresent(typeof(PictureBoxCustom)))
            {
                if (hoverItem.Index == indexAlbumFirstSelected)
                {
                    e.Effect = DragDropEffects.None;
                    hoverItem.EnsureVisible();
                    return;
                }
            }

            base.OnDragOver(e);

            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem source = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                if (source != null)
                {
                    e.Effect = DragDropEffects.Move;
                    hoverItem.EnsureVisible();
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else if (e.Data.GetDataPresent(typeof(PictureBoxCustom)))
            {
                PictureBoxCustom source = (PictureBoxCustom)e.Data.GetData(typeof(PictureBoxCustom));

                if (source != null)
                {
                    e.Effect = DragDropEffects.Move;
                    hoverItem.EnsureVisible();

                    //this.Items[hoverItem.Index].Selected = true;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            Point cp = this.PointToClient(new Point(e.X, e.Y));
            ListViewItem destination = this.GetItemAt(cp.X, cp.Y);

            if (base.SelectedItems.Count == 0)
                return;

            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem source = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                if (destination == null)
                    return;

                int dropIndex = destination.Index;

                ListViewItem insertItem = (ListViewItem)source.Clone();

                this.Items.Remove(source);
                this.Items.Insert(dropIndex, source);

                UpdatePositionAlbum();

                this.OnMessageSortAlbum();
            }
            else if (e.Data.GetDataPresent(typeof(PictureBoxCustom)))
            {
                PictureBoxCustom pbc = (PictureBoxCustom)e.Data.GetData(typeof(PictureBoxCustom));

                this.OnMessageMovePicture((Album)destination.Tag, pbc);
            }
        }

        private void UpdatePositionAlbum()
        {
            foreach (ListViewItem lvi in this.Items)
            {
                Album a = (Album)lvi.Tag;
                a.Position = lvi.Index;
            }
        }
    }
}
