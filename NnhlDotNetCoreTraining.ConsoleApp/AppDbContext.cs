using Microsoft.EntityFrameworkCore;
using NnhlDotNetCoreTraining.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NnhlDotNetCoreTraining.ConsoleApp
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=DESKTOP-3CLQTFU;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        
        public DbSet<BlogDataModel> Blogs { get; set; } 

    }
}
