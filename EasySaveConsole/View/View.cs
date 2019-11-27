using EasySaveConsole.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{

    
    public class View : Menu
    { 
        private string jsonSave;
        private readonly string path = @"..\SaveState\InMemorySave.json";
        //private SequentialMenu sequentialMenu;

        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Display Saves", ArrowPosition.Top, typeof(DisplaySave)),
            new MenuAction("Save Creation", ArrowPosition.Middle, typeof(SaveCreation)),
            new MenuAction("Exit", ArrowPosition.Down),
        };

        public View()
        {
        }

        public void Menu()
        {     
            Console.CursorVisible = false;
            if (!Directory.Exists(@"..\SaveState"))
            {
                Directory.CreateDirectory(@"..\SaveState");
                File.Create(@"..\SaveState\InMemorySave.json").Close();
                File.Create(@"..\SaveState\Logs.json").Close();
                File.Create(@"..\SaveState\SaveProgression.json").Close();
            }
            Thread.Sleep(1000);
            jsonSave = Tools.ReadData(path);
            Tools.IsValidJson<Backups>(jsonSave);

            Console.Clear();

            while (!IsFinish)
            {
                DrawMenu(menuAction, "");
            }
        }

        protected override void FunctionFirstPosition()
        {
            menuAction[0].Instanciate(path);
        }
        protected override void FunctionSecondPosition()
        {
            menuAction[1].Instanciate(path);
        }
    }

    
}
