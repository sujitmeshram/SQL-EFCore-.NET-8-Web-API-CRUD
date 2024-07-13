using Microsoft.EntityFrameworkCore;
using SQL_EFCore_.NET_8_Web_API_CRUD.Model;
using System.Collections.Generic;

namespace SQL_EFCore_.NET_8_Web_API_CRUD
{
    public class ApplicationDBContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
    }
}
