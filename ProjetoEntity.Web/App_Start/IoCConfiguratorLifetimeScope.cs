using Autofac;

namespace ProvaCandidato.App_Start
{
    public static class IoCConfiguratorLifetimeScope
    {
        public static IContainer ContainerLifetimeScope { get; set; }

        public static void ConfigureDependencyInjection()
        {
            ContainerBuilder builder = new ContainerBuilder();
            BootStrapper.RegisterServices(builder, true);
            ContainerLifetimeScope = builder.Build();
        }
    }
}