using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppProject1
{
    class DataConversion
    {
        static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable FileData)
        {
            using (SqlConnection dbConnection = new SqlConnection("Server = DATAMATIKERDATA; Database = team2; User Id =  t2login; Password =  t2login2234;"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "Muncipality";
                    foreach (var column in FileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(FileData);
                }
            }
        }
            private static DataTable GetDataTableFromCSVFile(string csv_file_path)
        {
            DataTable csvData;
            try
            {
                using (StreamReader csvReader = new StreamReader(csv_file_path)) 
                {
                    csvData = new DataTable();
                    string[] headers = csvReader.ReadLine().Split(',');
                    for (int i = 0; i < headers.Count(); i++)
                    {
                        csvData.Columns.Add();
                    }
                    while (!csvReader.EndOfStream)
                    {
                        string[] rows = csvReader.ReadLine().Split(',');
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
                
                return null;
            }
            return csvData;
        }
    }

    
}
