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
    /// Author: Tajs Hjulmann

    public partial class Graph : UserControl
    {
        
        public Graph()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            canGraph.Children.Clear();
            string sql = null;
            SqlCommand command;
            double gsnit = 0;
            SqlConnection connection = new SqlConnection(@"Server = DATAMATIKERDATA; Database = team2; User Id = t2login; Password = t2login2234;");
            command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "SELECT Skive FROM dbo.Municipality_tested_persons_time_series";

                List<string> SmitteData = new List<string>();
                String Brottekst = "";

                using (SqlDataReader reader = command.ExecuteReader())

                {
                    while (reader.Read())

                    {
                        SmitteData.Add(reader[0].ToString());
                    }
                    SmitteData.RemoveRange(SmitteData.Count-2,2);
                    
                        for (int j = SmitteData.Count() - 14; j < SmitteData.Count();)
                        {
                            Brottekst = (Brottekst +(SmitteData.Count()-j+1) +" dage siden:\t"+ Convert.ToInt32(SmitteData[j]) + "\n");
                            gsnit += Convert.ToDouble(SmitteData[j]);
                            j++;
                            
                            

                        }

                        gsnit = gsnit / 14;
                    if (gsnit > Convert.ToDouble(SmitteData[SmitteData.Count() - 14]))
                        { Advarsel.Text = 
                            "\t:::ADVARSEL:::\nMed udgangspunkt i dataen for\nde seneste 15 dage\nanbefales der restriktioner"+
                            "\nGennemstnitlige antal\npositive tests:\n"+gsnit; }
                    else
                        Advarsel.Text = ":::INGEN_ADVARSEL:::\nDet gennemsnitlige antal positive tests gennem de seneste 15 dage er " + gsnit + "\nYderligere restriktioner anbefales ikke";
                    Advarsel.Text += "\n\nHer er dataen for\nde seneste 15 dage, foruden\ndagens og gårsdagens tal:\n\n" + Brottekst;
                    reader.Close();

                }

                // Alt herefter er graf
                const double margin = 10;
                double xmin = margin;
                double xmax = canGraph.Width - margin;
                double ymax = margin;
                double ymin = canGraph.Height - margin;
                const double step = 20;

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
                int posTest = 0;
                for (double y = ymax; y <= canGraph.Height - step; y += step)
                {
                    yaxis_geom.Children.Add(new LineGeometry(new Point(xmin - margin / 2, y),new Point(xmin + margin / 2, y)));
                   Text(xmin - 3.5* margin, ymin-y, posTest.ToString() , Color.FromRgb(255, 255, 255));
                    posTest += 100;
                }

                Path yaxis_path = new Path();
                yaxis_path.StrokeThickness = 1;
                yaxis_path.Stroke = Brushes.Black;
                yaxis_path.Data = yaxis_geom;

                canGraph.Children.Add(yaxis_path);

                //making data sets
                int rownum = SmitteData.Count()-14;

                Brush brushes = Brushes.Blue;
                Random rand = new Random();
                    int rowlabl = 15;
                    int start_y = (int)ymin;
                    int step2 = (int)ymin;
                    PointCollection points = new PointCollection();
                    for (double x = xmin; x <= step*14; x += step)
                    {
                        
                        start_y = step2;
                        points.Add(new Point(x, -Convert.ToDouble(SmitteData[rownum]) /5 + (double)ymin));
                        
                        Text(x,Convert.ToDouble(ymin+10),rowlabl.ToString(), Color.FromRgb(255, 255, 255));
                        rownum++;
                        rowlabl--;


                    }

                    Polyline dataline = new Polyline();
                        dataline.StrokeThickness = 1;
                        dataline.Stroke = brushes;
                        dataline.Points = points;

                    canGraph.Children.Add(dataline);
                
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
        //Nedenfor er metoden som sætter tekst på akserne
        private void Text(double x, double y, string text, Color color)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;

            textBlock.Foreground = new SolidColorBrush(color);

            Canvas.SetLeft(textBlock, x);

            Canvas.SetTop(textBlock, y);

            canGraph.Children.Add(textBlock);

        }
        
    }
    //Kode af Tajs Hjulmann
}