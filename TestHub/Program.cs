using TestHub.Core.Context;
using TestHub.Core.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


using (var context = new TestHubDbContext())
{
    // Створення сідера і виклик методу Seed()
    var userSeeder = new UserSeeder(context);
    userSeeder.Seed();
    
    var testSeeder = new TestSeeder(context);
    testSeeder.Seed();
    
    var categorySeeder = new CategorySeeder(context);
    categorySeeder.Seed();
    
    var testCategorySeeder = new TestCategorySeeder(context);
    testCategorySeeder.Seed();
    
    var questionTypeSeeder = new QuestionTypeSeeder(context);
    questionTypeSeeder.Seed();
    
    var questionSeeder = new QuestionSeeder(context);
    questionSeeder.Seed();
    
    var answerSeeder = new AnswerSeeder(context);
    answerSeeder.Seed();

    var testMetadataSeeder = new TestMetadataSeeder(context);
    testMetadataSeeder.Seed();
    
    var testSessionSeeder = new TestSessionSeeder(context);
    testSessionSeeder.Seed();

    var statusSessionQuestionSeeder = new StatusSessionQuestionSeeder(context);
    statusSessionQuestionSeeder.Seed();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();