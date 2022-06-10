/* 
--------------------PostgreSQLTutorial--------------------
1. Программа создает 3 таблицы в базе данных "Avito".
Таблицы: 
- "Users": информация о пользователе;
- "Products": товар, который продает пользователь.
- "SubwayStations": станция метро, на которой можно забрать товар;
2. Программа заполняет таблицы данными.
3. Программа выводит содержимое таблиц на выбор.
4. Программа поддерживает ввод данных в таблицу на выбор.

Управление:
- DownArrow: шаг по меню вниз;
- UpArrow: шаг по меню вврех;
- Enter: вход в меню;
- Backspace: для возврата в основное меню;
- Escape: выход из программы.
 */

using System;
using PostgreSQLTutorial.ConsoleMenu;

namespace PostgreSQLTutorial
{
    class Program
    {
        const string CONNECTIONSTRING = "Host=localhost;Port=5432;Username=postgres;Password=123456789;Database=Avito;";

        static void Main(string[] args)
        {
            Tables.ConnectionString = CONNECTIONSTRING;

            // проверка связи с базой данных
            if(!Tables.CheckConnection())
            {
                Console.ReadKey();
                return;
            }

            // создание таблиц
            Tables.CreateSubwayStations();
            Tables.CreateUsers();
            Tables.CreateProducts();

            // заполнение таблиц
            Tables.FillingSubwayStations();
            Tables.FillingUsers();
            Tables.FillingProducts();

            // инициализация меню
            Menu menu = new Menu("База данных \"Avito\"");
            // меню с возможностью чтения таблиц из базы данных 
            menu.Add(new Item("Вывод таблицы \"SubwayStations\"", Tables.ReadSubwayStations));
            menu.Add(new Item("Вывод таблицы \"Users\"", Tables.ReadUsers));
            menu.Add(new Item("Вывод таблицы \"Products\"", Tables.ReadProducts));
            // меню с возможностью ввода данных в таблицу с сохранением в базе данных 
            menu.Add(new Item("Добавить в таблицу \"SubwayStations\"", Tables.AddSubwayStation));
            menu.Add(new Item("Добавить в таблицу \"Users\"", Tables.AddUser));
            menu.Add(new Item("Добавить в таблицу \"Products\"", Tables.AddProduct));

            // обновление меню по действиям от пользователя
            menu.Updating();
        }
    }
}
