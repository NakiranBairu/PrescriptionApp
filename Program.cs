using Microsoft.EntityFrameworkCore;
using PrescriptionApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Use SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Enforce lowercase URLs
app.Use(async (context, next) =>
{
    context.Request.Path = context.Request.Path.Value?.ToLower() ?? "";
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Create and seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    
    if (!context.Prescriptions.Any())
    {
        context.Prescriptions.AddRange(
            new Prescription { MedicationName = "Atorvastatin", FillStatus = "Filled", Cost = 19.99m, RequestTime = DateTime.Now.AddDays(-5) },
            new Prescription { MedicationName = "Lisinopril", FillStatus = "Pending", Cost = 12.50m, RequestTime = DateTime.Now.AddDays(-2) },
            new Prescription { MedicationName = "Metformin", FillStatus = "New", Cost = 8.75m, RequestTime = DateTime.Now.AddDays(-1) }
        );
        context.SaveChanges();
    }
}

app.Run();