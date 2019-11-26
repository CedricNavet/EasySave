using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    public class OneSaveMenu : Menu
    {
        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Save a File", ArrowPosition.Top),
            new MenuAction("Save a Folder", ArrowPosition.Middle),
            new MenuAction("Retour", ArrowPosition.Down),
        };

        public OneSaveMenu()
        {
            ShowMenu();
        }

        private void ShowMenu()
        {
            while (!IsFinsih)
            {
                DrawMenu(menuAction);
            }
            
        }

        protected override void FunctionFirstPosition()
        {
            SaveOneFile();
        }
        protected override void FunctionSecondPosition()
        {
            SaveOneFolder();
        }

        private void SaveOneFolder()
        {
            string filepathSource = "";
            string filepathDestination = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Give the folder source :");
                filepathSource = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("Give the folder target :");
                filepathDestination = Console.ReadLine();

            } while (Model.Tools.CopyFiles(filepathSource, filepathDestination, true));
            Console.Clear();
        }

        private void SaveOneFile()
        {
            string filepathSource = "";
            string filepathDestination = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Give the file source :");
                filepathSource = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("Give the file target :");
                filepathDestination = Console.ReadLine();

            } while (Model.Tools.CopyFiles(filepathSource, filepathDestination, false));
            Console.Clear();         
            
        }
    }
}
