using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Models.EF;

namespace Models
{
    public class CustomersModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.EntityHistory.IsEnabled = true;
            Configuration.Auditing.IsEnabled = true;

            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CustomersModule).GetAssembly());
        }

    }
}
