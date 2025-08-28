using System;
using Microsoft.EntityFrameworkCore;
using Entity.Domain.Models.Implements.ModelSecurity;

namespace Entity.DataInit.dataInitModelSecurity
{
    public static class UserDataInit
    {
        public static void SeedUser(this ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     id = 1,
                     //name = "camilosada12",
                     PasswordHash = "admin123",
                     email = "camiloandreslosada901@gmail.com",
                     active = true,
                     is_deleted = false,
                     PersonId = 1,
                     documentTypeId = 1,
                     documentNumber = "123456789",
                     created_date = new DateTime(2023, 01, 01),
                 },
                 new User
                 {
                     id = 2,
                     //name = "sara12312",
                     PasswordHash = "sara12312",
                     email = "sarita@gmail.com",
                     active = true,
                     is_deleted = false,
                     PersonId = 2,
                     documentTypeId = 2,
                     documentNumber = "0123432121",
                     created_date = new DateTime(2023, 02, 01),
                 }
            );
        }
    }
}
