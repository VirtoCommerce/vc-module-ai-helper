using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.AiHelper.Core;
using VirtoCommerce.AiHelper.Core.Services;
using VirtoCommerce.AiHelper.Data;
using VirtoCommerce.AiHelper.Data.Services;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.AiHelper.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {

        serviceCollection.AddSingleton<DummyAiProvider>();

        serviceCollection.AddSingleton<AiProviderRegistrar>();
        serviceCollection.AddSingleton<IAiProviderFactory>(serviceProvider => serviceProvider.GetService<AiProviderRegistrar>());
        serviceCollection.AddSingleton<IAiProviderRegistrar>(serviceProvider => serviceProvider.GetService<AiProviderRegistrar>());

        serviceCollection.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Anchor>());

    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        var serviceProvider = appBuilder.ApplicationServices;

        // Register settings
        var settingsRegistrar = serviceProvider.GetRequiredService<ISettingsRegistrar>();
        settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);

        // Register permissions
        var permissionsRegistrar = serviceProvider.GetRequiredService<IPermissionsRegistrar>();
        permissionsRegistrar.RegisterPermissions(ModuleInfo.Id, "AiHelper", ModuleConstants.Security.Permissions.AllPermissions);

        var aiProviderRegistrar = appBuilder.ApplicationServices.GetService<IAiProviderRegistrar>();
        aiProviderRegistrar.Register<DummyAiProvider>(() => appBuilder.ApplicationServices.GetService<DummyAiProvider>());

        var settingsManager = appBuilder.ApplicationServices.GetRequiredService<ISettingsManager>();
        ModuleConstants.Settings.General.AiHelperTextGenerationProvider.AllowedValues =
            ModuleConstants.Settings.General.AiHelperTextGenerationProvider.AllowedValues
            .Concat(aiProviderRegistrar.GetAiProvidersByService<IAiTextGenerationService>().Select(x => x.ProviderType).ToArray()).Distinct().ToArray();

    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
