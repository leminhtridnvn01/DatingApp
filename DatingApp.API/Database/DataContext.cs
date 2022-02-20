using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.DatingApp.DatingApp.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core.DatingApp.DatingApp.API.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<User> Users {get; set;}
    }
}