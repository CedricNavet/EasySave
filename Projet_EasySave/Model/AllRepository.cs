using Projet_EasySave.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_EasySave.Model
{
    public interface ITravailRepository : IGenericDataRepository<Travail>
    {
    }

    public class TravailtRepository : GenericDataRepository<Travail>, ITravailRepository
    {
    }


}
