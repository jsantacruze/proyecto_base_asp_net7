using business_layer.AutoMapper;
using business_layer.Contracts;
using business_layer.Persons.Helpers;
using data_access;
using domain_layer;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using security_layer.TokensManager;
using System.Text;
using webapi_services.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    builder.Services.AddCors(o => o.AddPolicy("corsApp", builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

    var connectionString = builder.Configuration.GetConnectionString("webapi_sql_server_db");
    builder.Services.AddDbContext<DatabaseContext>(opt =>
                opt.UseSqlServer(connectionString)
                );

    builder.Services.AddOptions();
    builder.Services.AddMediatR(typeof(FindPerson.FindPersonRequestHandler).Assembly);
    builder.Services.AddControllers(opt => {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        opt.Filters.Add(new AuthorizeFilter(policy));
    });
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();

    var security_builder = builder.Services.AddIdentityCore<SystemUser>();
    var identityBuilder = new IdentityBuilder(security_builder.UserType, security_builder.Services);
    identityBuilder.AddRoles<IdentityRole>();
    identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<SystemUser, IdentityRole>>();

    identityBuilder.AddEntityFrameworkStores<DatabaseContext>();
    identityBuilder.AddSignInManager<SignInManager<SystemUser>>();
    builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DigitalsoftToken"));
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false
        };
     });

    builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
    builder.Services.AddScoped<IUserSession, UserSession>();
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API Servicios InventPro ERP",
            Version = "v1"
        });
        c.CustomSchemaIds(c => c.FullName);
    });
    
    builder.Services.AddHttpContextAccessor();

    var app = builder.Build();

app.UseCors("corsApp");
app.UseMiddleware<ExceptioManagerMiddleware>();


using (var scope = app.Services.CreateScope())
{
    var db_context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    var user_manager = scope.ServiceProvider.GetRequiredService<UserManager<SystemUser>>();
    db_context.Database.Migrate();
    UserIdentityDataInicializer.Inicialize(db_context, user_manager).Wait();
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
    app.UseSwaggerUI();
    }

app.UseAuthentication();

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
