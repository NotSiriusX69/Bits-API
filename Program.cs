using Bits_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

var AllowCORS = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowCORS,
                  policy =>
                  {
                      policy.WithOrigins("http://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
                  });
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "session_id";
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.MaxAge = options.IdleTimeout;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

// Register Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AdminService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<BitsContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// To Enable CORS 
app.UseCors(AllowCORS);

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
