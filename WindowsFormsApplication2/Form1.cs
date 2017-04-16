using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        public object Documents { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

            IEnumerable<VideoInfo> videos = DownloadUrlResolver.GetDownloadUrls(txturl.Text);

            VideoInfo video = videos.First(p => p.VideoType == VideoType.Mp4 && p.Resolution == Convert.ToInt32(comboBox1.Text));

            if (video.RequiresDecryption)
                DownloadUrlResolver.DecryptDownloadUrl(video);

             var VideoDownloader  = new VideoDownloader(video, Path.Combine("C://Downloads", video.Title + video.VideoExtension));

            VideoDownloader.DownloadProgressChanged += Downloader_DownloadProgressChanged;


           
            VideoDownloader.Execute();

            
         }

        private void Downloader_DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            Invoke (new MethodInvoker(delegate () 
            {

                progressBar1.Value = (int)e.ProgressPercentage;
                lblprecentage.Text = $"{string.Format("{0:0.##}", e.ProgressPercentage)}%";
                progressBar1.Update();




            }));
                



            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void txturl_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
        }
    }
}
