using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public interface IModel
    {
        IList<Travail> GetAllTravail();
        Travail GetTravailByName(string departmentName);
        void AddTravail(params Travail[] departments);
        void UpdateTravail(params Travail[] departments);
        void RemoveTravail(params Travail[] departments);
        void ToJson<T>(T[] ts);
    }
}
