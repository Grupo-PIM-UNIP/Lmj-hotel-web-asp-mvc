using LmjHotelWebApplication.Data;
using LmjHotelWebApplication.Services.Contratos;
using LmjHotelWebApplication.Services.Implementacoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace LmjHotelWebApplication
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
            services.AddControllersWithViews();

            // Obtendo a string de conex?o do SQL Server do arquivo appsettings.json
            var connectionString = Configuration["SqlServerConnection:SqlServerStringConnection"];

            // (Opcional) Obtendo o Assembly do sua m?quina local
            var myAssemblyComputer = typeof(Startup)
                .GetTypeInfo().Assembly
                .GetName().Name;

            // Injetando a dependencia com o banco de dados SQL Server com entityframeworkecore
            services.AddDbContext<SqlServerDbContext>(
                options => options.UseSqlServer(connectionString, sql =>
                sql.MigrationsAssembly(myAssemblyComputer)));

            /* Injentando a dependencia das camadas de servi?os da aplica??o,
               indicando ao framework as interfaces junto a suas implementa??es  */
            services.AddScoped<IHospedeService, HospedeService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IQuartoService, QuartoService>();

            // Injentando autentifica??o via cookies
            /* Caso o usu?rio necessite de autentifica??o para acessar alguma tela e o mesmo n?o estiver logado,
               o usu?rio ser? direcionado a tela abaixo para que o mesmo se autentifique */
            services.AddAuthentication("cookies")
                .AddCookie("cookies", options => options.LoginPath = "/hospede/login");
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

            app.UseAuthentication();

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
