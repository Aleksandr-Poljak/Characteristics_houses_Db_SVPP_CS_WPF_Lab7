using SVPP_CS_WPF_Lab7_Characteristics_houses_Db;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db_
{
    /// <summary>
    /// Логика взаимодействия для FindHouse.xaml
    /// </summary>
    public partial class FindHouse : Window
    {
        // Результаты поиска
        public List<House> Houses { get; private set; } = new();
        // Событие происходит после окончания поиска
        public EventHandler? FindingCompletionEvent;

        public FindHouse()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки Искать.
        /// Ищет записи в базе данных согласно выбору на радиокнопках и введенным данным.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Find_Click(object sender, RoutedEventArgs e)
        {
            Houses.Clear();

            // Если выбран поиск по Id.
            if(RB_FindId.IsChecked == true)
            {
                House? result = House.FindByID(int.Parse(TextBox_IdInput.Text));
                if(result != null) Houses.Add(result);
            }

            // Если выбран поиск по названию города и улицы.
            if (RB_FindNames.IsChecked == true)
            {   string city = TextBox_CityInput.Text;
                // Улица может быть null. Тогда поиск только по городам.
                string? street = TextBox_StreetInput.Text != string.Empty
                    ? TextBox_StreetInput.Text : null;

                List <House> results = House.FindByNames(city, street);
                if(results.Count > 0) Houses.AddRange(results);
            }
            FindingCompletionEvent?.Invoke(this, new EventArgs());

        }
        /// <summary>
        /// Обработчик события нажатия кнопки Выход. 
        /// </summary>
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки Очистить все.
        /// Очищает поля ввода
        /// </summary>
        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_IdInput.Text = string.Empty;
            TextBox_CityInput.Text = string.Empty;
            TextBox_StreetInput.Text = string.Empty;
        }
    }
}
