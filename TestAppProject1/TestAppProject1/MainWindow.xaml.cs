using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
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
using System.Net;

namespace TestAppProject1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); this.DataContext = this;
            if (Directory.Exists("C:/corona_data") == false)
                Directory.CreateDirectory("C:/corona_data");
            using (var client = new WebClient())
            {

                var url = "https://covid19.ssi.dk/overvagningsdata/download-fil-med-overvaagningdata";
                string html = client.DownloadString(url);

                var pos = html.IndexOf("https://files.ssi.dk/covid19/overvagning/data/data-epidemiologiske-rapport");
                var endpos = html.IndexOf('"', pos);
                string link = html.Substring(pos, endpos - pos);

                var decodedLink = WebUtility.HtmlDecode(link);
                string resource = decodedLink;
                DateTime time = DateTime.Now;
                
                DateTime.Parse(time.ToString()).ToString("yyyy-MM-dd");
                MessageBox.Show(resource);
                client.DownloadFile(new Uri(resource), @"c:\corona_data\NyesteCoronadata"+ DateTime.Parse(time.ToString()).ToString("yyyy-MM-dd")+".zip");
                
                    //ZipFile.ExtractToDirectory(@"c:\corona_data\NyesteCoronadata"+ DateTime.Parse(time.ToString()).ToString("yyyy-MM-dd") + ".zip", @"c:\corona_data\Coronadata\" + DateTime.Parse(time.ToString()).ToString("yyyy-MM-dd"));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
