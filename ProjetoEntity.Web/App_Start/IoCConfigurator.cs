using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace ProvaCandidato.App_Start
{
    public static class IoCConfigurator
    {
        public static void ConfigureDependencyInjection()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            BootStrapper.RegisterServices(builder);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}