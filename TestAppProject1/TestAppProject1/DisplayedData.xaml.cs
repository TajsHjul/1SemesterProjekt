using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for DisplayedData.xaml
    /// </summary>
    public partial class DisplayedData : UserControl
    {
        public DisplayedData()
        {
            InitializeComponent();

            //Display date above DataGrid's
            string date_Today = DateTime.Today.ToString("dd-MM-yyyy");

            Textblock_TodaysDate.Text = date_Today;

        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            //ConnectionString and sqlQuery
            string connectionString = (@"Server = DATAMATIKERDATA; Database = team2; User Id = t2login; Password = t2login2234;");
            string sqlQuery = "SELECT * FROM dbo.Municipality";

            SqlConnection connection = new SqlConnection(connectionString);


            try
            {
                //Create DataTable and populate it with sqlQuery results
                DataTable dt_new = new DataTable();
                using (connection)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_new);
                        }
                    }
                }

                //Set DataGrad to display DataTable
                DataGrid_NewData.ItemsSource = dt_new.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }

        }
    }
}
