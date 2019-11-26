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
        private string path;
        private SequentialMenu sequentialMenu;

        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Occasional Save", ArrowPosition.Top, typeof(OneSaveMenu)),
            new MenuAction("Sequential Save", ArrowPosition.Middle, typeof(Model.Logs)),
            new MenuAction("Quitter", ArrowPosition.Down),
        };

        public View(Model.Model model)
        {
            this.model = model;
        }

        public void Menu()
        {     
            Console.CursorVisible = false;

            List<Backups> backups = new List<Backups>();
            Backups backups1 = new Backups() { BackupsName = "Temp", BackupType = BackupType.mirror, Source = @"C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes fichiers\autres", Target = @"C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes fichiers\autres", TimeToSave = new DateTime(2019, 11, 30, 2, 2, 2) };
            backups.Add(backups1);
            string temp = Model.Tools.ObjectToJson<List<Backups>>(backups);
            Console.WriteLine(temp);
            //Model.Tools.WriteData(temp, @"C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes Devoirs\Autres\C#\EasySave\EasySaveConsole\SaveState\InMemorySave.json");
            do
            {
                Console.Clear();
                Console.WriteLine("Give the folder where is InMemorySave.json");
                path = Console.ReadLine();
                string pathJson = path + @"\InMemorySave.json";
                jsonSave = Model.Tools.ReadData(pathJson);
            } while (false);// Check if is json is correct
                        
            Thread Savethread = new Thread(SaveThread);
            Savethread.Start();

            Console.Clear();

            SequentialMenu sequentialMenu = new SequentialMenu();
            //while (!IsFinsih)
            //{
            //    DrawMenu(menuAction);
            //}
            //Savethread.Suspend();
        }

        protected override void FunctionFirstPosition()
        {
            menuAction[0].Instanciate();
        }

        protected override void FunctionSecondPosition()
        {
            menuAction[1].Instanciate();
        }

        public void SaveThread()
        {
            BackGroundSave save = new BackGroundSave();
            save.StartSave(jsonSave, path);
        }
    }

    
}
