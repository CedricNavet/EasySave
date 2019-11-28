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

        private string stringAddingToDisplay;
        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Sequential Save", ArrowPosition.Top),
            new MenuAction("Unique Save", ArrowPosition.Middle),
            new MenuAction("Return", ArrowPosition.Down),
        };
        private string pathJson;

        public DisplaySave(string pathJson)
        {
            this.pathJson = pathJson;
            stringAddingToDisplay = "";
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
            Console.WriteLine("Display :");
            IList<Backups> savedData = Tools.JsonToObject<Backups>(Tools.ReadData(pathJson));

            for (int i = 0; i < savedData.Count(); i++)
            {
                Console.WriteLine("BackupName : {0}, Source : {1}, Target : {2}, LastSavedOn : {3}, SaveType : {4}", savedData[i].BackupsName, savedData[i].Source, savedData[i].Target, savedData[i].TimeToSave, savedData[i].BackupType);
                Console.WriteLine("                                -----------------------------------------------------------------------------                           ");
            }
        }

        protected override void FunctionFirstPosition()
        {
            BackGroundSave groundSave = new BackGroundSave();
            groundSave.StartSave(pathJson, BackGroundSave.SaveType.sequential);
            groundSave.Dispose();
        }

        protected override void FunctionSecondPosition()
        {
            string nameToSave;
            IList<string> allName = new List<string>();
            Console.WriteLine("TRAVAUX DE SAUVEGARDES ENREGISTRÉS");
            IList<Backups> savedData = Tools.JsonToObject<Backups>(Tools.ReadData(pathJson));
            if(savedData.Count == 0)
            {
                Console.WriteLine("No Save In Memory");
                Thread.Sleep(1000);
                return;
            }

            for (int i = 0; i < savedData.Count; i++)
            {
                Console.WriteLine("Fichier n°{0} : {1}", i, savedData[i].BackupsName);
                allName.Add(savedData[i].BackupsName);
            }

            do
            {
                Console.WriteLine("Veuillez entrer le nom de votre Fichier à sauvegarder :");
                nameToSave = Console.ReadLine();
            } while (!allName.Contains(nameToSave));

           Console.WriteLine(nameToSave);

            BackGroundSave groundSave = new BackGroundSave();
            groundSave.StartSave(pathJson, BackGroundSave.SaveType.unique, nameToSave);
            groundSave.Dispose();
        }

    }
}
