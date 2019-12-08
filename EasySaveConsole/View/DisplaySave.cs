using EasySaveConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    public class DisplaySave : Menu
    {
        private string lang;
        private string stringAddingToDisplay;
        List<MenuAction> menuAction;
        private string pathJson;
        private IList<Backups> savedData;

        public DisplaySave(string pathJson, string lang)
        {
            this.lang = lang;
            this.pathJson = pathJson;
            stringAddingToDisplay = "";

            switch (lang)
            {
                case "EN":
                    menuAction = new List<MenuAction>() {
                    new MenuAction("Sequential Save", ArrowPosition.Top),
                    new MenuAction("Unique Save", ArrowPosition.Middle),
                    new MenuAction("Return", ArrowPosition.Down),
                    };
                    break;
                case "FR":
                    menuAction = new List<MenuAction>() {
                    new MenuAction("Sauvegarde séquentielle", ArrowPosition.Top),
                    new MenuAction("Sauvegarde Unique", ArrowPosition.Middle),
                    new MenuAction("Retour", ArrowPosition.Down),
                    };
                    break;
            }


            ShowMenu();
        }

        private void ShowMenu()
        {
            while (!IsFinish)
            {
                SequentialDisplay();
                DrawMenu(menuAction, stringAddingToDisplay);
            }
        }
        private void SequentialDisplay()
        {
            //String display;
            //do
            //{
            Console.Clear();

            switch (lang)
            {
                case "EN":
                    Console.WriteLine("Display :");
                    savedData = Tools.JsonToObject<Backups>(Tools.ReadData(pathJson));

                    for (int i = 0; i < savedData.Count(); i++)
                    {
                        Console.WriteLine("BackupName : {0}, Source : {1}, Target : {2}, LastSavedOn : {3}, SaveType : {4}", savedData[i].BackupsName, savedData[i].Source, savedData[i].Target, savedData[i].TimeToSave, savedData[i].BackupType);
                        Console.WriteLine("                                -----------------------------------------------------------------------------                           ");
                    }
                    break;
                case "FR":
                    {
                        Console.WriteLine("Affichage :");
                        IList<Backups> savedData = Tools.JsonToObject<Backups>(Tools.ReadData(pathJson));

                        for (int i = 0; i < savedData.Count(); i++)
                        {
                            Console.WriteLine("Nom : {0}, Source : {1}, Destination : {2}, Dernière sauvegarde : {3}, Type de sauvegarde : {4}", savedData[i].BackupsName, savedData[i].Source, savedData[i].Target, savedData[i].TimeToSave, savedData[i].BackupType);
                            Console.WriteLine("                                -----------------------------------------------------------------------------                           ");
                        }
                    }
                    break;
            }

        }

        protected override void FunctionFirstPosition()
        {
            IList<Backups> savedData = Tools.JsonToObject<Backups>(Tools.ReadData(pathJson));
            if (savedData.Count == 0)
            {
                switch (lang)
                {
                    case "FR":
                        Console.WriteLine("Pas de sauvegarde en mémoire");
                        break;
                    case "EN":
                        Console.WriteLine("No Save In Memory");
                        break;
                }
                Thread.Sleep(1000);
                return;
            }
            BackGroundSave groundSave = new BackGroundSave(lang);
            groundSave.StartSave(pathJson, BackGroundSave.SaveType.sequential);
            groundSave.Dispose();
        }

        protected override void FunctionSecondPosition()
        {
            string nameToSave;
            IList<string> allName = new List<string>();
            Console.Clear();
            IList<Backups> savedData = Tools.JsonToObject<Backups>(Tools.ReadData(pathJson));
            if (savedData.Count == 0)
            {
                switch (lang)
                {
                    case "FR":
                        Console.WriteLine("Pas de sauvegarde en mémoire");
                        break;
                    case "EN":
                        Console.WriteLine("No Save In Memory");
                        break;
                }
                Thread.Sleep(1000);
                return;
            }
            switch (lang)
            {
                case "FR":
                    Console.WriteLine("TRAVAUX DE SAUVEGARDES");
                    break;
                case "EN":
                    Console.WriteLine("SAVED WORK");
                    break;
            }
            for (int i = 0; i < savedData.Count; i++)
            {
                switch (lang)
                {
                    case "FR":
                        Console.WriteLine("Travail n°{0} : {1}", i, savedData[i].BackupsName);
                        break;
                    case "EN":
                        Console.WriteLine("Work n°{0} : {1}", i, savedData[i].BackupsName);
                        break;
                }
                allName.Add(savedData[i].BackupsName);
            }

            do
            {
                switch (lang)
                {
                    case "FR":
                        Console.WriteLine("Entrer le nom du travail que vous voulez sauvegarder : ");
                        break;
                    case "EN":
                        Console.WriteLine("Enter the name of the work you want to save :");
                        break;
                }
                nameToSave = Console.ReadLine();
            } while (!allName.Contains(nameToSave));

            Console.WriteLine(nameToSave);

            BackGroundSave groundSave = new BackGroundSave(lang);
            groundSave.StartSave(pathJson, BackGroundSave.SaveType.unique, nameToSave);
            groundSave.Dispose();
        }

    }
}
