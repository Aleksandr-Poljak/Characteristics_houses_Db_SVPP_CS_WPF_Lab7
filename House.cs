using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db_
{
    public class House: INotifyPropertyChanged
    {
        int id = 0;
        string city = string.Empty;
        string street = string.Empty;
        int number = 0;
        int? flat = null;
        bool hasElevator = false;
        int? floor = null;
        int? tel = null;
        string? owner = null;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id 
        {   get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string City 
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public string Street 
        { 
            get => street; 
            set
            {
                street = value;
                OnPropertyChanged(nameof(Street));
            }
        }
        public int Number 
        { 
            get => number; 
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public int? Flat 
        { 
            get => flat;
            set
            {
                flat = value;
                OnPropertyChanged(nameof(Flat));
            }
        }
        public bool HasElevator 
        { 
            get => hasElevator;
            set 
            {
                hasElevator = value;
                OnPropertyChanged(nameof(HasElevator));
            }
        }
        public int? Floor
        {
            get => floor;
            set
            {
                floor = value;
                OnPropertyChanged(nameof(Floor));
            } 
        }
        public int? Tel 
        { 
            get => tel; 
            set
            {
                tel = value;
                OnPropertyChanged(nameof(Tel));
            }   
        }
        public string? Owner 
        {
            get => owner; 
            set
            {
                owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }

        public House(int id, string city, string street, int number, 
            bool hasElevator = false, int? flat = null, int? floor = null,  
            string? owner1 = null, int? tel = null)
        {
            Id = id;
            City = city;
            Street = street;
            Number = number;
            Flat = flat;
            HasElevator = hasElevator;
            Floor = floor;
            Tel = tel;
            Owner = owner1;
        }

        public void OnPropertyChanged([CallerMemberName] string prop="")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Udpade()
        {

        }
        
        public void Insert()
        {

        }
             
        public static void Delete()
        {

        }

        public static void Find()
        {

        }

        public override string ToString()
        {
            string info = $"Id- {id.ToString()} City- {City}, Street- {Street}, Number- {Number}\n" +
                $"Flat- {Flat?.ToString()}, HasElevator- {HasElevator}, Floor- {Floor?.ToString()}" +
                $"Tel: {Tel?.ToString()}\nOwner- {owner?.ToString()}";
            return info;
        }
    }
}
