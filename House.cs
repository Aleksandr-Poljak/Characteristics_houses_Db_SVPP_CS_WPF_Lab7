using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db_
{
    public class House
    {
        int id = 0;
        string city = string.Empty;
        string street = string.Empty;
        int number = 0;
        int? flat = null;
        bool hasElevator = false;
        int? floor = null;
        int? tel = null;
        string? Owner = null;

        public int Id { get => id; set => id = value; }
        public string City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }
        public int Number { get => number; set => number = value; }
        public int? Flat { get => flat; set => flat = value; }
        public bool HasElevator { get => hasElevator; set => hasElevator = value; }
        public int? Floor { get => floor; set => floor = value; }
        public int? Tel { get => tel; set => tel = value; }
        public string? Owner1 { get => Owner; set => Owner = value; }

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
            Owner1 = owner1;
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
                $"Tel: {Tel?.ToString()}\nOwner- {Owner?.ToString()}";
            return info;
        }
    }
}
