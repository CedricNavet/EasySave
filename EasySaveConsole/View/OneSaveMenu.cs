using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    public class OneSaveMenu : Menu
    {
        private bool isFinish = false;
        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Save a File", ArrowPosition.Top, typeof(Model.Logs)),
            new MenuAction("Save a Folder", ArrowPosition.Middle, typeof(Model.Logs)),
            new MenuAction("Action 3", ArrowPosition.Down, typeof(Model.Logs)),
        };

        public OneSaveMenu()
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
    }
}
