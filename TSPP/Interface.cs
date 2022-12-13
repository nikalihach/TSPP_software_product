using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace TSPP
{
    internal class Interface
    {
        public void pictureBox_MouseEnter(PictureBox pictureBox, Label label, int x)
        {
            pictureBox.Location = new Point(pictureBox.Location.X + x, pictureBox.Location.Y);
            label.Location = new Point(label.Location.X + x, label.Location.Y);
        }

        public void pictureBox_MouseLeave(PictureBox pictureBox, Label label, int x)
        {
            pictureBox.Location = new Point(pictureBox.Location.X - x, pictureBox.Location.Y);
            label.Location = new Point(label.Location.X - x, label.Location.Y);
        }

        public void pictureBox_MouseEnter_button(PictureBox pictureBox, Label label, int size, int x)
        {
            pictureBox.Size = new Size(pictureBox.Size.Width + size, pictureBox.Size.Height + size);
            label.Location = new Point(label.Location.X - x, label.Location.Y);
            label.ForeColor = Color.Red;
        }

        public void pictureBox_MouseLeave_button(PictureBox pictureBox, Label label, int size, int x)
        {
            pictureBox.Size = new Size(pictureBox.Size.Width - size, pictureBox.Size.Height - size);
            label.Location = new Point(label.Location.X + x, label.Location.Y);
            label.ForeColor = Color.DarkGray;
        }
    }
}
