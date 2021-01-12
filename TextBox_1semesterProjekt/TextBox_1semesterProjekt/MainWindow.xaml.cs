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
                string sql = null;
                SqlCommand command;

                SqlConnection connection = new SqlConnection(@"Server = DATAMATIKERDATA; Database = team2; User Id = t2login; Password = t2login2234;");
                command = new SqlCommand(sql, connection);



                try
                {
                    connection.Open();



                    command.Connection = connection;
                    command.CommandText = "SELECT muncipality_name FROM dbo.Muncipality";
                    using (SqlDataReader reader = command.ExecuteReader())

                    {
                        while (reader.Read())
                        {
                           
                            NewData.Text = reader["muncipality_name"].ToString();

                        }

                    }
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


