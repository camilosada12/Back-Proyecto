using Microsoft.EntityFrameworkCore;
using Entity.Domain.Models.Implements.ModelSecurity;

namespace Entity.DataInit.dataInitModelSecurity
{
    /// <summary>
    /// Clase estática para inicializar datos semilla para la entidad RolFormPermission.
    /// </summary>
    public static class RolFormPermissionDataInit
    {
        /// <summary>
        /// Método de extensión para agregar datos iniciales (seed) para RolFormPermission.
        /// </summary>
        /// <param name="modelBuilder">El constructor del modelo para configuración.</param>
        public static void SeedRolFormPermission(this ModelBuilder modelBuilder)
        {
            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<RolFormPermission>().HasData(
                // permisos para el rol usuario (RolId = 2)
                new RolFormPermission { id = 1, RolId = 2, FormId = 1, PermissionId = 1, is_deleted = false , created_date = seedDate, }, // leer
                new RolFormPermission { id = 2, RolId = 2, FormId = 1, PermissionId = 2, is_deleted = false , created_date = seedDate, }, // crear
                new RolFormPermission { id = 3, RolId = 2, FormId = 1, PermissionId = 3, is_deleted = false , created_date = seedDate, }, // editar
                new RolFormPermission { id = 4, RolId = 2, FormId = 1, PermissionId = 4, is_deleted = false , created_date = seedDate, }, // eliminar lógico

                // permisos para el rol admin (RolId = 1)
                new RolFormPermission { id = 5, RolId = 1, FormId = 1, PermissionId = 1, is_deleted = false , created_date = seedDate, },
                new RolFormPermission { id = 6, RolId = 1, FormId = 1, PermissionId = 2, is_deleted = false , created_date = seedDate, },
                new RolFormPermission { id = 7, RolId = 1, FormId = 1, PermissionId = 3, is_deleted = false , created_date = seedDate, },
                new RolFormPermission { id = 8, RolId = 1, FormId = 1, PermissionId = 4, is_deleted = false , created_date = seedDate, },
                new RolFormPermission { id = 9, RolId = 1, FormId = 1, PermissionId = 5, is_deleted = false , created_date = seedDate, }, // ver eliminados
                new RolFormPermission { id = 10, RolId = 1, FormId = 1, PermissionId = 6, is_deleted = false , created_date = seedDate, } // recuperar
            );
        }
    }
}
