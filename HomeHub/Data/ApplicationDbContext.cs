using System;
using System.Collections.Generic;
using System.Text;
using HomeHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HomeHub.ViewModels;

namespace HomeHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }

        public DbSet<HomeHub.ViewModels.DeviceViewModel> DeviceViewModel { get; set; }
    }
}
