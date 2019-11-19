using EasySaveConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    public class View
    {
        private Model.Model model;
        enum ArrowPosition { 
            Top,
            Middle,
            Down 
        };
        private ArrowPosition arrowPosition;

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

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += CheckKey;
            timer.Interval = 10;
            timer.Enabled = true;
            Console.WriteLine("Hello");
            while (true)
            {

            }
        }

        private void CheckKey(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Console.ReadKey().Key == ConsoleKey.DownArrow)
            {
                if (arrowPosition == ArrowPosition.Down)
                {
                    arrowPosition = ArrowPosition.Top;
                    Console.Clear();
                    Console.WriteLine("Top");
                }
                else
                {
                    arrowPosition += 1;
                    Console.Clear();
                    Console.WriteLine("middle or down");
                }
                
            }
            else if (Console.ReadKey().Key == ConsoleKey.UpArrow)
            {
                if (arrowPosition == ArrowPosition.Top)
                {
                    arrowPosition = ArrowPosition.Down;
                    Console.Clear();
                    Console.WriteLine("Bottom");
                }
                else
                {
                    arrowPosition -= 1;
                    Console.Clear();
                    Console.WriteLine("middle or top");
                }
            }
            
            
            
        }
    }
}
