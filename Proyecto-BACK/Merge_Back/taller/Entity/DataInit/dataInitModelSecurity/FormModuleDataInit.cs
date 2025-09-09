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
    /// Clase estática para inicializar datos de la entidad <see cref="FormModule"/>.
    /// </summary>
    public static class FormModuleDataInit
    {
        /// <summary>
        /// Método de extensión para inicializar datos semilla (seed) para la entidad <see cref="FormModule"/>.
        /// </summary>
        /// <param name="modelBuilder">Instancia de <see cref="ModelBuilder"/> usada para configurar el modelo de datos.</param>
        public static void SeedFormModule(this ModelBuilder modelBuilder)
        {

            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<FormModule>().HasData(
                new FormModule
                {
                    id = 1,
                    formid = 1,    
                    moduleid = 1, 
                    is_deleted = false,
                    created_date = seedDate,
                },
                new FormModule
                {
                    id = 2,
                    formid = 2,
                    moduleid = 2,
                    is_deleted = false,
                    created_date = seedDate,
                }
            );
        }
    }
}
