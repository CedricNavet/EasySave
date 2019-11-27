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
            base.FunctionFirstPosition();
        }

        protected override void FunctionSecondPosition()
        {
            base.FunctionSecondPosition();
        }
    }
}
