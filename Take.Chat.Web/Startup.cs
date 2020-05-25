using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using Take.Chat.Business;
using Take.Chat.Infrastructure.Constants;
using Take.Chat.Infrastructure.Handlers;
using Take.Chat.Interfaces.Business;
using Take.Chat.Interfaces.Repository;
using Take.Chat.Repository;

namespace Take.Chat.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddMemoryCache();

            services.AddTransient<IChatMessagesBusiness, ChatMessagesBusiness>();
            services.AddTransient<IChatUserBusiness, ChatUserBusiness>();
            services.AddTransient<IChatUsersRepository, ChatUserRepository>();
            services.AddWebSocketManager();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, IMemoryCache cache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseWebSockets();
            app.MapSockets("/chat", serviceProvider.GetService<WebSocketMessageHandler>());
            app.MapSockets("/users", serviceProvider.GetService<WebSocketMessageHandler>());
            app.UseStaticFiles();

            //Default channels load
            var entryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);
            var defaultChannels = new List<string> { "#general", "#teste" };
            cache.Set(CacheKeys.AVAILABLE_CHANNELS, defaultChannels);
        }
    }
}
