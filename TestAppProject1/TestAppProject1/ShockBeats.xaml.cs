using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Interaction logic for ShockBeats.xaml
    /// </summary>
    public partial class ShockBeats : UserControl
    {
        public ShockBeats()
        {
            InitializeComponent();
        }
        private void Beat_Click(object sender, RoutedEventArgs e)
        {
           
            SoundPlayer player = new SoundPlayer("..\\..\\Sick_Beats\\System_Shock.wav");
            player.Load();
            player.Play();
        }
    }
}
