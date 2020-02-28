using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //1. Standard - register concrete type for whole appsettings file. Pass concrete type in via DI (see HomeController)
            services.AddSingleton(Configuration.Get<AppSettings>());

            //2. Configure using IOptions<T> - NB IOptions<T> will be registered with a Singleton lifecycle
            // Concrete type SettingsA will then be available via DI (see TestAController)
            services.Configure<SettingsA>(Configuration.GetSection("SettingsA"));

            //3. Configure using IOptionsSnapshot<T> - NB IOptionsSnapshot<T> will be registered with a Scoped lifecycle
            //When using IOptionsSnapshot, changes the the underlying Configuration object are reflected
            // Concrete type SettingsB will then be available via DI (see TestBController)
            services.Configure<SettingsB>(Configuration.GetSection("SettingsB"));

            //4.Register using an Implementation Factory - Forward the registration of a service type onto an existing registration
            //using an overload of TryAddSingleton which takes an Implementation Factory - this is a Func with a single
            //parameter of the IServiceProvider.The delegate will be invoked once the IServiceProvider is built so we have access to
            //previously registered services. This allows us to directly inject the interface using DI rather than an instance of
            //IOptions<T> or IOptionsSnapshot<T>
            services.Configure<SettingsC>(Configuration.GetSection("SettingsC"));
            services.TryAddSingleton<ISettingsC>(sp => sp.GetRequiredService<IOptions<SettingsC>>().Value);


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}