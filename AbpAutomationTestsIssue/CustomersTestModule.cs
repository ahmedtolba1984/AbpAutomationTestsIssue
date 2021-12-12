using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.TestBase;
using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.EF;

namespace AbpAutomationTestsIssue
{
    [DependsOn(typeof(AbpTestBaseModule), typeof(CustomersModule))]
    public class CustomersTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            //EF Core InMemory DB does not support transactions.
            Configuration.UnitOfWork.IsTransactional = false;
            var services = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();


            var builder = new DbContextOptionsBuilder<CustomersDbContext>();
            builder.UseInMemoryDatabase("InMemoryDb").UseInternalServiceProvider(services);

            IocManager.IocContainer.Register(Component.For<DbContextOptions<CustomersDbContext>>()
                .Instance(builder.Options).LifestyleSingleton());

            base.PreInitialize();
        }
    }
}
