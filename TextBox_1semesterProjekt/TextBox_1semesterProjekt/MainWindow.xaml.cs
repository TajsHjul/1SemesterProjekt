using System;
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
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace TextBox_1semesterProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            String post = "localhost";
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[post].ConnectionString);

            try 
            {
               

                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Muncipality.ID FROM Muncipality ";
                    //whenever you want to get some data from the database
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NewData.Text = reader["Muncipality"].ToString();
                        }
                    }

                }

                MessageBox.Show("Hello, world.");

            }

            catch (Exception l)

            {
                MessageBox.Show("Error:" + l);
            }
           

            finally
            {

               

            }

        }
    }
}
