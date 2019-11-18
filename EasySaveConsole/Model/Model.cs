using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class Model : IModel
    {
        private readonly ITravailRepository _travailRepository;

        public Model()
        {
            _travailRepository = new TravailtRepository();      
        }

        public void AddTravail(params Travail[] travails)
        {
            _travailRepository.Add(travails);
        }

        public IList<Travail> GetAllTravail()
        {
            return _travailRepository.GetAll();
        }

        public Travail GetTravailByName(string travailName)
        {
            return _travailRepository.GetSingle(
                d => d.Name.Equals(travailName));
        }

        public void RemoveTravail(params Travail[] travails)
        {
            _travailRepository.Remove(travails);
        }

        public void UpdateTravail(params Travail[] travails)
        {
            _travailRepository.Update(travails);
        }

    }
}
