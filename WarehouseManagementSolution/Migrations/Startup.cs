using System.Reflection;
using FluentMigrator.Runner;

namespace FluentMigrator;

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
        services.AddControllers();

        services.AddFluentMigratorCore()
            .ConfigureRunner(config => 
                config.AddPostgres()
                    .WithGlobalConnectionString(@"Host=localhost;Database=WarehouseManagement;User ID=postgres;Password=postgres;Port=5432;")
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All())
            .AddLogging(config => config.AddFluentMigratorConsole());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        //Database.Migrate("Server=DESKTOP-8BIQ23M\\SQLEXPRESS01;Database=WarehouseManagement;Trusted_Connection=True;TrustServerCertificate=True;");

        using var scope = app.ApplicationServices.CreateScope();
        var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
        migrator.MigrateUp();
    }
}