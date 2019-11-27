using EasySaveConsole.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySaveConsole.View
{
    public class SaveCreation : Menu
    {
        string temp;
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
            Console.WriteLine("Choissisez votre Dossier Source");
            save.Source = Console.ReadLine();
            Console.WriteLine("Choissisez votre Dossier de destination");
            save.Target = Console.ReadLine();
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
            var jsonFile = JsonConvert.DeserializeObject<List<Backups>>(Tools.ReadData(@"..\..\..\EasySaveConsole\SaveState\InMemorySave.json"));
            if (jsonFile == null)
            {
                temp = Tools.ObjectToJson<Backups>(save);
            }
            else
            {
                jsonFile.Add(save);
                temp = Tools.ObjectToJson<List<Backups>>(jsonFile);
            }
            
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