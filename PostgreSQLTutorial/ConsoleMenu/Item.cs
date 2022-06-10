using System;
using System.Collections.Generic;
using System.Text;

namespace PostgreSQLTutorial.ConsoleMenu
{
    class Item
    {
        bool _selected = false;

        public int Number { get; set; }
        public string Name { get; }
        public Action EnterFunction { get; set; }

        public Item(string name)
        {
            Name = name;
        }
        public Item(string name, Action enterFunction) : this(name)
        {
            EnterFunction = enterFunction;
        }

        public void Select(bool value)
        {
            _selected = value;
            Console.SetCursorPosition(0, Number);
            Display();
        }
        public void Display()
        {
            if (_selected)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine($" {Name} ");

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"  {Name}");
            }
        }
        public void Enter()
        {
            EnterFunction?.Invoke();
        }
    }
}
