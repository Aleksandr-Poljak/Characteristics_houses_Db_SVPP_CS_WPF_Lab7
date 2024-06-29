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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db_
{
    /// <summary>
    /// Логика взаимодействия для HouseViewUserControl.xaml
    /// </summary>
    public partial class HouseViewUserControl : UserControl
    {
        House house;
        public HouseViewUserControl(ref House houseElement)
        {
            InitializeComponent();
            house = houseElement;
            Grid_Main_HouseView.DataContext = house;
        }
    }
}
