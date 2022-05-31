using System;
using System.Collections.Generic;

namespace PostgreSQLTutorial.ConsoleMenu
{
    class Menu
    {
        LinkedList<Item> _items = new LinkedList<Item>();
        LinkedListNode<Item> _currentItem;

        public Menu(string headerName)
        {
            Console.Title = headerName;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Add(Item item)
        {
            _items.AddLast(item);

            if (_items.Count == 1)
            {
                _currentItem = _items.First;
                _currentItem.Value.Selected = true;
            }
        }

        public void Display()
        {
            Console.Clear();

            foreach (var i in _items)
                i.Display();

            ConsoleHelper.MsgMenuControl();
        }

        public void Navigation(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    StepDown();
                    break;
                case ConsoleKey.UpArrow:
                    StepUp();
                    break;
                case ConsoleKey.Enter:
                    StepIn();
                    break;
                case ConsoleKey.Escape:
                    Exit();
                    break;
            }
        }
        void StepDown()
        {
            _currentItem.Value.Selected = false;

            if (_currentItem.Next == null)
                _currentItem = _items.First;
            else
                _currentItem = _currentItem.Next;

            _currentItem.Value.Selected = true;

            Display();
        }
        void StepUp()
        {
            _currentItem.Value.Selected = false;

            if (_currentItem.Previous == null)
                _currentItem = _items.Last;
            else
                _currentItem = _currentItem.Previous;

            _currentItem.Value.Selected = true;

            Display();
        }
        void StepIn()
        {
            ConsoleHelper.DisplayHeader(_currentItem.Value.Name);

            _currentItem.Value.Enter();

            WaitingStepOut();

            Display();
        }
        void WaitingStepOut()
        {
            ConsoleHelper.MsgMenuStepOutOrExit();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Backspace)
                        break;
                    else if (key == ConsoleKey.Escape)
                        Exit();
                }
            }
        }

        public void Updating()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                    Navigation(Console.ReadKey().Key);
            }
        }

        void Exit()
        {
            Environment.Exit(0);
        }
    }
}
