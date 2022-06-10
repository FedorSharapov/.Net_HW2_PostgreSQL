using System;
using System.Collections.Generic;

namespace PostgreSQLTutorial.ConsoleMenu
{
    class Menu
    {
        List<Item> _items = new List<Item>();
        int _curNumItem = 0;

        public Menu(string headerName)
        {
            Console.Title = headerName;
            Console.CursorVisible = false;
        }

        public void Add(Item item)
        {
            _items.Add(item);
            item.Number = _items.Count - 1;

            if (_items.Count == 1)
                _items[0].Select(true);
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
            _items[_curNumItem].Select(false);

            if (_curNumItem == _items.Count - 1)
                _curNumItem = 0;
            else
                _curNumItem++;

            _items[_curNumItem].Select(true);
        }
        void StepUp()
        {
            _items[_curNumItem].Select(false);

            if (_curNumItem == 0)
                _curNumItem = _items.Count - 1;
            else
                _curNumItem--;

            _items[_curNumItem].Select(true);
        }
        void StepIn()
        {
            Display();

            ConsoleHelper.DisplayHeader(_items[_curNumItem].Name);

            _items[_curNumItem].Enter();

            WaitingStepOut();
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
                    {
                        Display();
                        break;
                    }
                    else if (key == ConsoleKey.Escape)
                        Exit();
                }
            }
        }

        public void Updating()
        {
            Display();

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
