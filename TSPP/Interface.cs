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
        public void pictureBox_MouseEnter(PictureBox pictureBox, Label label)
        {
            pictureBox.Location = new Point(pictureBox.Location.X + 10, pictureBox.Location.Y);
            label.Location = new Point(label.Location.X + 10, label.Location.Y);
        }

        public void pictureBox_MouseLeave(PictureBox pictureBox, Label label)
        {
            pictureBox.Location = new Point(pictureBox.Location.X - 10, pictureBox.Location.Y);
            label.Location = new Point(label.Location.X - 10, label.Location.Y);
        }
    }
}
