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
using System.Data.SqlClient;
using System.ComponentModel;

namespace TestAppProject1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 123123
    public partial class MainWindow : Window
    {
        //Initiate loadingbarwindow and backgroundworker
        LoadingBarWindow loadingBarWindow = new LoadingBarWindow();
        BackgroundWorker bg = new BackgroundWorker();

        public MainWindow()
        {
            //Show LoadingBarWindow while backgroundworker does its thing.
            loadingBarWindow.Show();
            loadingBarWindow.Activate();

            //Backgroundworker
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bg_RunWorkerCompleted);
            bg.RunWorkerAsync();
        }

        //task to be performed by backgroundworker
        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DownloadAndUnZipFiles();
                UploadCSVfilesToDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //task to run once bg_DoWork finishes.
        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingBarWindow.Close();
            MessageBox.Show("Files have been succesfully downloaded and uploaded to database.");
        }

        static void DownloadAndUnZipFiles()
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
            }
        }
        static void deleteFileByName(string fileToDelete)
        {
            string fileToDeleteDirectory = @"C:\corona_data\NyesteCoronaTal\";
            File.Delete(fileToDeleteDirectory + fileToDelete);
        }
        static void UploadCSVfilesToDatabase()
        {
            //upload .csv files in 'SourceFolderPath' as tables to connectionString database.
            try
            {
                //Declare Variables and provide values
                string SourceFolderPath = @"C:\corona_data\NyesteCoronaTal";
                string FileExtension = ".csv";
                string FileDelimiter = ";";
                string ColumnsDataType = "NVARCHAR(100)";
                string SchemaName = "dbo";

                string connectionString = @"Server = DATAMATIKERDATA; Database = team2; User Id = t2login; Password = t2login2234;";

                //Get files from folder
                string[] fileEntries = Directory.GetFiles(SourceFolderPath, "*" + FileExtension);
                foreach (string fileName in fileEntries)
                {

                    //Create Connection to SQL Server in which you would like to create tables and load data
                    SqlConnection SQLConnection = new SqlConnection();
                    SQLConnection.ConnectionString = connectionString;

                    //Writing Data of File Into Table
                    string TableName = "";
                    int counter = 0;
                    string line;
                    string ColumnList = "";

                    System.IO.StreamReader SourceFile =
                    new System.IO.StreamReader(fileName);

                    SQLConnection.Open();
                    while ((line = SourceFile.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            //Read the header and prepare Create Table Statement
                            ColumnList = "[" + line.Replace(FileDelimiter, "],[") + "]";
                            TableName = (((fileName.Replace(SourceFolderPath, "")).Replace(FileExtension, "")).Replace("\\", ""));
                            string CreateTableStatement = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[" + SchemaName + "].";
                            CreateTableStatement += "[" + TableName + "]')";
                            CreateTableStatement += " AND type in (N'U'))DROP TABLE [" + SchemaName + "].";
                            CreateTableStatement += "[" + TableName + "]  Create Table " + SchemaName + ".[" + TableName + "]";
                            CreateTableStatement += "([" + line.Replace(FileDelimiter, "] " + ColumnsDataType + ",[") + "] " + ColumnsDataType + ")";
                            SqlCommand CreateTableCmd = new SqlCommand(CreateTableStatement, SQLConnection);
                            CreateTableCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //Prepare Insert Statement and execute to insert data
                            string query = "Insert into " + SchemaName + ".[" + TableName + "] (" + ColumnList + ") ";
                            query += "VALUES('" + line.Replace(FileDelimiter, "','") + "')";

                            SqlCommand SQLCmd = new SqlCommand(query, SQLConnection);
                            SQLCmd.ExecuteNonQuery();
                        }

                        counter++;
                    }
                    SourceFile.Close();
                    SQLConnection.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("test" + exception);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
