using EasySaveConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{

    
    public class View : Menu
    { 
        private readonly Model.Model model;
        private string jsonSave;
        private string path = @"..\..\..\EasySaveConsole\SaveState\InMemorySave.json";
        //private SequentialMenu sequentialMenu;

        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Display Saves", ArrowPosition.Top, typeof(DisplaySave)),
            new MenuAction("Save Creation", ArrowPosition.Middle, typeof(SaveCreation)),
            new MenuAction("Exit", ArrowPosition.Down),
        };

        public View(Model.Model model)
        {
            this.model = model;
        }

        public void Menu()
        {     
            Console.CursorVisible = false;

            /*List<Backups> backups = new List<Backups>();
            Backups backups1 = new Backups() { BackupsName = "Temp", BackupType = BackupType.mirror, Source = @"C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes fichiers\autres", Target = @"C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes fichiers\autres", TimeToSave = new DateTime(2019, 11, 30, 2, 2, 2) };
            backups.Add(backups1);
            Backups backups2 = new Backups() { BackupsName = "TrucDeux", BackupType = BackupType.mirror, Source = @"C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes fichiers\autres", Target = @"C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes fichiers\autres", TimeToSave = new DateTime(2019, 11, 30, 2, 2, 2) };
            backups.Add(backups2);
            string temp = Tools.ObjectToJson<List<Backups>>(backups);
            Tools.WriteData(temp, @"..\..\..\EasySaveConsole\SaveState\InMemorySave.json");*/
            //do
            //{
            //    Console.Clear();
            //    /*Console.WriteLine("Give the folder where is InMemorySave.json");
            //    path = Console.ReadLine();*/
            //    string pathJson = path;
            //    jsonSave = Tools.ReadData(path);
            //} while (false);// Check if is json is correct
            jsonSave = Tools.ReadData(path);
            Tools.IsValidJson<Backups>(jsonSave);

            Console.Clear();

            while (!IsFinsih)
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
