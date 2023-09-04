using ALG.Web;
using ALG.Data.Services;
using ALG.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




// ** Required to enable asp-authorize Taghelper **            
builder.Services.AddHttpContextAccessor(); 

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
else 
{
    Seeder.SeedAlgorithms(new AlgorithmService());
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ** configure cors to allow full cross origin access to any webapi end points **
//app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// ** turn on authentication/authorisation **
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
