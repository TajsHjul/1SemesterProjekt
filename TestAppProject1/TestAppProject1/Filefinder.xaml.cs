using System;
using System.IO;
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
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Net;
using System.Data.SqlClient;
using System.IO.Compression;

namespace TestAppProject1
{
    /// <summary>
    /// Interaction logic for Filefinder.xaml
    /// </summary>
    public partial class Filefinder : UserControl
    {
        public Filefinder()
        {
            InitializeComponent(); this.DataContext = this;
        }
        private void Select_click(object sender, EventArgs e)
        {

            string fileContent = string.Empty;
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|Excel files (*.xlsx*)|*.*|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();

                    }
                }
            }


        }
        private void Connect_click(object sender, EventArgs e)
        {

            string connectionString = null;
            string sql = null;
            SqlDataReader dataReader;
            SqlConnection connection;
            SqlCommand command;

            //connectionString afhænger af hvilken type forbindelse der oprettes. Følgende forbinder via Windows Auth.
            connectionString = "Server = DATAMATIKERDATA; Database = team2; User Id =  t2login; Password =  t2login2234;";


            sql = "SELECT * FROM Muncipality";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageBox.Show("Du har nu forbindelse til Databasen. Godt gået ;)");
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    MessageBox.Show("Column nummer 7 indeholder " + dataReader.GetValue(7));
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection!"+ "\n" + ex);
            }
            
            

        }
        private void Download_click(object sender, EventArgs e)
        {
            

                using (var client = new WebClient())
                {

                    var url = "https://covid19.ssi.dk/overvagningsdata/download-fil-med-overvaagningdata";
                    string html = client.DownloadString(url);

                    var pos = html.IndexOf("https://files.ssi.dk/covid19/overvagning/data/data-epidemiologiske-rapport");
                    var endpos = html.IndexOf('"', pos);
                    string link = html.Substring(pos, endpos - pos);

                    var decodedLink = WebUtility.HtmlDecode(link);
                    string resource = decodedLink;


                    MessageBox.Show(resource);
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
    }
}
