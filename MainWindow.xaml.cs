using System.Collections.ObjectModel;
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
        ObservableCollection<House> houses = new ObservableCollection<House>();

        public MainWindow()
        {
            InitializeComponent();
            init_HouseViewUserControl();
            init_ListBox_AllHouses();


        }

        /// <summary>
        /// Инициализация пользовательского элемента упралвения отображения данных объекта House.
        /// </summary>
        private void init_HouseViewUserControl()
        {
            HouseViewUserControl houseView =  new HouseViewUserControl(ref house);
            houseView.Margin = new Thickness(5);
            Grid.SetColumn(houseView, 2);

            Grid_Main.Children.Add(houseView);
        }

        /// <summary>
        /// Инициализация ListBox с объектами House.
        /// </summary>
        private void init_ListBox_AllHouses()
        {
            ListBox_AllHouse.DataContext = houses;
            Btn_Update_Click(this, new RoutedEventArgs());
        }

        /// <summary>
        /// Обработчик кнопки Обновить.
        /// Запрашивает все объекты и обновляет коллекцию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            houses.Clear();
            foreach (House item in House.GetAllHouses()) houses.Add(item);

        }
    }
}