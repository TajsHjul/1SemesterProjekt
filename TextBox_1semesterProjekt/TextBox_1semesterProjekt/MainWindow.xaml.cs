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

            string result = DateTime.Today.AddDays(-1).ToString("dd-MM-yyyy");

            Console.WriteLine(result);


            string sql = null;
                SqlCommand command;

                SqlConnection connection = new SqlConnection(@"Server = DATAMATIKERDATA; Database = team2; User Id = t2login; Password = t2login2234;");
                command = new SqlCommand(sql, connection);

          try
          {
             connection.Open();
             command.Connection = connection;

                //MUNCIPALITY
                 command.CommandText = "SELECT muncipality_name FROM dbo.Muncipality";
                 command.CommandText = "SELECT muncipality_id FROM dbo.Muncipality";
                 command.CommandText = "SELECT muncipality_number FROM dbo.Muncipality";

                //CVS_DATA(SECTORS)
                 command.CommandText = "SELECT sequence FROM cvs_data(sectors)";
                 command.CommandText = "SELECT code FROM cvs_data(sectors)";
                 command.CommandText = "SELECT niveau FROM cvs_data(sectors)";
                 command.CommandText = "SELECT titel FROM cvs_data(sectors)";
                 command.CommandText = "SELECT general_notes FROM cvs_data(sectors)";
                 command.CommandText = "SELECT includes FROM cvs_data(sectors)";
                 command.CommandText = "SELECT also_includes FROM cvs_data(sectors)";
                 command.CommandText = "SELECT excludes FROM cvs_data(sectors)";
                 command.CommandText = "SELECT paragraph FROM cvs_data(sectors)";
                 command.CommandText = "SELECT measurement FROM cvs_data(sectors)";

                //INCIDENT_CASES_BY_AGE
                 command.CommandText = "SELECT agegroup FROM Incident_cases_by_age";
                 command.CommandText = "SELECT number_of_confirmed FROM Incident_cases_by_age";
                 command.CommandText = "SELECT number_of_tested FROM Incident_cases_by_age";
                 command.CommandText = "SELECT Positive_Percentage FROM Incident_cases_by_age";

                //MUNCIPALITY_INCIDENTS_DATA
                 command.CommandText = "SELECT date FROM Muncipality_incidents_data";
                 command.CommandText = "SELECT incidents FROM Muncipality_incidents_data";

                //MUNCIPALITY_TESTED_POSITIVE
                 command.CommandText = "SELECT muncipality_id FROM Muncipality_tested_positive";
                 command.CommandText = "SELECT muncipality_navn FROM Muncipality_tested_positive";
                 command.CommandText = "SELECT number_of_tested FROM Muncipality_tested_positive";
                 command.CommandText = "SELECT number_of_confirmed_covid19 FROM Muncipality_tested_positive";
                 command.CommandText = "SELECT populations_number FROM Muncipality_tested_positive";
                 command.CommandText = "SELECT cumulative_incident FROM Muncipality_tested_positive";

                //NEWLY_ADMITTED_OVER_TIME
                 command.CommandText = "SELECT capital_city FROM Newly_admitted_over_time";
                 command.CommandText = "SELECT Sjælland FROM Newly_admitted_over_time";
                 command.CommandText = "SELECT Syddanmark FROM Newly_admitted_over_time";
                 command.CommandText = "SELECT Midtjylland FROM Newly_admitted_over_time";
                 command.CommandText = "SELECT Nordjylland FROM Newly_admitted_over_time";
                 command.CommandText = "SELECT unknown_region FROM Newly_admitted_over_time";
                 command.CommandText = "SELECT total FROM Newly_admitted_over_time";

                //TEST_REGIONER
                 command.CommandText = "SELECT ugenr FROM Test_regioner";
                 command.CommandText = "SELECT Region_Hovedstaden FROM Test_regioner";
                 command.CommandText = "SELECT Region_Midtjylland FROM Test_regioner";
                 command.CommandText = "SELECT Region_Nordjylland FROM Test_regioner";
                 command.CommandText = "SELECT Region_Sjælland FROM Test_regioner";
                 command.CommandText = "SELECT Region_Syddanmark FROM Test_regioner";
                 command.CommandText = "SELECT Statens_Serum_Institut FROM Test_regioner";
                 command.CommandText = "SELECT Testcenter_Danmark FROM Test_regioner";
                 command.CommandText = "SELECT Total FROM Test_regioner";
                 command.CommandText = "SELECT Kumulativ_total FROM Test_regioner";

                //TESTED_FOR_INCIDENT_DATA
                 command.CommandText = "SELECT number_of_tested FROM Tested_for_incident_data";
                 command.CommandText = "SELECT date FROM Tested_for_incident_data";







                using (SqlDataReader reader = command.ExecuteReader())

                {
               while (reader.Read())
               {
                   //MUNCIPALITY
                     NewData.Text = reader["muncipality_name"].ToString();
                     NewData.Text = reader["muncipality_id"].ToString();
                     NewData.Text = reader["muncipality_number"].ToString();


                   //CVS_DATA(SECTORS)
                     NewData.Text = reader["sequence"].ToString();
                     NewData.Text = reader["code"].ToString();
                     NewData.Text = reader["niveau"].ToString();
                     NewData.Text = reader["titel"].ToString();
                     NewData.Text = reader["general_notes"].ToString();
                     NewData.Text = reader["includes"].ToString();
                     NewData.Text = reader["also_includes"].ToString();
                     NewData.Text = reader["excludes"].ToString();
                     NewData.Text = reader["paragraph"].ToString();
                     NewData.Text = reader["measurement"].ToString();


                   //INCIDENT_CASES_BY_AGE
                     NewData.Text = reader["agegroup"].ToString();
                     NewData.Text = reader["number_of_confirmed"].ToString();
                     NewData.Text = reader["number_of_tested"].ToString();
                     NewData.Text = reader["Positive_Percentage"].ToString();

                    //MUNCIPALITY_INCIDENTS_DATA
                     NewData.Text = reader["date"].ToString();
                     NewData.Text = reader["incidents"].ToString();

                    //MUNCIPALITY_TESTED_POSITIVE
                      NewData.Text = reader["muncipality_id"].ToString();
                      NewData.Text = reader["muncipality_navn"].ToString();
                      NewData.Text = reader["number_of_tested"].ToString();
                      NewData.Text = reader["number_of_confirmed_covid19"].ToString();
                      NewData.Text = reader["populations_number"].ToString();
                      NewData.Text = reader["cumulative_incident"].ToString();

                     //NEWLY_ADMITTED_OVER_TIME
                      NewData.Text = reader["capital_city"].ToString();
                      NewData.Text = reader["Sjælland"].ToString();
                      NewData.Text = reader["Syddanmark"].ToString();
                      NewData.Text = reader["Midtjylland"].ToString();
                      NewData.Text = reader["Nordjylland"].ToString();
                      NewData.Text = reader["unknown_region"].ToString();
                      NewData.Text = reader["total"].ToString();

                     //TEST_REGIONER
                      NewData.Text = reader["ugenr"].ToString();
                      NewData.Text = reader["Region_Hovedstaden"].ToString();
                      NewData.Text = reader["Region_Midtjylland"].ToString();
                      NewData.Text = reader["Region_Nordjyallnd"].ToString();
                      NewData.Text = reader["Region_Sjælland"].ToString();
                      NewData.Text = reader["Region_Syddanmark"].ToString();
                      NewData.Text = reader["Statens_Serum_Institut"].ToString();
                      NewData.Text = reader["Testcenter_Danmark"].ToString();
                      NewData.Text = reader["Total"].ToString();
                      NewData.Text = reader["Kumulativ_total"].ToString();

                     //TESTED_FOR_INCIDENT_DATA
                      NewData.Text = reader["number_of_tested"].ToString();
                      NewData.Text = reader["date"].ToString();





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


