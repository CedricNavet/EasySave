using EasySaveConsole.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasySaveConsole.View
{
    public class SaveCreation : Menu
    {
        private bool isFinish = false;
        private string stringAddingToDisplay;
        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Display Sequence", ArrowPosition.Top),
            new MenuAction("Create Sequence", ArrowPosition.Middle),
            new MenuAction("Return", ArrowPosition.Down),
        };
        private string pathJson;

        public SaveCreation(string pathJson)
        {
            this.pathJson = pathJson;
            stringAddingToDisplay = "";
            ShowMenu();
        }

        private void ShowMenu()
        {
            SequentialCreation();
            //while (!isFinish)
            //{
            //    //SequentialDisplay();
            //    //DrawMenu(menuAction, stringAddingToDisplay);
            //}
        }

        protected override void FunctionFirstPosition()
        {
            //SequentialDisplay();
        }
        protected override void FunctionSecondPosition()
        {
            SequentialCreation();
        }

        private void SequentialCreation()
        {
            Backups save = new Backups();
            Console.WriteLine("Choissisez le nom de votre Save :");
            save.BackupsName = Console.ReadLine();
            do
            {
                Console.Clear();
                Console.WriteLine("Choissisez votre Dossier Source");
                save.Source = Console.ReadLine();
                Console.WriteLine("Choissisez votre Dossier de destination");
                save.Target = Console.ReadLine();
            } while (!(Directory.Exists(save.Source) && Directory.Exists(save.Target)));
            
            Console.WriteLine("Choissisez le type de sauvegarde (differential ou mirror) :");
            var list = new string[] { "mirror", "differential" };
            string type = Console.ReadLine();

            while (!list.Contains(type))
            {
                Console.WriteLine("Choissisez le type de sauvegarde (differential ou mirror) :");
                type = Console.ReadLine();
            }
            int typeEnum = 50;
            switch (type)
            {
                case "mirror":
                    typeEnum = 0;
                    break;
                case "differential":
                    typeEnum = 1;
                    break;
            }
            save.BackupType = (BackupType) typeEnum;
            save.TimeToSave = DateTime.Now;
            //string temp = Tools.ObjectToJson<Backups>(save);
            var jsonFile = JsonConvert.DeserializeObject<List<Backups>>(Tools.ReadData(@"..\..\..\EasySaveConsole\SaveState\InMemorySave.json"));
            jsonFile.Add(save);
            var temp = Tools.ObjectToJson<List<Backups>>(jsonFile);
            /*
             * Dans temp = 
             * Rajouter le vrai sytème de BackUp
             * 
             * 
             */
            //Tools.backUp(save, @"..\..\..\EasySaveConsole\SaveState\");
            Tools.WriteData(temp, @"..\..\..\EasySaveConsole\SaveState\InMemorySave.json");
        }

        
   
    }
}