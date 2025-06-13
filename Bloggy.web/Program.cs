using Bloggy.web.Data;
using Bloggy.web.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});




// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BloggieDBContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));
builder.Services.AddDbContext<AuothDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AuothDbConnectionString")));
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AuothDBContext>();
builder.Services.AddScoped<ITagRepository , TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageUploadRepository, cloudinaryRepository>();
builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();
builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();
builder.Services.AddScoped<IUserManagerRepository, UserManagerRepository>();

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
