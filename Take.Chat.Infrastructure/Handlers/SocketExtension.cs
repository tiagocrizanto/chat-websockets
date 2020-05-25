using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Take.Chat.Infrastructure.Middlewares;

namespace Take.Chat.Infrastructure.Handlers
{
    public static class SocketExtension
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();
            foreach (Type type in Assembly.GetExecutingAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                    services.AddSingleton(type);
            }

            return services;
        }

        public static IApplicationBuilder MapSockets(this IApplicationBuilder app, PathString path, SocketHandler socketHandler)
        {
            return app.Map(path, (x) => x.UseMiddleware<SocketMiddleware>(socketHandler));
        }
    }
}
