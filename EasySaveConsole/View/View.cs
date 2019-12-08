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
        List<MenuAction> menuAction;
        private string lang;


        public View()
        {
        }

        public void Menu()
        {
           
            do
            {
                Console.Clear();
                Console.WriteLine("Choose your language (FR or EN):");
                this.lang = Console.ReadLine();
                Console.WriteLine(lang);
                if (lang.Equals("FR") || lang.Equals("EN"))
                    {
                    break;
                    }
            } while (!lang.Equals("FR") || !lang.Equals("EN"));

            switch(lang)
            {
                case "EN":
                    menuAction = new List<MenuAction>() {
                    new MenuAction("Display Saves", ArrowPosition.Top, typeof(DisplaySave)),
                    new MenuAction("Save Creation", ArrowPosition.Middle, typeof(SaveCreation)),
                    new MenuAction("Exit", ArrowPosition.Down),
                    };
                    break;
                case "FR":
                    menuAction = new List<MenuAction>() {
                    new MenuAction("Montrer les sauvegardes", ArrowPosition.Top, typeof(DisplaySave)),
                    new MenuAction("Créer un travail de sauvegarde", ArrowPosition.Middle, typeof(SaveCreation)),
                    new MenuAction("Sortir", ArrowPosition.Down),
                    };
                    break;
                default:
                    Console.WriteLine("No");
                    break;
            }

            Console.CursorVisible = false;
            if (!Directory.Exists(@"..\SaveState"))
            {
                Directory.CreateDirectory(@"..\SaveState");
                File.Create(@"..\SaveState\InMemorySave.json").Close();
                File.Create(@"..\SaveState\Logs.json").Close();
                File.Create(@"..\SaveState\SaveProgression.json").Close();
            }
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
            menuAction[0].Instanciate(path, lang);
        }
        protected override void FunctionSecondPosition()
        {
            menuAction[1].Instanciate(path, lang);
        }
    }

    
}
