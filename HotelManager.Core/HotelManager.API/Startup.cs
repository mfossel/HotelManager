using HotelManager.Core.Infranstructure;
using HotelManager.Core.Repository;
using HotelManager.Data.Infrastructure;
using HotelManager.Data.Repository;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using HotelManager.Core.Domain;
using HotelManager.API.Infrastructure;
using SimpleInjector.Extensions.ExecutionContextScoping;
using HotelManager.API.App_start;

[assembly: OwinStartup(typeof(HotelManager.Api.Startup))]
namespace HotelManager.Api
    {
        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                var container = ConfigureSimpleInjector(app);
                ConfigureOAuth(app, container);

                HttpConfiguration config = new HttpConfiguration
                {
                    DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
                };

                WebApiConfig.Register(config);
                app.UseCors(CorsOptions.AllowAll);
                app.UseWebApi(config);
            }

            public void ConfigureOAuth(IAppBuilder app, Container container)
            {
                Func<IAuthorizationRepository> authRepositoryFactory = container.GetInstance<IAuthorizationRepository>;

                var authorizationOptions = new OAuthAuthorizationServerOptions
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/api/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                    Provider = new HotelManagerAuthorizationServerProvider(authRepositoryFactory)
                };

                // Token Generation
                app.UseOAuthAuthorizationServer(authorizationOptions);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            }

            public Container ConfigureSimpleInjector(IAppBuilder app)
            {
                var container = new Container();

                container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

                container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
                container.Register<IUnitOfWork, UnitOfWork>();

                container.Register<ICustomerRepository, CustomerRepository>();
                container.Register<IReservationRepository, ReservationRepository>();
                container.Register<IRoomRepository, RoomRepository>();
                container.Register<IWorkorderRepository, WorkorderRepository>();
                container.Register<IUserRepository, UserRepository>();
                container.Register<IUserStore<User, string>, UserStore>(Lifestyle.Scoped);
                container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Scoped);

                // more code to facilitate a scoped lifestyle
                app.Use(async (context, next) =>
                {
                    using (container.BeginExecutionContextScope())
                    {
                        await next();
                    }
                });

                container.Verify();

                return container;
            }
        }
    }
