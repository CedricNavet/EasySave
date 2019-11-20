using EasySaveConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{

    public enum ArrowPosition
    {
        Top,
        Middle,
        Down
    };
    public class View
    {
        private readonly Model.Model model;
        
        public ArrowPosition arrowPosition;

        List<MenuAction> menuAction = new List<MenuAction>() {
            new MenuAction("Action 1", ArrowPosition.Top, typeof(Model.Logs)),
            new MenuAction("Action 2", ArrowPosition.Middle, typeof(Model.Logs)),
            new MenuAction("Action 3", ArrowPosition.Down, typeof(Model.Logs)),
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
            Console.WriteLine("Hello");

            while (true)
            {
                DrawMenu();
            }
        }

        private void DrawMenu()
        {
            foreach (MenuAction item in menuAction)
            {
                if(item.ArrowPosition == arrowPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(item.Name);
                }
                else
                {
                    Console.WriteLine(item.Name);
                }
                Console.ResetColor();
            }
           

            ConsoleKey ckey = Console.ReadKey().Key;
            CheckKey(ckey);
        }
        private void CheckKey(ConsoleKey consoleKey)
        {
            if (consoleKey == ConsoleKey.DownArrow)
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
            else if (consoleKey == ConsoleKey.UpArrow)
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
            else if (consoleKey == ConsoleKey.Enter)
            {
                foreach (MenuAction item in menuAction)
                {
                    if(arrowPosition == item.ArrowPosition)
                    {
                        item.Instanciate();
                    }
                }
            }         
        }
    }

    public class MenuAction
    {
        public readonly string Name;
        public readonly ArrowPosition ArrowPosition;
        public readonly Type ClassName;

        public MenuAction(string name, ArrowPosition arrowPosition, Type ClassName)
        {
            this.Name = name;
            this.ArrowPosition = arrowPosition;
            this.ClassName = ClassName;
        }

        public object Instanciate()
        {
            return (object)Activator.CreateInstance(ClassName);
        }
    }
}
