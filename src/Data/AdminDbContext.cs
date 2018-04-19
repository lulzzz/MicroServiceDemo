using System;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AdminContext:DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {
        }
    }
}
