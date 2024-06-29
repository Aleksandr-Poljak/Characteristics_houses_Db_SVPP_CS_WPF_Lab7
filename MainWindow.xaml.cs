using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        House house = new();

        public MainWindow()
        {
            InitializeComponent();
            init_HouseViewUserControl();
                     
        }

        private void init_HouseViewUserControl()
        {
            HouseViewUserControl houseView =  new HouseViewUserControl(ref house);
            houseView.Margin = new Thickness(5);
            Grid.SetColumn(houseView, 2);

            Grid_Main.Children.Add(houseView);
        }
    }
}