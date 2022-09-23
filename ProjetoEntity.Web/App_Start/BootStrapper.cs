using Autofac;
using AutoMapper;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Interface;
using ProvaCandidato.Data.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaCandidato.App_Start
{
    public static class BootStrapper
    {
        public static void RegisterServices(ContainerBuilder builder, bool testeUnitario = false)
        {
            #region automapper
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
            #endregion

            #region contexto

            if (!testeUnitario)
            {
                builder.RegisterType<ContextoPrincipal>()
                .AsSelf()
                .InstancePerRequest();
            }
            else
            {
                builder.RegisterType<ContextoPrincipal>()
                .AsSelf()
                .InstancePerLifetimeScope();
            }
            #endregion

            builder.SetarVidaUtilInstancia<ICidadeRepositorio, CidadeRepositorio>(testeUnitario);
            builder.SetarVidaUtilInstancia<IClienteRepositorio, ClienteRepositorio>(testeUnitario);
        }
        public static void SetarVidaUtilInstancia<T, E>(this ContainerBuilder builder, bool testeUnitario)
        {
            if (testeUnitario)
            {
                builder.RegisterType<E>()
                .AsSelf()
                .As<T>()
                .InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<E>()
                .AsSelf()
                .As<T>()
                .InstancePerRequest();
            }
        }
    }
}