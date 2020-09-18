using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalIR.Server.Hubs;

namespace SignalIR.Server
{
    public class Startup
    {
        readonly string CorsPolicyOrigin = "CorsPolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddCors(options => options.AddPolicy(CorsPolicyOrigin,
            builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowCredentials()
                       .WithOrigins("https://localhost:5002");
            }));
            services.AddSignalR();
            services.AddHostedService<Worker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseCors(CorsPolicyOrigin);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ClockHub>("/hubs/clock");
            });
        }
    }
}