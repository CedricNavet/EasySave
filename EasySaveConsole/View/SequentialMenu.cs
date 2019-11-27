using EasySaveConsole.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySaveConsole.View
{
    public class SequentialMenu : Menu
    {
        private bool isFinish = false;
        private string stringAddingToDisplay;
        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Display Sequence", ArrowPosition.Top),
            new MenuAction("Create Sequence", ArrowPosition.Middle),
            new MenuAction("Return", ArrowPosition.Down),
        };
        private string pathJson;

        public SequentialMenu(string pathJson)
        {
            this.pathJson = pathJson;
            stringAddingToDisplay = "";
            ShowMenu();
        }

        private void ShowMenu()
        {
            while (!isFinish)
            {
                DrawMenu(menuAction, stringAddingToDisplay);
            }
        }

        protected override void FunctionFirstPosition()
        {
            SequentialDisplay();
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
            Tools.backUp(save, @"..\..\..\EasySaveConsole\SaveState\");
            Tools.WriteData(temp, @"..\..\..\EasySaveConsole\SaveState\InMemorySave.json");
        }

        private void SequentialDisplay()
        {
            String display;
            do
            {
                Console.Clear();
                Console.WriteLine("Display :");
                IList<Backups> savedData = Tools.JsonToObject<Backups>(Tools.ReadData(pathJson));

                for (int i = 0; i < savedData.Count(); i++)
                {
                    Console.WriteLine("BackupName : {0}, Source : {1}, Target : {2}, LastSavedOn : {3}, SaveType : {4}", savedData[i].BackupsName, savedData[i].Source, savedData[i].Target, savedData[i].TimeToSave, savedData[i].BackupType);
                    Console.WriteLine("                                -----------------------------------------------------------------------------                           ");
                }
                display = Console.ReadLine();
            } while ((Tools.IsValidJson<Logs>(display))==true); //tester si la string est ok, vérifier que c'est un .JSON
          
            IList<Logs> temp = Tools.JsonToObject<Logs>(display); //retourne une liste
            //print la liste dans la console avec un foreach
            int count = 0;
            foreach (Logs element in temp)
            {
                count++;
                Console.WriteLine($"{count}:{element}");
            }
            Console.WriteLine($"Nombre d'élément {count}");
        }
   
    }
}