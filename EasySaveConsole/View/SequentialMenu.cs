using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    class SequentialMenu : Menu
    {
        private bool isFinish = false;
        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Display Sequence", ArrowPosition.Top),
            new MenuAction("Create Sequence", ArrowPosition.Middle),
            new MenuAction("Return", ArrowPosition.Down),
        };

        public SequentialMenu()
        {
            ShowMenu();
        }

        private void ShowMenu()
        {
            while (!isFinish)
            {
                DrawMenu(menuAction);
            }
        }
        protected override void DrawMenu(List<MenuAction> menuAction)
        {
            base.DrawMenu(menuAction);
        }

        protected override void CheckKey(ConsoleKey consoleKey, List<MenuAction> menuAction)
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
                if (arrowPosition == ArrowPosition.Top)
                {
                    SequentialDisplay();
                }
                else if (arrowPosition == ArrowPosition.Middle)
                {
                    SequentialCreation();
                }
                else if (arrowPosition == ArrowPosition.Down)
                {
                    //retour
                }
            }
        }

        private void SequentialCreation()
        {
            throw new NotImplementedException();
        }

        private void SequentialDisplay()
        {
            throw new NotImplementedException();
        }
    }
    }

