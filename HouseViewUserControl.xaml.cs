using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db
{
    /// <summary>
    /// Визуально отображает данные переданного объекта House, позволяет его редактировать.
    /// </summary>
    public partial class HouseViewUserControl : UserControl ,INotifyPropertyChanged
    {
        public House House ;

        private bool inputEnabled = false;
        public bool InputEnabled
        {
            get => inputEnabled;
            private set 
            {
                if (inputEnabled != value)
                {
                    inputEnabled = value;
                    OnPropertyChanged(nameof(InputEnabled));
                }
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HouseViewUserControl()
        {
            InitializeComponent();
        }
        public HouseViewUserControl(ref House houseElement) : this()
        {
            ViewHouse(ref houseElement);
        }

        /// <summary>
        /// Включает и отключает поля ввода (изменение объекта)
        /// </summary>
        public void InputIsEnabled(bool IsEnabled)
        {
            foreach (var item in Grid_HouseValues.Children)
            {
                if (item is Control element) element.IsEnabled = IsEnabled;
            }
            TextBox_Id.IsEnabled = false;
            InputEnabled = IsEnabled;
        }

        /// <summary>
        /// Отображет новый объект.
        /// </summary>
        public void ViewHouse(ref House houseElement)
        {
            this.House = houseElement;
            Grid_Main_HouseView.DataContext = this.House;
        }
    }
}
