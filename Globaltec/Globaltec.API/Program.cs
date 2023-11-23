using Globaltec.Domain.Utils;
using Globaltec.Servico.Servicos;
using Globaltec.Servico.Servicos.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Globaltec - API .Net 7",
        Description = "API em .NET 7 para o teste de desenvolvedor da Globaltec.",
        Contact = new OpenApiContact()
        {
            Email = "eliseu.dev@outlook.com",
            Url = new Uri("https://eliseu.dev.br")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Insira o token de autenticação.",
        Name = "Authentication",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "Bearer "
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference 
                {
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer"
                }, 
                Scheme = "oauth2", 
                Name = "Bearer", 
                In = ParameterLocation.Header,
            },
            new string[] { }
        }
    });

    options.MapType<DateTime>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("yyyy-MM-dd")
    });
});

var key = Encoding.ASCII.GetBytes(KeyAuth.KeyAuthToken);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<IPersonService, PersonService>();
builder.Services.AddSingleton<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
