using System;
using System.Collections.Generic;
using System.Text;

namespace PostgreSQLTutorial.ConsoleMenu
{
    class Item
    {
        string _name;
        public string Name { get {return _name;} }
        public bool Selected { get; set; }
        public Action EnterFunction { get; set; }

        public Item(string name, Action enterFunction)
        {
            _name = name;
            EnterFunction = enterFunction;
        }
        public void Display()
        {
            if(Selected)
                Console.BackgroundColor = ConsoleColor.DarkGray;      
            
            Console.WriteLine(_name);

            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Enter()
        {
            EnterFunction?.Invoke();
        }
    }
}
