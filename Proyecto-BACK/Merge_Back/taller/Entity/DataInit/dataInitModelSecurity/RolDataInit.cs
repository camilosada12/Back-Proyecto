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
    /// Clase estática para inicializar datos semilla (seed) para la entidad <see cref="Rol"/>.
    /// </summary>
    public static class RolDataInit
    {
        /// <summary>
        /// Método de extensión para agregar datos iniciales (seed) a la entidad <see cref="rol"/>.
        /// </summary>
        /// <param name="modelBuilder">Instancia de <see cref="ModelBuilder"/> usada para configurar el modelo de datos.</param>
        public static void SeedRol(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    id = 1,
                    name = "Administrador",
                    description = "Rol con todos los permisos",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                },
                new Rol
                {
                    id = 2,
                    name = "Usuario",
                    description = "Rol estándar para usuarios normales",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                }
            );
        }
    }
}
