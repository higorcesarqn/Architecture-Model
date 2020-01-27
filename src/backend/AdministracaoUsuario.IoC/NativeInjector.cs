using AdministracaoUsuario.Domain.Commands.RoleCommands.Create;
using AdministracaoUsuario.Domain.Commands.RoleCommands.Update;
using AdministracaoUsuario.Domain.Commands.UserCommands.ChangePassword;
using AdministracaoUsuario.Domain.Commands.UserCommands.Create;
using AdministracaoUsuario.Domain.Commands.UserCommands.Update;
using AdministracaoUsuario.Infrastructure.Repositories;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace AdministracaoUsuario.IoC
{
    public static class NativeInjector
    {
        public static ContainerBuilder AddAdministracaoUsuario<TDbContext>(this ContainerBuilder container)
            where TDbContext : DbContext
        {
            //repositories
            container.RegisterType<UserRepository<TDbContext>>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            container.RegisterType<RoleRepository<TDbContext>>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            container.RegisterType<PermissionRepository<TDbContext>>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //Commands Handlers
            container.RegisterType<CreateNewUserCommandHandler>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            container.RegisterType<UpdateUserCommandHandler>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            container.RegisterType<ChangePasswordCommandHandler>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            container.RegisterType<UpdateRoleCommandHandler> ()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            container.RegisterType<CreateNewRoleCommandHandler>()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            return container;
        }
    }
}
