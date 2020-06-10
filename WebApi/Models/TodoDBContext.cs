using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Models {
    public class TodoDBContext : DbContext {

        public DbSet<TodoItem> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Initial Catalog=test;MultipleActiveResultSets=true;User ID=admin;Password=admin");
        }

    }
}
