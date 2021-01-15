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
        //connectionstring + bool for combobox
        private string connectionString = @"Server = DATAMATIKERDATA; Database = team2; User Id = t2login; Password = t2login2234;";
        private bool handle = true;
        public DisplayedData()
        {
            InitializeComponent();

            //Display date for last update.
            string date_Today = DateTime.UtcNow.ToString("M/dd/yyyy - HH:mm");
            Textblock_TodaysDate.Text = "Sidst opdateret: " + date_Today;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        //Switch for combobox cases, executing DisplaySqlTable(string FullSqlQuery) results.
        private void Handle()
        {
            try
            {
                switch (cmbSelect.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
                {
                    case "Cases_by_age":
                        DisplaySqlTable("SELECT * FROM Cases_by_age");
                        break;

                    case "Municipality_test_pos":
                        DisplaySqlTable("SELECT * FROM Municipality_test_pos");
                        break;

                    case "Municipality_tested_persons_time_series":
                        DisplaySqlTable("SELECT * FROM Municipality_tested_persons_time_series");
                        break;

                    case "Newly_admitted_over_time":
                        DisplaySqlTable("SELECT * FROM Newly_admitted_over_time");
                        break;

                    case "Test_regioner":
                        DisplaySqlTable("SELECT * FROM Test_regioner");
                        break;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //method to display SQL-Query results in datagrid. Write Query in method.
        private void DisplaySqlTable(string fullSQLquery)
        {
            string sqlQuery = fullSQLquery;

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                //Create DataTable and populate it with sqlQuery results
                DataTable dt = new DataTable();
                using (connection)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                //Set DataGrad to display DataTable
                DataGrid_DisplayData.ItemsSource = dt.DefaultView;
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
