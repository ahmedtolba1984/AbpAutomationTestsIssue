using Abp.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models.EF
{
    public class CustomersDbContext : AbpDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {

        }


        protected override long? GetAuditUserId()
        {
            var userId = base.GetAuditUserId();
            return userId ?? AbpSession.UserId;
        }
    }
}
