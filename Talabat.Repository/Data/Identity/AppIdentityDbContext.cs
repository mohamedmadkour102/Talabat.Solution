using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Data.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):base(options) 
        {
            
        }

        // بعمل اوفراريد للأون موديل كرييتيج لو انا استخدمت وغيرت حاجه بالفلونت لو معملتش حاجه هو هيروح ينفذ بتاعت الكلاس اللي انت وارث منه 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // عشان ينفذ بتاعت البيز 
            base.OnModelCreating(builder);

            // عملنا اوفرارايد للفانكشن عشان ينفذ ديه 
            builder.Entity<Address>().ToTable("Addresses");
        }
    }
}
