using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Configuration;
using NetCoreConfiguration.Services;
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
            //When using IOptionsSnapshot, changes to the underlying Configuration object are reflected
            //Concrete type SettingsB will then be available via DI (see TestBController)
            services.Configure<SettingsB>(Configuration.GetSection("SettingsB"));

            //4. Register using an Implementation Factory - Forward the registration of a service type onto an existing registration
            //Use an overload of TryAddSingleton which takes an Implementation Factory - this is a Func with a single
            //parameter of the IServiceProvider. The delegate will be invoked once the IServiceProvider is built so we have access to
            //previously registered services. This allows us to directly inject the interface using DI rather than an instance of
            //IOptions<T> or IOptionsSnapshot<T>
            services.Configure<SettingsC>(Configuration.GetSection("SettingsC"));
            services.TryAddSingleton<ISettingsC>(sp => sp.GetRequiredService<IOptions<SettingsC>>().Value);

            //5. If you want to configure some settings by loading values from some Service e.g. a Service that access the DB.
            //You cannot access services registered in ConfigureServices from inside ConfigureServices. Instead, create a class derived
            //from IConfigureOptions (ConfigureSettingsDOptions) and inject the required service into it using DI
            //Then register the IConfigureOptions as a Singleton -> NB this example uses IOptions<T> so only works for
            //Services registered with a Singleton lifecycle
            services.Configure<SettingsD>(Configuration.GetSection("SettingsD"));
            services.AddSingleton<IdService>();
            services.AddSingleton<IConfigureOptions<SettingsD>, ConfigureSettingsDOptions>();

            //6. As mentioned, the previous example will only work for Singleton services. For scoped services, we have 2 options.
            //One option is to user IOptionsSnapshot<T> and register the IConfigureOptions as Scoped
            services.Configure<SettingsE>(Configuration.GetSection("SettingsE"));
            services.AddScoped<IdServiceSnap>();
            services.AddScoped<IConfigureOptions<SettingsE>, ConfigureSettingsEOptions>();
            //However, as we're now using a Scoped lifecycle, the Guid value in the IdServiceSnap will be different after 
            //each request. This may or may not cause issues depending on what you're using the Guid for


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
