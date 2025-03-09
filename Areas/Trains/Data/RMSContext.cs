using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Areas.Trains.Models;
using RailwayManagementSystem.Areas.Stations.Models;

namespace RailwayManagementSystem.Data
{
    public class RMSContext : DbContext
    {
        public RMSContext (DbContextOptions<RMSContext> options)
            : base(options)
        {
        }

        public DbSet<Train> Train { get; set; } = default!;
        public DbSet<Station> Station { get; set; } = default!;
    }
}
