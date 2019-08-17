using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace LolAV
{
    public partial class IconPick : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        Assembly assembly = Assembly.GetExecutingAssembly();
        public IconPick()
        {
            InitializeComponent();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(this.assembly.Location);
            this.label1.Text = "Version "+ fileVersionInfo.ProductVersion +"\nCreated by PixelHir\ngithub.com/PixelHir";
        }

        private void PictureClick(object sender, EventArgs e)
        {
            var image = (PictureBox)sender;
            var id = image.Tag.ToString();
            Thread iconSetter = new Thread(() => League.SetIcon(id, this));
            iconSetter.Start();

        }

        private void Status_Click(object sender, EventArgs e)
        {

        }

        private void title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
