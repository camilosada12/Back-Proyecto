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
    /// Clase estática para inicializar datos de la entidad <see cref="Form"/>.
    /// </summary>
    public static class FormDataInit
    {
        /// <summary>
        /// Método de extensión para inicializar datos semilla (seed) para la entidad <see cref="Form"/>.
        /// </summary>
        /// <param name="modelBuilder">Instancia de <see cref="ModelBuilder"/> usada para configurar el modelo de datos.</param>
        public static void SeedForm(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>().HasData(
                new Form
                {
                    id = 1,
                    name = "Formulario de acuerdo de pago",
                    description = "Formulario de creacion de acuerdo de pago",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                },
                new Form
                {
                    id = 2,
                    name = "Formulario de creacion de multas",
                    description = "Formulario para agregar nuevas multas",
                    active = true,
                    is_deleted = false,
                    created_date = new DateTime(2023, 01, 01),
                }
            );
        }
    }
}
