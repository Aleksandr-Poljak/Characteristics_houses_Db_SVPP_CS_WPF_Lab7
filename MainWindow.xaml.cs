﻿using SVPP_CS_WPF_Lab7_Characteristics_houses_Db_;
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

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Класс недвижимоисти 
        House house = new();
        //Пользовательский элемент управления для отображения и редактирования
        //данных объекта недвижимости
        HouseViewUserControl HouseViewUC;

        //Коллекция объектов недвижимости с привязкой к ListBox
        ObservableCollection<House> houses = new ObservableCollection<House>();

        // Окно поиска
        FindHouse? findHouseWindow = null;


        public MainWindow()
        {
            InitializeComponent();
            init_HouseViewUserControl();
            init_DynamicButtons();
            init_ListBox_AllHouses();
        }

        /// <summary>
        /// Инициализация пользовательского элемента управления отображения данных объекта House.
        /// </summary>
        private void init_HouseViewUserControl()
        {
            HouseViewUC = new HouseViewUserControl(ref house);
            HouseViewUC.Name = "MainWindow_HouseViewUC";
            HouseViewUC.Margin = new Thickness(5);
            Grid.SetColumn(HouseViewUC, 2);

            Grid_Main.Children.Add(HouseViewUC);
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
        /// Создает динамические кнопки Сохранить и Отмена.
        /// Кнопки видны, когда пользовательский элемент управления HouseViewUserControl
        /// доступен для ввода данных.
        /// Создает кнопки, добавляет им стиль c сеттерами.
        /// </summary>
        private void init_DynamicButtons()
        {
            List<Button> buttons = createButtonsSaveCancel();

            foreach (Button btn in buttons)
            {
                Button btnCopy = btn;
                // Не передавать элементы по ссылке из списка в цикле foreach.
                installStyleDynamicVisible(ref btnCopy);
                StackPanel_BtnSaveCancel.Children.Add(btnCopy);
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки Обновить.
        /// Запрашивает все объекты и обновляет коллекцию.
        /// </summary>
        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            houses.Clear();
            HouseViewUC.InputIsEnabled(false);
            foreach (House item in House.GetAllHouses()) houses.Add(item);

        }

        /// <summary>
        /// Обработчик события нажатия кнопки Просмотр
        /// </summary>
        private void Btn_View_Click(object sender, RoutedEventArgs e)
        {
            HouseViewUC.InputIsEnabled(false);
            House item = (House)ListBox_AllHouse.SelectedItem;
            if (item is not null)
                HouseViewUC.ViewHouse(ref item);
        }

        /// <summary>
        /// Обработчик события нажатия кнопки Вставить.
        /// Открывает диалоговое окно для ввода данных. Сохранят введенные данные
        /// в базе данных и в коллекции объектов.
        /// </summary>
        private void Btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            HouseViewUC.InputIsEnabled(false);
            NewHouse newHouseWindow = new();
            if (newHouseWindow.ShowDialog() == true)
            {
                newHouseWindow.house.Insert();
                houses.Add(newHouseWindow.house);
            }
        }

        /// <summary>
        /// Обработчик события кнопки Редактировать.
        /// Включает поля HouseViewUserContro для редактирования. 
        /// </summary>
        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            House item = (House)ListBox_AllHouse.SelectedItem;
            if (item is not null && item.Id != 0)
            {
                HouseViewUC.ViewHouse(ref item);
                HouseViewUC.InputIsEnabled(true);
            }

        }

        /// <summary>
        /// Обработчик события нажатия кнопки Удалить.
        /// Удаляет объект из базы данных и ListBox.
        /// </summary>
        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            HouseViewUC.InputIsEnabled(false);
            House item = (House)ListBox_AllHouse.SelectedItem;
            if (item is not null)
            {
                MessageBoxResult result = MessageBox.Show($"Удалить\n{item.ToString()}?",
                    "Удалить", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    item.Delete();
                    Btn_Update_Click(sender, e);
                }
            }


        }

        /// <summary>
        /// Обработчик события нажатия кнопки Найти.
        /// Открывает окно поиска
        /// </summary>
        private void Btn_Find_Click(object sender, RoutedEventArgs e)
        {
            HouseViewUC.InputIsEnabled(false);
            // Окно поиска не модальное, но может быть открыто только в одном экземпляре.
            if (findHouseWindow is not null)
            {
                findHouseWindow.Focus();
            }
            else
            {
                houses.Clear();

                findHouseWindow = new FindHouse() { Owner = this };
                findHouseWindow.Closed += (EventHandler)((sender, e) => this.findHouseWindow = null);
                // Обработчик события очищает коллекцию связанную с ListBox.
                // Заполняет коллекцию объектами поиска
                findHouseWindow.FindingCompletionEvent +=
                    (sender, e) =>
                    {
                        houses.Clear();
                        foreach (House item in findHouseWindow.Houses) houses.Add(item);
                    };

                findHouseWindow.Show();
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки Сохранить.
        /// Обновляет данные в базе данных и ListBox.
        /// </summary>
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            HouseViewUC.InputIsEnabled(false);
            HouseViewUC.House.Update();

            Btn_Update_Click(this, e);
        }

        /// <summary>
        /// Обработчик нажатия Отменить при редактировании данных.
        /// </summary>
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            HouseViewUC.InputIsEnabled(false);
        }

        /// <summary>
        /// Устанавливает стиль кнопкам.
        /// Кнопки видны когда пользовательский элемент  управления HouseViewUserControl доступен
        /// доступен для редактирования данных.
        /// </summary>
        private void installStyleDynamicVisible(ref Button btn)
        {
            Style btnStyle = new Style(typeof(Button));
            btnStyle.Setters.Add(new Setter(Button.VisibilityProperty, Visibility.Collapsed));

            Binding visibilityBinding = new Binding("InputEnabled")
            {
                //Кнопка видна, когда пользовательский элемент
                //управления HouseViewUserControl доступен  для редактирования данных.
                Source = HouseViewUC,
                Converter = new BooleanToVisibilityConverter()
            };
            btnStyle.Setters.Add(new Setter(Button.VisibilityProperty, visibilityBinding));

            btn.Style = btnStyle;
        }

        /// <summary>
        /// Создает две динамические кнопки Сохранить и Отмена.
        /// Возвращает конпки в списке.
        /// </summary>
        private List<Button> createButtonsSaveCancel()
        {
            List<Button> buttonsLst = new List<Button>();

            FontFamily fontFamily = (FontFamily)
                new FontFamilyConverter().ConvertFromString("Comic Sans MS")!;
            double fontSize = 14;
            Thickness padding = new(3);
            Brush foreground = Brushes.WhiteSmoke;


            Button btSave = new Button()
            {
                Name = "Btn_Save",
                Content = "Сохранить",
                Background = Brushes.DarkBlue,
                FontSize = fontSize,
                FontFamily = fontFamily,
                Padding = padding,
                Foreground = foreground,
            };
            btSave.Click += Btn_Save_Click;

            Button btCancel = new Button()
            {
                Name = "Btn_Cancel",
                Content = "Отмена",
                Background = Brushes.DarkRed,
                FontSize = fontSize,
                FontFamily = fontFamily,
                Padding = padding,
                Margin = new Thickness(15, 0, 0, 0),
                Foreground = foreground,
            };
            btCancel.Click += Btn_Cancel_Click;

            // Размер кнопки Отмена равен размеру кнопки Сохранить.
            Binding bndWidth = new Binding()
            { Source = btSave, Path = new PropertyPath("ActualWidth") };
            btCancel.SetBinding(Button.WidthProperty, bndWidth);

            buttonsLst.Add(btSave);
            buttonsLst.Add(btCancel);

            return buttonsLst;
        }
    }
}
