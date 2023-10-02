using TestHub.Infrastructure.Context;
using TestHub.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TestHub.Infrastructure.Repository;
using TestHub.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddDbContext<TestHubDbContext>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<AnswerService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<DataSeederService>();
builder.Services.AddScoped<DataSeederService.DataSeeder>();
builder.Services.AddScoped<FirebaseService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin", p =>
    {
        p.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<TestHubDbContext>(options =>
{
    options.UseSqlServer("Server=.;Database=TestHubDb;Trusted_Connection=true;TrustServerCertificate=true;");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestHub API v1", Version = "v1" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestHub API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowMyOrigin");
app.UseAuthorization();



//Will create the database if it does not already exist
using (var context = new TestHubDbContext())
{
    context.Database.Migrate(); 
}

// Call DataSeeder to initialize the data
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dataSeeder = serviceProvider.GetRequiredService<DataSeederService.DataSeeder>();
    dataSeeder.SeedData();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();