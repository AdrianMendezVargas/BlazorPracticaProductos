using BlazorPracticaPrimerParcial.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPracticaPrimerParcial.Data {
    public class Contexto : DbContext{

        public DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source= Data\\Registro.db");
        }

    }
}
