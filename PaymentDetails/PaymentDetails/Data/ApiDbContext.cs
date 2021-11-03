using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentDetails.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace PaymentDetails.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public virtual DbSet<PaymentDetail> PaymentDetail { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
