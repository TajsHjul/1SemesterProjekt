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
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        public Graph()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            string sql = null;
            SqlCommand command;

            SqlConnection connection = new SqlConnection(@"Server = DATAMATIKERDATA; Database = team2; User Id = t2login; Password = t2login2234;");
            command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Connection = connection;

                //MUNCIPALITY
                command.CommandText = "SELECT Total FROM dbo.Test_regioner";

                List<string> SmitteData = new List<string>();
                String ByNavneWithNewLineConstants = "";

                using (SqlDataReader reader = command.ExecuteReader())

                {
                    while (reader.Read())

                    {
                        //MUNCIPALITY
                        SmitteData.Add(reader[0].ToString());
                        // NewData.Text = reader["muncipality_name"].ToString();
                    }
                    SmitteData.RemoveRange(SmitteData.Count-1,1);
                    foreach (string smittedatum in SmitteData)
                    {
                        ByNavneWithNewLineConstants = (ByNavneWithNewLineConstants + Convert.ToInt32(smittedatum) + "\n");
                    }


                    

                

                    Advarsel.Text = "Her er det viste datasæt\n"+ByNavneWithNewLineConstants;
                    reader.Close();

                }
                // Alt herefter er graf
                const double margin = 10;
                double xmin = margin;
                double xmax = canGraph.Width - margin;

                double ymin = canGraph.Height - margin;
                const double step = 10;

                // Make the X axis.
                GeometryGroup xaxis_geom = new GeometryGroup();
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(0, ymin), new Point(canGraph.Width, ymin)));
                for (double x = xmin + step;
                    x <= canGraph.Width - step; x += step)
                {
                    xaxis_geom.Children.Add(new LineGeometry(
                        new Point(x, ymin - margin / 2),
                        new Point(x, ymin + margin / 2)));
                }

                Path xaxis_path = new Path();
                xaxis_path.StrokeThickness = 1;
                xaxis_path.Stroke = Brushes.Black;
                xaxis_path.Data = xaxis_geom;

                canGraph.Children.Add(xaxis_path);

                // Make the Y ayis.
                GeometryGroup yaxis_geom = new GeometryGroup();
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin, 0), new Point(xmin, canGraph.Height)));
                for (double y = step; y <= canGraph.Height - step; y += step)
                {
                    yaxis_geom.Children.Add(new LineGeometry(
                        new Point(xmin - margin / 2, y),
                        new Point(xmin + margin / 2, y)));
                }

                Path yaxis_path = new Path();
                yaxis_path.StrokeThickness = 1;
                yaxis_path.Stroke = Brushes.Black;
                yaxis_path.Data = yaxis_geom;

                canGraph.Children.Add(yaxis_path);

                //making data sets
                int rownum = 0;
                Brush brushes = Brushes.Red;
                Random rand = new Random();
                
                    int start_y = (int)ymin;
                    int step2 = (int)ymin;
                    PointCollection points = new PointCollection();
                    for (double x = xmin; x <= xmax; x += step)
                    {

                        start_y = step2;
                        points.Add(new Point(x, -Convert.ToDouble(SmitteData[rownum])/1000 + (double)ymin));
                        Text(x,10,rownum.ToString(), Color.FromRgb(0,0,0));
                        rownum++;
                        

                    }

                    Polyline polyline = new Polyline();
                    polyline.StrokeThickness = 1;
                    polyline.Stroke = brushes;
                    polyline.Points = points;

                    canGraph.Children.Add(polyline);
                
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
        private void Text(double x, double y, string text, Color color)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;

            textBlock.Foreground = new SolidColorBrush(color);

            Canvas.SetLeft(textBlock, x);

            Canvas.SetTop(textBlock, y);

            canGraph.Children.Add(textBlock);

        }
        /*private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            const double margin = 10;
            double xmin = margin;
            double xmax = canGraph.Width - margin;

            double ymin = canGraph.Height - margin;
            const double step = 10;

            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymin), new Point(canGraph.Width, ymin)));
            for (double x = xmin + step;
                x <= canGraph.Width - step; x += step)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymin - margin / 2),
                    new Point(x, ymin + margin / 2)));
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            canGraph.Children.Add(xaxis_path);

            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, canGraph.Height)));
            for (double y = step; y <= canGraph.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            canGraph.Children.Add(yaxis_path);
            string connectionString = null;
            string sql = null;
            SqlDataReader dataReader;
            SqlConnection connection;

            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue };
            //connectionString afhænger af hvilken type forbindelse der oprettes. Følgende forbinder via Windows Auth.
            connectionString = "Server = DATAMATIKERDATA; Database = team2; User Id =  t2login; Password =  t2login2234;";

            SqlCommand command;
            sql = "SELECT 'Region Midtjylland' FROM dbo.Test_regioner";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageBox.Show("Du har nu forbindelse til Databasen. Godt gået ;)");
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {

                    int rownum = 0;
                    for (int data_set = 0; data_set < 3; data_set++)
                    {
                        int start_y = (int)ymin;
                        int step2 = (int)ymin;
                        PointCollection points = new PointCollection();

                        for (double x = xmin; x <= xmax; x += step)
                        {

                            
                            points.Add(new Point(x, Convert.ToDouble(dataReader[rownum]) ));
                            step2++;
                            rownum++;

                        }

                        Polyline polyline = new Polyline();
                        polyline.StrokeThickness = 1;
                        polyline.Stroke = brushes[data_set];
                        polyline.Points = points;

                        canGraph.Children.Add(polyline);
                    }
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection!" + "\n" + ex);
            }

            

            // Make some data sets.
        
        }
        */
    }
}