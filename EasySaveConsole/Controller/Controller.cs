using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasySaveConsole.Controller
{
    public class Controller
    {
        private View.View view;

        View.Menu menu = new View.Menu();

        public Controller(View.View view)
        {
            this.view = view;
            view.Menu();
        }

       

    }
}
