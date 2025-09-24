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
            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<Form>().HasData(
                new Form
                {
                    id = 1,
                    name = "Formulario de acuerdo de pago",
                    description = "Formulario de creacion de acuerdo de pago",
                    active = true,
                    Route = "acuerdoPago",
                    Icon = "pi pi-fw pi-home",
                    is_deleted = false,
                    created_date = seedDate,
                },
                new Form
                {
                    id = 2,
                    name = "Formulario de creacion de multas",
                    description = "Formulario para agregar nuevas multas",
                    active = true,
                    Route = "CreacionMulta",
                    Icon = "pi pi-fw pi-homeing",
                    is_deleted = false,
                    created_date = seedDate,
                },
                new Form
                {
                    id = 3,
                    name = "Formulario tipo de  multas",
                    description = "Formulario tipo de  multas",
                    active = true,
                    Route = "tipos-multas",
                    Icon = "pi pi-fw pi-id-card",
                    is_deleted = false,
                    created_date = seedDate,
                },
                 new Form
                {
                    id = 4,
                    name = "Formulario Notificacion de multas",
                    description = "Formulario Notificacion de multas",
                    active = true,
                     Route = "notificaciones",
                     Icon = "pi pi-fw pi-check-square",
                     is_deleted = false,
                    created_date = seedDate,
                },
               new Form
                {
                    id = 5,
                    name = "Formularios",
                    description = "Formularios",
                    active = true,
                     Route = "dashboard",
                   Icon = "pi pi-fw pi-file",
                   is_deleted = false,
                    created_date = seedDate,
                },
                 new Form
                {
                    id = 6,
                    name = "Form modules",
                    description = "Form modules",
                    active = true,
                     Route = "dashboard",
                     Icon = "pi pi-fw pi-clone",
                     is_deleted = false,
                    created_date = seedDate,
                },
                  new Form
                  {
                      id = 7,
                      name = "Modulos",
                      description = "Modulos",
                      active = true,
                      Route = "dashboard",
                      Icon = "pi pi-fw pi-th-large",
                      is_deleted = false,
                      created_date = seedDate,
                  },
                  new Form
                {
                    id = 8,
                    name = "personas",
                    description = "personas",
                    active = true,
                     Route = "dashboard",
                      Icon = "pi pi-fw pi-users",
                      is_deleted = false,
                    created_date = seedDate,
                },
               new Form
                {
                    id = 9,
                    name = "permisos",
                    description = "permisos",
                    active = true,
                     Route = "dashboard",
                   Icon = "pi pi-fw pi-lock-open",
                   is_deleted = false,
                    created_date = seedDate,
                },
                new Form
                {
                    id = 10,
                    name = "Rol Form Permission",
                    description = "Rol Form Permission",
                    active = true,
                     Route = "dashboard",
                    Icon = "pi pi-fw pi-key",
                    is_deleted = false,
                    created_date = seedDate,
                },
                new Form
                {
                    id = 11,
                    name = "Roles",
                    description = "Roles",
                    active = true,
                     Route = "dashboard",
                    Icon = "pi pi-fw pi-users",
                    is_deleted = false,
                    created_date = seedDate,
                },
               new Form
                {
                    id = 12,
                    name = "Usuarios",
                    description = "Usuarios",
                    active = true,
                     Route = "dashboard",
                   Icon = "pi pi-fw pi-user",
                   is_deleted = false,
                    created_date = seedDate,
                },
                 new Form
                {
                    id = 13,
                    name = "Rol Usuario",
                    description = "Rol Usuario",
                    active = true,
                     Route = "dashboard",
                     Icon = "pi pi-fw pi-user-plus",
                     is_deleted = false,
                    created_date = seedDate,
                },
                new Form
                {
                    id = 14,
                    name = "departamento",
                    description = "departamento",
                    active = true,
                     Route = "dashboard",
                    Icon = "pi pi-fw pi-briefcase",
                    is_deleted = false,
                    created_date = seedDate,
                },
                  new Form
                {
                    id = 15,
                    name = "Tipo de Documento",
                    description = "Tipo de Documento",
                    active = true,
                     Route = "dashboard",
                      Icon = "pi pi-fw pi-briefcase",
                      is_deleted = false,
                    created_date = seedDate,
                },
                 new Form
                {
                    id = 16,
                    name = "Municipio",
                    description = "Municipio",
                    active = true,
                     Route = "dashboard",
                     Icon = "pi pi-fw pi-briefcase",
                     is_deleted = false,
                    created_date = seedDate,
                },
                new Form
                {
                    id = 17,
                    name = "Frecuencia de pago ",
                    description = "Frecuencia de pago",
                    active = true,
                     Route = "dashboard",
                    Icon = "pi pi-fw pi-briefcase",
                    is_deleted = false,
                    created_date = seedDate,
                },
                new Form
                {
                    id = 18,
                    name = "Perfil",
                    description = "Perfil",
                    active = true,
                     Route = "dashboard",
                    Icon = "pi pi-fw pi-briefcase",
                    is_deleted = false,
                    created_date = seedDate,
                },
                new Form
                {
                    id = 19,
                    name = "Notificacion de acuerdo",
                    description = "Notificacion de acuerdo ",
                    active = true,
                     Route = "dashboard",
                    Icon = "pi pi-fw pi-briefcase",
                    is_deleted = false,
                    created_date = seedDate,
                },
                 new Form
                {
                    id = 20,
                    name = "inicio",
                    description = "inicio ",
                    active = true,
                     Route = "consultar-ingresar",
                     Icon = "pi pi-fw pi-home",
                     is_deleted = false,
                    created_date = seedDate,
                }

            );
        }
    }
}
