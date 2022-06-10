using Npgsql;
using PostgreSQLTutorial.ConsoleMenu;
using PostgreSQLTutorial.Entities;
using System;
using System.Linq;

namespace PostgreSQLTutorial
{
    static class Tables
    {
        public static string ConnectionString { get; set; }

        static Tables()
        {
            Console.CursorVisible = false;
        }

        #region 0. Проверка свзи с БД
        /// <summary>
        /// Проверка связи с базой данных
        /// </summary>
        public static bool CheckConnection()
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString: ConnectionString);
                connection.Open();

                using NpgsqlCommand cmd = new NpgsqlCommand(cmdText: "SELECT version()", connection: connection);
                string affectedRowsCount = cmd.ExecuteNonQuery().ToString();
                Console.WriteLine(value: $"Table created. Affected rows count: {affectedRowsCount}");

                return true;
            }
            catch (Exception ex)
            {
                ConsoleHelper.MsgError("Ошибка: " + ex.ToString());
                return false;
            }
        }
        #endregion

        #region 1. Создание таблицы
        /// <summary>
        /// Создание таблицы
        /// </summary>
        public static void Create(string sql)
        {
            try 
            { 
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString: ConnectionString);
            connection.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText: sql, connection: connection);
            string affectedRowsCount = cmd.ExecuteNonQuery().ToString();
            Console.WriteLine(value: $"Table created. Affected rows count: {affectedRowsCount}");
            }
            catch (Exception ex)
            {
                ConsoleHelper.MsgError("Ошибка: " + ex.ToString());
            }
        }
        /// <summary>
        /// Создание таблицы "SubwayStations"
        /// </summary>
        public static void CreateSubwayStations()
        {
            string sql = @"
CREATE SEQUENCE IF NOT EXISTS subwaystations_id_seq;

CREATE TABLE IF NOT EXISTS ""SubwayStations""
(
    ""Id""              INTEGER                    NOT NULL    DEFAULT NEXTVAL('subwaystations_id_seq'),
    ""Name""            CHARACTER VARYING(32)      NOT NULL,
  
    CONSTRAINT subwaystations_pkey PRIMARY KEY (""Id""),
    CONSTRAINT subwayStations_uniq UNIQUE (""Name"")
);";

            Create(sql);
        }
        /// <summary>
        /// Создание таблицы "Users"
        /// </summary>
        public static void CreateUsers()
        {
            string sql = @"
CREATE SEQUENCE IF NOT EXISTS users_id_seq;

CREATE TABLE IF NOT EXISTS ""Users""
(
    ""Id""                  BIGINT                     NOT NULL    DEFAULT NEXTVAL('users_id_seq'),
    ""FirstName""           CHARACTER VARYING(64)      NOT NULL,
    ""Email""               CHARACTER VARYING(256)     NOT NULL,
    ""PhoneNumber""         CHARACTER VARYING(16)      NOT NULL,
  
    CONSTRAINT users_pkey PRIMARY KEY (""Id""),
    CONSTRAINT users_email_uniq UNIQUE (""Email""),
    CONSTRAINT users_phonenum_uniq UNIQUE (""PhoneNumber"")
);

CREATE UNIQUE INDEX IF NOT EXISTS users_email_uniq_idx ON ""Users""(lower(""Email""));
";

            Create(sql);
        }

        /// <summary>
        /// Создание таблицы "Products"
        /// </summary>
        public static void CreateProducts()
        {
            string sql = @"
CREATE SEQUENCE IF NOT EXISTS products_id_seq;

CREATE TABLE IF NOT EXISTS ""Products""
(
    ""Id""                  BIGINT                      NOT NULL    DEFAULT NEXTVAL('products_id_seq'),
    ""Name""                CHARACTER VARYING(256)      NOT NULL,
    ""Price""               INTEGER                     NOT NULL,
    ""Description""         CHARACTER VARYING(2048),
    ""SubwayStationId""     INTEGER,
    ""UserId""              BIGINT                      NOT NULL,
  
    CONSTRAINT products_pkey PRIMARY KEY (""Id""),
    CONSTRAINT fk_users_id FOREIGN KEY (""UserId"") REFERENCES ""Users"" (""Id"") ON DELETE CASCADE,
    CONSTRAINT fk_subwaystations_id FOREIGN KEY (""SubwayStationId"") REFERENCES ""SubwayStations"" (""Id"") ON DELETE CASCADE);
";

            Create(sql);
        }
        #endregion

        #region 2. Заполнение таблиц
        /// <summary>
        /// Заполнение таблицы "SubwayStations"
        /// </summary>
        public static void FillingSubwayStations()
        {
            string[] subwayStations = new string[] {"Автово","Адмиралтейская","Академическая","Балтийская","Бухарестская","Василеостровская",
                "Владимирская","Волковская","Выборгская","Горьковская","Гостиный двор","Гражданский проспект","Девяткино","Достоевская",
                "Елизаровская","Звёздная","Звенигородская","Кировский завод","Комендантский проспект","Крестовский остров","Купчино",
                "Ладожская","Ленинский проспект","Лесная","Лиговский проспект","Ломоносовская","Маяковская","Международная","Московская",
                "Московские ворота","Нарвская","Невский проспект","Новочеркасская","Обводный канал","Обухово","Озерки","Парк Победы",
                "Парнас","Петроградская","Пионерская","Площадь Александра Невского I","Площадь Восстания","Площадь Ленина","Площадь Мужества",
                "Площадь Невского","Политехническая","Приморская","Пролетарская","Проспект Большевиков","Проспект Ветеранов",
                "Проспект Просвещения","Пушкинская","Рыбацкое","Садовая","Сенная площадь","Спасская","Спортивная","Старая Деревня",
                "Технологический институт I","Технологический институт II","Удельная","Улица Дыбенко","Фрунзенская","Чёрная речка",
                "Чернышевская","Чкаловская","Электросила" };

            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                try
                {
                    foreach (var name in subwayStations)
                        db.SubwayStations.Add(new SubwayStation { Name = name });

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.MsgError(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Заполнение таблицы "Users"
        /// </summary>
        public static void FillingUsers()
        {
            User[] users = new User[5];

            users[0] = new User { FirstName = "Федор", Email = "fedor@mail.ru", PhoneNumber = "+79990000001" };
            users[1] = new User { FirstName = "Анастасия", Email = "anastasia@mail.ru", PhoneNumber = "+79990000002" };
            users[2] = new User { FirstName = "Иван", Email = "ivan@mail.ru", PhoneNumber = "+79990000003" };
            users[3] = new User { FirstName = "Варвара", Email = "varvara@mail.ru", PhoneNumber = "+79990000004" };
            users[4] = new User { FirstName = "Татьяна", Email = "tatiana@mail.ru", PhoneNumber = "+79990000005" };

            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                try
                {
                    foreach (var user in users)
                        db.Users.Add(user);

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.MsgError(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Заполнение таблицы "Products"
        /// </summary>
        public static void FillingProducts()
        {
            Product[] products = new Product[5];

            products[0] = new Product { Name = "Смартфон", Price = 20000, Description = "новый", SubwayStationId = 5, UserId = 1 };
            products[1] = new Product { Name = "Ноутбук", Price = 50000, Description = "бу, в отличном состоянии", SubwayStationId = 12, UserId = 1 };
            products[2] = new Product { Name = "Кроссовер", Price = 2000000, Description = "комплектация Luxe, 2020 г., 1 владелец", SubwayStationId = 1, UserId = 2 };
            products[3] = new Product { Name = "Седан", Price = 1000000, Description = "комплектация Comfort, 2022 г., новый", SubwayStationId = 20, UserId = 2 };
            products[4] = new Product { Name = "Конструктор", Price = 3000, Description = "все детали в комплекте", SubwayStationId = 15, UserId = 3 };

            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                try
                {
                    foreach (var product in products)
                        db.Products.Add(product);

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.MsgError(ex.ToString());
                }
            }
        }

        #endregion

        #region 3. Вывод содержимого таблиц
        /// <summary>
        /// Вывод содержимого таблицы "SubwayStations"
        /// </summary>
        public static void ReadSubwayStations()
        {
            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                var subwayStations = db.SubwayStations.ToList();

                foreach (var s in subwayStations)
                    Console.WriteLine(s.ToString());
            }
        }

        /// <summary>
        /// Вывод содержимого таблицы "Users"
        /// </summary>
        public static void ReadUsers()
        {
            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                var users = db.Users.ToList();

                foreach (var u in users)
                    Console.WriteLine(u.ToString());
            }
        }

        /// <summary>
        /// Вывод содержимого таблицы "Products"
        /// </summary>
        public static void ReadProducts()
        {
            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                var products = db.Products.ToList();

                foreach (var p in products)
                    Console.WriteLine(p.ToString());
            }
        }
        #endregion

        #region 4. Добавление данных в таблицы
        /// <summary>
        /// Добавление станции метро в таблицу
        /// </summary>
        static void AddSubwayStation(SubwayStation station)
        {
            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                db.SubwayStations.Add(station);
                db.SaveChanges();
            }
        }
        public static void AddSubwayStation()
        {
            try
            {
                SubwayStation station = new SubwayStation();

                Console.Write("Введите \"Name\": ");
                station.Name = Console.ReadLine();

                AddSubwayStation(station);

                Console.WriteLine($"Станция метро \"{station.Name}\" добавлена в таблицу \"SubwayStations\"!\r\n");
            }
            catch (Exception ex)
            {
                ConsoleHelper.MsgError(ex.ToString());
            }
        }
        /// <summary>
        /// Добавление пользователя в таблицу
        /// </summary>
        static void AddUser(User user)
        {
            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static void AddUser()
        {
            try
            {
                User user = new User();

                Console.Write("Введите \"FirstName\": ");
                user.FirstName = Console.ReadLine();

                Console.Write("Введите \"Email\": ");
                user.Email = Console.ReadLine();

                Console.Write("Введите \"PhoneNumber\": ");
                user.PhoneNumber = Console.ReadLine();

                AddUser(user);

                Console.WriteLine($"Пользователь \"{user.FirstName}\" добавлен(а) в таблицу \"Users\"!\r\n");
            }
            catch (Exception ex)
            {
                ConsoleHelper.MsgError(ex.ToString());
            }
        }
        /// <summary>
        /// Добавление товара в таблицу
        /// </summary>
        static void AddProduct(Product product)
        {
            using (ApplicationContext db = new ApplicationContext(ConnectionString))
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }
        public static void AddProduct()
        {
            try
            {
                Product product = new Product();

                Console.Write("Введите \"Name\": ");
                product.Name = Console.ReadLine();

                Console.Write("Введите \"Price\": ");
                product.Price = Convert.ToInt32(Console.ReadLine());

                Console.Write("Введите \"Description\": ");
                product.Description = Console.ReadLine();

                Console.Write("Введите \"SubwayStationId\": ");
                product.SubwayStationId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Введите \"UserId\": ");
                product.UserId = Convert.ToInt64(Console.ReadLine());

                AddProduct(product);

                Console.WriteLine($"Товар \"{product.Name}\" добавлен в таблицу \"Products\"!\r\n");
            }
            catch (Exception ex)
            {
                ConsoleHelper.MsgError(ex.ToString());
            }
        }
        #endregion
    }
}
