using System;
using System.Threading.Tasks;
using Abp;
using Abp.TestBase;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.EF;
using Xunit;

namespace AbpAutomationTestsIssue
{
    public class CustomersTests : AbpIntegratedTestBase<CustomersTestModule>
    {
        [Fact]
        public async Task Customer_Should_Be_Created_Successfully()
        {
            var customer = new Customer
            {
                Name = "Customer1",
                Address = "Address1"
            };

            const int tenantId = 1;

            await UsingDbContextAsync(tenantId, async context =>
            { 
                await context.Customers.AddAsync(customer);
            });

            await UsingDbContextAsync(tenantId, async context =>
            {
                var result = await context.Customers.ToListAsync();
                return;
            });

        }

        protected IDisposable UsingTenantId(int? tenantId)
        {
            var previousTenantId = AbpSession.TenantId;
            AbpSession.TenantId = tenantId;
            return new DisposeAction(() => AbpSession.TenantId = previousTenantId);
        }


        protected void LoginAsDefaultTenantAdmin()
        {
            const int defaultTenantId = 1;
            AbpSession.TenantId = defaultTenantId;
            AbpSession.UserId = 1;
        }

        protected async Task UsingDbContextAsync(int? tenantId, Func<CustomersDbContext, Task> action)
        {
            using (UsingTenantId(tenantId))
            {
                await using (var context = LocalIocManager.Resolve<CustomersDbContext>())
                {
                    LoginAsDefaultTenantAdmin();
                    await action(context);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}