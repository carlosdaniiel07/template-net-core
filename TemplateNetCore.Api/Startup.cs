using TemplateNetCore.Api.Infraestructure.Extensions;
using TemplateNetCore.Api.Middlewares;
using TemplateNetCore.Domain.Models;
using TemplateNetCore.Infra.Mapping;

namespace TemplateNetCore.Api
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
            services
                .AddDbContext(Configuration.GetConnectionString("Local"))
                .AddScopedServices()
                .AddTransientServices()
                .AddSingletonServices()
                .AddMediator()
                .AddAuthenticationJwt(Configuration)
                .AddSwagger()
                .AddAutoMapper(config => config.AddProfile<AutoMapping>(), typeof(Startup))
                .Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddHttpContextAccessor();
            services.AddControllers();
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

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Template .NET Core");
            });

            app.UseMiddleware<GlobalErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
