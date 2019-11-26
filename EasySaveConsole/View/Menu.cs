using EasySaveConsole.Model;
using System;
using System.IO;
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

    public class Menu : IDisposable
    {
        protected bool IsFinsih = false;
        protected ArrowPosition arrowPosition;

        protected virtual void DrawMenu(List<MenuAction> menuAction, string stringAddingToDisplay)
        {
            Console.WriteLine(stringAddingToDisplay);
            foreach (MenuAction item in menuAction)
            {
                if (item.ArrowPosition == arrowPosition)
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
            CheckKey(ckey, menuAction);
        }

        protected virtual void CheckKey(ConsoleKey consoleKey, List<MenuAction> menuAction)
        {
            if (consoleKey == ConsoleKey.DownArrow)
            {
                if (arrowPosition == ArrowPosition.Down)
                {
                    arrowPosition = ArrowPosition.Top;
                    Console.Clear();
                }
                else
                {
                    arrowPosition += 1;
                    Console.Clear();
                }
            }
            else if (consoleKey == ConsoleKey.UpArrow)
            {
                if (arrowPosition == ArrowPosition.Top)
                {
                    arrowPosition = ArrowPosition.Down;
                    Console.Clear();
                }
                else
                {
                    arrowPosition -= 1;
                    Console.Clear();
                }
            }
            else if (consoleKey == ConsoleKey.Enter)
            {
                if (arrowPosition == ArrowPosition.Top)
                {
                    Console.Clear();
                    FunctionFirstPosition();
                }
                else if (arrowPosition == ArrowPosition.Middle)
                {
                    Console.Clear();
                    FunctionSecondPosition();
                }
                else if (arrowPosition == ArrowPosition.Down)
                {
                    Console.Clear();
                    FunctionLastPosition();
                }
            }
        }

        protected virtual void FunctionFirstPosition()
        {
            throw new NotImplementedException();
        }

        protected virtual void FunctionSecondPosition()
        {
            throw new NotImplementedException();
        }

        protected virtual void FunctionLastPosition()
        {
            IsFinsih = true;
            Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~Menu()
        // {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
