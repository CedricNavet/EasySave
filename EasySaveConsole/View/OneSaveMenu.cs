using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    public class OneSaveMenu : Menu
    {
        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Save a File", ArrowPosition.Top),
            new MenuAction("Save a Folder", ArrowPosition.Middle),
            new MenuAction("Retour", ArrowPosition.Down),
        };

        public OneSaveMenu()
        {
            ShowMenu();
        }

        private void ShowMenu()
        {
            while (!IsFinsih)
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
            base.CheckKey(consoleKey, menuAction);
        }
    }
}
