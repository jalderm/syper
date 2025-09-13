using Volo.Abp.Account;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;

namespace Syper;

[DependsOn(
    typeof(SyperDomainSharedModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule)
)]
public class SyperApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SyperDtoExtensions.Configure();
    }
}
