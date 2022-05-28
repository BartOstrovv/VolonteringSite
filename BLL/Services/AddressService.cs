using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AddressService
    {
        private readonly AddressRepository _repo;

        public AddressService(AddressRepository repo)
        {
            _repo = repo;
        }

     }
}
