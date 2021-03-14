using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Problem3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnPlay.IsEnabled = false;
            btnStop.IsEnabled = false;
            medVideo.LoadedBehavior = MediaState.Manual;
        }

        private void btnGetVideo_Click(object sender, RoutedEventArgs e)
        {
            VideoAPI video;
            string url = "http://pcbstuou.w27.wh-2.com/webservices/3033/api/random/video";

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    video = JsonConvert.DeserializeObject<VideoAPI>(response.Content.ReadAsStringAsync().Result);
                    medVideo.Source = new Uri(video.url);
                    btnPlay.Content = "Play";
                    btnPlay.IsEnabled = true;
                    btnStop.IsEnabled = true;
                }
            }          
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            string status = btnPlay.Content.ToString();
            switch (status)
            {
                case "Play":
                    medVideo.Play();
                    btnPlay.Content = "Pause";
                    break;
                case "Pause":
                    medVideo.Pause();
                    btnPlay.Content = "Play";
                    break;
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            medVideo.Stop();
        }
    }
}
