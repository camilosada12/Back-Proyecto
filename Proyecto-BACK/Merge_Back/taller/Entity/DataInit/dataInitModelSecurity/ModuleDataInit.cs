using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Implements.ModelSecurity;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit.dataInitModelSecurity
{
    /// <summary>
    /// Clase estática para inicializar datos semilla (seed) de la entidad <see cref="Module"/>.
    /// </summary>
    public static class ModuleDataInit
    {
        /// <summary>
        /// Método de extensión para agregar datos iniciales (seed) a la entidad <see cref="Module"/>.
        /// </summary>
        /// <param name="modelBuilder">Instancia de <see cref="ModelBuilder"/> usada para configurar el modelo de datos.</param>
        public static void SeedModule(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>().HasData(
                new Module
                {
                    id = 1,
                    name = "Módulo de hacienda",
                    description = "Módulo para administración general",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                },
                new Module
                {
                    id = 2,
                    name = "Módulo de inspectora",
                    description = "Módulo encargado de crear nuevas multas",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                },
                new Module
                {
                    id = 3 ,
                    name = "Modulo de usuario",
                    description =  "modulo encargado para visualizar las multas inspuestas",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                }
            );
        }
    }
}
