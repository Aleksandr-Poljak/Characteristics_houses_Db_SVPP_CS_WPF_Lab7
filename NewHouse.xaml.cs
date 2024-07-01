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
using System.Windows.Shapes;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db
{
    /// <summary>
    /// Логика взаимодействия для NewHouse.xaml
    /// </summary>
    public partial class NewHouse : Window
    {
        public House house = new(); // Новый объект
        public NewHouse()
        {
            InitializeComponent();
            // Включить поля ввода
            HouseViewUC.InputIsEnabled(true); 
            // Добавить объект для отображения и редактирования
            HouseViewUC.ViewHouse(ref house); 

        }

        private void Bt_Save_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
            this.Close();
        }
    }
}
