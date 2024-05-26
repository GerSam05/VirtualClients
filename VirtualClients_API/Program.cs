using Microsoft.AspNetCore.Mvc;
using VirtualClients_API.ContextDb;
using VirtualClients_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EnableCors
var myCorsRules = "CorsRules";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCorsRules, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//LowercaseUrls
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

//Context
builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("DbContext"));

//Service
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<CondicionService>();

//ModelState
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myCorsRules);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
