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
    /// 123123
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            string mainFolder = @"c:\corona_data";
            if (Directory.Exists(mainFolder) == true)
                Directory.Delete(mainFolder, true);

            Directory.CreateDirectory(mainFolder);
            using (var client = new WebClient())
            {

                var url = "https://covid19.ssi.dk/overvagningsdata/download-fil-med-overvaagningdata";
                string html = client.DownloadString(url);

                var pos = html.IndexOf("https://files.ssi.dk/covid19/overvagning/data/data-epidemiologiske-rapport");
                var endpos = html.IndexOf('"', pos);
                string link = html.Substring(pos, endpos - pos);

                var decodedLink = WebUtility.HtmlDecode(link);
                string resource = decodedLink;
                client.DownloadFile(new Uri(resource), @"c:\corona_data\NyesteCoronadata.zip");

                string zipPath = @"c:\corona_data\NyesteCoronadata.zip";
                string extractPath = @"c:\corona_data\NyesteCoronaTal";

                ZipFile.ExtractToDirectory(zipPath, extractPath);
                File.Delete(zipPath);

                //Delete specific unwanted .csv file(s) in mainFolder by name
                deleteFileByName("Cases_by_sex.csv");
                deleteFileByName("Deaths_over_time.csv");
                deleteFileByName("Municipality_cases_time_series.csv");
                deleteFileByName("Region_summary.csv");
                deleteFileByName("Test_pos_over_time.csv");

                MessageBox.Show("Files have been downloaded succesfully and are now ready to be uploaded to database.");
            }
        }
        static void deleteFileByName(string fileToDelete)
        {
            string fileToDeleteDirectory = @"C:\corona_data\NyesteCoronaTal\";
            File.Delete(fileToDeleteDirectory + fileToDelete);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
