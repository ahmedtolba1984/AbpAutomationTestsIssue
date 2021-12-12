using System.Data.Common;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public static class DbContextConfigurer
    {
        public static void Configure<TContext>(DbContextOptionsBuilder<TContext> builder, string connectionString,
            int? timeout = null, bool disableTracking = false) where TContext : AbpDbContext
        {
            builder.UseSqlServer(connectionString, opts => opts.CommandTimeout(timeout ?? GetDefaultCommandTimeout()));
        }

        private static int? GetDefaultCommandTimeout()
        {
            return (int?)TimeSpan.FromSeconds(30).TotalSeconds;
        }

        public static void Configure<TContext>(DbContextOptionsBuilder<TContext> builder, DbConnection connection,
            int? timeout = null, bool disableTracking = false) where TContext : AbpDbContext
        {
            builder.UseSqlServer(connection, opts => opts.CommandTimeout(timeout ?? GetDefaultCommandTimeout()));
        } 
    }
}
