using TemplateNetCore.Api.Infraestructure;
using TemplateNetCore.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Template .NET Core");
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseRateLimiter();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.MapControllers();

app.Run();

public partial class Program { }