using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PersonDataService
    {
        private readonly PersonDataRepository _repo;
        public PersonDataService(PersonDataRepository repo) => _repo = repo;
    }
}
