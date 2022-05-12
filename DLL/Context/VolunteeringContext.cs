using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class VolunteeringContext : IdentityDbContext
    {
        public VolunteeringContext(DbContextOptions<VolunteeringContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PersonData> People { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
