using System;
using Microsoft.EntityFrameworkCore;
using Entity.Domain.Models.Implements.ModelSecurity;

namespace Entity.DataInit.dataInitModelSecurity
{
    public static class UserDataInit
    {
        public static void SeedUser(this ModelBuilder modelBuilder) 
        {
            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     id = 1,
                     name = "camilosada12",
                     password = "admin123",
                     email = "camiloandreslosada901@gmail.com",
                     active = true,
                     is_deleted = false,
                     PersonId = 1,
                     created_date = seedDate,
                 },
                 new User
                 {
                     id = 2,
                     name = "sara12312",
                     password = "sara12312",
                     email = "sarita@gmail.com",
                     active = true,
                     is_deleted = false,
                     PersonId = 2,
                     created_date = seedDate,
                 }
            );
        }
    }
}
