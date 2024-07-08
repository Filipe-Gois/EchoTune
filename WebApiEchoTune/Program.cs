using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using WebApiEchoTune.Contexts;
using WebApiEchoTune.Interfaces;
using WebApiEchoTune.Repositories;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Carrega as variáveis de ambiente do arquivo .env
Env.Load();

// Add services to the container.
builder.Services
    .AddControllers()
        .AddNewtonsoftJson(options =>
        {
            // Ignora os loopings nas consultas
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            // Ignora valores nulos ao fazer junções nas consultas
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
    );

// Adiciona serviço de Jwt Bearer (forma de autenticação)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Valida quem está solicitando
        ValidateIssuer = true,

        // Valida quem está recebendo
        ValidateAudience = true,

        // Define se o tempo de expiração será validado
        ValidateLifetime = true,

        // Forma de criptografia e valida a chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("EchoTune-webapi-chave-symmetricsecuritykey")),

        // Valida o tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(30),

        // Nome do issuer (de onde está vindo)
        ValidIssuer = "API-EchoTune",

        // Nome do audience (para onde está indo)
        ValidAudience = "API-EchoTune"
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Adicione o gerador do Swagger à coleção de serviços
builder.Services.AddSwaggerGen(options =>
{
    // Adiciona informações sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API EchoTune",
        Description = "Backend API",
        Contact = new OpenApiContact
        {
            Name = "EchoTune"
        }
    });

    // Usando a autenticação no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
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
                }
            },
            new string[] {}
        }
    });
});

// Obter a string de conexão do arquivo .env
var connectionString = Environment.GetEnvironmentVariable("ConnectionStringDataBase");

builder.Services.AddDbContext<EchoTuneContext>(options =>
    options.UseSqlServer(connectionString));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Registra a interface IExercicioRepository e sua implementação ExercicioRepository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IMusicaRepository, MusicaRepository>();

builder.Services.AddScoped<IPlaylistRepository, PlayListRepository>();

var app = builder.Build();

// Habilite o middleware para atender ao documento JSON gerado e à interface do usuário do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Para atender à interface do usuário do Swagger na raiz do aplicativo
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
