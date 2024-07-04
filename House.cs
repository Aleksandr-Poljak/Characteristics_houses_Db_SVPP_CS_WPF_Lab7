using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SVPP_CS_WPF_Lab7_Characteristics_houses_Db
{
    public class House: INotifyPropertyChanged, IDataErrorInfo
    {

        static DbManager dbManager = new DbManager("DefaultConnection");

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

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case nameof(City):
                        if (City.Length > 50 || City.Length == 0)
                            error = "Неверная длина!";
                        break;
                    case nameof(Street):
                        if (Street.Length > 50 || Street.Length == 0)
                            error = "Неверная длина!";
                        break;
                    case nameof(Number):
                        if (Number < 1)
                            error = "Неверно указан номер дома";
                        break;
                    case nameof(Flat):
                        if(Flat is int f && f < 1)
                            error = "Неверно указан номер квартиры";
                        break;
                    case nameof(Floor):
                        if (Floor is int fl && fl < 0)
                            error = "Неверно указан этаж";
                        break;
                    case nameof(Owner):
                        if (Owner is string && Owner.Length > 50)
                            error = "Слишком длинное поле!";
                        break;

                }

                return error;
            }
        }

        public House() { }
        public House(string city, string street, int number,
            bool hasElevator = false, int? flat = null, int? floor = null,
            string? owner1 = null, int? tel = null) : this()
        {
            City = city;
            Street = street;
            Number = number;
            Flat = flat;
            HasElevator = hasElevator;
            Floor = floor;
            Tel = tel;
            Owner = owner1;
        }

        static House()
        {
            SeedRandomData();
        }

        public void OnPropertyChanged([CallerMemberName] string prop="")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Update()
        {
            using(var conn = dbManager.GetNewConnection())
            {
                string sqlStr = "Update Housing SET " +
                    "City=@City, Street=@Street, Number=@Number, Flat=@Flat, Floor=@Floor, " +
                    "HasElevator=@HasElevator, Tel=@Tel, OwnerFIO=@OwnerFIO " +
                    "WHERE(Id=@Id)";
                SqlCommand sqlCmd_Update = new(sqlStr, conn);

                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("Id", Id),
                    new SqlParameter("City", City),
                    new SqlParameter("Street", Street),
                    new SqlParameter("Number", Number),
                    new SqlParameter("Flat", (object)Flat ?? DBNull.Value),
                    new SqlParameter("HasElevator", HasElevator),
                    new SqlParameter("Floor", (object)Floor ?? DBNull.Value),
                    new SqlParameter("Tel", (object)Tel ?? DBNull.Value),
                    new SqlParameter("OwnerFIO", (object)Owner ?? DBNull.Value),
                };

                sqlCmd_Update.Parameters.AddRange(param);
                sqlCmd_Update.ExecuteNonQuery();
            }

        }
        
        /// <summary>
        /// Сохрнаяет объект в базе данных.
        /// </summary>
        public void Insert()
        {
            using(SqlConnection conn = dbManager.GetNewConnection())
            {
                string sqlStr = "INSERT INTO Housing " +
                    "(City, Street, Number, Flat, HasElevator, Floor, Tel, OwnerFIO)" +
                    " VALUES (@City, @Street, @Number, @Flat, @HasElevator, @Floor, @Tel, @OwnerFIO);" +
                    "SET @id=SCOPE_IDENTITY()";

                SqlCommand sqlCmd_Insert = new(sqlStr, conn);
                //--Параметры--
                // Выходной параметр c id записи.
                SqlParameter idParam = new("id", System.Data.SqlDbType.Int);
                idParam.Direction = System.Data.ParameterDirection.Output;
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("City", City),
                    new SqlParameter("Street", Street),
                    new SqlParameter("Number", Number),
                    new SqlParameter("Flat", (object)Flat ?? DBNull.Value),
                    new SqlParameter("HasElevator", HasElevator),
                    new SqlParameter("Floor", (object)Floor ?? DBNull.Value),
                    new SqlParameter("Tel", (object)Tel ?? DBNull.Value),
                    new SqlParameter("OwnerFIO", (object)Owner ?? DBNull.Value),
                    idParam
                };

                sqlCmd_Insert.Parameters.AddRange(param);
                sqlCmd_Insert.ExecuteNonQuery();
                this.Id = (int) idParam.Value;
            }
        }
         
        /// <summary>
        /// Удаляет объект из базы данных.
        /// </summary>
        public void Delete()
        {
            using (SqlConnection conn = dbManager.GetNewConnection())
            {
                string sqlStr = "DELETE FROM Housing WHERE(Id=@Id)";
                SqlCommand sqlCmd_Delete = new(sqlStr, conn);
                sqlCmd_Delete.Parameters.Add(new SqlParameter("Id", Id));
                sqlCmd_Delete.ExecuteNonQuery();

                this.Id = 0;
                this.City = String.Empty;
                this.Street = String.Empty;
                this.Number = 0;
                this.HasElevator = false;
                this.Owner = null;
                this.tel = null;
                this.Flat = null;
                this.Floor = null;
            }

        }
        
        /// <summary>
        /// Ищет объект в базе данных по id. Возвращает объект или null.
        /// </summary>
        public static House? FindByID(int id)
        {
            House? resultHouse = null;
            using (SqlConnection conn = dbManager.GetNewConnection())
            {
                string sqlStr = "SELECT * FROM Housing WHERE (Id=@Id)";
                SqlCommand sqlCmd_Find = new(sqlStr, conn);
                sqlCmd_Find.Parameters.Add(new SqlParameter("Id", id));
                SqlDataReader reader = sqlCmd_Find.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        House newHouse = new House(
                            (string)reader["City"],
                            (string)reader["Street"],
                            (int)reader["Number"],
                            (bool)reader["HasElevator"],
                            reader["Flat"] is System.DBNull ? null : (int)reader["Flat"],
                            reader["Floor"] is System.DBNull ? null : (int)reader["Floor"],
                            reader["OwnerFIO"] is System.DBNull ? null : (string)reader["OwnerFIO"],
                            reader["Tel"] is System.DBNull ? null : (int)reader["Tel"]
                            );
                        newHouse.Id = (int)reader["Id"];
                        resultHouse = newHouse;
                    }
                }
            }
            return resultHouse;
        }

        /// <summary>
        /// Выполняет поиск в базе данных по городу или городу и улице.
        /// Возвращает список объектов или пустой список.
        /// </summary>
        /// <returns></returns>
        public static List<House> FindByNames(string city, string? street = null)
        {
            List<House> lstHouses = new List<House>();
            string sqlStr = String.Empty;

            using (SqlConnection conn = dbManager.GetNewConnection())
            {
                sqlStr = "SELECT * FROM Housing WHERE (City=@City)";
                if (street != null) sqlStr += " AND (Street=@Street)";

                SqlCommand sqlCmdFind = new(sqlStr, conn);
                sqlCmdFind.Parameters.Add(new SqlParameter("City", city));
                if (street != null) sqlCmdFind.Parameters.Add(
                    new SqlParameter("Street", street)
                    );

                SqlDataReader reader = sqlCmdFind.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        House newHouse = new House(
                            (string)reader["City"],
                            (string)reader["Street"],
                            (int)reader["Number"],
                            (bool)reader["HasElevator"],
                            reader["Flat"] is System.DBNull ? null : (int)reader["Flat"],
                            reader["Floor"] is System.DBNull ? null : (int)reader["Floor"],
                            reader["OwnerFIO"] is System.DBNull ? null : (string)reader["OwnerFIO"],
                            reader["Tel"] is System.DBNull ? null : (int)reader["Flat"]
                            );
                        newHouse.Id = (int)reader["Id"];
                        lstHouses.Add(newHouse);
                    }
                }
            }

            return lstHouses;
        }

        /// <summary>
        /// Возвращает  все записи с базы данных.
        /// </summary>
        public static IEnumerable<House> GetAllHouses()
        {
            using (SqlConnection conn = dbManager.GetNewConnection())
            {
                string sqlStr = "SELECT * FROM Housing";
                SqlCommand sqlCmd_GetAll = new SqlCommand(sqlStr, conn);
                SqlDataReader reader = sqlCmd_GetAll.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        House house = new House(
                            (string)reader["City"],
                            (string)reader["Street"],
                            (int)reader["Number"],
                            (bool)reader["HasElevator"],
                            reader["Flat"] is System.DBNull ? null : (int)reader["Flat"],
                            reader["Floor"] is System.DBNull ? null : (int)reader["Floor"],
                            reader["OwnerFIO"] is System.DBNull ? null : (string)reader["OwnerFIO"],
                            reader["Tel"] is System.DBNull ? null : (int)reader["Tel"]
                            );
                        house.Id = (int) reader["Id"];
                        yield return house;
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет есть ли в базе данных данные,если нет- создает 3 строки.
        /// </summary>
        public static void SeedRandomData()
        {
            string sqlStr = "SELECT COUNT(Id) FROM Housing";
            using(SqlConnection conn = dbManager.GetNewConnection())
            {
                SqlCommand sqlCmd_CountId = new SqlCommand(sqlStr, conn);
                int result = (int) sqlCmd_CountId.ExecuteScalar();
                
                if(result < 2)
                {
                    House tempHouse1 = new House("Минск", "Одинцова", 15, 
                        true, 35, 9, "Василевская А. А.", 56123);
                    House tempHouse2 = new House("Минск", "Вороняского", 20,
                        false, null, null, "Халаев С.А", null);
                    House tempHouse3 = new House("Минск", "Богдановича", 34,
                        false, 10, 5, null, null);
                    tempHouse1.Insert();
                    tempHouse2.Insert();
                    tempHouse3.Insert();
                }
            }

        }

        public override string ToString()
        {
            string info = $"ID {Id}: {City}, ул. {Street}, дом {Number}, кв. {Flat?.ToString()}\n" +
                $"Владелец: {Owner?.ToString()}";
            return info;
        }
    }
}
