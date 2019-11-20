using EasySaveConsole.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{

    public enum ArrowPosition
    {
        Top,
        Middle,
        Down
    };

    public class Menu
    {
        
        protected ArrowPosition arrowPosition;

        protected virtual void DrawMenu(List<MenuAction> menuAction)
        {
            foreach (MenuAction item in menuAction)
            {
                if (item.ArrowPosition == arrowPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(item.Name);
                }
                else
                {
                    Console.WriteLine(item.Name);
                }
                Console.ResetColor();
            }


            ConsoleKey ckey = Console.ReadKey().Key;
            CheckKey(ckey, menuAction);
        }
        protected virtual void CheckKey(ConsoleKey consoleKey, List<MenuAction> menuAction)
        {
            if (consoleKey == ConsoleKey.DownArrow)
            {
                if (arrowPosition == ArrowPosition.Down)
                {
                    arrowPosition = ArrowPosition.Top;
                    Console.Clear();
                }
                else
                {
                    arrowPosition += 1;
                    Console.Clear();
                }

            }
            else if (consoleKey == ConsoleKey.UpArrow)
            {
                if (arrowPosition == ArrowPosition.Top)
                {
                    arrowPosition = ArrowPosition.Down;
                    Console.Clear();
                }
                else
                {
                    arrowPosition -= 1;
                    Console.Clear();
                }
            }
            else if (consoleKey == ConsoleKey.Enter)
            {
                foreach (MenuAction item in menuAction)
                {
                    if (arrowPosition == item.ArrowPosition)
                    {
                        Console.Clear();
                        item.Instanciate();
                    }
                }
            }
        }

    }
}
