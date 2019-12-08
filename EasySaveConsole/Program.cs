using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            View.View view = new View.View();
            Controller.Controller controller = new Controller.Controller(view);
        }
    }
}
