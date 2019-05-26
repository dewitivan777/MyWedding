using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestProject
{
    public class Identitydbcontext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public Identitydbcontext(DbContextOptions<Identitydbcontext> options) : base(options)
        {

        }

    }
}