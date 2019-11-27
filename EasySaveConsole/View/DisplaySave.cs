using EasySaveConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    public class DisplaySave : Menu
    {

        private bool isFinish = false;
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
            while (!isFinish)
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
            //display = Console.ReadLine();
            //} while ((Tools.IsValidJson<Logs>(display))==true); //tester si la string est ok, vérifier que c'est un .JSON

            //IList<Logs> temp = Tools.JsonToObject<Logs>(display); //retourne une liste
            ////print la liste dans la console avec un foreach
            //int count = 0;
            //foreach (Logs element in temp)
            //{
            //    count++;
            //    Console.WriteLine($"{count}:{element}");
            //}
            //Console.WriteLine($"Nombre d'élément {count}");
        }

        protected override void FunctionFirstPosition()
        {
            BackGroundSave groundSave = new BackGroundSave();
            groundSave.StartSave(pathJson, BackGroundSave.SaveType.sequential);
        }

        protected override void FunctionSecondPosition()
        {
            string nameToSave;
            IList<string> allName = new List<string>();
            Console.WriteLine("SAVED WORK");
            IList<Backups> savedData = Tools.JsonToObject<Backups>(Tools.ReadData(pathJson));

            for (int i = 0; i < savedData.Count; i++)
            {
                Console.WriteLine("Work n°{0} : {1}", i, savedData[i].BackupsName);
                allName.Add(savedData[i].BackupsName);
            }

            do
            {
                Console.WriteLine("Enter the name of the work you want to save :");
                nameToSave = Console.ReadLine();
            } while (!allName.Contains(nameToSave));

           Console.WriteLine(nameToSave);

            BackGroundSave groundSave = new BackGroundSave();
            groundSave.StartSave(pathJson, BackGroundSave.SaveType.unique);
        }

    }
}
