using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internship_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string source = textBox1.Text;
            Browser(source);
            Image file;
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG (*.JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                file = Image.FromFile(f.FileName);
                pictureBox1.Image = file;
            }
        }

        public static void Browser(string source) 
        {
            var th = new Thread(() =>
            {
                var webBrowser = new WebBrowser();
                webBrowser.ScrollBarsEnabled = false;
                webBrowser.IsWebBrowserContextMenuEnabled = false;
                webBrowser.AllowNavigation = true;

                webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
                webBrowser.DocumentText = source;
                Application.Run();
            }
            );
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) 
        {
            var webBrowser = (WebBrowser)sender;
            using (Bitmap bitmap =
                new Bitmap(
                    webBrowser.Width,
                    webBrowser.Height))
            {
                webBrowser
                    .DrawToBitmap(
                    bitmap,
                    new System.Drawing
                        .Rectangle(0, 0, bitmap.Width, bitmap.Height));
                bitmap.Save(@"filename.jpg",
                    System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
