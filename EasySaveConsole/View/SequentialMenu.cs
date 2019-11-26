using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    class SequentialMenu : Menu
    {
        private bool isFinish = false;
        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Display Sequence", ArrowPosition.Top),
            new MenuAction("Create Sequence", ArrowPosition.Middle),
            new MenuAction("Return", ArrowPosition.Down),
        };

        public SequentialMenu()
        {
            ShowMenu();
        }

        private void ShowMenu()
        {
            while (!isFinish)
            {
                DrawMenu(menuAction);
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
            throw new NotImplementedException();
        }

        private void SequentialDisplay()
        {
            String display;
            do
            {

            }
            while {
                //tester si la string est ok, vérifier que c'est un .JSON
            }
            List<Model.Logs> temp = Model.Tools.JsonToObject<Model.Logs>(display); //retourne une liste
            //print la liste dans la console avec un foreach
        }
    }
}

/*
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
*/