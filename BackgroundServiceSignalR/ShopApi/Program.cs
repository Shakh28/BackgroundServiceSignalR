using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopApi.Data;
using ShopApi.Hubs;
using ShopApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data source=app.db");
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<PaymentService>();
builder.Services.AddSignalR();

builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(corsPolicy =>
    {
        corsPolicy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.MapHub<ContractsHub>("/contracts-hub");

app.Run();
