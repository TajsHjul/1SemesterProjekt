using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace TestAppProject1
{
    /// <summary>
    /// Interaction logic for UpdateDb.xaml
    /// </summary>
    public partial class UpdateDb : UserControl
    {
        public UpdateDb()
        {
            InitializeComponent();
            

        }
        /*
        private void Update_click(object sender, EventArgs e)
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

                MessageBox.Show(".csv files have been uploaded to database succesfully.");
            }
            catch (Exception exception)
            {
                MessageBox.Show("test" + exception);
            }
        }
        static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable FileData)
        {
            using (SqlConnection dbConnection = new SqlConnection("Server = DATAMATIKERDATA; Database = team2; User Id =  t2login; Password =  t2login2234;"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "dbo.Test_regioner";
                    foreach (var column in FileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(FileData);
                }
            }
        }
        private static DataTable GetDataTableFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (StreamReader csvReader = new StreamReader(csv_file_path))
                {
                    
                    string[] headers = csvReader.ReadLine().Split(';');
                    for (int i = 0; i < headers.Count(); i++)
                    {
                        csvData.Columns.Add();
                    }
                    if (!csvReader.EndOfStream)
                    {
                        string[] rows = csvReader.ReadLine().Split(';');
                        DataRow datarow = csvData.NewRow();
                        for (int i = 0; i < rows.Count(); i++)
                        {
                            datarow[i] = rows[i];
                        }
                        csvData.Rows.Add(datarow);
                    }

                }
            }
            catch (Exception ex)
            {
               MessageBox.Show("Kan ikke skabe DataTable fra fil.\n"+ ex);
               return null;
            }
            return csvData;
        }
        */
        private void CSVtoDATABASE_Click(object sender, RoutedEventArgs e)
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

                MessageBox.Show(".csv files have been uploaded to database succesfully.");
            }
            catch (Exception exception)
            {
                MessageBox.Show("test" + exception);
            }

        }
    }
}
