using EasySaveConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{

    
    public class View : Menu
    {
        private readonly Model.Model model;

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
            ////List<Model.Travail> temp = .Request("Select * from travail");
            //Travail travail = new Travail();
            //travail.Id = 2;
            //travail.Name = "t";
            //model.AddTravail(travail);
            //List<Travail> l = new List<Travail>();
            //l.Add(travail);
            //string temp = Tools.ObjectToJson<List<Travail>>(l);
            //Console.WriteLine(temp);
            //List <Travail> temp2 = (List<Travail>)model.GetAllTravail();
            //Console.WriteLine(temp2.Count);

            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Elapsed += CheckKey;
            //timer.Interval = 10;
            //timer.Enabled = true;
            
            Console.CursorVisible = false;

            while (!IsFinsih)
            {
                DrawMenu(menuAction);
            }
        }

        protected override void FunctionFirstPosition()
        {
            menuAction[0].Instanciate();
        }

        protected override void FunctionSecondPosition()
        {
            menuAction[1].Instanciate();
        }


    }

    
}
