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
    /// Визуально отображает данные переданного объекта House, позволяет его редактировать.
    /// </summary>
    public partial class HouseViewUserControl : UserControl
    {
        House house;
        public HouseViewUserControl()
        {
            InitializeComponent();
        }
        public HouseViewUserControl(ref House houseElement) : this()
        {
            house = houseElement;
            Grid_Main_HouseView.DataContext = house;
        }

        /// <summary>
        /// Включает и отключает поля ввода (изменения объекта)
        /// </summary>
        public void InputIsEnabled(bool IsEnabled)
        {
            foreach (var item in Grid_Main_HouseView.Children)
            {
                if (item is Control element) element.IsEnabled = IsEnabled;
            }
        }
    }
}
