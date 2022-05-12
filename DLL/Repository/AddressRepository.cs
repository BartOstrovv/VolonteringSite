using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class AddressRepository : BaseRepository<Address>
    {
        public AddressRepository(VolunteeringContext context) : base(context)
        {
        }
    }
}
