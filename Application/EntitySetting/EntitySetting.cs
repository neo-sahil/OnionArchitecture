using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntitySetting
{
    public static class EntitySetting
    {
        public static void AddEntitySettings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Rate).HasColumnType("decimal(10,2)");
        }
    }
}
