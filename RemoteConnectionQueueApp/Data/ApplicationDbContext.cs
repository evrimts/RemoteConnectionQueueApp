using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RemoteConnectionQueueApp.Areas.Identity.Data;
using RemoteConnectionQueueApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteConnectionQueueApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RemoteConnection> RemoteConnection { get; set; }
        public DbSet<Queue> Queue { get; set; }
    }
}
