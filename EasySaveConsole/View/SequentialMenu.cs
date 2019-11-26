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
            throw new NotImplementedException();
        }
    }
}

